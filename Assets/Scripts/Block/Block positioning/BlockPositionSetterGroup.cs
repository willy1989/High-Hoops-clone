using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPositionSetterGroup : MonoBehaviour
{
    private BlockPositionSetter[] blockPositionSetters;

    private void Awake()
    {
        blockPositionSetters = GetComponentsInChildren<BlockPositionSetter>();
    }

    public void SetBlocksIntoPosition()
    {
        foreach(BlockPositionSetter item in blockPositionSetters)
        {
            item.MoveIntoPosition();
        }
    }
}
