using UnityEngine;

public class TextBubbleManager : MonoBehaviour
{
    [SerializeField] GameObject speachBubble;

    private float currentTime;
    private bool isShowing = false;
    private float displayTime = 3f;

    void Start()
    {
        speachBubble.SetActive(false);
    }

    void Update()
    {
        if (isShowing)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= displayTime)
            {
                speachBubble.SetActive(false);
                isShowing = false;
            }
        }
    }

    public void ShowBubble(float time)
    {
        speachBubble.SetActive(true);
        currentTime = 0;
        displayTime = time;
        isShowing = true;
    }
}