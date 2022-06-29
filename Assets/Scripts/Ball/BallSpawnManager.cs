using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BallSpawnManager : MonoBehaviour
{
    [SerializeField] private BallVfx ballVfx;
    [SerializeField] private BallColorManager ballColorManager;
    [SerializeField] private BallAnimation ballAnimation;

    [SerializeField] private Collider ballCollider;

    [SerializeField] private Renderer ballRenderer;

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

    public IEnumerator SpawnBallModel()
    {
        ballCollider.enabled = false;
        ballRenderer.enabled = false;

        if (ballColorManager.CurrentBallColor == BallColor.Blue)
            yield return ballVfx.PlayBlueImplosion(duration: 2.5f, endPadding: 0.5f);
        else
            yield return ballVfx.PlayRedImplosion(duration: 2.5f, endPadding: 0.5f);

        ballCollider.enabled = true;
        ballRenderer.enabled = true;

        SoundEffectPlayer.Instance.PlaySoundEffect(SoudEffect.BallSpawn);

        ballAnimation.SpawnBallAnimation();
    }
}

