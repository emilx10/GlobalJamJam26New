using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyData[] enemyTypes; // assign Warden and HellLeech

    [Header("Spawn Area Vertical")]
    public float spawnMinX = -5f;
    public float spawnMaxX = 5f;
    public float spawnminY = -5f;
    public float spawnmaxY = -5f;
    [Header("Spawn Interval")]
    public float spawnInterval = 2f;
    float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = spawnInterval;
            SpawnRandomEnemy();
        }
    }

    void SpawnRandomEnemy()
    {
        if (enemyTypes.Length == 0) return;

        // pick random enemy type
        EnemyData data = enemyTypes[Random.Range(0, enemyTypes.Length)];

        Vector2 pos = Vector2.zero;

        if (data.type == EnemyType.HellLeech)
        {
            pos = new Vector2(data.minY, data.maxY);
        }
        else if (data.type == EnemyType.Warden)
        {
            pos = new Vector2(data.minX, data.maxX); // start horizontal movement at minX
        }

        EnemyPool.Instance.Spawn(data, pos);
    }

    public void ReduceSpawnInterval(float amount)
    {
        spawnInterval = Mathf.Max(0.1f, spawnInterval - amount); // clamp to avoid zero or negative
        Debug.Log("New spawn interval: " + spawnInterval);
    }

}