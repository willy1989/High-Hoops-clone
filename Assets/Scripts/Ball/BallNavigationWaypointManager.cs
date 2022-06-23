using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNavigationWaypointManager : MonoBehaviour
{
    [SerializeField] private BallAnimation ballAnimation;

    [SerializeField] private BallVfx ballVfx;

    [SerializeField] private ColorBlock previousTarget;

    [SerializeField] private ColorBlock nextTarget;

    public ColorBlock PreviousTarget => previousTarget;

    public ColorBlock NextTarget => nextTarget;

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

        else if (other.CompareTag(Constants.ColorBlock_Tag))
        {
            ColorBlock waypoint = other.GetComponent<ColorBlock>();

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

    public void SetNextTarget(ColorBlock colorBlock)
    {
        previousTarget = nextTarget;
        nextTarget = colorBlock.NextBlock;
    }

    public void SetNextTarget(ColorBlock previousWaypoint, ColorBlock nextWaypoint)
    {
        previousTarget = previousWaypoint;
        nextTarget = nextWaypoint;
        touchedEndZoneOnce = false;
    }
}
