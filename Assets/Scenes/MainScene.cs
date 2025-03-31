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
using UnityEngine.UI;

/// <summary>
/// The Main Scene,
/// responsible for starting the FairBid SDK and displaying the different ads - banner, interstitial, rewarded
/// </summary>
public class MainScene : MonoBehaviour {

    /// <summary>
    /// The app id provided through the Fyber console
    /// "109613" can be used a sample application.
    /// </summary>
    /// TODO replace with your own app id.
    private const String PUBLISHER_APP_ID = "211501";
    

    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start() {
        setFairBidVersionTextView();
        startFairBidSdk(PUBLISHER_APP_ID);
    }

    /// <summary>
    /// Changes the current scene.
    /// </summary>
    /// <param name="sceneName">Scene name.</param>
    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Helper method for initializing the SDK with the given app id
    /// </summary>
    /// <param name="appId">The app id provided through the Fyber console</param>
    private void startFairBidSdk(String appId) {
        FairBid.Start(appId);
    }

    /// <summary>
    /// Helper method for showing the test suite
    /// </summary>
    public void ShowTestSuite() {
        FairBid.ShowTestSuite();
    }

    /// <summary>
    /// Helper method for displaying the FairBid SDK version
    /// </summary>
    private void setFairBidVersionTextView()
    {
        Text fairbidTextViewVersion = transform.Find("Background/DTVersionTV").GetComponent<Text>();
        fairbidTextViewVersion.text = "DT FAIRBID " + FairBid.Version;
    }
}
