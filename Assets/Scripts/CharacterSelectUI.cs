using UnityEngine;

public class CharacterSelectUI : MonoBehaviour
{
    public Transform contentParent;
    public GameObject cardPrefab;

    public CharacterDisplay previewManager;

    void Start()
    {
        Populate();
    }

    void Populate()
    {
        foreach (var character in previewManager.characters)
        {
            GameObject cardObj = Instantiate(cardPrefab, contentParent);

            CharacterCard card = cardObj.GetComponent<CharacterCard>();

            card.Setup(character, previewManager);
        }
    }
}