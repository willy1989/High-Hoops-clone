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

        if(blockRenderer != null)
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

            block.transform.position = newPosition;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        block.transform.position = endPosition.position;

        if (animator != null)
            animator.enabled = true;
    }
}
