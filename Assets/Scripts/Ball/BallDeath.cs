using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BallDeath : MonoBehaviour
{
    [SerializeField] private VisualEffect whiteExplosion;
    [SerializeField] private VisualEffect blackExplosion;
    [SerializeField] private VisualEffect whiteImplosion;
    [SerializeField] private VisualEffect blackImplosion;
    [SerializeField] private BallColorManager ballColorManager;
    [SerializeField] private BallAnimation ballAnimation;

    [SerializeField] private Collider ballCollider;

    [SerializeField] private Renderer ballRenderer;

    public void ToggleBallOn()
    {
        if (ballColorManager.CurrentBallColor == BallColor.White)
            StartCoroutine(SpawnBallModel(whiteImplosion));
        else
            StartCoroutine(SpawnBallModel(blackImplosion));
    }

    public void ToggleBallOff()
    {
        ballCollider.enabled = false;
        ballRenderer.enabled = false;

        if (ballColorManager.CurrentBallColor == BallColor.White)
            whiteExplosion.Play();
        else
            blackExplosion.Play();
    }


    private IEnumerator SpawnBallModel(VisualEffect visualEffect)
    {
        ballCollider.enabled = false;
        ballRenderer.enabled = false;

        visualEffect.Play();
        yield return new WaitForSeconds(2f);
        visualEffect.Stop();
        yield return new WaitForSeconds(0.5f);

        ballCollider.enabled = true;
        ballRenderer.enabled = true;

        ballAnimation.SpawnBallAnimation();
    }

    public void Reset()
    {
        ToggleBallOn();
    }
}

