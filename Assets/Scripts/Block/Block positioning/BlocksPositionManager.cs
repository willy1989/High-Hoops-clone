using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksPositionManager : MonoBehaviour, IResetable
{
    [SerializeField] private BlockPositionSetterGroup[] blockPositionSetterGroups;

    private BlockPositionSetterGroup previousBlockPositionSetterGroup;

    private BlockPositionSetterGroup nextBlockPositionSetterGroup;

    [SerializeField] private BallZMovement ballZMovement;

    private int firstBlocksThreshold = 6;

    private IEnumerator setBlockCoroutine;

    private void Awake()
    {
        ResetState();
    }

    private void Start()
    {
        GameLoopManager.Instance.ResetGameEvent += ResetState;
    }

    private void SetBlocksIntoPosition()
    {
        if (blockPositionSetterGroups.Length == 0)
            return;

        StartCoroutine(SetFirstBlocksIntoPosition());

        setBlockCoroutine = SetRestBlocksIntoPosition();

        StartCoroutine(setBlockCoroutine);
    }

    // The first few blocks are laid down before the player starts moving
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

    // The blocks are gradually laid in front of the player as he or she moves across the level.
    private IEnumerator SetRestBlocksIntoPosition()
    {
        float distanceBetweenBlocks = (previousBlockPositionSetterGroup.transform.position -
                                      nextBlockPositionSetterGroup.transform.position).magnitude;

        int blockIndex = firstBlocksThreshold;

        int maxIndex = blockPositionSetterGroups.Length - 1;

        while(blockIndex < maxIndex)
        {
            if (ballZMovement.transform.position.z > distanceBetweenBlocks)
            {
                blockPositionSetterGroups[blockIndex].SetBlocksIntoPosition();

                blockIndex++;

                previousBlockPositionSetterGroup = nextBlockPositionSetterGroup;

                nextBlockPositionSetterGroup = blockPositionSetterGroups[blockIndex];

                distanceBetweenBlocks += (previousBlockPositionSetterGroup.transform.position -
                                      nextBlockPositionSetterGroup.transform.position).magnitude;
            }

            yield return null;
        }
    }

    public void SetBlockPositionSetterGroups(BlockPositionSetterGroup[] _blockPositionSetterGroups)
    {
        blockPositionSetterGroups = _blockPositionSetterGroups;

        previousBlockPositionSetterGroup = blockPositionSetterGroups[firstBlocksThreshold];
        nextBlockPositionSetterGroup = blockPositionSetterGroups[firstBlocksThreshold+1];
    }

    public void ResetState()
    {
        if (setBlockCoroutine != null)
        {
            StopCoroutine(setBlockCoroutine);
        }

        SetBlocksIntoPosition();
    }
}
