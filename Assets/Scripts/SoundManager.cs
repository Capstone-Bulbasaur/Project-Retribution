using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;
    private AudioSource soundEffectAudio;

    // Memory match audio clips
    public AudioClip rightMatchSound;
    public AudioClip wrongMatchSound;
    public AudioClip potPickedSound;
    public AudioClip winGameSound;

    // Hub world audio clips

    void Start ()
    {
        if (Instance == null){
            Instance = this;
        }
        else if (Instance != this){
            Destroy(gameObject);
        }
        AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources){
            if (source.clip == null){
                soundEffectAudio = source;
            }
        }
    }

    public void PlayOneShot(AudioClip clip)
    {
        soundEffectAudio.PlayOneShot(clip);
    }
}
