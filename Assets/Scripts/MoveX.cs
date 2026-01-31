using UnityEngine;

public class MoveX : MonoBehaviour
{
    float speed;
    float maxX;

    public void Setup(float s, float x)
    {
        speed = s;
        maxX = x;
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (transform.position.x >= maxX)
            Destroy(gameObject);
    }
}
