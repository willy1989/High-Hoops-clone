using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : Singleton<SoundEffectManager>
{
    [SerializeField] private SoundEffect_ScriptableObject[] soundEffects;

    private Dictionary<string, AudioSource> audioSourceDictionary = new Dictionary<string, AudioSource>();

    protected override void Awake()
    {
        base.Awake();

        SetupAudioSource();
    }

    private void SetupAudioSource()
    {
        for (int i = 0; i < soundEffects.Length; i++)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.clip = soundEffects[i].AudioClip;

            audioSource.playOnAwake = false;

            audioSourceDictionary.Add(soundEffects[i].Name.ToLower().Trim(), audioSource);
        }
    }

    public void PlaySoundEffect(string soundEffectName)
    {
        try
        {
            soundEffectName = soundEffectName.ToLower().Trim();

            audioSourceDictionary[soundEffectName].Play();
        }

        catch (KeyNotFoundException)
        {
            Debug.LogError(soundEffectName.ToString() + " didn't match the name of any of the SoundEffects. Please make sure they match.");
        }
    }
}
