using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public GameObject[] largeAsteroidPrefabs;
    public GameObject[] mediumAsteroidPrefabs;
    public GameObject[] smallAsteroidPrefabs;
    public int initialAsteroidCount = 3; 
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
    }
    Vector2 GetSpawnPositionFromAngle(float angle)
    {
        float radius = 15f; 
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
        isWaveInProgress = false; 
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

       
        float deviationAngle = Random.Range(-45f, 45f);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + deviationAngle;
        Vector2 deviatedDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

        asteroidRb.velocity = deviatedDirection * speed;

        
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
/* Here is the explanation for the code above:
1. I created a new class called AsteroidManager, which will be responsible for managing the asteroids.
2. I added public fields for the asteroid prefabs. These fields will be populated in the Unity Editor.
3. I added public fields for the initial asteroid count and speed. I will use these fields to initialize the first wave of asteroids.
4. I added private fields for the current wave asteroid count, the total number of asteroids in the current wave, and the number of active asteroids.
5. I added a private field for the boolean variable isWaveInProgress, which will be used to check if a wave is in progress.
6. I added the Start method, which will be used to initialize the first wave of asteroids.
7. I added the InitializeWave method, which will be used to initialize a new wave of asteroids.
8. I added the SpawnAsteroidsAtRandomPositions method, which will be used to spawn a given number of asteroids at random positions.
9. I added the GetSpawnPositionFromAngle method, which will be used to get the spawn position of an asteroid from an angle.
10. I added the SpawnAsteroids method, which will be used to spawn a given number of asteroids at a given position and direction.
11. I added the Update method, which will be used to check if a wave is in progress.
12. I added the AsteroidDestroyed method, which will be called when an asteroid is destroyed.
13. I added the StartNextWave method, which will be used to start the next wave of asteroids.
14. I added the SpawnRandomAsteroid method, which will be used to spawn a single asteroid.
15. I added the GetAsteroidPrefabs method, which will be used to get the asteroid prefabs of a given size. */