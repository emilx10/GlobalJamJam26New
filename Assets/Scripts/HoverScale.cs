using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class HoverScaleRelative : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Scaling Factor")]
    [Tooltip("1.1 means 10% larger than its starting size")]
    public float scaleMultiplier = 1.1f;
    public float lerpTime = 0.15f;

    private Vector3 originalScale;
    private Vector3 targetHoverScale;
    private Coroutine scaleCoroutine;

    void Awake()
    {
        originalScale = transform.localScale;

        targetHoverScale = originalScale * scaleMultiplier;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAnimation();
        scaleCoroutine = StartCoroutine(AnimateScale(targetHoverScale));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAnimation();
        scaleCoroutine = StartCoroutine(AnimateScale(originalScale));
    }

    private void StopAnimation()
    {
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
    }

    private IEnumerator AnimateScale(Vector3 endScale)
    {
        float elapsed = 0f;
        Vector3 startScale = transform.localScale;

        while (elapsed < lerpTime)
        {
            elapsed += Time.unscaledDeltaTime;
            float percent = elapsed / lerpTime;

            transform.localScale = Vector3.Lerp(startScale, endScale, Mathf.SmoothStep(0, 1, percent));
            yield return null;
        }

        transform.localScale = endScale;
    }
    void OnDisable()
    {
        StopAnimation();
        transform.localScale = originalScale;
    }
}