using UnityEngine;

public class ChaosManager : MonoBehaviour
{
    public static ChaosManager Instance;

    [Header("UI Panels")]
    public GameObject chaosCardPanel;
    public GameObject skillTreePanel;

    void Awake()
    {
        Instance = this;
    }

    public void ShowChaosCards()
    {
        if (chaosCardPanel != null)
            chaosCardPanel.SetActive(true);
    }

    public void HideChaosCards()
    {
        if (chaosCardPanel != null)
            chaosCardPanel.SetActive(false);
    }

    public void ShowSkillTree()
    {
        if (skillTreePanel != null)
            skillTreePanel.SetActive(true);
    }

    public void HideSkillTree()
    {
        if (skillTreePanel != null)
            skillTreePanel.SetActive(false);
    }

    public void ApplyCard(ChaosCardData card)
    {
        PlayerStats.Instance.Apply(card.playerModifier);
        EnemyManager.Instance.ApplyGlobal(card.enemyModifier);

        HideChaosCards();
        Time.timeScale = 1f;
    }
}