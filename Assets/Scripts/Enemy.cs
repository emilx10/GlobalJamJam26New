using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 1f;     // Floating speed
    public float maxY = 5f;          // Y-position to despawn
    public int maxHP = 20;
    public int currentHP;

    void Awake()
    {
        currentHP = maxHP;
    }

    void OnEnable()
    {
        currentHP = maxHP;
    }

    void Update()
    {
        FloatUp();
        CheckDespwan();
    }

    void FloatUp()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    void CheckDespwan()
    {
        if (transform.position.y >= maxY)
            EnemyPool.Instance.ReturnEnemy(this.gameObject);
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Drop Chaos Orb
        ChaosOrbPool.Instance.Spawn(transform.position);

        EnemyPool.Instance.ReturnEnemy(gameObject);
    }
}