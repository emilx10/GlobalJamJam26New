using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChaosManager : MonoBehaviour
{
    public GameObject chaosCardPanel;
    public Button[] chaosCards;
    public TextMeshProUGUI[] chaosTexts;

    public GameObject skillTreePanel;
    public Button[] skillButtons;

    float skillTimer = 10f;
    bool choosingSkill;

    void Start()
    {
        chaosCardPanel.SetActive(false);
        skillTreePanel.SetActive(false);

        foreach (Button b in chaosCards)
            b.onClick.AddListener(ChooseChaos);

        foreach (Button b in skillButtons)
            b.onClick.AddListener(ChooseSkill);
    }

    public void ShowChaosCards()
    {
        chaosCardPanel.SetActive(true);

        for (int i = 0; i < chaosTexts.Length; i++)
            chaosTexts[i].text = GetRandomChaosText();
    }

    public void HideChaosCards()
    {
        chaosCardPanel.SetActive(false);
    }

    public void ChooseChaos()
    {
        // apply chaos effect later
        GameManager.Instance.ExitChaos();
    }

    public void ShowSkillTree()
    {
        skillTreePanel.SetActive(true);
        skillTimer = 10f;
        choosingSkill = true;
    }

    void Update()
    {
        if (!choosingSkill) return;

        skillTimer -= Time.unscaledDeltaTime;
        if (skillTimer <= 0f)
            ChooseSkill();
    }

    public void ChooseSkill()
    {
        choosingSkill = false;
        GameManager.Instance.ExitSkillTree();
    }

    public void HideSkillTree()
    {
        skillTreePanel.SetActive(false);
    }

    string GetRandomChaosText()
    {
        string[] chaos =
        {
            "+DMG / Faster Enemies",
            "Double HP / Low Fire Rate",
            "Enemies Split On Death"
        };
        return chaos[Random.Range(0, chaos.Length)];
    }
}