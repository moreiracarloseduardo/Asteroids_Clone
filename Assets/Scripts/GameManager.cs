using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject shipPrefab;

    void Start()
    {
        // Spawn the nave at the center of the screen
        SpawnShip();
    }

    void SpawnShip()
    {
        // Instantiate a new nave at the center of the screen
        Instantiate(shipPrefab, Vector3.zero, Quaternion.identity)
            .AddComponent<ScreenWrapper>();
    }
    
}
