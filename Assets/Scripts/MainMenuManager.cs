using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_InputField inputField;
    public TextMeshProUGUI title;

    public GameObject mainMenuUI;
    public GameObject characterSelectorUI;

    [Header("Character Displays")]
    public CharacterDisplay previewDisplay;
    public CharacterDisplay titleCharacterDisplay;

    private const string DEFAULT_PLAYER_NAME = "RED";
    private const string DEFAULT_CHARACTER_ID = "animal-fox";
    private const string TITLE_PREFIX = "The Hungry ";

    private void Start()
    {
        characterSelectorUI.SetActive(false);
        mainMenuUI.SetActive(true);

        LoadPlayerName();
        RefreshSelectedCharacterUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit requested");
            Application.Quit();
        }
    }

    void LoadPlayerName()
    {
        if (inputField != null)
        {
            inputField.text = GlobalGameManager.instance.playerName;
        }
    }

    public void HandleUsernameInput(string value)
    {
        GlobalGameManager.instance.playerName =
            string.IsNullOrWhiteSpace(value)
            ? DEFAULT_PLAYER_NAME
            : value;
    }

    public void HandleStartButton()
    {
        SceneManager.LoadScene("GameStage");
    }

    public void HandleCharSelectorBtn()
    {
        ShowCharacterSelector(true);
    }

    public void HandleBackBtn()
    {
        ShowCharacterSelector(false);
    }

    void ShowCharacterSelector(bool show)
    {
        characterSelectorUI.SetActive(show);
        mainMenuUI.SetActive(!show);
    }

    public void HandleConfirmBtn()
    {
        string selectedId =
            string.IsNullOrEmpty(previewDisplay.characterID)
            ? DEFAULT_CHARACTER_ID
            : previewDisplay.characterID;

        GlobalGameManager.instance.saveData.selectedCharacterId = selectedId;

        GlobalGameManager.instance.SaveGame();

        RefreshSelectedCharacterUI();

        ShowCharacterSelector(false);
    }

    void RefreshSelectedCharacterUI()
    {
        string selectedId =
            GlobalGameManager.instance.saveData.selectedCharacterId;

        if (string.IsNullOrEmpty(selectedId))
        {
            selectedId = DEFAULT_CHARACTER_ID;
        }

        CharacterData selectedCharacter =
            titleCharacterDisplay.GetCharacterById(selectedId);

        if (selectedCharacter == null)
            return;

        title.text = TITLE_PREFIX + selectedCharacter.displayName;

        titleCharacterDisplay.Show(selectedCharacter);
    }
}