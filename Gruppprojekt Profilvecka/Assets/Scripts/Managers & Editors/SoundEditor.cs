using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class SoundEditor
{
    public string name;

    public AudioClip ClipSound;

    [Range(0f,1f)]
    public float volume;
    [Range(.1f,3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource Source;
}
