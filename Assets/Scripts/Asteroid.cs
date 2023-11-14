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
            FindObjectOfType<AsteroidManager>().AsteroidDestroyed(this); 
            FindObjectOfType<GameManager>().AsteroidDestroyed(Size);
            switch (Size)
            {
                case AsteroidSize.Large:
                    SoundManager.Instance.PlayDestructionSoundLarge();
                    break;
                case AsteroidSize.Medium:
                    SoundManager.Instance.PlayDestructionSoundMedium();
                    break;
                case AsteroidSize.Small:
                    SoundManager.Instance.PlayDestructionSoundSmall();
                    break;
            }
        }
    }
}
/* Here is the explanation for the code above:
1. The AsteroidSize enum is used to define the size of the asteroid. 
    The AsteroidSize property is used to get and set the size of the asteroid.
2. The Speed property is used to get and set the speed of the asteroid.
3. The isDestroyed variable is used to check if the asteroid is destroyed.
4. The OnTriggerEnter2D method is overridden to detect collisions of the asteroid. 
    When the asteroid collides with a shot, the asteroid is destroyed, 
    the AsteroidDestroyed method is called to update the AsteroidManager, 
    the AsteroidDestroyed method is called to update the GameManager, 
    and the PlayDestructionSoundLarge, PlayDestructionSoundMedium, or 
    PlayDestructionSoundSmall method is called to play the destruction sound. */