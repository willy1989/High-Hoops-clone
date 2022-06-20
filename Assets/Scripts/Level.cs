using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private BallNavigationWaypoint firstWayPoint;

    [SerializeField] private BallNavigationWaypoint secondWayPoint;

    private BlockPositionSetterGroup[] blockPositionSetterGroups;

    [SerializeField] private BallNavigationWaypoint[][] ballNavigationWaypoints;

    public BallNavigationWaypoint FirstWayPoint => firstWayPoint;
    public BallNavigationWaypoint SecondWayPoint => secondWayPoint;

    public BlockPositionSetterGroup[] BlockPositionSetterGroups => blockPositionSetterGroups;

    private void Awake()
    {
        blockPositionSetterGroups = GetComponentsInChildren<BlockPositionSetterGroup>();

        ballNavigationWaypoints = new BallNavigationWaypoint[blockPositionSetterGroups.Length][];

        AssignNextWaypoints();
    }

    private void AssignNextWaypoints()
    {
        for(int i = 0; i < blockPositionSetterGroups.Length; i++)
        {
            ballNavigationWaypoints[i] = blockPositionSetterGroups[i].GetComponentsInChildren<BallNavigationWaypoint>();
        }


        for(int x = 0; x < ballNavigationWaypoints.Length-1; x++)
        {
            BallNavigationWaypoint[] currentGroup = ballNavigationWaypoints[x];
            BallNavigationWaypoint[] nextGroup = ballNavigationWaypoints[x+1];

            for (int y = 0; y < currentGroup.Length; y++)
            {
                BallNavigationWaypoint current = currentGroup[y];

                BallColor currentBallColor = current.gameObject.GetComponent<ColorBlock>().BallColor;

                for (int z = 0; z < nextGroup.Length; z++)
                {
                    BallNavigationWaypoint next = nextGroup[z];

                    BallColor nextBallColor = next.gameObject.GetComponent<ColorBlock>().BallColor;

                    if(nextBallColor == currentBallColor || next.CompareTag(Constants.LevelEnd_Tag) == true)
                    {
                        current.NextBounceBlock = next;
                    }
                }
            }
        }
    }
}
