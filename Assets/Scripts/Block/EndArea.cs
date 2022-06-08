using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndArea : MonoBehaviour
{
    public Action ReachLevelEndEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PlayerBall_Tag))
        {
            ReachLevelEndEvent?.Invoke();
        }
    }
}
