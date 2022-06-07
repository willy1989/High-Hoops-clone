using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallYMovement : MonoBehaviour
{
    [SerializeField] private BallNavigationWaypointManager ballNavigationWaypointManager;

    [SerializeField] private AnimationCurve yMovementFunction;

    [HideInInspector] public bool CanMove;

    private void Awake()
    {
        CanMove = true;
    }

    private void Update()
    {
        MoveAlongFunction();
    }

    private void MoveAlongFunction()
    {
        if (CanMove == false)
            return;

        BallNavigationWaypoint previousWaypoint = ballNavigationWaypointManager.PreviousTarget;

        BallNavigationWaypoint nextWaypoint = ballNavigationWaypointManager.NextTarget;

        float t = (Mathf.Abs(previousWaypoint.transform.position.z - transform.position.z)) 
                   / Mathf.Abs(nextWaypoint.transform.position.z - previousWaypoint.transform.position.z);

        float newYPosition = nextWaypoint.BounceApex * yMovementFunction.Evaluate(t);

        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
    } 
}
