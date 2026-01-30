using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillNode : MonoBehaviour
{
    public SkillNode[] unlocks;
    public int cost = 5;
    public SkillEffect effect;
    public bool isRoot;

    Button button;
    bool unlocked;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Choose);

        SetVisible(isRoot);
    }

    public void SetVisible(bool v)
    {
        gameObject.SetActive(v);
    }

    void Choose()
    {
        if (unlocked) return;

        // Check currency
        if (CurrencyManager.Instance.ChaosOrbs < cost)
        {
            StartCoroutine(AutoCloseSkillTree());
            return;
        }

        CurrencyManager.Instance.Spend(cost);
        unlocked = true;

        effect.Apply();

        button.interactable = false;

        StartCoroutine(RevealChildrenThenStartRun());
    }

    IEnumerator RevealChildrenThenStartRun()
    {
        foreach (var n in unlocks)
            n.SetVisible(true);

        yield return new WaitForSecondsRealtime(3f);

        GameManager.Instance.ExitSkillTree(); // Exit panel, start new run
    }

    IEnumerator AutoCloseSkillTree()
    {
        yield return new WaitForSecondsRealtime(5f);

        GameManager.Instance.ExitSkillTree();
    }
}