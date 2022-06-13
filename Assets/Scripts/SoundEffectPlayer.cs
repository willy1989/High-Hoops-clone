using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : Singleton<SoundEffectPlayer>
{
    [SerializeField] private AudioSource jumpAudioSource;
    [SerializeField] private AudioSource deathAudioSource;
    [SerializeField] private AudioSource spawnAudioSource;
    [SerializeField] private AudioSource victoryAudioSource;
    [SerializeField] private AudioSource hitHoopCenterAudioSource;
    [SerializeField] private AudioSource hitHoopSideAudioSource;

    private Dictionary<SoudEffect, AudioSource> soundEffectDictionary;

    protected override void Awake()
    {
        base.Awake();

        soundEffectDictionary = new Dictionary<SoudEffect, AudioSource>()
        {
            { SoudEffect.Jump,  jumpAudioSource},
            { SoudEffect.BallDeath,  deathAudioSource},
            { SoudEffect.BallSpawn,  spawnAudioSource},
            { SoudEffect.Victory,  victoryAudioSource},
            { SoudEffect.HitHoopCenter,  hitHoopCenterAudioSource},
            { SoudEffect.HitHoopSide,  hitHoopSideAudioSource}
        };
    }

    public void PlaySoundEffect(SoudEffect soundEffect)
    {
        soundEffectDictionary[soundEffect].Play();
    }
}

public enum SoudEffect
{
    Jump,
    BallDeath,
    BallSpawn,
    Victory,
    HitHoopCenter,
    HitHoopSide
}
