using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public RunManager runManager;
    public ChaosManager chaosManager;

    void Awake()
    {
        Instance = this;
    }

    public void StartRun()
    {
        Time.timeScale = 1f;
        runManager.StartRun();
    }

    public void EnterChaos()
    {
        Time.timeScale = 0f;
        chaosManager.ShowChaosCards();
    }

    public void ExitChaos()
    {
        chaosManager.HideChaosCards();
        Time.timeScale = 1f;
    }

    public void EnterSkillTree()
    {
        Time.timeScale = 0f;
        chaosManager.ShowSkillTree();
    }

    public void ExitSkillTree()
    {
        chaosManager.HideSkillTree();
        StartRun();
    }
}