using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public float damage = 1;
    public float moveSpeed = 5;
    public float HP = 10;

    void Awake() => Instance = this;

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