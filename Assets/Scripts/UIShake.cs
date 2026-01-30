using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIShake : MonoBehaviour
{
    [Header("Shake Properties")]
    public float duration = 0.3f;
    public float amplitude = 0.2f;
    public float frequency = 40f;

    public float amplitudeX = 0.3f;
    public float amplitudeY = 0.1f;

    RectTransform rectTransform;
    Vector2 originalPos;
    Coroutine shakeRoutine;

    [Header("Alpha Fade")]
    public Image image;
    public float alphaSpeed = 2f;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPos = rectTransform.anchoredPosition;
    }

    private void OnEnable()
    {
        SkillNode.onSkillPressed += Play;
    }

    private void OnDisable()
    {
        SkillNode.onSkillPressed -= Play;
    }

    public void Play()
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        shakeRoutine = StartCoroutine(ShakeAndFade());
    }

    IEnumerator ShakeAndFade()
    {
        float time = 0f;
        originalPos = rectTransform.anchoredPosition;

        Color c = image.color;
        float originalAlpha = c.a;

        while (time < duration)
        {
            float t = time / duration;

            float noise = Mathf.Sin(time * frequency * Mathf.PI * 2f);
            float x = noise * amplitude * amplitudeX * 100f;
            float y = noise * amplitude * amplitudeY * 100f;
            rectTransform.anchoredPosition = originalPos + new Vector2(x, y);

            float alpha = Mathf.Sin(t * Mathf.PI);
            c.a = Mathf.Lerp(0f, 2f, alpha);
            image.color = c;

            time += Time.unscaledDeltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = originalPos;
        c.a = originalAlpha;
        image.color = c;

        shakeRoutine = null;
    }
}
