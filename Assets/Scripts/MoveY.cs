using UnityEngine;

public class MoveY : MonoBehaviour
{
    float speed;
    float maxY;

    public void Setup(float s, float y)
    {
        speed = s;
        maxY = y;
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if (transform.position.y >= maxY)
            Destroy(gameObject);
    }
}
