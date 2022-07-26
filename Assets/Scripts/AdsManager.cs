using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private bool testMode = true;

    private const string gameId = "4856427";

    private const string interstitialAdPlacementID = "Interstitial_Android";
    private const string bannerAdPlacementID = "Banner_Android";
    private const string rewardedAdPlacementID = "Rewarded_Android";

    private const float interstitialAdCountDown = 60f;
    private float currentInterstitialAdCountDown = interstitialAdCountDown;
    private IEnumerator interstitialAdCoroutine;

    private void Awake()
    {
        Advertisement.Initialize(gameId, testMode);
        Advertisement.AddListener(this);

        Advertisement.Load(interstitialAdPlacementID);
        Advertisement.Load(bannerAdPlacementID);
        ResetInterstitialAdCountDown();
    }

    private void Start()
    {
        StartCoroutine(ShowBannerAdCoroutine());
    }

    public void ShowInterstitialAd()
    {
        //Debug.Log("ShowInterstitialAd");

        if (Advertisement.IsReady(interstitialAdPlacementID) && currentInterstitialAdCountDown <= 0f)
        {
            Advertisement.Show(interstitialAdPlacementID);

            //Debug.Log("Actually showing add");

            ResetInterstitialAdCountDown();
        }
    }

    private void ResetInterstitialAdCountDown()
    {
        if (interstitialAdCoroutine != null)
            StopCoroutine(interstitialAdCoroutine);

        currentInterstitialAdCountDown = interstitialAdCountDown;

        interstitialAdCoroutine = startInterstitialAdCountDown();

        StartCoroutine(interstitialAdCoroutine);
    }

    private IEnumerator startInterstitialAdCountDown()
    {
        while (currentInterstitialAdCountDown > 0f)
        {
            currentInterstitialAdCountDown -= Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator ShowBannerAdCoroutine()
    {
        while (Advertisement.IsReady(bannerAdPlacementID) == false)
        {
            yield return null;
        }

        Advertisement.Show(bannerAdPlacementID);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {

    }

    public void OnUnityAdsReady(string placementId)
    {
        //Debug.Log(placementId + " ads are ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        //Debug.Log("Add error message: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //Debug.Log(placementId + " ad started showing");
    }

}