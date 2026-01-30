using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform trident;
    public float attackRange = 1.5f;
    public int attackDamage = 10;

    Vector2 moveInput;
    Rigidbody2D rb;

    PlayerInput playerInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        // Subscribe to InputSystem callbacks
        playerInput.actions["Move"].performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInput.actions["Move"].canceled += ctx => moveInput = Vector2.zero;

        playerInput.actions["Attack"].performed += ctx => Attack();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    void Update()
    {
        RotateTridentTowardsMouse();
    }

    void RotateTridentTowardsMouse()
    {
        if (trident == null) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (mousePos - trident.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        trident.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(trident.position, attackRange);
        foreach (Collider2D hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(attackDamage);
        }

       // Debug.Log("Trident attack executed!");
    }

    void OnDrawGizmosSelected()
    {
        if (trident == null) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(trident.position, attackRange);
    }
}