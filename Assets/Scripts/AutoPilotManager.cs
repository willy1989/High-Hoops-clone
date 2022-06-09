using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPilotManager : Singleton<AutoPilotManager>
{
    [SerializeField] private int grabbedLetterThreshold;

    [SerializeField] private BallXMovement ballXMovement;

    [SerializeField] private float autoPilotDuration;

    public float AutoPilotDuration => autoPilotDuration;

    public Action AutoPilotStartEvent;
    public Action AutoPilotEndEvent;

    private int hoopsCount = 0;

    protected override void Awake()
    {
        base.Awake();
    }

    public void RegisterHoop()
    {
        hoopsCount++;

        if (hoopsCount >= grabbedLetterThreshold)
        {
            
            StartAutoPilot();
            hoopsCount = 0;
        }
    }


    public void StartAutoPilot()
    {
        if (ballXMovement.AutoPilotOnOff == true)
            return;


        StartCoroutine(AutoPilot());
    }

    private IEnumerator AutoPilot()
    {
        AutoPilotStartEvent?.Invoke();
        ballXMovement.AutoPilotOnOff = true;

        yield return new WaitForSeconds(autoPilotDuration);

        ballXMovement.AutoPilotOnOff = false;
        AutoPilotEndEvent?.Invoke();
    }

    public void Reset()
    {
        hoopsCount = 0;
    }
}
