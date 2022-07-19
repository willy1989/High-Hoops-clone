using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragInput : MonoBehaviour
{
    public Vector2 DragInputVector { get; private set; }

    public Action FirstInputRegisteredEvent;

    private void Update()
    {
        UpdateDragInput();
    }
    private void UpdateDragInput()
    {
        if(Input.touchCount > 0)
        {
            Vector2 touchDelta = Input.GetTouch(0).deltaPosition;

            DragInputVector = new Vector2(touchDelta.x / Screen.width, touchDelta.y / Screen.height);
        }

        else
            DragInputVector = Vector2.zero;
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
