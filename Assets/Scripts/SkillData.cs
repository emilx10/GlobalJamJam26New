using UnityEngine;

[CreateAssetMenu(menuName = "Skill Tree/Skill")]
public class SkillData : ScriptableObject
{
    public string skillName;

    public string skillId;

    public int cost = 5;
    public SkillEffect effect;

    [Header("Locking")]
    public SkillData requiresSkill;   // must be unlocked first
}
