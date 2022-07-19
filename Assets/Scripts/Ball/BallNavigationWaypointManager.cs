using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNavigationWaypointManager : Singleton<BallNavigationWaypointManager>
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

    public Action BallReachedEndEvent;

    public Action BallCollidedWithWayPointEvent;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.ColorWall_Tag))
            SetNextWaypoint(other.GetComponent<ColorWall>());

        else if (other.CompareTag(Constants.Waypoint_Tag))
        {
            ballAnimation.BounceAnimation();
            SoundEffectManager.Instance.PlaySoundEffect("Jump");
            ballVfx.PlayVfx(ballVfxType: BallVfxType.DustPoof);

            Waypoint waypoint = other.GetComponent<Waypoint>();

            BallCollidedWithWayPointEvent?.Invoke();

            if (waypointGroupIndex != waypointGroups.Length-1)
            {
                other.enabled = false;
                SetNextWaypoint();
            }

            else if (waypointGroupIndex == waypointGroups.Length-1 && touchedEndZoneOnce == false)
            {
                BallReachedEndEvent?.Invoke();
                SoundEffectManager.Instance.PlaySoundEffect("Game win");
                touchedEndZoneOnce = true;
            }
        }
    }

    /// <summary>
    /// Each level is made of WaypointGroups. Each Waypoint they contain share the same z-axis position.
    /// Waypoint components are placed on blocks and provide information about their position and their color.
    /// The BallNavigationManager uses information from the Waypoints, e.g. its color and its position, to create a
    /// path for the ball to follow. 
    /// </summary>
    public void PrepareWaypoints()
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

    /// <summary>
    /// When the ball collides with a Waypoint, we define what the next Waypoint will be based on the ball's color. If among the Waypoints 
    /// of the next WaypointGroup no color matches that of the ball, then we use a default Waypoint, defined by the group.
    /// This last optioni is typically used for the block at the end of the level and Waypoints with a ColorWall in between.
    /// Color wall change the color of the ball, so we need to update the next waypoint.
    /// </summary>
    private void SetNextWaypoint()
    {
        if (waypointGroupIndex >= waypointGroups.Length)
            return;

        waypointGroupIndex++;

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

    private void SetNextWaypoint(ColorWall colorWall)
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
}
