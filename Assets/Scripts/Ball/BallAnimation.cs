using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void SpawnBallAnimation()
    {
        animator.SetTrigger(Constants.BallSpawn_AnimationTrigger);
    }

    public void BounceBallAnimation()
    {
        animator.SetTrigger(Constants.BlockCollision_AnimationTrigger);
    }
}
