using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    public int score = 0;
    public bool isPaused = false;
    public bool isDead = false;
    public float speed = 10f;

    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextBubbleManager bubbleManager;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private TextMeshProUGUI usernameTxt;
    [SerializeField] private GameObject GameOverUI;

    [Header("Others")]
    public AudioSource gameMusic;

    private Animator anim;
    private float nextMilestone = 100f;
    private float speedMult = 1.15f;
    private float MAX_SPEED = 80f;
    private string username;

    private void Awake()
    {
        //Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        username = GlobalGameManager.instance.playerName;

        if (PauseMenu != null) PauseMenu.gameObject.SetActive(false); //hidden by default
        if (GameOverUI != null) GameOverUI.gameObject.SetActive(false); //hidden by default
        if (scoreTxt != null) scoreTxt.text = "Score: " + score;
        if (usernameTxt != null) usernameTxt.text = username;
    }

    private void Update()
    {
        if (score >= nextMilestone)
        {
            OnHundredReached();
            nextMilestone += 100;
        }

        if (Input.GetKeyDown(KeyCode.P) && !isPaused && !isDead)
        {
            anim.speed = 0f;
            gameMusic.Pause();
            PauseMenu.gameObject.SetActive(true);
            TogglePause();
        }
        else if (Input.GetKeyDown(KeyCode.R) && isPaused && !isDead)
        {
            anim.speed = 1f;
            gameMusic.Play();
            PauseMenu.gameObject.SetActive(false);
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isDead)
        {
            ReturnToMainMenu();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        scoreTxt.text = "Score: " + score;
    }

    void OnHundredReached()
    {
        Debug.Log("Reached " + nextMilestone + "!");
        if (speed <= MAX_SPEED)
        {
            speed *= speedMult;
        } else speed = MAX_SPEED;

        if (bubbleManager != null)
        {
            bubbleManager.ShowBubble(2f);
        }
    }
    public void RestartGame()
    {
        LeaderboardDataHandler.AddScore(GlobalGameManager.instance.playerName, score);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {

        LeaderboardDataHandler.AddScore(GlobalGameManager.instance.playerName, score);
        SceneManager.LoadScene("MainMenu");   
    }

    public void HandleDeath()
    {
        gameMusic.Stop();
        isDead = true;
        GameOverUI.SetActive(true);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
    }

    public void SetAnimator(Animator a)
    {
        anim = a;
    }
}
