using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopArea_Side : HoopArea
{
    [SerializeField] private HoopAnimation hoopAnimation;

    new private void Awake()
    {
        base.Awake();
    }

    protected override void DoCollisionAction()
    {
        hoopAnimation.SpinHoopAnimation();
        SoundEffectManager.Instance.PlaySoundEffect("Hit hoop side");
    }
}
