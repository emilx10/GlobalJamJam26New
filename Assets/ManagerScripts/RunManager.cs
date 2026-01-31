using UnityEngine;

public class RunManager : MonoBehaviour
{
    public static RunManager Instance;

    bool chaos20Triggered;
    bool chaos10Triggered;
    bool runEnded;           // true when HP reaches 0
    public bool runPaused;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Update()
    {
        if (PlayerStats.Instance == null || runPaused) return;

        // Clamp HP to >= 0
        PlayerStats.Instance.HP = Mathf.Max(0f, PlayerStats.Instance.HP);
        float time = PlayerStats.Instance.HP;

        // Chaos card at 20s
        if (time <= 20f && !chaos20Triggered)
        {
            chaos20Triggered = true;
            PauseRun();
            ChaosManager.Instance.ShowChaosCards(ResumeRun);
        }

        // Chaos card at 10s
        if (time <= 10f && !chaos10Triggered)
        {
            chaos10Triggered = true;
            PauseRun();
            ChaosManager.Instance.ShowChaosCards(ResumeRun);
        }

        // Run ends at 0
        if (time <= 0f && !runEnded)
        {
            runEnded = true;
            PauseRun();
            GameManager.Instance.EnterSkillTree(); // shows panel and pauses game
        }
    }

    void PauseRun()
    {
        runPaused = true;
        Time.timeScale = 0f; // freeze game ///THIS MAKE SHADERS NOT WORK N
    }

    void ResumeRun()
    {
        runPaused = false;
        Time.timeScale = 1f; // resume game
    }

    public void StartNewRun()
    {
        PlayerStats.Instance.HP = PlayerStats.Instance.MaxHP;
        chaos20Triggered = false;
        chaos10Triggered = false;
        runEnded = false;
        ChaosManager.Instance.ResetRun();
        ResumeRun();
    }
}
