using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragInput : MonoBehaviour
{
    public Vector2 DragInputVector { get; private set; }

    public Action FirstInputRegisteredEvent;

    [Range(0f,5f)]
    [SerializeField] private float firstInputDelay;

    private void Update()
    {
        UpdateDragInput();
    }

    private void UpdateDragInput()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 temp = touch.deltaPosition;

            DragInputVector = new Vector2(temp.x / Screen.width, temp.y / Screen.height);
        }

        else
        {
            DragInputVector = Vector2.zero;
        }
    }


    public void StartListenToFirstInput()
    {
        StartCoroutine(ListenToFirstInputCoroutine());
    }

    private IEnumerator ListenToFirstInputCoroutine()
    {
        while(Input.touchCount == 0)
        {
            yield return null;
        }

        FirstInputRegisteredEvent?.Invoke();
    }
}
