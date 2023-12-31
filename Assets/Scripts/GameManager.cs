using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject shipPrefab;
    public GameObject[] livesUI;
    public TextMeshProUGUI scoreText;
    public int score = 0;
    private int lives;

    void Start()
    {
        lives = livesUI.Length;
        SpawnShip();
        UpdateScoreText();
    }

    void SpawnShip()
    {
        // Instantiate a new nave at the center of the screen
        Instantiate(shipPrefab, Vector3.zero, Quaternion.identity)
            .AddComponent<ScreenWrapper>();
    }
    public void PlayerHit()
    {
        lives--;
        if (lives >= 0)
        {
            livesUI[lives].SetActive(false); // Desativa o GameObject correspondente à vida perdida
        }
        if (lives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia o jogo
            score = 0; // Reinicia a pontuação
            UpdateScoreText();
        }
        else
        {
            // Faz o jogador piscar e ficar invulnerável por 3 segundos
            Ship ship = FindObjectOfType<Ship>();
            ship.StartCoroutine(ship.BlinkAndInvincible(3f));
        }
    }
    public void AsteroidDestroyed(Asteroid.AsteroidSize size)
    {
        switch (size)
        {
            case Asteroid.AsteroidSize.Large:
                score += 20;
                break;
            case Asteroid.AsteroidSize.Medium:
                score += 50;
                break;
            case Asteroid.AsteroidSize.Small:
                score += 100;
                break;
        }
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

}
/* Here is the explanation for the code above:
1. The Start method is called when the script is instantiated.
2. The SpawnShip method instantiates a new nave at the center of the screen.
3. The PlayerHit method is called when the nave is hit by an asteroid.
4. The AsteroidDestroyed method is called when an asteroid is destroyed.
5. The UpdateScoreText method updates the score text with the current score. */