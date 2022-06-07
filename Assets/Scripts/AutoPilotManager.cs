using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPilotManager : Singleton<AutoPilotManager>
{
    [SerializeField] private int hoopsThreshold;

    [SerializeField] private BallXMovement ballXMovement;

    private int hoopsCount = 0;

    protected override void Awake()
    {
        base.Awake();
    }

    public void RegisterHoop()
    {
        hoopsCount++;

        if (hoopsCount >= hoopsThreshold)
        {
            ballXMovement.StartAutoPilot();
            hoopsCount = 0;
        }
    }
}
