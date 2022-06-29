using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] private Animator autoLettersAnimator;

    private void Start()
    {
        AutoPilotManager.Instance.AutoPilotStartEvent += StartAutoLettersAnimation;
        AutoPilotManager.Instance.AutoPilotEndEvent += StopAutoLettersAnimation;
    }

    private void StartAutoLettersAnimation()
    {
        autoLettersAnimator.SetTrigger(Constants.AutoOn_AnimationTrigger);
    }

    private void StopAutoLettersAnimation()
    {
        autoLettersAnimator.SetTrigger(Constants.AutoOff_AnimationTrigger);
    }
}
