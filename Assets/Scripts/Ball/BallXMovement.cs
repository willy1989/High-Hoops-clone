using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallXMovement : MonoBehaviour
{
    [SerializeField] private Transform ballSpawnPosition;

    [SerializeField] private BallNavigationWaypointManager ballNavigationWaypointManager;

    [SerializeField] private Transform xAxisTarget;

    [SerializeField] private DragInput dragInput;

    [SerializeField] private float targetMovementSpeed;

    [Range(0.1f, 1.0f)]
    [SerializeField] private float ballToTargetLerpSpeed;

    [SerializeField] float leftBoundary;

    [SerializeField] float rightBoundary;

    [HideInInspector] public bool CanMove;

    [SerializeField] private bool autoPilotOnOff;

    [SerializeField] private float autoPilotDuration;

    private void Awake()
    {
        CanMove = true;
    }

    private void Update()
    {
        if (autoPilotOnOff == true)
            AutoPilotXAxisMovement();

        else if(CanMove == true && dragInput.DragInputVector != Vector2.zero)
            MoveTarget();

        MoveBallTowardsTarget();
    }

    public void StartAutoPilot()
    {
        if (autoPilotOnOff)
            return;

        StartCoroutine(AutoPilot());
    }

    private IEnumerator AutoPilot()
    {
        autoPilotOnOff = true;

        yield return new WaitForSeconds(autoPilotDuration);

        autoPilotOnOff = false;
    }

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
        BallNavigationWaypoint previousWaypoint = ballNavigationWaypointManager.PreviousTarget;

        BallNavigationWaypoint nextWaypoint = ballNavigationWaypointManager.NextTarget;

        float t = (Mathf.Abs(previousWaypoint.transform.position.z - transform.position.z)) 
                   / Mathf.Abs(nextWaypoint.transform.position.z - previousWaypoint.transform.position.z);

        float newXposition = Mathf.Lerp(previousWaypoint.transform.position.x, nextWaypoint.transform.position.x, t);

        xAxisTarget.position = new Vector3(newXposition, transform.position.y, transform.position.z);
    }

    public void Reset()
    {
        transform.position = ballSpawnPosition.position;
        xAxisTarget.position = ballSpawnPosition.position;
        autoPilotOnOff = false;
    }
}
