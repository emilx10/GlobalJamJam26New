using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform trident;

    [Header("Attack")]
    public float attackRange = 1.5f;
    public float attackAngle = 60f; // cone angle
    public float attackCooldown = 0.5f;

    Vector2 moveInput;
    Rigidbody2D rb;
    PlayerInput playerInput;
    public Enemy enemy;

    [Header("Stun Chance")]
    public bool canStun = false;        // toggled by button
    [Range(0f, 1f)]
    public float stunChance = 0.2f;     // 20% default chance
    public float stunDuration = 1f;     // how long enemies get stunned

    public UnityEvent onSlash;

    float lastAttackTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        playerInput.actions["Move"].performed +=
            ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInput.actions["Move"].canceled +=
            _ => moveInput = Vector2.zero;

        playerInput.actions["Attack"].performed +=
            _ => TryAttack();
    }

    void FixedUpdate()
    {
        if (PlayerStats.Instance == null) return;

        rb.MovePosition(
            rb.position +
            moveInput *
            PlayerStats.Instance.moveSpeed *
            Time.fixedDeltaTime
        );
    }

    void Update()
    {
        RotateTridentTowardsMouse();
    }
    public void ActivateStunChance(bool active)
    {
        canStun = active;
        Debug.Log("Stun chance active: " + canStun);
    }

    void RotateTridentTowardsMouse()
    {
        if (trident == null || Camera.main == null) return;

        Vector3 mousePos =
            Camera.main.ScreenToWorldPoint(
                Mouse.current.position.ReadValue());

        Vector2 dir = (mousePos - trident.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //trident.rotation = Quaternion.Euler(0, 0, angle);
        trident.transform.eulerAngles = new Vector3 (0, 0, angle);
    }

    void TryAttack()
    {
        if (Time.time < lastAttackTime + attackCooldown) return;
        lastAttackTime = Time.time;

        Attack();
    }

    void Attack()
    {
        float damage = PlayerStats.Instance.damage;

        Collider2D[] hits =
            Physics2D.OverlapCircleAll(trident.position, attackRange);

        foreach (Collider2D hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy == null) continue;

            Vector2 toEnemy =
                (enemy.transform.position - trident.position).normalized;

            Vector2 forward = trident.right;

            float angle =
                Vector2.Angle(forward, toEnemy);

            if (angle <= attackAngle * 0.5f)
            {
                enemy.TakeDamage(Mathf.RoundToInt(damage));
            }
        }
        if (canStun && Random.value <= stunChance)
        {
            enemy.Stun(stunDuration); // call the stun method on the enemy
        }

        onSlash.Invoke();
    }

    void OnDrawGizmosSelected()
    {
        if (trident == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(trident.position, attackRange);

        Vector3 left =
            Quaternion.Euler(0, 0, -attackAngle / 2) * trident.right;
        Vector3 right =
            Quaternion.Euler(0, 0, attackAngle / 2) * trident.right;

        Gizmos.DrawLine(trident.position,
            trident.position + left * attackRange);
        Gizmos.DrawLine(trident.position,
            trident.position + right * attackRange);
    }

    public void IncreaseAttackRange(float percent)
    {
        attackRange *= (1f + percent); // percent = 0.7 for +70%
        Debug.Log("New attack range: " + attackRange);
    }
}