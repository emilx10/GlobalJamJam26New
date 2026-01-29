using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;

    float fireTimer;
    Vector2 moveInput;

    void Update()
    {
        Move();
        AutoShoot();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void Move()
    {
        transform.Translate(moveInput * moveSpeed * Time.deltaTime);
    }

    void AutoShoot()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer > 0f) return;

        GameObject enemy = FindClosestEnemy();
        if (!enemy) return;

        fireTimer = fireRate;
        Vector2 dir = (enemy.transform.position - transform.position).normalized;

        Instantiate(bulletPrefab, transform.position, Quaternion.identity)
            .GetComponent<Bullet>()
            .Init(dir);
    }

    GameObject FindClosestEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        float min = Mathf.Infinity;
        Enemy closest = null;

        foreach (Enemy e in enemies)
        {
            float d = Vector2.Distance(transform.position, e.transform.position);
            if (d < min)
            {
                min = d;
                closest = e;
            }
        }

        return closest ? closest.gameObject : null;
    }
}