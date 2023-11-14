using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    public GameObject shotPrefab;
    public Transform shotSpawnPoint;
    public float fireInterval = 0.2f; 
    public float despawnTime = 2f; 

    private bool isFiring = false;
    private float timeSinceLastShot = 0f;

    private List<GameObject> bulletPool = new List<GameObject>();
    void Update()
    {
        HandleShootingInput();
        UpdateBulletPool();
    }
    void HandleShootingInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isFiring = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isFiring = false;
        }

        if (isFiring)
        {
            timeSinceLastShot += Time.deltaTime;

            if (timeSinceLastShot >= fireInterval)
            {
                FireBullet();
                timeSinceLastShot = 0f;
            }
        }
    }

    void FireBullet()
    {
        
        GameObject newBullet = GetPooledBullet();

        if (newBullet == null)
        {
            
            newBullet = Instantiate(shotPrefab, shotSpawnPoint.position, shotSpawnPoint.rotation);
            bulletPool.Add(newBullet);
        }
        else
        {
            
            newBullet.transform.position = shotSpawnPoint.position;
            newBullet.transform.rotation = shotSpawnPoint.rotation;
            newBullet.SetActive(true);
        }

        
        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = shotSpawnPoint.up * FindObjectOfType<ShipController>().maxThrustSpeed * 2f;

        
        newBullet.GetComponent<Shot>().SetSpawnTime(Time.time);
        SoundManager.Instance.PlayShootSound();
    }

    void UpdateBulletPool()
    {
        
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeSelf)
            {
                continue;
            }

            if (IsTimeToDespawn(bulletPool[i]))
            {
                bulletPool[i].SetActive(false);
            }
        }
    }


    bool IsTimeToDespawn(GameObject bullet)
    {
       
        return Time.time - bullet.GetComponent<Shot>().GetSpawnTime() > despawnTime;
    }

    GameObject GetPooledBullet()
    {
        
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeSelf)
            {
                return bulletPool[i];
            }
        }

       
        GameObject newBullet = Instantiate(shotPrefab, shotSpawnPoint.position, shotSpawnPoint.rotation);
        bulletPool.Add(newBullet);

        return newBullet;
    }
    
}
/* Here is the explanation for the code above:
1. The shotPrefab variable is used to store the shot prefab that will be instantiated when the player fires. This variable is set in the inspector.
2. The shotSpawnPoint variable is used to store the shot spawn point. This variable is set in the inspector.
3. The fireInterval variable is used to store the interval between shots. This variable is set in the inspector.
4. The despawnTime variable is used to store the time that the shot will be despawned. This variable is set in the inspector.
5. The isFiring variable is used to store if the player is firing or not.
6. The timeSinceLastShot variable is used to store the time elapsed since the last shot.
7. The bulletPool variable is used to store the list of pooled bullets.
8. The Update method is used to handle the shooting input and update the bullet pool.
9. The HandleShootingInput method is used to handle the shooting input.
10. The FireBullet method is used to fire the bullet.
11. The UpdateBulletPool method is used to update the bullet pool.
12. The IsTimeToDespawn method is used to check if it is time to despawn the specified bullet.
13. The GetPooledBullet method is used to get a pooled bullet from the bullet pool. */