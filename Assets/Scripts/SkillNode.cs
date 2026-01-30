using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using TMPro;
using UnityEngine.Events;

public class SkillNode : MonoBehaviour
{
    public static UnityAction onSkillPressed;
    public int cost = 5;
    public SkillEffect effect;
    public bool isUnlocked = false;

    [Header("Visual")]
    public float unlockLerpTime = 0.35f;
    public string sliderProperty = "_Slider";
    public event Action<SkillNode> OnUnlocked;
    public TMP_Text Title;
    public Image image;

    Button button;
    Material runtimeMat;

    void Awake()
    {
        button = GetComponent<Button>();
        if (image != null && image.material != null)
        {
            runtimeMat = Instantiate(image.material);
            image.material = runtimeMat;
            runtimeMat.SetFloat(sliderProperty, 1f);
        }
        button.onClick.AddListener(OnClick);
    }

    public void SetLocked(bool locked)
    {
        button.interactable = !locked;
    }

    public void Show(bool interactable)
    {
        gameObject.SetActive(true);
        SetLocked(!interactable);
    }

    public void ChanggeData(SkillData data)
    {
        Title.text = data.skillName;
    }

    void OnClick()
    {
        if (isUnlocked) return;
        if (CurrencyManager.Instance.ChaosOrbs < cost) return;

        CurrencyManager.Instance.Spend(cost);
        isUnlocked = true;

        if (effect != null) effect.Apply();
        SetLocked(true);

        StartCoroutine(LerpUnlock());
        OnUnlocked?.Invoke(this);
        onSkillPressed?.Invoke();
    }

    IEnumerator LerpUnlock()
    {
        float t = 0f;
        while (t < unlockLerpTime)
        {
            t += Time.unscaledDeltaTime;
            float v = Mathf.Lerp(1f, 0f, t / unlockLerpTime);
            if (runtimeMat != null) runtimeMat.SetFloat(sliderProperty, v);
            yield return null;
        }
        if (runtimeMat != null) runtimeMat.SetFloat(sliderProperty, 0f);
    }
}