using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 1.5f;

    float timer;

    void Update()
    {
        if (Time.timeScale == 0f) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = spawnRate;
            Vector2 pos = Random.insideUnitCircle.normalized * 8f;
            Instantiate(enemyPrefab, pos, Quaternion.identity);
        }
    }
}