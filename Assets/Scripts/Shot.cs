using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private float spawnTime;

    void Start()
    {
        spawnTime = Time.time;
    }

    public float GetSpawnTime()
    {
        return spawnTime;
    }
    public void SetSpawnTime(float time)
    {
        spawnTime = time;
    }
}
