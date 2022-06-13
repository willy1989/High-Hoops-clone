using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksPositionManager : MonoBehaviour
{
    [SerializeField] private BlockPositionSetterGroup[] blockPositionSetterGroups;

    [SerializeField] private BallZMovement ballZMovement;

    private int firstBlocksThreshold = 6;

    private IEnumerator setBlockCoroutine;

    private void SetBlocksIntoPosition()
    {
        StartCoroutine(SetFirstBlocksIntoPosition());

        setBlockCoroutine = SetRestBlocksIntoPosition();

        StartCoroutine(setBlockCoroutine);
    }

    private IEnumerator SetFirstBlocksIntoPosition()
    {
        int blockIndex = 0;

        while (blockIndex < firstBlocksThreshold)
        {
            blockPositionSetterGroups[blockIndex].SetBlocksIntoPosition();

            blockIndex++;

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator SetRestBlocksIntoPosition()
    {
        float distanceBetweenBlocks = 6;

        int blockIndex = firstBlocksThreshold;

        int maxIndex = blockPositionSetterGroups.Length - 1;

        while(blockIndex <= maxIndex)
        {
            if(ballZMovement.transform.position.z > distanceBetweenBlocks)
            {
                blockPositionSetterGroups[blockIndex].SetBlocksIntoPosition();

                blockIndex++;
                distanceBetweenBlocks += 6;
            }

            yield return null;
        }
    }

    public void SetNextBlockPositionSetterGroups(BlockPositionSetterGroup[] _blockPositionSetterGroups)
    {
        blockPositionSetterGroups = _blockPositionSetterGroups;
    }

    public void Reset()
    {
        if (setBlockCoroutine != null)
        {
            StopCoroutine(setBlockCoroutine);
        }

        SetBlocksIntoPosition();
    }
}
