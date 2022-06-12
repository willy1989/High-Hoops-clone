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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.LevelEnd_Tag))
        {
            ballAnimation.BounceAnimation();

            if(touchedEndZoneOnce == false)
            {
                GameLoopManager.Instance.GameWinPhase();
                touchedEndZoneOnce = true;
            }
        }

        else if (other.CompareTag(Constants.BouceBlock_Tag))
        {
            BallNavigationWaypoint bounceBlock = other.GetComponent<BallNavigationWaypoint>();

            SetNextTarget(bounceBlock);

            bounceBlock.DisableBlockCollider();

            ballAnimation.BounceAnimation();

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
