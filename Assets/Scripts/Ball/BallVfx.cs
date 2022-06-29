using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BallVfx : MonoBehaviour, IResetable
{
    [SerializeField] private VisualEffect blueExplosion;
    [SerializeField] private VisualEffect redExplosion;
    [SerializeField] private VisualEffect blueImplosion;
    [SerializeField] private VisualEffect redImplosion;
    [SerializeField] private VisualEffect dustPoof;

    [SerializeField] private TrailRenderer trailRenderer;

    private void Awake()
    {
        ResetState();
    }

    private void Start()
    {
        AutoPilotManager.Instance.AutoPilotStartEvent += ToggleTrailRenderer;
        AutoPilotManager.Instance.AutoPilotEndEvent += ToggleTrailRenderer;
        GameLoopManager.Instance.ResetGameEvent += ResetState;
    }

    public void PlayBlueExplosion()
    {
        blueExplosion.Play();
    }

    public void PlayRedExplosion()
    {
        redExplosion.Play();
    }

    public IEnumerator PlayRedImplosion(float duration, float endPadding)
    {
        yield return StartCoroutine(PlayVfx(redImplosion, duration, endPadding));
    }

    public IEnumerator PlayBlueImplosion(float duration, float endPadding)
    {
        yield return StartCoroutine(PlayVfx(blueImplosion, duration, endPadding));
    }

    private IEnumerator PlayVfx(VisualEffect visualEffect, float duration, float endPadding)
    {
        visualEffect.Play();
        yield return new WaitForSeconds(duration);
        visualEffect.Stop();
        yield return new WaitForSeconds(endPadding);
    }

    public void PlayDustPoof()
    {
        dustPoof.Play();
    }

    public void ToggleTrailRenderer()
    {
        trailRenderer.enabled = !trailRenderer.enabled;
    }

    public void ResetState()
    {
        trailRenderer.enabled = false;
    }
}
