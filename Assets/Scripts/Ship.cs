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
