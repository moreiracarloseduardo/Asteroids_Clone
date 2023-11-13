using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public GameObject[] largeAsteroidPrefabs;
    public GameObject[] mediumAsteroidPrefabs;
    public GameObject[] smallAsteroidPrefabs;
    public int initialAsteroidCount = 3; // Número inicial de asteroides
    public float minAsteroidSpeed = 1f; // Velocidade mínima dos asteroides
    public float maxAsteroidSpeed = 2f; // Velocidade máxima dos asteroides
    public float initialAsteroidSpeed = 1.0f;

    private int asteroidsDestroyed = 0;
    private int currentWaveAsteroidCount;
    private bool isWaveInProgress = false;
    private int totalAsteroidsInWave;
    private int activeAsteroids;

    void Start()
    {
        currentWaveAsteroidCount = initialAsteroidCount;
        totalAsteroidsInWave = currentWaveAsteroidCount * 7;
        activeAsteroids = 0;
        SpawnAsteroids(currentWaveAsteroidCount, Asteroid.AsteroidSize.Large, initialAsteroidSpeed, GetRandomSpawnPosition());
    }
    void SpawnAsteroids(int count, Asteroid.AsteroidSize size, float speed, Vector2 position)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnRandomAsteroid(size, speed, position, false);
            activeAsteroids++;
        }
    }
    void Update()
    {
        if (!isWaveInProgress && asteroidsDestroyed >= currentWaveAsteroidCount)
        {
            StartCoroutine(StartNextWave());
        }
    }
    IEnumerator StartNextWave()
    {
        isWaveInProgress = true;
        yield return new WaitForSeconds(3);
        asteroidsDestroyed = 0;
        currentWaveAsteroidCount += 2;
        totalAsteroidsInWave = currentWaveAsteroidCount * 7;
        activeAsteroids = totalAsteroidsInWave; // Inicializando activeAsteroids
        SpawnBigAsteroid(currentWaveAsteroidCount, Asteroid.AsteroidSize.Large, initialAsteroidSpeed, Vector2.zero);
        isWaveInProgress = false;
    }
    public void AsteroidDestroyed(Asteroid asteroid)
    {
        activeAsteroids--;

        Vector2 destroyedPosition = asteroid.transform.position;

        if (asteroid.Size == Asteroid.AsteroidSize.Large)
        {
            SpawnAsteroids(2, Asteroid.AsteroidSize.Medium, asteroid.Speed * 2.4f, destroyedPosition);
        }
        else if (asteroid.Size == Asteroid.AsteroidSize.Medium)
        {
            SpawnAsteroids(2, Asteroid.AsteroidSize.Small, asteroid.Speed * 2.6f, destroyedPosition);
        }

        if (activeAsteroids == 0)
        {
            currentWaveAsteroidCount += 2;
            totalAsteroidsInWave = currentWaveAsteroidCount * 7;
            SpawnAsteroids(currentWaveAsteroidCount, Asteroid.AsteroidSize.Large, initialAsteroidSpeed, GetRandomSpawnPosition());
        }
    }
    void SpawnBigAsteroid(int count, Asteroid.AsteroidSize size, float speed, Vector2 position)
    {
        bool moveTowardsCenter = (size == Asteroid.AsteroidSize.Large);
        for (int i = 0; i < count; i++)
        {
            SpawnRandomAsteroid(size, speed, position, moveTowardsCenter);
            activeAsteroids++;
        }
    }

    void SpawnRandomAsteroid(Asteroid.AsteroidSize size, float speed, Vector2 position, bool moveTowardsCenter)
    {
        GameObject[] prefabs = new GameObject[0];

        switch (size)
        {
            case Asteroid.AsteroidSize.Large:
                prefabs = largeAsteroidPrefabs;
                break;
            case Asteroid.AsteroidSize.Medium:
                prefabs = mediumAsteroidPrefabs;
                break;
            case Asteroid.AsteroidSize.Small:
                prefabs = smallAsteroidPrefabs;
                break;
        }

        GameObject asteroidPrefab = prefabs[Random.Range(0, prefabs.Length)];
        GameObject asteroid = Instantiate(asteroidPrefab, position, Quaternion.identity);
        asteroid.GetComponent<Asteroid>().Size = size;
        asteroid.GetComponent<Asteroid>().Speed = speed;

        Rigidbody2D asteroidRb = asteroid.GetComponent<Rigidbody2D>();
        Vector2 direction;
        if (moveTowardsCenter)
        {
            direction = GetRandomSpawnDirection(position);
        }
        else
        {
            direction = Random.insideUnitCircle.normalized;
        }
        asteroidRb.velocity = direction * speed;
    }
    Vector2 GetRandomSpawnDirection(Vector2 spawnPosition)
    {
        // Get the direction towards the center of the screen
        Vector2 centerDirection = -spawnPosition.normalized;

        // Add a larger random offset to the direction
        float offsetAngle = Random.Range(-60f, 60f);
        float angle = Mathf.Atan2(centerDirection.y, centerDirection.x) * Mathf.Rad2Deg + offsetAngle;

        Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        return direction.normalized;
    }

    Vector2 GetRandomSpawnPosition()
    {
        Camera mainCamera = Camera.main;

        float halfHeight = mainCamera.orthographicSize;
        float halfWidth = halfHeight * mainCamera.aspect;

        // Defining a margin to ensure that asteroids start off the screen
        float spawnDistance = 2f;

        // Random direction for the initial position (not totally diagonal)
        Vector2 spawnPosition = new Vector2(Random.Range(-halfWidth, halfWidth), Random.Range(-halfHeight, halfHeight));
        Vector2 spawnDirection = GetRandomSpawnDirection(spawnPosition);

        float spawnX = spawnDirection.x * (halfWidth + spawnDistance) - 1f;
        float spawnY = spawnDirection.y * (halfHeight + spawnDistance) - 1f;

        spawnPosition = new Vector2(spawnX, spawnY);

        return spawnPosition;
    }

}
