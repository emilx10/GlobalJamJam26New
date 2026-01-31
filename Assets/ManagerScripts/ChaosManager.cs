using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaosManager : MonoBehaviour
{
    public static ChaosManager Instance;

    public GameObject chaosCardPanel;
    private HashSet<ChaosCardData> chosenThisRun = new();
    [SerializeField] Image chaosTimerSand; // the sand Image (Filled)
    [SerializeField] float chaosChoiceTime = 5f;
    Coroutine chaosTimerRoutine;
    System.Action onCardPicked;

    void Awake()
    {
        Instance = this;
    }

    // Show cards and pause run
    public void ShowChaosCards(System.Action callback)
    {
        onCardPicked = callback;

        if (chaosCardPanel != null)
            chaosCardPanel.SetActive(true);
        if (chaosTimerSand != null)
            chaosTimerSand.fillAmount = 1f;

        foreach (var btn in chaosCardPanel.GetComponentsInChildren<Button>())
        {
            var cardBtn = btn.GetComponent<ChaosCardButton>();
            if (cardBtn != null)
                btn.interactable = !chosenThisRun.Contains(cardBtn.cardData);
        }

        //  START CHAOS TIMER
        if (chaosTimerRoutine != null)
            StopCoroutine(chaosTimerRoutine);

        chaosTimerRoutine = StartCoroutine(ChaosChoiceTimer());
    }


    // Called when player clicks a chaos card
    public void ApplyCard(ChaosCardData card)
    {
        if (chaosTimerRoutine != null)
            StopCoroutine(chaosTimerRoutine);

        PlayerStats.Instance.Apply(card.playerModifier);
        EnemyManager.Instance.ApplyGlobal(card.enemyModifier);

        chosenThisRun.Add(card);

        if (chaosCardPanel != null)
            chaosCardPanel.SetActive(false);

        onCardPicked?.Invoke();
    }
    void AutoPickCard()
    {
        var buttons = chaosCardPanel.GetComponentsInChildren<ChaosCardButton>(true);

        List<ChaosCardButton> valid = new();

        foreach (var b in buttons)
        {
            var btn = b.GetComponent<Button>();
            if (btn != null && btn.interactable && !chosenThisRun.Contains(b.cardData))
            {
                valid.Add(b);
            }
        }

        if (valid.Count > 0)
        {
            ApplyCard(valid[Random.Range(0, valid.Count)].cardData);
        }
        else
        {
            // failsafe: close panel and resume run
            chaosCardPanel.SetActive(false);
            onCardPicked?.Invoke();
        }
    }


    IEnumerator ChaosChoiceTimer()
    {
        float t = chaosChoiceTime;

        while (t > 0f)
        {
            t -= Time.unscaledDeltaTime;

            if (chaosTimerSand != null)
                chaosTimerSand.fillAmount = Mathf.Clamp01(t / chaosChoiceTime);

            yield return null;
        }

        AutoPickCard();
    }
    public void ResetRun()
    {
        chosenThisRun.Clear();
    }
}
