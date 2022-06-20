using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNavigationWaypoint : MonoBehaviour
{
    [SerializeField] private float bounceApex;

    public float BounceApex => bounceApex;

    [SerializeField] public BallNavigationWaypoint NextBounceBlock;


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
