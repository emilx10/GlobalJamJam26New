using UnityEngine;

[CreateAssetMenu(menuName = "Chaos/Card")]
public class ChaosCardData : ScriptableObject
{
    public string title;
    [TextArea] public string description;

    public Modifier playerModifier;
    public Modifier enemyModifier;
}