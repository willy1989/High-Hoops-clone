using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constants.BouceBlock_Tag))
        {
            animator.SetTrigger(Constants.BlockCollision_AnimationTrigger);
        }
    }
}
