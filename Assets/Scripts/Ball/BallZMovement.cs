using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallZMovement : MonoBehaviour
{
    [SerializeField] private float zMovementSpeed;

    [SerializeField] private Transform ballSpawnPosition;

    [HideInInspector] public bool CanMove;



    private void Awake()
    {
        CanMove = true;
    }

    private void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        if (CanMove == false)
            return;

        transform.position += Vector3.forward * zMovementSpeed * Time.deltaTime;
    }

    public void Reset()
    {
        transform.position = ballSpawnPosition.position;
    }
}
