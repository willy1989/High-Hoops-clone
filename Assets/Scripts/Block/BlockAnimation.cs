using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constants.PlayerBall_Tag))
        {
            animator.SetTrigger(Constants.BallCollision_AnimationTrigger);
        }
    }
}
