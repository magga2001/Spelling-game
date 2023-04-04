using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code is taken from
//Thirslund, A. (Director). (2017, May 31). Introduction to AUDIO in Unity [Video file]. In YouTube. Retrieved March 05, 2023, from https://www.youtube.com/watch?v=6OT43pvUyfY&amp;t=644s
[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;

    public bool music;

    [HideInInspector]
    public AudioSource source;
}