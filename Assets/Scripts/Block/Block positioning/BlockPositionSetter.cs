using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPositionSetter : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Range(0.1f, 10f)]
    [SerializeField] private float moveDuration;

    [SerializeField] private AnimationCurve movementCurve;

    [SerializeField] private Transform block;
    [SerializeField] private Renderer blockRenderer;
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;

    

    private void Awake()
    {
        if(animator != null)
            animator.enabled = false;

        block.transform.position = startPosition.position;
        blockRenderer.enabled = false;
    }

    public void MoveIntoPosition()
    {
        StartCoroutine(MoveIntoEndPositionCoroutine());
    }

    private IEnumerator MoveIntoEndPositionCoroutine()
    {
        blockRenderer.enabled = true;

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            float t = movementCurve.Evaluate(elapsedTime / moveDuration);

            Vector3 newPosition = startPosition.position + (endPosition.position - startPosition.position) * t;

            //Vector3 newPosition = Vector3.Lerp(startPosition.position, endPosition.position, t);
            //Quaternion newRotation = Quaternion.Lerp(startPosition.rotation, endPosition.rotation, t);

            block.transform.position = newPosition;
            //block.transform.rotation = newRotation;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        block.transform.position = endPosition.position;

        if (animator != null)
            animator.enabled = true;

        //block.transform.rotation = endPosition.rotation;
    }
}
