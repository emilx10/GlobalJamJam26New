using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public GameObject bulletPrefab;
    public int poolSize = 20;

    List<GameObject> pool = new();

    void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject b = Instantiate(bulletPrefab);
            b.SetActive(false);
            pool.Add(b);
        }
    }

    public GameObject GetBullet()
    {
        foreach (var b in pool)
        {
            if (!b.activeInHierarchy)
                return b;
        }

        GameObject nb = Instantiate(bulletPrefab);
        nb.SetActive(false);
        pool.Add(nb);
        return nb;
    }
}