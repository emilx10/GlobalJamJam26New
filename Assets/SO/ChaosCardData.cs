using UnityEngine;

[CreateAssetMenu(menuName = "Chaos/Card")]
public class ChaosCardData : ScriptableObject
{
    [Header("UI Text")]
    [TextArea] public string mainEffectText;
    [TextArea] public string hiddenSideEffectText;

    [Header("Main Effect")]
    public Modifier playerModifier;
    public Modifier enemyModifier;

    [Header("Linked Stat (Optional Side Effect)")]
    public bool linkSecondStat;
    public StatType linkedStat;
    public float linkedValue;
}
