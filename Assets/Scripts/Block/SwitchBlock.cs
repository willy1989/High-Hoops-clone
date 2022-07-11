using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBlock : MonoBehaviour
{
    [SerializeField] private AnimationCurve switchFunction;

    [SerializeField] private float switchDuration;

    private float targetYRotation = 0f;

    private void OnEnable()
    {
        BallNavigationWaypointManager.Instance.BallCollidedWithWayPointEvent += SwitchBlocks;
    }

    private void OnDisable()
    {
        BallNavigationWaypointManager.Instance.BallCollidedWithWayPointEvent -= SwitchBlocks;
    }

    private void SwitchBlocks()
    {
        StartCoroutine(SwitchCoroutine());
    }

    private IEnumerator SwitchCoroutine()
    {
        float elapsedTime = 0f;

        float currentRotation = targetYRotation;

        targetYRotation += 180f;

        while(elapsedTime <= switchDuration)
        {
            float t = switchFunction.Evaluate(elapsedTime / switchDuration);

            float newYRotation = Mathf.Lerp(currentRotation, targetYRotation, t);

            transform.rotation = Quaternion.Euler(new Vector3(0f, newYRotation, 0f));

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.rotation = Quaternion.Euler(new Vector3(0f, targetYRotation, 0f));
    }
}
