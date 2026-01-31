using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public EnemyData data;

    [Header("Runtime Stats")]
    public float moveSpeed;
    public int maxHP;
    public int currentHP;
    public int damage;
    public bool isStunned = false;
    [Header("Drops")]
    public int baseSoulDrop;     // Base drop from EnemyData
    public int currentSoulDrop;  // Modified by Chaos Cards


    public UnityEvent onHit;
    public UnityEvent onDied;

    void OnEnable()
    {
        if (data == null) return;

        currentHP = maxHP;
        EnemyManager.Instance?.RegisterEnemy(this);
    }


    public void Stun(float duration)
    {
        if (!isActiveAndEnabled) return;
        StartCoroutine(StunRoutine(duration));
    }

    private IEnumerator StunRoutine(float duration)
    {
        isStunned = true;
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats player = collision.GetComponentInParent<PlayerStats>();
        if (player == null) return;
        if (!player.gameObject.CompareTag("Player")) return;

        player.TakeDamage(damage);
    }

    /// <summary>
    /// Initialize the enemy with EnemyData (called by pool)
    /// </summary>
    public void Init(EnemyData enemyData)
    {
        data = enemyData;

        // Base stats from EnemyData
        moveSpeed = data.moveSpeed;
        maxHP = data.maxHP;
        damage = data.damage;
        currentHP = maxHP;

        // Initialize soul drop
        baseSoulDrop = data.soulDrop;    // <- Make sure EnemyData has this field
        currentSoulDrop = baseSoulDrop;

        // Apply all currently active global modifiers
        EnemyManager.Instance?.ApplyCurrentModifiers(this);
    }


    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            Die();
            onDied.Invoke();
        }
        else
        {
            onHit.Invoke();
        }
    }

    /// <summary>
    /// Apply a chaos card modifier to this enemy
    /// </summary>
    public void ApplyModifier(Modifier modifier)
    {
        switch (modifier.stat)
        {
            case StatType.Damage:
                damage += Mathf.RoundToInt(modifier.value);
                break;
            case StatType.MoveSpeed:
                moveSpeed += modifier.value;
                break;
            case StatType.MaxHP:
                maxHP += Mathf.RoundToInt(modifier.value);
                currentHP += Mathf.RoundToInt(modifier.value);
                break;
            case StatType.SoulDrop:  // <- new StatType for soul drops
                currentSoulDrop += Mathf.RoundToInt(modifier.value);
                break;
        }
    }

    public void Die()
    {
        for (int i = 0; i < currentSoulDrop; i++)
        {
            ChaosOrbPool.Instance.Spawn(transform.position);
            Destroy(gameObject); 
        }
    }
}