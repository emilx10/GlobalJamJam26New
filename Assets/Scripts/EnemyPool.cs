using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance;

    public GameObject enemyPrefab;
    public int poolSize = 20;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(enemyPrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetEnemy(Vector2 spawnPos)
    {
        if (pool.Count == 0)
        {
            GameObject obj = Instantiate(enemyPrefab);
            obj.SetActive(true);
            obj.transform.position = spawnPos;
            return obj;
        }

        GameObject enemy = pool.Dequeue();
        enemy.transform.position = spawnPos;
        enemy.SetActive(true);
        return enemy;
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        pool.Enqueue(enemy);
    }
}