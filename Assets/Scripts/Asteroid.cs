using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public AsteroidSize Size { get; set; }
    public enum AsteroidSize
    {
        Large,
        Medium,
        Small
    }
    public float Speed { get; set; }
    private bool isDestroyed = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shot") && !isDestroyed)
        {
            isDestroyed = true;
            Destroy(gameObject);
            FindObjectOfType<AsteroidManager>().AsteroidDestroyed(this); // Incrementa o contador de asteroides destruídos
            FindObjectOfType<GameManager>().AsteroidDestroyed(Size);
        }
    }
}
