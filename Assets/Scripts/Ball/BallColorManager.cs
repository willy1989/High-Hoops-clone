using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColorManager : MonoBehaviour
{
    [SerializeField] private BallColor currentBallColor;

    public BallColor CurrentBallColor => currentBallColor;

    [SerializeField] private BallDeath ballDeath;

    [SerializeField] private Renderer Ballrenderer;

    [SerializeField] private Material whiteMaterial;

    [SerializeField] private Material blackMaterial;

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
            Ballrenderer.material = whiteMaterial;
        }

        else
        {
            Ballrenderer.material = blackMaterial;
        }
    }

    public void Reset()
    {
        SetBallColor(ballColor: BallColor.Black);
    }
}


public enum BallColor
{
    Black,
    White
}
