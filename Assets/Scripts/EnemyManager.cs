using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public float globalDamage;
    public float globalSpeed;

    void Awake() => Instance = this;

    public void ApplyGlobal(Modifier mod)
    {
        switch (mod.stat)
        {
            case StatType.Damage:
                globalDamage += mod.value;
                break;
            case StatType.MoveSpeed:
                globalSpeed += mod.value;
                break;
        }
    }
}