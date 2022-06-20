using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNavigationWaypointManager : MonoBehaviour
{
    [SerializeField] private BallAnimation ballAnimation;

    [SerializeField] private BallVfx ballVfx;

    private BallNavigationWaypoint previousTarget;

    private BallNavigationWaypoint nextTarget;

    public BallNavigationWaypoint PreviousTarget => previousTarget;

    public BallNavigationWaypoint NextTarget => nextTarget;

    private bool touchedEndZoneOnce = false;

    public Action ballReachedEndEvent;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.LevelEnd_Tag))
        {
            ballAnimation.BounceAnimation();
            SoundEffectPlayer.Instance.PlaySoundEffect(SoudEffect.Jump);
            ballVfx.PlayDustPoof();

            if (touchedEndZoneOnce == false)
            {
                ballReachedEndEvent?.Invoke();
                SoundEffectPlayer.Instance.PlaySoundEffect(SoudEffect.Victory);
                touchedEndZoneOnce = true;
            }
        }

        else if (other.CompareTag(Constants.BouceBlock_Tag))
        {
            BallNavigationWaypoint waypoint = other.GetComponent<BallNavigationWaypoint>();

            if (waypoint == null)
                return;

            SetNextTarget(waypoint);

            waypoint.DisableBlockCollider();

            ballAnimation.BounceAnimation();

            SoundEffectPlayer.Instance.PlaySoundEffect(SoudEffect.Jump);

            ballVfx.PlayDustPoof();
        }

        else if (other.CompareTag(Constants.ColorWall_Tag))
        {
            ColorWall colorwall = other.GetComponent<ColorWall>();

            if (colorwall == null)
                return;

            nextTarget = colorwall.NextTarget;

            other.gameObject.SetActive(false);
        }
    }

    public void SetNextTarget(BallNavigationWaypoint bounceBlock)
    {
        previousTarget = nextTarget;
        nextTarget = bounceBlock.NextBounceBlock;
    }

    public void SetNextTarget(BallNavigationWaypoint previousWaypoint, BallNavigationWaypoint nextWaypoint)
    {
        previousTarget = previousWaypoint;
        nextTarget = nextWaypoint;
        touchedEndZoneOnce = false;
    }
}
