[System.Serializable]
public class Modifier
{
    public StatType stat;
    public float value;
}

public enum StatType
{
    Damage,
    MoveSpeed,
    MaxHP,
    AttackSpeed,
    SoulDrop,
    AttackRange
}