using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HoopVfx : MonoBehaviour
{
    [SerializeField] private VisualEffect sparksVFX;

    public void PlaySparksVFX()
    {
        sparksVFX.Play();
    }
}
