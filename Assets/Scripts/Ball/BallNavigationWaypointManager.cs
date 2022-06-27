using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// How the navigation works.
/// Each level is made of WaypointGroups. Each Waypoint they contain share the same z-axis position.
/// Waypoint components are placed on blocks and provide information about their position and their color.
/// The BallNavigationManager uses information from the Waypoints, e.g. its color and its position, to create a
/// path for the ball to follow. 
/// When colliding with a Waypoint, when define what the next Waypoint will be, based on the ball's color. If among the Waypoints 
/// of the next WaypointGroup, no color matches that of the ball, then we use a default Waypoint, defined by the group.
/// This is typically used for the end of the level and Waypoints with a ColorWall in between. 
/// </summary>

public class BallNavigationWaypointManager : MonoBehaviour
{
    [SerializeField] private BallColorManager ballColorManager;

    [SerializeField] private BallAnimation ballAnimation;

    [SerializeField] private BallVfx ballVfx;

    [SerializeField] private LevelLoader levelLoader;

    [SerializeField] private WaypointGroup[] waypointGroups;

    public Waypoint PreviousWaypoint { get; private set; }

    public Waypoint NextWaypoint { get; private set; }

    private int waypointGroupIndex;

    private bool touchedEndZoneOnce = false;

    public Action ballReachedEndEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.ColorWall_Tag))
            WallCollision(other.GetComponent<ColorWall>());

        else if (other.CompareTag(Constants.Waypoint_Tag))
        {
            ballAnimation.BounceAnimation();
            SoundEffectPlayer.Instance.PlaySoundEffect(SoudEffect.Jump);
            ballVfx.PlayDustPoof();

            Waypoint waypoint = other.GetComponent<Waypoint>();

            if (waypointGroupIndex == waypointGroups.Length - 1 && touchedEndZoneOnce == false)
            {
                ballReachedEndEvent?.Invoke();
                SoundEffectPlayer.Instance.PlaySoundEffect(SoudEffect.Victory);
                touchedEndZoneOnce = true;
            }

            else
                SetNextWaypoint();
        }
    }

    private void WallCollision(ColorWall colorWall)
    {
        if (colorWall == null)
            return;

        WaypointGroup nextWayPointGroup = waypointGroups[waypointGroupIndex];

        if (colorWall.BallColor == BallColor.Red &&
            nextWayPointGroup.RedWaypoint != null)
            NextWaypoint = nextWayPointGroup.RedWaypoint;

        else if (colorWall.BallColor == BallColor.Blue &&
                 nextWayPointGroup.BlueWaypoint != null)
            NextWaypoint = nextWayPointGroup.BlueWaypoint;

        colorWall.gameObject.SetActive(false);
    }

    public void SetUpWaypoints()
    {
        waypointGroups = levelLoader.CurrentLevel.GetComponentsInChildren<WaypointGroup>();

        foreach(WaypointGroup item in waypointGroups)
        {
            item.AssignWaypoints();
        }

        SetFirstWaypoint();
    }

    private void SetFirstWaypoint()
    {
        PreviousWaypoint = waypointGroups[0].DefaultWaypoint;
        NextWaypoint = waypointGroups[1].DefaultWaypoint;
        touchedEndZoneOnce = false;

        waypointGroupIndex = 1;
    }


    private void SetNextWaypoint()
    {
        waypointGroupIndex++;

        if (waypointGroupIndex >= waypointGroups.Length)
            return;

        PreviousWaypoint = NextWaypoint;

        WaypointGroup nextWayPointGroup = waypointGroups[waypointGroupIndex];
        
        if (waypointGroupIndex == waypointGroups.Length-1)
            NextWaypoint = nextWayPointGroup.DefaultWaypoint;

        else if (ballColorManager.CurrentBallColor == BallColor.Red && 
                 nextWayPointGroup.RedWaypoint != null)
            NextWaypoint = nextWayPointGroup.RedWaypoint;

        else if (ballColorManager.CurrentBallColor == BallColor.Blue &&
                 nextWayPointGroup.BlueWaypoint != null)
            NextWaypoint = nextWayPointGroup.BlueWaypoint;

        else
        {
            NextWaypoint = nextWayPointGroup.DefaultWaypoint;
        }
    }

    
}
