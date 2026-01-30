using UnityEngine;
using UnityEngine.UI;

public class SkillNode : MonoBehaviour
{
    public SkillNode[] unlocks;     // Connected nodes
    public LineRenderer[] lines;    // Lines to children

    public int cost = 5;
    public SkillEffect effect;

    Button button;
    bool unlocked;
    public bool isRoot = false;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(Choose);

        if (isRoot)
            SetVisible(true); // root node visible
        else
            SetVisible(false); // children hidden
    }
    public void SetVisible(bool v)
    {
        gameObject.SetActive(v);
        foreach (var l in lines)
            l.gameObject.SetActive(v);
    }

    void Choose()
    {
        if (unlocked) return;
        if (CurrencyManager.Instance.ChaosOrbs < cost) return;

        CurrencyManager.Instance.Spend(cost);
        unlocked = true;

        effect.Apply();

        foreach (var n in unlocks)
            n.SetVisible(true);
    }
}