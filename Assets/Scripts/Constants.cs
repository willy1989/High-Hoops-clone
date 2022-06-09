using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    #region Tags
    public const string BouceBlock_Tag = "BounceBlock";
    public const string LevelEnd_Tag = "LevelEnd";
    public const string PlayerBall_Tag = "PlayerBall";
    public const string ColorWall_Tag = "ColorWall";
    #endregion

    #region Animation triggers
    public const string BallCollision_AnimationTrigger = "BallCollision";
    public const string BallSpawn_AnimationTrigger = "BallSpawn";
    public const string BlockCollision_AnimationTrigger = "BlockCollision";
    public const string StartTutorial_AnimationTrigger = "StartTutorial";
    public const string EndTutorial_AnimationTrigger = "EndTutorial";
    public const string AutoOn_AnimationTrigger = "AutoOn";
    public const string AutoOff_AnimationTrigger = "AutoOff";
    public const string HoopSideCollision_AnimationTrigger = "HoopSideCollision";
    public const string HoopCenterCollision_AnimationTrigger = "HoopCenterCollision";
    #endregion

    #region Cinemachine triggers
    public const string StartCamera_CameraState = "StartCamera";
    public const string FollowCamera_CameraState = "FollowCamera";
    #endregion
}
