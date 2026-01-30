using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 1f;
    float lifeTime = 3f;

    Vector2 dir;

    public void Init(Vector2 direction)
    {
        dir = direction;
        lifeTime = 3f;
    }

    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
            gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemy>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}