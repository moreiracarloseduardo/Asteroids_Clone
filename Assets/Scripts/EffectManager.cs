using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }

    public GameObject effectPrefab;
    public ObjectPool effectPool;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            effectPool = new ObjectPool(effectPrefab, 10);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public class ObjectPool
    {
        private GameObject prefab;
        private Queue<GameObject> pool;

        public ObjectPool(GameObject prefab, int initialSize)
        {
            this.prefab = prefab;
            pool = new Queue<GameObject>();

            for (int i = 0; i < initialSize; i++)
            {
                GameObject obj = GameObject.Instantiate(prefab);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }
        }

        public GameObject Get()
        {
            if (pool.Count == 0)
            {
                GameObject obj = GameObject.Instantiate(prefab);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }

            return pool.Dequeue();
        }

        public void Return(GameObject obj)
        {
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}
