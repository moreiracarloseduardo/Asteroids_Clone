using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shot"))
        {
            // Se colidir com um tiro, destrói o asteroide
            Destroy(gameObject);
        }
    }
}
