using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // Singleton Instance
    public static AudioController audioController = null;

    // Components
    public AudioSource sfxSource;    // The audio player for sound effects
    public AudioSource MusicSource;  // The audio player for music

    public AudioClip[] Music;   // Music array

    private int i = 0;

    // Initialise this instance
    private void Awake()
    {
        //Debug.Assert(audioController == null, this.gameObject);  // Assert if audioController already exists
        if (audioController = null)
            audioController = this;
        //else
            Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip, 2f);
    }

    public void PlayMusic()
    {
        if (!MusicSource.isPlaying)
        {
            if (i < Music.Length - 1)
            {
                i++;
            }
            else
            {
                i = 0;
            }

            MusicSource.clip = Music[i];
            MusicSource.volume = 0.05f;
            MusicSource.Play();
        }
    }
}