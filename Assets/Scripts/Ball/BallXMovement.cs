using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallXMovement : MonoBehaviour, IResetable
{
    [SerializeField] private BallNavigationWaypointManager ballNavigationWaypointManager;

    [SerializeField] private Transform xAxisTarget;

    [SerializeField] private DragInput dragInput;

    [SerializeField] private float targetMovementSpeed;

    [Range(0.1f, 1.0f)]
    [SerializeField] private float ballToTargetLerpSpeed;

    [SerializeField] float leftBoundary;

    [SerializeField] float rightBoundary;

    private bool canMove;

    [SerializeField] public bool AutoPilotOnOff;

    private void Start()
    {
        GameLoopManager.Instance.ResetGameEvent += ResetState;
    }

    private void Update()
    {
        if (AutoPilotOnOff == true)
            AutoPilotXAxisMovement();

        else if(canMove == true && dragInput.DragInputVector != Vector2.zero)
            MoveTarget();

        MoveBallTowardsTarget();
    }

    // To make lateral movement smooth, we don't move the ball directly on the left axis.
    // Instead we move a target, and then interpolate the ball's position to that of the target.
    private void MoveTarget()
    {
        Vector3 newPosition = xAxisTarget.position + Vector3.right * dragInput.DragInputVector.x * targetMovementSpeed * Time.deltaTime;

        if (newPosition.x >= leftBoundary && newPosition.x <= rightBoundary)
            xAxisTarget.position = newPosition;
    }

    // Careful. Not framerate independent
    private void MoveBallTowardsTarget()
    {
        transform.position = Vector3.Lerp(transform.position, xAxisTarget.transform.position, ballToTargetLerpSpeed);
    }

    private void AutoPilotXAxisMovement()
    {
        Waypoint previousWaypoint = ballNavigationWaypointManager.PreviousWaypoint;

        Waypoint nextWaypoint = ballNavigationWaypointManager.NextWaypoint;

        float t = (Mathf.Abs(previousWaypoint.transform.position.z - transform.position.z)) 
                   / Mathf.Abs(nextWaypoint.transform.position.z - previousWaypoint.transform.position.z);

        float newXposition = Mathf.Lerp(previousWaypoint.transform.position.x, nextWaypoint.transform.position.x, t);

        xAxisTarget.position = new Vector3(newXposition, transform.position.y, transform.position.z);
    }

    public void ToggleMovement(bool onOff)
    {
        canMove = onOff;
    }

    public void ResetState()
    {
        xAxisTarget.localPosition = Vector3.zero;
        AutoPilotOnOff = false;
        canMove = false;
    }
}
