using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColorManager : MonoBehaviour, IResetable
{
    [SerializeField] private BallColor currentBallColor;

    public BallColor CurrentBallColor => currentBallColor;

    [SerializeField] private BallSpawnManager ballDeath;

    [SerializeField] private BallAnimation ballAnimation;

    public Action ballCollisionOppositeColorEvent; 

    private void Awake()
    {
        ResetState();
    }

    private void Start()
    {
        GameLoopManager.Instance.ResetGameEvent += ResetState;
        ResetState();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constants.Waypoint_Tag))
        {
            ColorBlock colorBlock = other.GetComponent<ColorBlock>();

            if (colorBlock == null)
                return;

            if (currentBallColor != colorBlock.BallColor)
            {
                ballDeath.UnSpawnBallModel();
                ballCollisionOppositeColorEvent?.Invoke();
            }
        }

        else if(other.CompareTag(Constants.ColorWall_Tag))
        {
            ColorWall colorWall = other.GetComponent<ColorWall>();

            if (colorWall == null)
                return;

            SetBallColor(ballColor: colorWall.BallColor);
        }
    }

    private void SetBallColor(BallColor ballColor)
    {
        currentBallColor = ballColor;

        if (currentBallColor == BallColor.Blue)
        {
            ballAnimation.ChangeColor(startBallColor: BallColor.Red, targetBallColor: BallColor.Blue);
        }

        else
        {
            ballAnimation.ChangeColor(startBallColor: BallColor.Blue, targetBallColor: BallColor.Red);
        }
    }

    public void ResetState()
    {
        currentBallColor = BallColor.Red;
    }
}


public enum BallColor
{
    Red,
    Blue
}
