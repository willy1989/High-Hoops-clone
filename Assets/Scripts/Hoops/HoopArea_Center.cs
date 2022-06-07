using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopArea_Center : HoopArea
{
    [SerializeField] private HoopArea_Side hoopArea_Side;

    new private void Awake()
    {
        base.Awake();
    }

    protected override void DoCollisionAction()
    {
        hoopArea_Side.DisableCollider();
        Debug.Log("Player ball hit CENTER of hoop");
        AutoPilotManager.Instance.RegisterHoop();
    }
}
