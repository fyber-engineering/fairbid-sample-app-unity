using UnityEngine;
using UnityEngine.UI;
using Fyber;
using System;
using UnityEngine.SceneManagement;

/*
* A Scene demonstrating how to request and display rewarded ads using the FairBid SDK.
*/
public class RewardedScene : MonoBehaviour, RewardedListener
{
    /*
    * The Rewarded's placement name - as configured at Fyber console
    * "RewardedPlacementIdExample" can be used using the provided example APP_ID
    * TODO change to your own configured placement.
    */
    private const String RewardedPlacementName = "RewardedPlacementIdExample";
    private Animation mAnimation;

    void Start()
    {
        initAnimationObject();
        setFairBidRewardedListener();
        mAnimation.resetAnimation();
    }

    /*
    * Internal sample method. initialize the animation view used to display callbacks and events.
    */
    private void initAnimationObject()
    {
        mAnimation = new Animation();
        mAnimation.mRequestAdButton = transform.Find("Background/RequestAd").GetComponent<Button>();
        mAnimation.mShowAdButton = transform.Find("Background/ShowAd").GetComponent<Button>();
        mAnimation.CleanCllBackListButton = transform.Find("Background/CleanCallBacksButton").GetComponent<Button>();
        mAnimation.LogsAdapter = transform.Find("Background/ScrollView").GetComponent<LogsViewAdapter>();
        mAnimation.mSpinnerBackground = transform.Find("Background/RequestAd/ProgressBackground").GetComponent<Image>();
        mAnimation.mSpinnerProgress = transform.Find("Background/RequestAd/ProgressBackground/Progress").GetComponent<Image>();
        mAnimation.mBackButton = transform.Find("Background/Header/BackButton").GetComponent<Image>();
        mAnimation.mRequestAdButton.onClick.AddListener(() => OnRequestAdButtonClicked(RewardedPlacementName));
        mAnimation.mShowAdButton.onClick.AddListener(() => OnShowAdButtonClicked(RewardedPlacementName));
    }

    /*
    * Helper inteneral method to return to the main scene
    */
    public void DestroyScene()
    {
        SceneManager.LoadScene("MainScreen");
    }

    /*
    * This function provides an example of Listening to FairBid Rewarded Callbacks and events.
    */
    private void setFairBidRewardedListener()
    {
        Rewarded.SetRewardedListener(this);
    }

    /*
    * Called when the requestButton is clicked
    * This function provides an example for calling the API method Rewarded.rqueest in order to request a rewarded placement
    * @param rewardedPlacementName name of placement to be requested
    */
    private void OnRequestAdButtonClicked(String rewardedPlacementName)
    {
        if (!Rewarded.IsAvailable(rewardedPlacementName))
        {
            Rewarded.Request(rewardedPlacementName);
            mAnimation.startRequestAnimation();
        }
        else
        {
            mAnimation.onAdAvailableAnimation();
        }
    }

    /*
    * Called when the showButton is clicked
    * This function provides an example for calling the API method Rewarded.show in order to show the ad received in the provided placement
    * @param rewardedPlacementName name of placement to be displayed
    */
    private void OnShowAdButtonClicked(String rewardedPlacementName)
    {
        Rewarded.Show(rewardedPlacementName);
        mAnimation.resetAnimation();
    }

    /*
    * This an example of Listening to FairBid Rewarded Callbacks and events.
    */
    public void OnShow(string placementName)
    {
        mAnimation.addLog("OnShow()");
    }

    public void OnClick(string placementName)
    {

        mAnimation.addLog("OnClick()");
    }

    public void OnHide(string placementName)
    {

        mAnimation.addLog("OnHide()");
    }

    public void OnShowFailure(string placementName)
    {

        mAnimation.addLog("OnShowFailure()");
    }

    public void OnAvailable(string placementName)
    {
        mAnimation.addLog("OnAvailable()");
        mAnimation.onAdAvailableAnimation();
    }

    public void OnUnavailable(string placementName)
    {
        mAnimation.addLog("OnUnavailable()");
        mAnimation.resetAnimation();
    }

    public void OnAudioStart(string placementName)
    {
        mAnimation.addLog("OnAudioStart()");
    }

    public void OnAudioFinish(string placementName)
    {
        mAnimation.addLog("OnAudioFinish()");
    }

    public void OnCompletion(string placementName, bool userRewarded)
    {
        mAnimation.addLog(userRewarded ? "OnCompletion(), userRewarded:True" : "OnCompletion()");
    }

}
