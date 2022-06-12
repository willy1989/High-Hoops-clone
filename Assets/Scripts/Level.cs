using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private BallNavigationWaypoint firstWayPoint;

    [SerializeField] private BallNavigationWaypoint secondWayPoint;

    [SerializeField] private BlockPositionSetterGroup[] blockPositionSetterGroups;

    public BallNavigationWaypoint FirstWayPoint => firstWayPoint;
    public BallNavigationWaypoint SecondWayPoint => secondWayPoint;

    public BlockPositionSetterGroup[] BlockPositionSetterGroups => blockPositionSetterGroups;
}
