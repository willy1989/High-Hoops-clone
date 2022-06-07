using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopArea_Side : HoopArea
{
    new private void Awake()
    {
        base.Awake();
    }

    protected override void DoCollisionAction()
    {
        Debug.Log("Player ball hit SIDE of hoop");
    }
}
