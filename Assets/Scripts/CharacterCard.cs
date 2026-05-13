using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCard : MonoBehaviour
{
    public TMPro.TMP_Text nameText;
    public UnityEngine.UI.Button selectButton;

    private CharacterData data;

    public void Setup(CharacterData character, CharacterDisplay preview)
    {
        data = character;
        nameText.text = data.displayName;
        selectButton.onClick.AddListener(() =>
        {
            preview.Show(data);
        });
    }
}
