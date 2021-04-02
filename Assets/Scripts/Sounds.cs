using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sounds
{
    public string name;

    public AudioClip clip;

    public float volume;
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

    public float spatialBlend;
}

