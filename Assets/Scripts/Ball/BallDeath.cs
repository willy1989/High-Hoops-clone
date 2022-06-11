using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BallDeath : MonoBehaviour
{
    [SerializeField] private BallVfx ballVfx;
    [SerializeField] private BallColorManager ballColorManager;
    [SerializeField] private BallAnimation ballAnimation;

    [SerializeField] private Collider ballCollider;

    [SerializeField] private Renderer ballRenderer;

    public void ToggleBallOn()
    {
        StartCoroutine(SpawnBallModel());
    }

    public void ToggleBallOff()
    {
        ballCollider.enabled = false;
        ballRenderer.enabled = false;

        if (ballColorManager.CurrentBallColor == BallColor.White)
            ballVfx.PlayWhiteExplosion();
        else
            ballVfx.PlayBlackExplosion();
    }


    private IEnumerator SpawnBallModel()
    {
        ballCollider.enabled = false;
        ballRenderer.enabled = false;

        if (ballColorManager.CurrentBallColor == BallColor.White)
            yield return StartCoroutine(ballVfx.PlayWhiteImplosion(2f));
        else
            yield return StartCoroutine(ballVfx.PlayBlackImplosion(2f));

        yield return new WaitForSeconds(0.75f);

        ballCollider.enabled = true;
        ballRenderer.enabled = true;

        ballAnimation.SpawnBallAnimation();
    }

    public void Reset()
    {
        ToggleBallOn();
    }
}

