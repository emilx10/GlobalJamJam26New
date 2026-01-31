using UnityEngine;

public class EnemySpawnerMoveY : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    [Header("Spawn")]
    [SerializeField] float spawnInterval = 2f;
    [SerializeField] float minX = -6f;
    [SerializeField] float maxX = 6f;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float destroyY = 20f;

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
        float x = Random.Range(minX, maxX);
        Vector3 pos = new Vector3(x, transform.position.y, 0);

        GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);

        Enemy enemyStats = enemy.GetComponent<Enemy>();
        float actualSpeed = enemyStats != null ? enemyStats.moveSpeed : 3f;

        enemy.AddComponent<MoveY>().Setup(actualSpeed, destroyY);
    }

}
