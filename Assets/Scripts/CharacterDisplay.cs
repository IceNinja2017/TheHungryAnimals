using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterDisplay : MonoBehaviour
{
    public Transform visualHolder;
    public CharacterData[] characters;
    public string characterID;

    private GameObject currentModel;

    public void Start()
    {
        Show(GetCharacterById(GlobalGameManager.instance.saveData.selectedCharacterId));
    }

    public void ShowById(string id)
    {
        CharacterData data = GetCharacterById(id);

        if (data != null)
            Show(data);
    }

    public void Show(CharacterData data)
    {
        if (currentModel != null)
            Destroy(currentModel);

        currentModel = Instantiate(data.prefab, visualHolder);
        characterID = data.id;
        currentModel.transform.localPosition = Vector3.zero;
        currentModel.transform.localRotation = Quaternion.identity;

        GameManager gm = FindFirstObjectByType<GameManager>();
        if (gm != null)
        {
            gm.SetAnimator(currentModel.GetComponent<Animator>());
        }
    }

    public CharacterData GetCharacterById(string id)
    {
        foreach (var c in characters)
        {
            if (c.id == id)
                return c;
        }

        return characters[0];
    }
}