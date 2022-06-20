using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour, IResetable
{
    [SerializeField] private Animator cameraStateAnimator;

    private void Awake()
    {
        ResetState();
    }

    private void Start()
    {
        GameLoopManager.Instance.ResetGameEvent += ResetState;
    }

    private void SwitchToStartCamera()
    {
        cameraStateAnimator.Play(Constants.StartCamera_CameraState);
    }

    public void SwitchToFollowCamera()
    {
        cameraStateAnimator.Play(Constants.FollowCamera_CameraState);
    }

    public void SwitchToRotateAroundCamera()
    {
        cameraStateAnimator.Play(Constants.Rotate_CameraState);
    }

    public void ResetState()
    {
        SwitchToStartCamera();
    }

}
