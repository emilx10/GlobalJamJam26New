using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject skillTreePanel;
    [Header("Run Start Delay")]
    [SerializeField] private float startRunDelay = 2f; // seconds
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

        StartCoroutine(StartRunAfterDelay());
    }

    private IEnumerator StartRunAfterDelay()
    {
        yield return new WaitForSecondsRealtime(startRunDelay);
        RunManager.Instance.StartNewRun();
    }
}
