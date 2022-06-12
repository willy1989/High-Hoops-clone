using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColorManager : MonoBehaviour
{
    [SerializeField] private BallColor currentBallColor;

    public BallColor CurrentBallColor => currentBallColor;

    [SerializeField] private BallDeath ballDeath;

    [SerializeField] private BallAnimation ballAnimation; 

    public Action LoseEvent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constants.BouceBlock_Tag))
        {
            BlackWhiteBlock blackWhiteBlock = other.GetComponent<BlackWhiteBlock>();

            if (blackWhiteBlock == null)
                return;

            if (currentBallColor != blackWhiteBlock.BallColor)
            {
                ballDeath.ToggleBallOff();
                LoseEvent?.Invoke();
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

        if (currentBallColor == BallColor.White)
        {
            ballAnimation.ChangeColor(startBallColor: BallColor.Black, targetBallColor: BallColor.White);
        }

        else
        {
            ballAnimation.ChangeColor(startBallColor: BallColor.White, targetBallColor: BallColor.Black);
        }
    }

    public void Reset()
    {
        currentBallColor = BallColor.Black;
    }
}


public enum BallColor
{
    Black,
    White
}
