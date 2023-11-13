using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public GameObject[] asteroidPrefabs; // Lista de asteroides disponíveis
    public int initialAsteroidCount = 3; // Número inicial de asteroides
    public float minAsteroidSpeed = 1f; // Velocidade mínima dos asteroides
    public float maxAsteroidSpeed = 2f; // Velocidade máxima dos asteroides

    void Start()
    {
        SpawnAsteroids(initialAsteroidCount);
    }

    void SpawnAsteroids(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnRandomAsteroid();
        }
    }

    void SpawnRandomAsteroid()
    {
        // Choose a random type of asteroid from the list
        GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

        // Get the random position off the screen
        Vector2 spawnPosition = GetRandomSpawnPosition();

        // Instantiate the asteroid at the calculated position
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

        // Set a random speed for the asteroid
        Rigidbody2D asteroidRb = asteroid.GetComponent<Rigidbody2D>();
        float asteroidSpeed = Random.Range(minAsteroidSpeed, maxAsteroidSpeed);
        Vector2 randomDirection = GetRandomSpawnDirection(spawnPosition);
        asteroidRb.velocity = randomDirection * asteroidSpeed;

        asteroid.AddComponent<Asteroid>();
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
