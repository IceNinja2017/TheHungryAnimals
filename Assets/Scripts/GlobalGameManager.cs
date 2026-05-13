using UnityEngine;

public class GlobalGameManager : MonoBehaviour
{
    public static GlobalGameManager instance { get; private set; }

    public string playerName = "NO NAME";

    public PlayerSaveData saveData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            saveData = PlayerDataHandler.Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        PlayerDataHandler.Save(saveData);
    }
}