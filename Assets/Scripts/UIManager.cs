using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject winScreen;
    
    protected override void Awake()
    {
        base.Awake();
    }

    public void ToggleStartScreen(bool onOff)
    {
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
