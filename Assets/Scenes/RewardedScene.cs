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
/// A Scene demonstrating how to request and display rewarded ads using the FairBid SDK.
/// </summary>
public class RewardedScene : MonoBehaviour, RewardedListener {

    /// <summary>
    /// The Rewarded's placement name - as configured at Fyber console
    /// "RewardedPlacementIdExample" can be used using the provided example APP_ID
    /// </summary>
    /// TODO change to your own configured placement.
    private const String RewardedPlacementName = "197406";

    /// <summary>
    /// Helper for managing the user interface
    /// </summary>
    private PlacementSampleUIWrapper mUserInterfaceWrapper;

    /// <summary>
    /// Called when the requestButton is clicked
    /// This function provides an example for calling the API method Rewarded.Request in order to request a rewarded placement
    /// </summary>
    /// <param name="rewardedPlacementName">The name of placement to be requested.</param>
    private void OnRequestAdButtonClicked(String rewardedPlacementName) {
        if (!Rewarded.IsAvailable(rewardedPlacementName)) {
            Rewarded.Request(rewardedPlacementName);
            mUserInterfaceWrapper.startRequestAnimation();
        } else {
            mUserInterfaceWrapper.onAdAvailableAnimation();
        }
    }

    /// <summary>
    /// Called when the showButton is clicked
    /// This function provides an example for calling the API method Rewarded.Show in order to show the ad received in the provided placement
    /// </summary>
    /// <param name="rewardedPlacementName">name of placement to be displayed.</param>
    private void OnShowAdButtonClicked(String rewardedPlacementName) {
        Rewarded.Show(rewardedPlacementName);
        mUserInterfaceWrapper.resetAnimation();
    }

    /// <summary>
    /// This function provides an example of Listening to FairBid Rewarded Callbacks and events.
    /// </summary>
    private void setFairBidRewardedListener() {
        Rewarded.SetRewardedListener(this);
    }

    #region RewardedListener methods

    /// <summary>
    /// This an example of Listening to FairBid Rewarded Callbacks and events.
    /// </summary>
    /// <param name="placementName">The Placement name.</param>
    public void OnShow(string placementName) {
        mUserInterfaceWrapper.addLog("OnShow()");
    }

    /// <summary>
    /// This an example of Listening to FairBid Rewarded Callbacks and events.
    /// </summary>
    /// <param name="placementName">The Placement name.</param>
    public void OnClick(string placementName) {

        mUserInterfaceWrapper.addLog("OnClick()");
    }

    /// <summary>
    /// This an example of Listening to FairBid Rewarded Callbacks and events.
    /// </summary>
    /// <param name="placementName">The Placement name.</param>
    public void OnHide(string placementName) {

        mUserInterfaceWrapper.addLog("OnHide()");
    }

    /// <summary>
    /// This an example of Listening to FairBid Rewarded Callbacks and events.
    /// </summary>
    /// <param name="placementName">The Placement name.</param>
    public void OnShowFailure(string placementName) {

        mUserInterfaceWrapper.addLog("OnShowFailure()");
    }

    /// <summary>
    /// This an example of Listening to FairBid Rewarded Callbacks and events.
    /// </summary>
    /// <param name="placementName">The Placement name.</param>
    public void OnAvailable(string placementName) {
        mUserInterfaceWrapper.addLog("OnAvailable()");
        mUserInterfaceWrapper.onAdAvailableAnimation();
    }

    /// <summary>
    /// This an example of Listening to FairBid Rewarded Callbacks and events.
    /// </summary>
    /// <param name="placementName">The Placement name.</param>
    public void OnUnavailable(string placementName) {
        mUserInterfaceWrapper.addLog("OnUnavailable()");
        mUserInterfaceWrapper.resetAnimation();
    }

    /// <summary>
    /// This an example of Listening to FairBid Rewarded Callbacks and events.
    /// </summary>
    /// <param name="placementName"The Placement name.</param>
    /// <param name="userRewarded">If set to <c>true</c> user rewarded.</param>
    public void OnCompletion(string placementName, bool userRewarded) {
        mUserInterfaceWrapper.addLog(userRewarded ? "OnCompletion(), userRewarded:True" : "OnCompletion()");
    }

    #endregion

    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start() {
        initAnimationObject();
        setFairBidRewardedListener();
        mUserInterfaceWrapper.resetAnimation();
    }

    /// <summary>
    /// Internal sample method. initialize the placement user interface used to display callbacks and events.
    /// </summary>
    private void initAnimationObject() {
        mUserInterfaceWrapper = new PlacementSampleUIWrapper(false, transform, () => OnRequestAdButtonClicked(RewardedPlacementName), () => OnShowAdButtonClicked(RewardedPlacementName));
    }

    /// <summary>
    /// Internal sample method. returns to the main scene
    /// </summary>
    public void DestroyScene() {
        SceneManager.LoadScene("MainScreen");
    }

}
