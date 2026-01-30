using UnityEngine;

public class ChaosOrb : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CurrencyManager.Instance.Add(1);
            Destroy(gameObject);
        }
    }
}