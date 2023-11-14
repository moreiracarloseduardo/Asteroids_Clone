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
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o projétil colidiu com um asteroide
        if (other.gameObject.CompareTag("Asteroid"))
        {
            // Desativa o projétil
            this.gameObject.SetActive(false);
        }
    }
}
