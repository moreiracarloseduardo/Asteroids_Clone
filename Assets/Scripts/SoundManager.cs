using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance { get; private set; }

    public AudioClip shootSound;
    public AudioClip destructionSoundLarge;
    public AudioClip destructionSoundMedium;
    public AudioClip destructionSoundSmall;

    private AudioSource audioSource;

    void Awake()
    {
        // Implementa o padrão Singleton para garantir que só haja uma instância de SoundManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayShootSound()
    {
        audioSource.PlayOneShot(shootSound);
    }

    public void PlayDestructionSoundLarge()
    {
        audioSource.PlayOneShot(destructionSoundLarge);
    }

    public void PlayDestructionSoundMedium()
    {
        audioSource.PlayOneShot(destructionSoundMedium);
    }

    public void PlayDestructionSoundSmall()
    {
        audioSource.PlayOneShot(destructionSoundSmall);
    }
}
