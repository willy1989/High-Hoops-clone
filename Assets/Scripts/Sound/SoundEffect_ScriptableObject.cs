using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Sound effect", order = 1)]
public class SoundEffect_ScriptableObject : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private AudioClip audioClip;

    public string Name => name;
    public AudioClip AudioClip => audioClip;
}
