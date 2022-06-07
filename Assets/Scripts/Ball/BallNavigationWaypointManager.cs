using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNavigationWaypointManager : MonoBehaviour
{
    private BallNavigationWaypoint previousTarget;

    private BallNavigationWaypoint nextTarget;

    public BallNavigationWaypoint PreviousTarget => previousTarget;

    public BallNavigationWaypoint NextTarget => nextTarget;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.BouceBlock_Tag))
        {
            BallNavigationWaypoint bounceBlock = other.GetComponent<BallNavigationWaypoint>();

            SetNextTarget(bounceBlock);

            bounceBlock.DisableBlockCollider();
        }

        else if(other.CompareTag(Constants.ColorWall_Tag))
        {
            ColorWall colorwall = other.GetComponent<ColorWall>();

            if (colorwall == null)
                return;
            nextTarget = colorwall.NextTarget;
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
    }
}
