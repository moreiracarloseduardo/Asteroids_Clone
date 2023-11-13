using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private bool isDestroyed = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shot") && !isDestroyed)
        {
            isDestroyed = true;
            Destroy(gameObject);
            FindObjectOfType<AsteroidManager>().AsteroidDestroyed(); // Incrementa o contador de asteroides destruídos
        }
    }
}
