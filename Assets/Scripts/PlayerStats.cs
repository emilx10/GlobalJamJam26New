using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public PlayerController playerController;
    public float damage = 1;
    public float moveSpeed = 5;

    [Header("Timer HP")]
    public float MaxHP;
    public float HP; // seconds
    public TextMeshProUGUI hpText;

    // track if timer is running
    public bool timerPaused = false;

    void Awake()
    {
        Instance = this;
        HP = MaxHP;
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
                moveSpeed += mod.value / 10f;
                break;

            case StatType.MaxHP:
                MaxHP += mod.value;
                HP += mod.value; // IMPORTANT: carry HP into next run
                break;

            case StatType.AttackSpeed:
                playerController.attackCooldown -= playerController.attackCooldown * 0.1f;
                break;
            case StatType.AttackRange:
                // multiply by (1 + value) to increase by percentage
                if (playerController != null)
                    playerController.attackRange *= (1f + mod.value);
                break;
        }
    }
}