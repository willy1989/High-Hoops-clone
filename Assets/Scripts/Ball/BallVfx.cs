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

    private Dictionary<BallVfxType, VisualEffect> ballVfxDictionary = new Dictionary<BallVfxType, VisualEffect>();

    private void Awake()
    {
        ResetState();

        ballVfxDictionary.Add(BallVfxType.BlueExplosion, blueExplosion);
        ballVfxDictionary.Add(BallVfxType.RedExplosion, redExplosion);
        ballVfxDictionary.Add(BallVfxType.BlueImplosion, blueImplosion);
        ballVfxDictionary.Add(BallVfxType.RedImplosion, redImplosion);
        ballVfxDictionary.Add(BallVfxType.DustPoof, dustPoof);
    }

    private void Start()
    {
        AutoPilotManager.Instance.AutoPilotStartEvent += ToggleTrailRenderer;
        AutoPilotManager.Instance.AutoPilotEndEvent += ToggleTrailRenderer;
        GameLoopManager.Instance.ResetGameEvent += ResetState;
    }

    public void PlayVfx(BallVfxType ballVfxType)
    {
        if(ballVfxDictionary.ContainsKey(ballVfxType))
            ballVfxDictionary[ballVfxType].Play();
    }

    public IEnumerator PlayVfx(BallVfxType ballVfxType, float effecDuration, float endDelay)
    {
        ballVfxDictionary[ballVfxType].Play();
        yield return new WaitForSeconds(effecDuration);
        ballVfxDictionary[ballVfxType].Stop();
        yield return new WaitForSeconds(endDelay);
    }

    private void ToggleTrailRenderer()
    {
        trailRenderer.enabled = !trailRenderer.enabled;
    }

    public void ResetState()
    {
        trailRenderer.enabled = false;
    }
}

public enum BallVfxType
{
    BlueExplosion,
    RedExplosion,
    BlueImplosion,
    RedImplosion,
    DustPoof
}
