using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlockGroup : MonoBehaviour
{
    public ColorBlock DefaultRedBlock { get; private set; }
    public ColorBlock DefaultBlueBlock { get; private set; }

    public ColorBlock[] ColorBlocks { get; private set; }

    public void AssignColorBlocks()
    {
         ColorBlocks = GetComponentsInChildren<ColorBlock>();
    }

    public void AssignDefaultColorBlocks()
    {
        DefaultRedBlock = FindColorBlock(ColorBlocks, BallColor.Red);
        DefaultBlueBlock = FindColorBlock(ColorBlocks, BallColor.Blue);
    }

    private ColorBlock FindColorBlock(ColorBlock[] blocks, BallColor ballColor)
    {
        foreach (ColorBlock item in blocks)
        {
            if (item.BallColor == ballColor)
            {
                return item;
            }
        }

        return null;
    }
}
