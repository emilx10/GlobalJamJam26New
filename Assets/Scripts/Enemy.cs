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

    public UnityEvent onHit;
    public UnityEvent onDied;

    void OnEnable()
    {
        if (data == null) return;

        currentHP = maxHP;
        EnemyManager.Instance?.RegisterEnemy(this);
    }

    void Update()
    {
        if (data == null) return;

        switch (data.type)
        {
            case EnemyType.Warden:
                MoveVertical();
                break;
            case EnemyType.HellLeech:
                MoveHorizontal();
                break;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats player = collision.GetComponentInParent<PlayerStats>();
        if (player == null) return;
        if (!player.gameObject.CompareTag("Player")) return;

        player.TakeDamage(damage);
    }
    void MoveVertical()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

        if (transform.position.y >= data.maxY)
            EnemyPool.Instance.ReturnEnemy(this);
    }

    void MoveHorizontal()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        if (transform.position.x >= data.maxX)
            EnemyPool.Instance.ReturnEnemy(this);
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
        }
    }

    void Die()
    {
        ChaosOrbPool.Instance.Spawn(transform.position);
        EnemyPool.Instance.ReturnEnemy(this);
    }

    void OnDisable()
    {
        EnemyManager.Instance?.UnregisterEnemy(this);
    }
}