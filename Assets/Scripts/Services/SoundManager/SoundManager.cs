using System;
using UnityEngine;


public enum SoundType
{
    Button_Click,
    Background1,
    Connect
}

[Serializable]
public class Sound
{
    public SoundType Type;
    public AudioClip Clip;
    [Range(0f, 1f)] public float Volume;
}
public class SoundManager : GenericMonoSingleton<SoundManager>
{
    [SerializeField] private Sound[] _sounds;
    [SerializeField] private AudioSource _soundFX1;
    [SerializeField] private AudioSource _soundBG1;

    public void PlayFX1(SoundType type)
    {
        Sound sound = Array.Find(_sounds, i => i.Type == type);
        _soundFX1.clip = sound.Clip;
        _soundFX1.volume = sound.Volume;
        _soundFX1.Play();
    }

    public void PlayBG1(SoundType type)
    {
        Sound sound = Array.Find(_sounds, i => i.Type == type);
        _soundBG1.clip = sound.Clip;
        _soundBG1.volume = sound.Volume;
        _soundBG1.loop = true;
        _soundBG1.Play();
    }
}
