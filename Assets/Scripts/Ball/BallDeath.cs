using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDeath : MonoBehaviour
{
    [SerializeField] private Collider ballCollider;

    [SerializeField] private Renderer ballRenderer;

    public void ToggleBall(bool onOff)
    {
        ballCollider.enabled = onOff;
        ballRenderer.enabled = onOff;
    }

    public void Reset()
    {
        ToggleBall(onOff: true);
    }
}

