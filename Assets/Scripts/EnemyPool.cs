using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance;

    [Header("Enemy Types")]
    public EnemyData[] enemyTypes; // assign multiple EnemyData in inspector

    Dictionary<EnemyData, Queue<Enemy>> pools = new();

    void Awake()
    {
        Instance = this;

        // initialize queues
        foreach (var data in enemyTypes)
        {
            if (!pools.ContainsKey(data))
                pools[data] = new Queue<Enemy>();
        }
    }

    /// <summary>
    /// Spawns a random enemy type at the given position
    /// </summary>
    public Enemy Spawn(Vector2 pos)
    {
        if (enemyTypes.Length == 0)
        {
            Debug.LogError("No EnemyData assigned to EnemyPool!");
            return null;
        }

        // Pick a random type
        EnemyData data = enemyTypes[Random.Range(0, enemyTypes.Length)];

        return Spawn(data, pos);
    }

    /// <summary>
    /// Spawns a specific enemy type at the given position
    /// </summary>
    public Enemy Spawn(EnemyData data, Vector2 pos)
    {
        if (!pools.ContainsKey(data))
            pools[data] = new Queue<Enemy>();

        Enemy enemy;

        if (pools[data].Count > 0)
        {
            enemy = pools[data].Dequeue();
        }
        else
        {
            enemy = Instantiate(data.prefab).GetComponent<Enemy>();
        }

        enemy.transform.position = pos;
        enemy.gameObject.SetActive(true);
        enemy.Init(data);

        return enemy;
    }

    /// <summary>
    /// Returns enemy to the pool
    /// </summary>
    public void ReturnEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);

        if (!pools.ContainsKey(enemy.data))
            pools[enemy.data] = new Queue<Enemy>();

        pools[enemy.data].Enqueue(enemy);
    }
}