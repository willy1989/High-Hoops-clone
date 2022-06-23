using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public ColorBlock FirstWayPoint { get; private set; }

    public ColorBlock SecondWayPoint { get; private set; }

    [SerializeField] private ColorBlockGroup[] colorBlockGroups;

    public BlockPositionSetterGroup[] BlockPositionSetterGroups { get; private set; }


    private void Awake()
    {
        BlockPositionSetterGroups = GetComponentsInChildren<BlockPositionSetterGroup>();

        colorBlockGroups = GetComponentsInChildren<ColorBlockGroup>();

        foreach (ColorBlockGroup item in colorBlockGroups)
        {
            item.AssignColorBlocks();
            item.AssignDefaultColorBlocks();
        }

        FirstWayPoint = colorBlockGroups[0].ColorBlocks[0];
        SecondWayPoint = colorBlockGroups[1].ColorBlocks[0];

        AssignNextColorBlocks();
    }

    private void AssignNextColorBlocks()
    {
        for (int x = 0; x < colorBlockGroups.Length-1; x++)
        {
            ColorBlockGroup currentGroup = colorBlockGroups[x];
            ColorBlockGroup nextGroup = colorBlockGroups[x+1];

            if(currentGroup.DefaultBlueBlock != null &&
                nextGroup.DefaultBlueBlock != null)
            {
                currentGroup.DefaultBlueBlock.NextBlock = nextGroup.DefaultBlueBlock;
            }

            if (currentGroup.DefaultRedBlock != null &&
                nextGroup.DefaultRedBlock != null)
            {
                currentGroup.DefaultRedBlock.NextBlock = nextGroup.DefaultRedBlock;
            }
        }
    }
}
