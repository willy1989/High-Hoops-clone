using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalCameraRig : MonoBehaviour, IResetable
{
    [SerializeField] private float rotationSpeed;

    private bool canRotate = false;


    private void Awake()
    {
        ResetState();
    }

    private void Start()
    {
        GameLoopManager.Instance.ResetGameEvent += ResetState;
    }

    private void Update()
    {
        RotateRig();
    }

    private void RotateRig()
    {
        if (canRotate == false)
            return;

        float Yrotation = transform.rotation.eulerAngles.y;

        Yrotation += rotationSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(new Vector3(0f, Yrotation, 0f));
    }

    public void ToggleRotation()
    {
        canRotate = !canRotate;
    }

    public void ResetState()
    {
        canRotate = false;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
