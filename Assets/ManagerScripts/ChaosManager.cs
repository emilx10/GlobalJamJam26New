using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaosManager : MonoBehaviour
{
    public static ChaosManager Instance;

    public GameObject chaosCardPanel;
    private HashSet<ChaosCardData> chosenThisRun = new();

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

        // disable already chosen cards
        foreach (var btn in chaosCardPanel.GetComponentsInChildren<Button>())
        {
            var cardBtn = btn.GetComponent<ChaosCardButton>();
            if (cardBtn != null)
                btn.interactable = !chosenThisRun.Contains(cardBtn.cardData);
        }
    }

    // Called when player clicks a chaos card
    public void ApplyCard(ChaosCardData card)
    {
        PlayerStats.Instance.Apply(card.playerModifier);
        EnemyManager.Instance.ApplyGlobal(card.enemyModifier);

        chosenThisRun.Add(card);

        if (chaosCardPanel != null)
            chaosCardPanel.SetActive(false);

        // Resume the run
        onCardPicked?.Invoke();
    }

    public void ResetRun()
    {
        chosenThisRun.Clear();
    }
}
