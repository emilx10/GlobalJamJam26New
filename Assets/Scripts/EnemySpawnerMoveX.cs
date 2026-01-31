using UnityEngine;

public class EnemySpawnerMoveX : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    [Header("Spawn")]
    [SerializeField] float spawnInterval = 2f;
    [SerializeField] float minY = -4f;
    [SerializeField] float maxY = 4f;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float destroyX = 20f;

    float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = spawnInterval;
            Spawn();
        }
    }

    void Spawn()
    {
        float y = Random.Range(minY, maxY);
        Vector3 pos = new Vector3(transform.position.x, y, 0);

        GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);

        Enemy enemyStats = enemy.GetComponent<Enemy>();
        float actualSpeed = enemyStats != null ? enemyStats.moveSpeed : 3f;

        enemy.AddComponent<MoveX>().Setup(actualSpeed, destroyX);
    }
}
