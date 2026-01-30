using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;

public class SkillNode : MonoBehaviour
{
    public static UnityAction onSkillPressed;
    public int cost = 5;
    public SkillEffect effect;

    public List<SkillNode> connectedNodes;
    public bool isUnlocked = false;

    [Header("Visual")]
    public float unlockLerpTime = 0.35f;
    public string sliderProperty = "_Slider";

    public event Action<SkillNode> OnUnlocked;

    public TMP_Text Title;

    Button button;
    public Image image;
    Material runtimeMat;

    bool unlocked;

    void Awake()
    {
        button = GetComponent<Button>();

        runtimeMat = Instantiate(image.material);
        image.material = runtimeMat;

        runtimeMat.SetFloat(sliderProperty, 1f);

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
        if (unlocked) return;
        if (CurrencyManager.Instance.ChaosOrbs < cost) return;

        CurrencyManager.Instance.Spend(cost);
        unlocked = true;

        effect.Apply();
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
            runtimeMat.SetFloat(sliderProperty, v);
            yield return null;
        }

        runtimeMat.SetFloat(sliderProperty, 0f);
    }
}
