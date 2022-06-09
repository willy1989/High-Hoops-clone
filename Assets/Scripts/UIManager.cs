using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject winScreen;

    [SerializeField] private float showStartScreenDelay;
    
    protected override void Awake()
    {
        base.Awake();
    }

    public void ToggleStartScreen(bool onOff)
    {
        if (onOff == false)
            startScreen.SetActive(false);
        else
            StartCoroutine(ToggleStartScreenWithDelay(true));
    }

    private IEnumerator ToggleStartScreenWithDelay(bool onOff)
    {
        yield return new WaitForSeconds(showStartScreenDelay);
        startScreen.SetActive(onOff);
    }

    public void ToggleLoseScreen(bool onOff)
    {
        loseScreen.SetActive(onOff);
    }

    public void ToggleWinScreen(bool onOff)
    {
        winScreen.SetActive(onOff);
    }
}
