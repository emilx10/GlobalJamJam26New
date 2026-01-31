using System.Collections.Generic;
using UnityEngine;

public class DustPooler : MonoBehaviour
{
    public static DustPooler Instance;
    public CanvasDustParticle prefab;
    public int poolSize = 50;

    private List<CanvasDustParticle> pool = new List<CanvasDustParticle>();

    void Awake() => Instance = this;

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewParticle();
        }
    }

    private CanvasDustParticle CreateNewParticle()
    {
        CanvasDustParticle obj = Instantiate(prefab, transform);
        obj.gameObject.SetActive(false);
        pool.Add(obj);
        return obj;
    }

    public CanvasDustParticle GetParticle()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeSelf) return pool[i];
        }
        return CreateNewParticle();
    }
}