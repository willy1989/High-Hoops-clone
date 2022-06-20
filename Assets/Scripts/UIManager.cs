using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>, IResetable
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject winScreen;

    [SerializeField] private float showStartScreenDelay;

    [SerializeField] private Text Aletter;
    [SerializeField] private Text Uletter;
    [SerializeField] private Text Tletter;
    [SerializeField] private Text Oletter;

    [SerializeField] private RawImage autoPilotDurationBar;
    [SerializeField] private RawImage autoPilotDurationBarBackGround;

    private Dictionary<AutoLetter, Text> autoLetterDictionnary = new Dictionary<AutoLetter, Text>();


    protected override void Awake()
    {
        base.Awake();

        autoLetterDictionnary.Add(AutoLetter.A, Aletter);
        autoLetterDictionnary.Add(AutoLetter.U, Uletter);
        autoLetterDictionnary.Add(AutoLetter.T, Tletter);
        autoLetterDictionnary.Add(AutoLetter.O, Oletter);

        ResetState();
    }

    private void Start()
    {
        AutoPilotManager.Instance.AutoPilotEndEvent += ResetAllAutoLetters;

        AutoPilotManager.Instance.AutoPilotStartEvent += StartUpdateAutoPilotDurationBar;

        GameLoopManager.Instance.ResetGameEvent += ResetState;
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

    public void ToggleAutoLetter(AutoLetter letter, bool onOff)
    {
        autoLetterDictionnary[letter].enabled = onOff;
    }

    private void StartUpdateAutoPilotDurationBar()
    {
        StartCoroutine(UpdateAutoPilotBar());
    }

    private IEnumerator UpdateAutoPilotBar()
    {
        autoPilotDurationBar.enabled = true;

        autoPilotDurationBarBackGround.enabled = true;

        float duration = AutoPilotManager.Instance.AutoPilotDuration;

        float elapsedTime = 0;

        float startWidth = autoPilotDurationBar.rectTransform.sizeDelta.x;

        while (elapsedTime <= duration)
        {
            float t = elapsedTime / duration;

            float newWidth = Mathf.Lerp(startWidth, 0f, t);

            autoPilotDurationBar.rectTransform.sizeDelta = new Vector2(newWidth, autoPilotDurationBar.rectTransform.sizeDelta.y);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        autoPilotDurationBar.rectTransform.sizeDelta = new Vector2(startWidth, autoPilotDurationBar.rectTransform.sizeDelta.y);

        autoPilotDurationBar.enabled = false;
        autoPilotDurationBarBackGround.enabled = false;
    }

    public void ResetAllAutoLetters()
    {
        foreach (KeyValuePair<AutoLetter, Text> item in autoLetterDictionnary)
        {
            item.Value.enabled = false;
        }
    }

    public void ResetState()
    {
        ToggleLoseScreen(false);
        ToggleWinScreen(false);
        ToggleStartScreen(true);
        ResetAllAutoLetters();
        autoPilotDurationBar.enabled = false;
        autoPilotDurationBarBackGround.enabled = false;
    }
}
