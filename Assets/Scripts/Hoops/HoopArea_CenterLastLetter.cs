using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopArea_CenterLastLetter : HoopArea_Center
{
    [Range(1f, 10f)]
    [SerializeField] private float autoPilotDuration;

    new private void Awake()
    {
        base.Awake();
    }

    protected override void DoCollisionAction()
    {
        base.DoCollisionAction();

        AutoPilotManager.Instance.StartAutoPilot(autoPilotDuration);
    }
}
