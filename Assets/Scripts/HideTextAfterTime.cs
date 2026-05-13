using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HideTextAfterTime : MonoBehaviour
{
    private float timer = 0f;

    [SerializeField] private GameObject textObject;
    [SerializeField] private float duration = 3f;

    private bool isTimerStop = false;

    void Start()
    {
        if (textObject == null) return;
    }

    private void Update()
    {
        if (!isTimerStop) return;
        timer += Time.deltaTime;

        if (timer > duration)
        {
            textObject.SetActive(false);
            isTimerStop = true;
        }
    }
}
