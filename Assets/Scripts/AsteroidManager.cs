using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public GameObject[] largeAsteroidPrefabs;
    public GameObject[] mediumAsteroidPrefabs;
    public GameObject[] smallAsteroidPrefabs;
    public int initialAsteroidCount = 3; // Número inicial de asteroides
    public float initialAsteroidSpeed = 1.0f;
    private int currentWaveAsteroidCount;
    private bool isWaveInProgress = false;
    private int totalAsteroidsInWave;
    private int activeAsteroids;

    void Start()
    {
        InitializeWave(initialAsteroidCount);
    }
    void InitializeWave(int count)
    {
        currentWaveAsteroidCount = count;
        totalAsteroidsInWave = currentWaveAsteroidCount * 7;
        activeAsteroids = 0;

        Debug.Log("Initializing wave. Asteroid count: " + currentWaveAsteroidCount);

        SpawnAsteroidsAtRandomPositions(currentWaveAsteroidCount, Asteroid.AsteroidSize.Large, initialAsteroidSpeed);
    }
    void SpawnAsteroidsAtRandomPositions(int count, Asteroid.AsteroidSize size, float speed)
    {
        for (int i = 0; i < count; i++)
        {
            float angle = Random.Range(0f, 360f);
            Vector2 spawnPosition = GetSpawnPositionFromAngle(angle);
            SpawnRandomAsteroid(size, speed, spawnPosition, -spawnPosition.normalized);
        }
        Debug.Log("Spawned asteroids. Active asteroids: " + activeAsteroids);
    }
    Vector2 GetSpawnPositionFromAngle(float angle)
    {
        float radius = Mathf.Max(Screen.width, Screen.height);
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;
    }
    void SpawnAsteroids(int count, Asteroid.AsteroidSize size, float speed, Vector2 position, Vector2 direction)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnRandomAsteroid(size, speed, position, direction);
        }
    }

    void Update()
    {
        if (!isWaveInProgress && activeAsteroids == 0)
        {
            StartCoroutine(StartNextWave());
        }
        else
        {
        }
    }

    IEnumerator StartNextWave()
    {
        isWaveInProgress = true;
        yield return new WaitForSeconds(3);
        isWaveInProgress = false;  // Defina como false antes de chamar InitializeWave
        InitializeWave(currentWaveAsteroidCount + 2);
    }


    public void AsteroidDestroyed(Asteroid asteroid)
    {
        Vector2 destroyedPosition = asteroid.transform.position;
        Vector2 destroyedDirection = asteroid.GetComponent<Rigidbody2D>().velocity.normalized;

        if (asteroid.Size == Asteroid.AsteroidSize.Large)
        {
            SpawnAsteroids(2, Asteroid.AsteroidSize.Medium, asteroid.Speed * 2.4f, destroyedPosition, destroyedDirection);
        }
        else if (asteroid.Size == Asteroid.AsteroidSize.Medium)
        {
            SpawnAsteroids(2, Asteroid.AsteroidSize.Small, asteroid.Speed * 2.6f, destroyedPosition, destroyedDirection);
        }

        // Decrementa a contagem de asteroides ativos
        activeAsteroids--;

        if (activeAsteroids == 0)
        {
            isWaveInProgress = false;
        }
    }



    void SpawnRandomAsteroid(Asteroid.AsteroidSize size, float speed, Vector2 position, Vector2 direction)
    {
        activeAsteroids++;

        GameObject[] prefabs = GetAsteroidPrefabs(size);
        GameObject asteroidPrefab = prefabs[Random.Range(0, prefabs.Length)];
        GameObject asteroid = Instantiate(asteroidPrefab, position, Quaternion.identity);

        asteroid.GetComponent<Asteroid>().Size = size;
        asteroid.GetComponent<Asteroid>().Speed = speed;

        Rigidbody2D asteroidRb = asteroid.GetComponent<Rigidbody2D>();

        // Add a small deviation to the direction
        float deviationAngle = Random.Range(-45f, 45f);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + deviationAngle;
        Vector2 deviatedDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

        asteroidRb.velocity = deviatedDirection * speed;

        // Add a random angular velocity only for medium and small asteroids
        if (size != Asteroid.AsteroidSize.Large)
        {
            float angularVelocity = Random.Range(-200f, 200f);
            asteroidRb.angularVelocity = angularVelocity;
        }
    }


    GameObject[] GetAsteroidPrefabs(Asteroid.AsteroidSize size)
    {
        switch (size)
        {
            case Asteroid.AsteroidSize.Large:
                return largeAsteroidPrefabs;
            case Asteroid.AsteroidSize.Medium:
                return mediumAsteroidPrefabs;
            case Asteroid.AsteroidSize.Small:
                return smallAsteroidPrefabs;
            default:
                return new GameObject[0];
        }
    }


}
