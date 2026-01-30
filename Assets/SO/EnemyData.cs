using UnityEngine;

public enum EnemyType
{
    Warden,    // vertical
    HellLeech  // horizontal
}

[CreateAssetMenu(menuName = "Enemies/EnemyData")]
public class EnemyData : ScriptableObject
{
    public Enemy prefab;
    public EnemyType type;
    public float moveSpeed;
    public int maxHP;
    public int damage;

    public float minX, maxX; // for horizontal enemies
    public float minY, maxY; // for vertical enemies
}