using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopArea_Center : HoopArea
{
    [SerializeField] private HoopArea_Side hoopArea_Side;

    [SerializeField] private HoopAnimation hoopAnimation;

    [SerializeField] private HoopVfx hoopVfx;

    [SerializeField] private AutoLetter autoLetter;

    new private void Awake()
    {
        base.Awake();
    }

    protected override void DoCollisionAction()
    {
        hoopArea_Side.DisableCollider();
        Debug.Log("Player ball hit CENTER of hoop");
        hoopAnimation.DisolveHoopModel();
        hoopAnimation.ShakeHoopAnimation();
        hoopAnimation.RaiseAutoLetter();
        hoopVfx.PlaySparksVFX();
        UIManager.Instance.ToggleAutoLetter(letter: autoLetter, onOff: true);
        AutoPilotManager.Instance.RegisterHoop();
    }
}


public enum AutoLetter
{
    A,
    U,
    T,
    O
}
