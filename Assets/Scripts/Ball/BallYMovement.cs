using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallYMovement : MonoBehaviour
{
    [SerializeField] private BallNavigationWaypointManager ballNavigationWaypointManager;

    [SerializeField] private AnimationCurve yMovementFunction;

    [HideInInspector] public bool CanMove;

    private bool jumpInPlace;


    private float startYPosition;

    private void Awake()
    {
        startYPosition = transform.position.y;
    }

    private void Update()
    {
        MoveAlongFunction();
        JumpInPlace();
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

    public IEnumerator JumpInPlace()
    {
        jumpInPlace = true;

        float elapsedTime = 0f;

        float t;

        while(jumpInPlace == true)
        {
            t = elapsedTime % 1f;

            float newYPosition = yMovementFunction.Evaluate(t) * 5f;

            transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        } 
    }


    public void StartJumpInPlace()
    {
        StartCoroutine(JumpInPlace());
    }


    public void Reset()
    {
        jumpInPlace = false;
        transform.position = new Vector3(transform.position.x, startYPosition, transform.position.z);
    }
}
