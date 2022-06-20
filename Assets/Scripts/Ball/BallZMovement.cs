using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallZMovement : MonoBehaviour, IResetable
{
    [SerializeField] private float zMovementSpeed;

    [SerializeField] private Transform ballSpawnPosition;

    private bool canMove = false;

    private bool doubleSpeedOn = false;

    private void Awake()
    {
        canMove = true;
    }

    private void Start()
    {
        AutoPilotManager.Instance.AutoPilotStartEvent += ToggleDoubleSpeed;
        AutoPilotManager.Instance.AutoPilotEndEvent += ToggleDoubleSpeed;
        GameLoopManager.Instance.ResetGameEvent += ResetState;
    }

    private void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        if (canMove == false)
            return;

        if(doubleSpeedOn == false)
            transform.position += Vector3.forward * zMovementSpeed * Time.deltaTime;
        else
            transform.position += Vector3.forward * zMovementSpeed * 1.5f * Time.deltaTime;
    }

    private void ToggleDoubleSpeed()
    {
        doubleSpeedOn = !doubleSpeedOn;
    }

    public void ToggleMovement(bool onOff)
    {
        canMove = onOff;
    }

    public void ResetState()
    {
        canMove = false;
        transform.position = ballSpawnPosition.position;
        doubleSpeedOn = false;
    }
}
