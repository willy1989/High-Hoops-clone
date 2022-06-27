using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public BlockPositionSetterGroup[] BlockPositionSetterGroups { get; private set; }

    private void Awake()
    {
        BlockPositionSetterGroups = GetComponentsInChildren<BlockPositionSetterGroup>();
    }
}
