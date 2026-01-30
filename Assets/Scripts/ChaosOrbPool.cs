using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosOrbPool : MonoBehaviour
{
    public static ChaosOrbPool Instance;

    public GameObject orbPrefab;
    public int poolSize = 20;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(orbPrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public void Spawn(Vector2 position)
    {
        GameObject orb;
        if (pool.Count == 0)
        {
            orb = Instantiate(orbPrefab);
        }
        else
        {
            orb = pool.Dequeue();
        }

        orb.transform.position = position;
        orb.SetActive(true);

        // Optional: despawn after 10 seconds
        StartCoroutine(DespawnAfter(orb, 10f));
    }

    IEnumerator DespawnAfter(GameObject orb, float time)
    {
        yield return new WaitForSeconds(time);
        orb.SetActive(false);
        pool.Enqueue(orb);
    }
}