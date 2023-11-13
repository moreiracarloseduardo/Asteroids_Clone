using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public GameObject[] asteroidPrefabs; // Lista de asteroides disponíveis
    public int initialAsteroidCount = 3; // Número inicial de asteroides
    public float minAsteroidSpeed = 1f; // Velocidade mínima dos asteroides
    public float maxAsteroidSpeed = 3f; // Velocidade máxima dos asteroides

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
        // Escolhe um tipo aleatório de asteroide da lista
        GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

        // Obtém a posição aleatória fora da tela
        Vector2 spawnPosition = GetRandomSpawnPosition();

        // Instancia o asteroide na posição calculada
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

        // Define uma velocidade aleatória para o asteroide
        Rigidbody2D asteroidRb = asteroid.GetComponent<Rigidbody2D>();
        float asteroidSpeed = Random.Range(minAsteroidSpeed, maxAsteroidSpeed);
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        asteroidRb.velocity = randomDirection * asteroidSpeed;
    }

    Vector2 GetRandomSpawnDirection()
    {
        float angle;

        // Evitar cantos
        do
        {
            angle = Random.Range(0f, 360f);
        } while ((angle > 30f && angle < 60f) || (angle > 120f && angle < 150f) || (angle > 210f && angle < 240f) || (angle > 300f && angle < 330f));

        Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        return direction.normalized;
    }

    Vector2 GetRandomSpawnPosition()
    {
        Camera mainCamera = Camera.main;

        float halfHeight = mainCamera.orthographicSize;
        float halfWidth = halfHeight * mainCamera.aspect;

        // Definindo uma margem para garantir que os asteroides comecem fora da tela
        float spawnDistance = 2f;

        // Direção aleatória para a posição inicial (não totalmente diagonal)
        Vector2 spawnDirection = GetRandomSpawnDirection();

        float spawnX = spawnDirection.x * (halfWidth + spawnDistance) - 1f;
        float spawnY = spawnDirection.y * (halfHeight + spawnDistance) - 1f;

        Vector2 spawnPosition = new Vector2(spawnX, spawnY);

        return spawnPosition;
    }

}
