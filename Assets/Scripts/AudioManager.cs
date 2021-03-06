using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private static AudioManager _Instance;

    public static AudioManager instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = GameObject.FindObjectOfType<AudioManager>();
                if (_Instance == null)
                {
                    GameObject container = Instantiate(Resources.Load("SoundManager", typeof(GameObject))) as GameObject;

                    if (container != null)
                    {
                        AudioManager aud = container.GetComponent<AudioManager>();

                        if (aud != null)
                        {
                            _Instance = aud;
                        }
                    }
                }
            }
            return _Instance;
        }
    }

    private void Awake()
    {
        foreach (Sound s in instance.sounds)
        {
            s.source = this.gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volume / 2f, s.volume / 2f));

        s.source.Stop ();
    }

    public void ChangeVolume(string sound, float volume)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        
        s.source.volume = volume;
    }

    public bool CheckIfPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);

        return s.source.isPlaying;
    }
}
