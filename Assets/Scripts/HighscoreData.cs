using System;
using System.Collections.Generic;

[Serializable]
public class HighScoreEntry
{
    public int score;
    public string name;
}

[Serializable]
public class HighScoreList
{
    public List<HighScoreEntry> scores = new List<HighScoreEntry>();
}