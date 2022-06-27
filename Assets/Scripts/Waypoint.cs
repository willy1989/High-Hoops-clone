using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public WaypointType WaypointType { get; private set; }

    [SerializeField] private float bounceApex;

    private ColorBlock colorBlock;

    private float defaultBounceApex = 3;

    public float BounceApex => bounceApex;

    private void Awake()
    {
        if (bounceApex == 0)
            bounceApex = defaultBounceApex;

        colorBlock = GetComponent<ColorBlock>();

        SetWaypointTypeColor();
    }

    private void SetWaypointTypeColor()
    {
        if (colorBlock == null)
            return;

        if(colorBlock.BallColor == BallColor.Red)
            WaypointType = WaypointType.Red;

        else
            WaypointType = WaypointType.Blue;
    }
}

public enum WaypointType
{
    Blue,
    Red,
}
