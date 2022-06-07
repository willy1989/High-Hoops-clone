using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNavigationWaypoint : MonoBehaviour
{
    [SerializeField] private float bounceApex;

    public float BounceApex => bounceApex;

    [SerializeField] private BallNavigationWaypoint nextBounceBlock;

    public BallNavigationWaypoint NextBounceBlock => nextBounceBlock;

    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void DisableBlockCollider()
    {
        boxCollider.enabled = false;
    }
}
