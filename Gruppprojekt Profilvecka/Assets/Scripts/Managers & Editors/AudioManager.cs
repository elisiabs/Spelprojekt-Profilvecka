using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public SoundEditor[] sounds;

    void Awake()
    {
        foreach (SoundEditor s in sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.ClipSound;

            s.Source.volume = s.volume;
            s.Source.pitch = s.pitch;
        }
    }

    public void Play (string name)
    {
        SoundEditor s = Array.Find(sounds, sound => sound.name == name);
        s.Source.Play();
    }
}
