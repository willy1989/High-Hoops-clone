using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallZMovement : MonoBehaviour
{
    [SerializeField] private float zMovementSpeed;

    [SerializeField] private Transform ballSpawnPosition;

    [HideInInspector] public bool CanMove;

    private bool doubleSpeedOn = false;

    private void Awake()
    {
        CanMove = true;
    }

    private void Start()
    {
        AutoPilotManager.Instance.AutoPilotStartEvent += ToggleDoubleSpeed;
        AutoPilotManager.Instance.AutoPilotEndEvent += ToggleDoubleSpeed;
    }

    private void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        if (CanMove == false)
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

    public void Reset()
    {
        transform.position = ballSpawnPosition.position;
        doubleSpeedOn = false;
    }
}
