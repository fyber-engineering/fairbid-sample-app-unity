using UnityEngine;
using UnityEngine.UI;
using Fyber;
using System;
using UnityEngine.SceneManagement;

/*
* A Scene demonstrating how to request and display Banner ads using the FairBid SDK.
*/
public class BannerScene : MonoBehaviour, BannerListener
{
    /*
    * The Banner's placement name - as configured at Fyber console
    * "BannerPlacementIdExample" can be used using the provided example APP_ID
    * TODO change to your own configured placement.
    */
    public const String BannerPlacementName = "BannerPlacementIdExample";
    private Animation mAnimation;

    void Start()
    {
        initAnimationObject();
        setFairBidBannerListener();
        mAnimation.resetAnimation();
    }

    /*
     * Internal sample method. initialize the animation view used to display callbacks and events.
      */
    private void initAnimationObject()
    {
        mAnimation = new Animation();
        mAnimation.mRequestAdButton = transform.Find("Background/ShowBannerAd").GetComponent<Button>();
        mAnimation.mShowAdButton = transform.Find("Background/DestroyAd").GetComponent<Button>();
        mAnimation.CleanCllBackListButton = transform.Find("Background/CleanCallBacksButton").GetComponent<Button>();
        mAnimation.LogsAdapter = transform.Find("Background/ScrollView").GetComponent<LogsViewAdapter>();
        mAnimation.mSpinnerBackground = transform.Find("Background/ShowBannerAd/ProgressBackground").GetComponent<Image>();
        mAnimation.mSpinnerProgress = transform.Find("Background/ShowBannerAd/ProgressBackground/Progress").GetComponent<Image>();
        mAnimation.mBackButton = transform.Find("Background/Header/BackButton").GetComponent<Image>();
        mAnimation.mTopToggle = transform.Find("Background/ToggleTop").GetComponent<Toggle>();
        mAnimation.mBottomToggle = transform.Find("Background/ToggleBottom").GetComponent<Toggle>();
        mAnimation.mRequestAdButton.onClick.AddListener(() => OnShowBannerClicked(BannerPlacementName));
        mAnimation.mShowAdButton.onClick.AddListener(() => OnDestroyBannerClickked(BannerPlacementName));
    }

    /*
    * Helper inteneral method to return to the main scene
    */
    public void DestroyBannerScene()
    {
        Banner.Destroy(BannerPlacementName);
        SceneManager.LoadScene("MainScreen");
    }

    /*
     * This function provides an example of Listening to FairBid Banner Callbacks and events.
    */
    private void setFairBidBannerListener()
    {
        Banner.SetBannerListener(this);
    }

    /*
    * Called when the showBanner is clicked
    * This function provides an example for calling the API method Banner.display in order to display a banner placement
    * @param bannerPlacementName name of placement to be requested
    */
    private void OnShowBannerClicked(String bannerPlacementName)
    {
        BannerOptions bannerOptions = generateBannerOptions();
        Banner.Show(bannerPlacementName, bannerOptions);
        mAnimation.startRequestAnimation();
    }

    /*
    * Convenience method. Generates a new instance of BannerOptions and configure it accordingly.
    */
    private BannerOptions generateBannerOptions()
    {
        BannerOptions bannerOptions = new BannerOptions();
        if (mAnimation.isTopToggleSelcted())
        {
            bannerOptions.DisplayAtTheTop();
        }
        else
        {
            bannerOptions.DisplayAtTheBottom();
        }
        return bannerOptions;
    }

    /*
    * Called when the destroyBannerButton is clicked
    * This function provides an example for calling the API method Banner.destroy in order to destroy a banner placement
    * @param bannerPlacementName name of placement to be destroyed
    */
    private void OnDestroyBannerClickked(String bannerPlacementName)
    {
        Banner.Destroy(bannerPlacementName);
        mAnimation.resetAnimation();
    }

    /*
    * This an example of Listening to FairBid Banner Callbacks and events.
    */
    public void OnError(string placementName, string error)
    {
        mAnimation.addLog("OnError()");
        mAnimation.resetAnimation();
    }

    public void OnLoad(string placementName)
    {
        mAnimation.addLog("OnLoad()");
        mAnimation.onAdAvailableAnimation();
    }

    public void OnShow(string placementName)
    {
        mAnimation.addLog("OnShow()");
    }

    public void OnClick(string placementName)
    {
        mAnimation.addLog("OnClick()");
    }
}
