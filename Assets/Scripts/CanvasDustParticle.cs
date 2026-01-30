using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(CanvasGroup))]
public class CanvasDustParticle : MonoBehaviour
{
    private Image image;
    private CanvasGroup canvasGroup;

    private float verticalSpeed;
    private float driftSpeed;
    private float driftIntensity;
    private float lifetime;
    private float maxLifetime;
    private float phaseOffset;

    void Awake()
    {
        image = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Initialize(Sprite sprite, Vector2 startPos, Color color, float size)
    {
        image.sprite = sprite;
        image.color = color;
        transform.localPosition = startPos;
        transform.localScale = Vector3.one * size;

        verticalSpeed = Random.Range(5f, 15f);
        driftSpeed = Random.Range(0.5f, 1.5f);
        driftIntensity = Random.Range(5f, 15f);
        phaseOffset = Random.Range(0f, Mathf.PI * 2);

        maxLifetime = Random.Range(4f, 8f);
        lifetime = 0;

        canvasGroup.alpha = 0;
        gameObject.SetActive(true);
    }

    void Update()
    {
        lifetime += Time.unscaledDeltaTime;
        float normalizedTime = lifetime / maxLifetime;

        Vector3 pos = transform.localPosition;
        pos.y += verticalSpeed * Time.unscaledDeltaTime;

        pos.x += Mathf.Sin(Time.unscaledTime * driftSpeed + phaseOffset) * driftIntensity * Time.unscaledDeltaTime;

        transform.localPosition = pos;

        if (normalizedTime < 0.2f)
            canvasGroup.alpha = normalizedTime * 5f;
        else
            canvasGroup.alpha = 1f - ((normalizedTime - 0.2f) / 0.8f);

        if (lifetime >= maxLifetime)
        {
            gameObject.SetActive(false);
        }
    }
}