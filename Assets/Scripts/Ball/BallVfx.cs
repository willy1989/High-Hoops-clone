using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BallVfx : MonoBehaviour
{
    [SerializeField] private VisualEffect whiteExplosion;
    [SerializeField] private VisualEffect blackExplosion;

    public void PlayWhiteExplosion()
    {
        whiteExplosion.Play();
    }

    public void PlayBlackExplosion()
    {
        blackExplosion.Play();
    }
}
