using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BallDeath : MonoBehaviour, IResetable
{
    [SerializeField] private BallVfx ballVfx;
    [SerializeField] private BallColorManager ballColorManager;
    [SerializeField] private BallAnimation ballAnimation;

    [SerializeField] private Collider ballCollider;

    [SerializeField] private Renderer ballRenderer;
    private void Awake()
    {
        ResetState();
    }

    private void Start()
    {
        GameLoopManager.Instance.ResetGameEvent += ResetState;
    }

    public void ToggleBallOn()
    {
        StartCoroutine(SpawnBallModel());
    }

    public void ToggleBallOff()
    {
        ballCollider.enabled = false;
        ballRenderer.enabled = false;

        SoundEffectPlayer.Instance.PlaySoundEffect(SoudEffect.BallDeath);

        if (ballColorManager.CurrentBallColor == BallColor.Blue)
            ballVfx.PlayBlueExplosion();
        else
            ballVfx.PlayRedExplosion();
    }

    private IEnumerator SpawnBallModel()
    {
        ballCollider.enabled = false;
        ballRenderer.enabled = false;

        if (ballColorManager.CurrentBallColor == BallColor.Blue)
            yield return StartCoroutine(ballVfx.PlayBlueImplosion(2.5f));
        else
            yield return StartCoroutine(ballVfx.PlayRedImplosion(2.5f));

        yield return new WaitForSeconds(0.5f);

        ballCollider.enabled = true;
        ballRenderer.enabled = true;

        SoundEffectPlayer.Instance.PlaySoundEffect(SoudEffect.BallSpawn);

        ballAnimation.SpawnBallAnimation();
    }

    public void ResetState()
    {
        ToggleBallOn();
    }
}

