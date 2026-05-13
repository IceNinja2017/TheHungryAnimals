using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LeaderboardDataHandler
{
    private static string path = Application.persistentDataPath + "/highscores.json";

    public static List<HighScoreEntry> LoadSave()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<HighScoreList>(json).scores;
        }
        return new List<HighScoreEntry>();
    }

    public static void SaveScores(List<HighScoreEntry> scores)
    {
        HighScoreList wrapper = new HighScoreList();
        wrapper.scores = scores;

        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(path, json);
    }

    public static void AddScore(string name, int score)
    {
        List<HighScoreEntry> scores = LoadSave();

        HighScoreEntry existing = scores.FirstOrDefault(e => e.name == name);

        if (existing != null)
        {
            if (score > existing.score) 
            { 
                existing.score = score;
            }
            else
            {
                return;
            }
        }

        scores.Add(new HighScoreEntry { name = name, score = score, });

        scores = scores
            .GroupBy(x => x.name)
            .Select(g => g.OrderByDescending(x => x.score).First())
            .OrderByDescending(x => x.score)
            .Take(10)
            .ToList();

        SaveScores(scores);
    }
}
