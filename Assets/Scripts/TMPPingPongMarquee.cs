using UnityEngine;
using TMPro;

public class TMPPingPongMarquee : MonoBehaviour
{
    public RectTransform container;
    public TMP_Text text;

    public float speed = 30f;
    public float pauseTime = 1f;

    float textWidth;
    float containerWidth;

    float leftLimit;
    float rightLimit;

    int direction = -1;
    float pauseTimer;

    RectTransform textRect;

    void Awake()
    {
        if (!container)
            container = GetComponent<RectTransform>();

        if (!text)
            text = GetComponentInChildren<TMP_Text>();

        textRect = text.rectTransform;
    }

    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        text.ForceMeshUpdate();

        containerWidth = container.rect.width;
        textWidth = text.preferredWidth;

        leftLimit = 0f;
        rightLimit = Mathf.Max(0f, textWidth - containerWidth);

        textRect.anchoredPosition = Vector2.zero;

        direction = -1;
        pauseTimer = pauseTime;
    }

    void Update()
    {
        if (textWidth <= containerWidth) return;

        if (pauseTimer > 0f)
        {
            pauseTimer -= Time.deltaTime;
            return;
        }

        Vector2 pos = textRect.anchoredPosition;

        pos.x += direction * speed * Time.deltaTime;

        if (pos.x <= -rightLimit)
        {
            pos.x = -rightLimit;
            direction = 1;
            pauseTimer = pauseTime;
        }
        else if (pos.x >= leftLimit)
        {
            pos.x = leftLimit;
            direction = -1;
            pauseTimer = pauseTime;
        }

        textRect.anchoredPosition = pos;
    }
}