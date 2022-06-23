using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlock : MonoBehaviour
{
    [SerializeField] private BallColor ballColor;

    public BallColor BallColor => ballColor;

    [SerializeField] private float bounceApex;

    public float BounceApex => bounceApex;

    public ColorBlock NextBlock;

    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();

        if (bounceApex == 0)
            bounceApex = 3;

    }

    public void DisableBlockCollider()
    {
        boxCollider.enabled = false;
    }

}
