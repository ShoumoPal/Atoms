using JetBrains.Annotations;
using System;
using UnityEngine;


public enum SoundType
{
    Button_Click,
    Background1,
    Connect,
    Level_Complete
}

public enum SourceType
{
    FX1,
    FX2,
    BG1
}

[Serializable]
public class Source
{
    public SourceType Type;
    public AudioSource AudioSource;
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
    [SerializeField] private Source[] _sources;

    public void Play(SourceType sourceType, SoundType type)
    {
        Source source = Array.Find(_sources, i => i.Type == sourceType);
        Sound sound = Array.Find(_sounds, i => i.Type == type);
        source.AudioSource.clip = sound.Clip;
        source.AudioSource.volume = sound.Volume;
        source.AudioSource.Play();
    }
}
