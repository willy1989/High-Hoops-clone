using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallYMovement : MonoBehaviour, IResetable
{
    [SerializeField] private BallNavigationWaypointManager ballNavigationWaypointManager;

    [SerializeField] private AnimationCurve yMovementFunction;

    [Range(0.5f, 5f)]
    [SerializeField] private float jumpInPlaceHeight;

    [Range(0.5f, 5f)]
    [SerializeField] private float jumpInPlaceDuration;

    private bool canMove;

    private bool jumpInPlace;


    private float startYPosition;

    private void Awake()
    {
        startYPosition = transform.position.y;
        ResetState();
    }

    private void Start()
    {
        GameLoopManager.Instance.ResetGameEvent += ResetState;
    }

    private void Update()
    {
        MoveAlongJumpFunction();
    }

    private void MoveAlongJumpFunction()
    {
        if (canMove == false)
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
            t = elapsedTime % jumpInPlaceDuration;

            float newYPosition = yMovementFunction.Evaluate(t) * jumpInPlaceHeight;

            transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        } 
    }

    public void StartJumpInPlace()
    {
        StartCoroutine(JumpInPlace());
    }

    public void ToggleMovement(bool onOff)
    {
        canMove = onOff;
    }

    public void ResetState()
    {
        jumpInPlace = false;
        canMove = false;
        transform.position = new Vector3(transform.position.x, startYPosition, transform.position.z);
    }
}
