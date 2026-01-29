using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    Vector2 dir;

    public void Init(Vector2 direction)
    {
        dir = direction;
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}