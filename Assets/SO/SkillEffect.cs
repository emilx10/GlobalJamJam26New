using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Effect")]
public class SkillEffect : ScriptableObject
{
    public Modifier playerModifier;
    public Modifier enemyModifier;

    public void Apply()
    {
        if (playerModifier != null)
            PlayerStats.Instance.Apply(playerModifier);

        if (enemyModifier != null)
            EnemyManager.Instance.ApplyGlobal(enemyModifier);
    }
}