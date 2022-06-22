using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorWall : MonoBehaviour
{
    [SerializeField] private BallColor ballColor;

    public BallColor BallColor => ballColor;


    // When the ball hits the wall, we need to update the next target block.
    // We do this for the auto pilot, because if we don't, since we just change the ball's color,
    // the auto pilot may lead the ball to a block that was of its previous color.
    [SerializeField] private ColorBlock nextTarget;

    public ColorBlock NextTarget => nextTarget;
}
