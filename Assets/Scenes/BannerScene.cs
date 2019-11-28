//
//  Copyright 2019  Fyber N.V
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

using UnityEngine;
using Fyber;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// A Scene demonstrating how to request and display Banner ads using the FairBid SDK.
/// </summary>
public class BannerScene : MonoBehaviour, BannerListener {

    /// <summary>
    /// The Banner's placement name - as configured at Fyber console
    /// "BannerPlacementIdExample" can be used using the provided example APP_ID
    /// </summary>
    /// TODO change to your own configured placement.
    public const String BannerPlacementName = "197407";

    /// <summary>
    /// Helper for managing the user interface
    /// </summary>
    private PlacementSampleUIWrapper mUserInterfaceWrapper;

    /// <summary>
    /// Called when the showBanner is clicked
    /// This function provides an example for calling the API method Banner.display in order to display a banner placement
    /// </summary>
    /// <param name="bannerPlacementName">name of placement to be requested</param>
    private void OnShowBannerClicked(String bannerPlacementName) {
        BannerOptions bannerOptions = generateBannerOptions();
        Banner.Show(bannerPlacementName, bannerOptions);
        mUserInterfaceWrapper.startRequestAnimation();
    }

    /// <summary>
    /// Convenience method. Generates a new instance of BannerOptions and configure it accordingly.
    /// </summary>
    /// <returns>A new banner options instance.</returns>
    private BannerOptions generateBannerOptions() {
        BannerOptions bannerOptions = new BannerOptions();
        if (mUserInterfaceWrapper.isTopToggleSelcted()) {
            bannerOptions.DisplayAtTheTop();
        } else {
            bannerOptions.DisplayAtTheBottom();
        }
        return bannerOptions;
    }

    /// <summary>
    /// Sets the fair bid banner listener.
    /// </summary>
    private void setFairBidBannerListener() {
        Banner.SetBannerListener(this);
    }

    #region BannerListener

    /// <summary>
    /// Called when the destroyBannerButton is clicked
    /// This function provides an example for calling the API method Banner.destroy in order to destroy a banner placement
    /// </summary>
    /// <param name="bannerPlacementName">name of placement to be destroyed</param>
    private void OnDestroyBannerClicked(String bannerPlacementName) {
        Banner.Destroy(bannerPlacementName);
        mUserInterfaceWrapper.resetAnimation();
    }

    /// <summary>
    /// This an example of Listening to FairBid Banner Callbacks and events.
    /// </summary>
    /// <param name="placementName">The Placement name.</param>
    public void OnLoad(string placementName) {
        mUserInterfaceWrapper.addLog("OnLoad()");
        mUserInterfaceWrapper.onAdAvailableAnimation();
    }

    /// <summary>
    /// This an example of Listening to FairBid Banner Callbacks and events.
    /// </summary>
    /// <param name="placementName">The Placement name.</param>
    /// <param name="impressionData">The Impression Data.</param>
    public void OnShow(string placementName, ImpressionData impressionData)
    {
        mUserInterfaceWrapper.addLog("OnShow()");
    }

    /// <summary>
    /// This an example of Listening to FairBid Banner Callbacks and events.
    /// </summary>
    /// <param name="placementName">The Placement name.</param>
    public void OnRequestStart(string placementName)
    {
        mUserInterfaceWrapper.addLog("OnRequestStart()");
    }

    /// <summary>
    /// This an example of Listening to FairBid Banner Callbacks and events.
    /// </summary>
    /// <param name="placementName">The Placement name.</param>
    public void OnClick(string placementName) {
        mUserInterfaceWrapper.addLog("OnClick()");
    }

    /// <summary>
    /// This an example of Listening to FairBid Banner Callbacks and events.
    /// </summary>
    /// <param name="placementName">The Placement name.</param>
    /// <param name="error">Error.</param>
    public void OnError(string placementName, string error) {
        mUserInterfaceWrapper.addLog("OnError()");
        mUserInterfaceWrapper.resetAnimation();
    }

    #endregion

    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start() {
        initAnimationObject();
        setFairBidBannerListener();
        mUserInterfaceWrapper.resetAnimation();
    }

    /// <summary>
    /// Internal sample method. initialize the placement user interface used to display callbacks and events.
    /// </summary>
    private void initAnimationObject() {
        mUserInterfaceWrapper = new PlacementSampleUIWrapper(true, transform, () => OnShowBannerClicked(BannerPlacementName), () => OnDestroyBannerClicked(BannerPlacementName));
    }

    /// <summary>
    /// Internal sample method. returns to the main scene
    /// </summary>
    public void DestroyBannerScene() {
        Banner.Destroy(BannerPlacementName);
        SceneManager.LoadScene("MainScreen");
    }
}
