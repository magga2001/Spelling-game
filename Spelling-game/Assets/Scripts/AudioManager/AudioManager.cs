using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public string music;

    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            if (s.music)
            {
                s.source.volume = PlayerPrefs.GetFloat("music", 1f);
            }
            else
            {
                s.source.volume = PlayerPrefs.GetFloat("sfx", 1f);
            }

        }
    }

    private void Start()
    {
        Play(music);
    }

    public void ChangeVolume(bool music)
    {
        foreach (Sound s in sounds)
        {
            if (s.music && music)
            {
                s.volume = PlayerPrefs.GetFloat("music", 1f);
                s.source.volume = PlayerPrefs.GetFloat("music", 1f);
            }

            if (!s.music && !music)
            {
                s.volume = PlayerPrefs.GetFloat("sfx", 1f);
                s.source.volume = PlayerPrefs.GetFloat("sfx", 1f);
            }
        }
    }

    public void Play(String name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
            return;
        }

        s.source.Play();
    }
}