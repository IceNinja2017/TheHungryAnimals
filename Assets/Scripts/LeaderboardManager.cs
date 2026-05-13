using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using System.Linq;

public class LeaderboardManager : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighScoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("HighscoreEntryContainer");
        entryTemplate = transform.Find("HighscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        highscoreEntryList = LeaderboardDataHandler.LoadSave();

        highscoreEntryList = highscoreEntryList.OrderByDescending(e => e.score).Take(10).ToList();

        highscoreEntryTransformList = new List<Transform>();

        foreach (HighScoreEntry highscoreEntry in highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighScoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        int score = highscoreEntry.score;
        string name = highscoreEntry.name;

        switch (rank)
        {
            default: rankString = "<color=#ADD8E6>" + rank + "th</color>"; break;
            case 1: rankString = "<b><color=#FFD700>1st</color></b>"; break;
            case 2: rankString = "<b><color=#C0C0C0>2nd</color></b>"; break;
            case 3: rankString = "<b><color=#CD7F32>3rd</color></b>"; break;
        }
        entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = rankString;

        entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();
        entryTransform.Find("MarqueeContainer/nameText").GetComponent<TextMeshProUGUI>().text = name;
        transformList.Add(entryTransform);

    }
}
