using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaypointGroup : MonoBehaviour
{
    public Waypoint BlueWaypoint { get; private set; }
    public Waypoint RedWaypoint { get; private set; }
    public Waypoint DefaultWaypoint { get; private set; }

    public void AssignWaypoints()
    {
        Waypoint[] waypoints = GetComponentsInChildren<Waypoint>();

        foreach (Waypoint item in waypoints)
        {
            if (item.WaypointType == WaypointType.Blue && BlueWaypoint == null)
            {
                BlueWaypoint = item;
            }

            else if (item.WaypointType == WaypointType.Red && RedWaypoint == null)
            {
                RedWaypoint = item;
            }
        }

        DefaultWaypoint = waypoints[0];
    }
}

