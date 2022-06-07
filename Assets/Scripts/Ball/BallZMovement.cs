using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallZMovement : MonoBehaviour
{
    [SerializeField] private float zMovementSpeed;

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
}
