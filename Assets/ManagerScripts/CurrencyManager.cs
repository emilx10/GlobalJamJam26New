using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    public int ChaosOrbs = 0;
    public TextMeshProUGUI chaosOrbText; // Assign in Inspector

    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;

        UpdateUI();
    }

    public void Add(int amount)
    {
        ChaosOrbs += amount;
        UpdateUI();
    }

    public bool Spend(int amount)
    {
        if (ChaosOrbs < amount) return false;

        ChaosOrbs -= amount;
        UpdateUI();
        return true;
    }

    void UpdateUI()
    {
        if (chaosOrbText != null)
            chaosOrbText.text = "Chaos: " + ChaosOrbs.ToString();
    }
}