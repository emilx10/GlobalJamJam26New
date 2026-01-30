using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHP = 3f;
    float currentHP;

    Transform player;
    public GameObject chaosOrbPrefab;
    void OnEnable()
    {
        currentHP = maxHP;
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            2f * Time.deltaTime
        );
    }

    public void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0f)
            Die();
    }

    void Die()
    {
        DropChaosOrb();
        gameObject.SetActive(false);
    }
    void DropChaosOrb()
    {
        if (chaosOrbPrefab != null)
        {
            Instantiate(chaosOrbPrefab, transform.position, Quaternion.identity);
        }
    }
}