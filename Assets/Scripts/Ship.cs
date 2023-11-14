using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid") && !isInvincible)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.PlayerHit();
        }
    }

    public IEnumerator BlinkAndInvincible(float duration)
    {
        isInvincible = true;
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        spriteRenderer.enabled = true;
        isInvincible = false;
    }
}
/* Here is the explanation for the code above:
1. I check the collision of the ship with the asteroid. If the ship collides with an asteroid and the ship is not invincible, then it will call the PlayerHit() method that is in the GameManager script.
2. In the PlayerHit() method, I check if the player is still alive. If the player is still alive, then it will call the BlinkAndInvincible() method in the Ship script.
3. In the BlinkAndInvincible() method, I set isInvincible to true. Then, I set endTime to the current time plus the duration of the invincible time. The invincible time is 3 seconds. 
4. Then, I enter the while loop. The while loop will be executed until the endTime is reached. The while loop will alternate betIen setting the spriteRenderer to visible and invisible.
5. Finally, I set isInvincible to false. This will allow the player to be able to be hit by an asteroid again. */
