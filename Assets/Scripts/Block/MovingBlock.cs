using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    [SerializeField] private Transform block;

    private float elapsedTime;

    [SerializeField] private float moveDuration;

    [SerializeField] AnimationCurve movementCurve;

    private bool canMove = true;

    private void Awake()
    {
        movementCurve.MoveKey(0, new Keyframe(0, startPosition.localPosition.x));
        movementCurve.MoveKey(1, new Keyframe(0.5f, endPosition.localPosition.x));
        movementCurve.MoveKey(2, new Keyframe(1, startPosition.localPosition.x));
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (canMove == false)
            return;

        elapsedTime += Time.deltaTime;

        elapsedTime %= moveDuration;

        float newXposition = movementCurve.Evaluate(elapsedTime / moveDuration);

        block.localPosition = new Vector3(newXposition, block.localPosition.y, block.localPosition.z);
    }
}
