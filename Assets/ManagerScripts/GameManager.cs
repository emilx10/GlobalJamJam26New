using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject skillTreePanel;

    void Awake()
    {
        Instance = this;
    }

    // Called when run ends
    public void EnterSkillTree()
    {
        if (skillTreePanel != null)
            skillTreePanel.SetActive(true);

        // game already paused by RunManager
    }

    // Close skill tree
    public void ExitSkillTree()
    {
        if (skillTreePanel != null)
            skillTreePanel.SetActive(false);

        RunManager.Instance.StartNewRun();
    }
}
