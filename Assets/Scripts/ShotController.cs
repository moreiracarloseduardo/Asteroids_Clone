using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    public GameObject shotPrefab;
    public Transform shotSpawnPoint;
    public float fireInterval = 0.2f; // Intervalo entre os tiros em segundos
    public float despawnTime = 2f; // Tempo até o tiro ser despawned em segundos

    private bool isFiring = false;
    private float timeSinceLastShot = 0f;

    // Lista para armazenar os tiros em pool
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

            // Verifica se passou tempo suficiente para disparar outro tiro
            if (timeSinceLastShot >= fireInterval)
            {
                FireBullet();
                timeSinceLastShot = 0f;
            }
        }
    }

    void FireBullet()
    {
        // Procura por um tiro disponível na pool
        GameObject newBullet = GetPooledBullet();

        if (newBullet == null)
        {
            // Se a pool estiver vazia, cria um novo tiro
            newBullet = Instantiate(shotPrefab, shotSpawnPoint.position, shotSpawnPoint.rotation);
            bulletPool.Add(newBullet);
        }
        else
        {
            // Se houver um tiro disponível na pool, reutiliza-o
            newBullet.transform.position = shotSpawnPoint.position;
            newBullet.transform.rotation = shotSpawnPoint.rotation;
            newBullet.SetActive(true);
        }

        // Define a velocidade do tiro para sempre avançar
        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = shotSpawnPoint.up * FindObjectOfType<ShipController>().maxThrustSpeed * 2f;

        // Redefine o tempo de spawn do projétil
        newBullet.GetComponent<Shot>().SetSpawnTime(Time.time);
    }

    void UpdateBulletPool()
    {
        // Desativa tiros que existem há muito tempo
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
        // Verifica se o tiro existe há muito tempo
        return Time.time - bullet.GetComponent<Shot>().GetSpawnTime() > despawnTime;
    }

    GameObject GetPooledBullet()
    {
        // Procura por um tiro disponível na pool
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeSelf)
            {
                return bulletPool[i];
            }
        }

        // Se a pool estiver vazia, cria um novo tiro
        GameObject newBullet = Instantiate(shotPrefab, shotSpawnPoint.position, shotSpawnPoint.rotation);
        bulletPool.Add(newBullet);

        return newBullet;
    }
    
}
