using System;
using System.Collections.Generic;

[Serializable]
public class PlayerSaveData
{
    public int coinsOrTotalScore; // optional later
    public string selectedCharacterId;

    public List<string> unlockedCharacters = new List<string>();
}