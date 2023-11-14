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
        
        if (other.gameObject.CompareTag("Asteroid"))
        {
           
            this.gameObject.SetActive(false);

          
            GameObject effect = EffectManager.Instance.effectPool.Get();
            effect.transform.position = transform.position;
            effect.SetActive(true);
        }
    }
}
/* Here is the explanation for the code above:
1. The code above is the code for the bullet to hit the asteroid.
2. If the bullet hits the asteroid, then the bullet will be hidden.
3. When the bullet hits the asteroid, the effect will appear.
4. The effect will appear where the bullet hits the asteroid. */