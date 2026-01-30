using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public float damage = 1;
    public float moveSpeed = 5;

    [Header("Timer HP")]
    public float HP; // seconds
    public TextMeshProUGUI hpText;

    // track if timer is running
    public bool timerPaused = false;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (HP <= 0 || timerPaused) return; // STOP timer if paused

        HP -= Time.deltaTime;
        UpdateUI();

        if (HP <= 0)
        {
            HP = 0;
            GameManager.Instance.EnterSkillTree();
        }
    }

    public void TakeDamage(float seconds)
    {
        HP -= seconds;
        if (HP < 0) HP = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (hpText != null)
            hpText.text = Mathf.CeilToInt(HP).ToString();
    }

    public void ResetTimer(float newTime)
    {
        HP = newTime;
        UpdateUI();
    }

    public void PauseTimer(bool paused)
    {
        timerPaused = paused;
    }

    public void Apply(Modifier mod)
    {
        switch (mod.stat)
        {
            case StatType.Damage:
                damage += mod.value;
                break;

            case StatType.MoveSpeed:
                moveSpeed += mod.value;
                break;

            case StatType.MaxHP:
                HP += mod.value;
                break;
        }
    }
}