using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragInput : MonoBehaviour
{
    public Vector2 DragInputVector { get; private set; }

    public Action FirstInputRegisteredEvent;

    [Range(0f,2f)]
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
            Debug.Log("DragInputVector.magnitude = " + DragInputVector.magnitude);
        }

        else
        {
            DragInputVector = Vector2.zero;
        }
    }

    public void ListenToFirstInput()
    {
        StartCoroutine(ListenToFirstInputCoroutine());
    }

    private IEnumerator ListenToFirstInputCoroutine()
    {
        // We add a short delay as the game is reset.
        // We do this to prevent the player from triggering the FirstInputRegisteredEvent
        // as he or she presses the restart button.
        yield return new WaitForSeconds(firstInputDelay);

        while(Input.touchCount == 0)
        {
            yield return null;
        }

        FirstInputRegisteredEvent?.Invoke();
    }
}
