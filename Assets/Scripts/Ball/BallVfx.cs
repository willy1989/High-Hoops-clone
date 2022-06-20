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

    public IEnumerator PlayRedImplosion(float duration)
    {
        yield return StartCoroutine(PlayVfx(redImplosion, duration));
    }

    public IEnumerator PlayBlueImplosion(float duration)
    {
        yield return StartCoroutine(PlayVfx(blueImplosion, duration));
    }

    private IEnumerator PlayVfx(VisualEffect visualEffect, float duration)
    {
        visualEffect.Play();
        yield return new WaitForSeconds(duration);
        visualEffect.Stop();
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
