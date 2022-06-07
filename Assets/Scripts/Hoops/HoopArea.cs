using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HoopArea : MonoBehaviour
{
    private Collider hoopCollider;

    protected void Awake()
    {
        hoopCollider = GetComponent<Collider>();
    }

    public void DisableCollider()
    {
        hoopCollider.enabled = false;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PlayerBall_Tag))
        {
            DoCollisionAction();
        }
    }

    protected abstract void DoCollisionAction();
}
