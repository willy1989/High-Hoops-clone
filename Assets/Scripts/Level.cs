using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private BallNavigationWaypoint firstWayPoint;

    [SerializeField] private BallNavigationWaypoint secondWayPoint;

    [SerializeField] private EndArea endArea;

    public BallNavigationWaypoint FirstWayPoint => firstWayPoint;
    public BallNavigationWaypoint SecondWayPoint => secondWayPoint;

    public EndArea EndArea => endArea;
}
