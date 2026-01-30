using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPBar : MonoBehaviour
{
    [Header("References")]
    public Image fillImage;           // Image type: Filled
    public TextMeshProUGUI hpText;    // Timer text
    public PlayerStats playerStats;    // Link your PlayerStats (HP = timer)

    private float maxHP;

    void Start()
    {
        if (playerStats == null)
            playerStats = PlayerStats.Instance;

        if (playerStats != null)
            maxHP = playerStats.HP; // store starting HP (timer)
    }

    void Update()
    {
        if (playerStats == null) return;

        // Update fill
        fillImage.fillAmount = playerStats.HP / maxHP;

        // Update text (round up to integer seconds)
        if (hpText != null)
            hpText.text = Mathf.CeilToInt(playerStats.HP).ToString();
    }
}
