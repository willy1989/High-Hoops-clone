using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BallVfx : MonoBehaviour
{
    [SerializeField] private VisualEffect whiteExplosion;
    [SerializeField] private VisualEffect blackExplosion;
    [SerializeField] private VisualEffect whiteImplosion;
    [SerializeField] private VisualEffect blackImplosion;
    [SerializeField] private VisualEffect dustPoof;

    [SerializeField] private TrailRenderer trailRenderer;

    private void Start()
    {
        AutoPilotManager.Instance.AutoPilotStartEvent += ToggleTrailRenderer;
        AutoPilotManager.Instance.AutoPilotEndEvent += ToggleTrailRenderer;
    }

    public void PlayWhiteExplosion()
    {
        whiteExplosion.Play();
    }

    public void PlayBlackExplosion()
    {
        blackExplosion.Play();
    }

    public IEnumerator PlayBlackImplosion(float duration)
    {
        yield return StartCoroutine(PlayVfx(blackImplosion, duration));
    }

    public IEnumerator PlayWhiteImplosion(float duration)
    {
        yield return StartCoroutine(PlayVfx(whiteImplosion, duration));
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

    public void Reset()
    {
        trailRenderer.enabled = false;
    }
}
