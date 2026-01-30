using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public float spawnInterval = 2f;
    public float spawnXMin = -7f;
    public float spawnXMax = 7f;
    public float spawnY = -5f; // start below screen

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        float x = Random.Range(spawnXMin, spawnXMax);
        Vector2 spawnPos = new Vector2(x, spawnY);
        EnemyPool.Instance.GetEnemy(spawnPos);
    }
}