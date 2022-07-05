using System;
using System.Collections;
using UnityEngine;

public class AutoPilotManager : Singleton<AutoPilotManager>, IResetable
{
    [SerializeField] private BallNavigationWaypointManager ballNavigationWaypointManager;

    [SerializeField] private BallXMovement ballXMovement;

    [SerializeField] private int grabbedLetterThreshold;

    public Action AutoPilotStartEvent;
    public Action AutoPilotEndEvent;

    private int hoopsCount = 0;
    public float AutoPilotDuration { get; private set; }

    private IEnumerator autoPilotCoroutine;

    protected override void Awake()
    {
        base.Awake();

        ballNavigationWaypointManager.ballReachedEndEvent += StopAutoPilot;

        ResetState();
    }

    private void Start()
    {
        GameLoopManager.Instance.ResetGameEvent += ResetState;
    }

    public void RegisterHoop()
    {
        hoopsCount++;

        SoundEffectPlayer.Instance.PlaySoundEffect(SoudEffect.HitHoopCenter);
    }

    public void StartAutoPilot(float autoPilotDuration)
    {
        if (hoopsCount >= grabbedLetterThreshold)
        {
            AutoPilotDuration = autoPilotDuration;
            autoPilotCoroutine = AutoPilot();
            StartCoroutine(autoPilotCoroutine);
            hoopsCount = 0;
        }
    }

    private void StopAutoPilot()
    {
        if(autoPilotCoroutine != null)
        {
            StopCoroutine(autoPilotCoroutine);
            AutoPilotEndEvent?.Invoke();
        }
            
        ballXMovement.AutoPilotOnOff = false;
    }

    private IEnumerator AutoPilot()
    {
        AutoPilotStartEvent?.Invoke();
        ballXMovement.AutoPilotOnOff = true;

        yield return new WaitForSeconds(AutoPilotDuration);

        ballXMovement.AutoPilotOnOff = false;
        AutoPilotEndEvent?.Invoke();
    }

    public void ResetState()
    {
        hoopsCount = 0;
        autoPilotCoroutine = null;
        ballXMovement.AutoPilotOnOff = false;
    }
}
