using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillNode : MonoBehaviour
{
    public SkillNode[] unlocks;     // Connected nodes
    public int cost = 5;
    public SkillEffect effect;

    Button button;
    bool unlocked;
    public bool isRoot;
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Choose);
        if (isRoot)
        {
            SetVisible(true);
        }
        else
        {
            SetVisible(false); // hide by default
        }
    }

    public void SetVisible(bool v)
    {
        gameObject.SetActive(v);
    }

    void Choose()
    {
        if (unlocked) return;
        if (CurrencyManager.Instance.ChaosOrbs < cost) return;

        CurrencyManager.Instance.Spend(cost);
        unlocked = true;

        effect.Apply();

        // Show children for 3 seconds
        StartCoroutine(RevealChildrenThenStartRun());
    }

    IEnumerator RevealChildrenThenStartRun()
    {
        foreach (var n in unlocks)
            n.SetVisible(true);

        // Wait 3 seconds
        yield return new WaitForSecondsRealtime(3f);

        // Hide children again (optional)
        // foreach (var n in unlocks)
        //     n.SetVisible(false);

        // Close skill tree and start new run
        GameManager.Instance.ExitSkillTree();
    }
}