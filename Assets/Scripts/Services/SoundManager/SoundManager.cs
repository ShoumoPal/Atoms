using System;
using UnityEngine;

// Enum for sound type

public enum SoundType
{
    Button_Click,
    Background1,
    Connect,
    Pass_Sound,
    Level_Complete,
    Game_Over
}

// enum for source type

public enum SourceType
{
    FX1,
    FX2,
    BG1
}

// Class for audio source

[Serializable]
public class Source
{
    public SourceType Type;
    public AudioSource AudioSource;
}

// Class for sounds

[Serializable]
public class Sound
{
    public SoundType Type;
    public AudioClip Clip;
    [Range(0f, 1f)] public float Volume;
}

// Script for managing all the sounds in game

public class SoundManager : GenericMonoSingleton<SoundManager>
{
    [SerializeField] private Sound[] _sounds;
    [SerializeField] private Source[] _sources;

    public void Play(SourceType sourceType, SoundType type)
    {
        Source source = Array.Find(_sources, i => i.Type == sourceType);
        Sound sound = Array.Find(_sounds, i => i.Type == type);
        source.AudioSource.clip = sound.Clip;
        source.AudioSource.volume = sound.Volume;
        source.AudioSource.Play();
    }

    public void Stop(SourceType sourceType)
    {
        Source source = Array.Find(_sources, i => i.Type == sourceType);
        source.AudioSource.Stop();
    }
}
