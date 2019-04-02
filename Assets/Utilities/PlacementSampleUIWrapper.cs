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

using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Placement sample UI Wrapper.
/// This is a helper class which helps manage the ui across the various placement samples.
/// </summary>
public class PlacementSampleUIWrapper {
    
	/// <summary>
	/// The request ad button.
	/// </summary>
	public Button mRequestAdButton;
	/// <summary>
	/// The show ad button.
	/// </summary>
	public Button mShowAdButton;
	/// <summary>
	/// The spinner background.
	/// </summary>
	public Image mSpinnerBackground;
	/// <summary>
	/// The spinner progress sprite.
	/// </summary>
	public Image mSpinnerProgress;
	/// <summary>
	/// The back button.
	/// </summary>
	public Image mBackButton;
	/// <summary>
	/// The clean callback list button.
	/// </summary>
	private Button mCleanCallbackListButton;
	/// <summary>
	/// The logs adapter, this helps us manage the callback list
	/// </summary>
	private LogsViewAdapter mLogsAdapter;
	/// <summary>
	/// The top toggles.
	/// </summary>
	public Toggle mTopToggle;
	/// <summary>
	/// The bottom toggle.
	/// </summary>
	public Toggle mBottomToggle;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:PlacementSampleUIWrapper"/> class.
    /// </summary>
    /// <param name="banner">If set to <c>true</c> banner.</param>
    /// <param name="transform">Transform.</param>
    /// <param name="request">Request.</param>
    /// <param name="show">Show.</param>
    public PlacementSampleUIWrapper(Boolean banner, Transform transform, UnityEngine.Events.UnityAction request, UnityEngine.Events.UnityAction show) {
        if (banner) {
            mRequestAdButton = transform.Find("Background/ShowBannerAd").GetComponent<Button>();
            mShowAdButton = transform.Find("Background/DestroyAd").GetComponent<Button>();
            CleanCallBackListButton = transform.Find("Background/CleanCallBacksButton").GetComponent<Button>();
            LogsAdapter = transform.Find("Background/ScrollView").GetComponent<LogsViewAdapter>();
            mSpinnerBackground = transform.Find("Background/ShowBannerAd/ProgressBackground").GetComponent<Image>();
            mSpinnerProgress = transform.Find("Background/ShowBannerAd/ProgressBackground/Progress").GetComponent<Image>();
            mBackButton = transform.Find("Background/Header/BackButton").GetComponent<Image>();
            mTopToggle = transform.Find("Background/ToggleTop").GetComponent<Toggle>();
            mBottomToggle = transform.Find("Background/ToggleBottom").GetComponent<Toggle>();

            mRequestAdButton.onClick.AddListener(request);
            mShowAdButton.onClick.AddListener(show);
        } else {
            mRequestAdButton = transform.Find("Background/RequestAd").GetComponent<Button>();
            mShowAdButton = transform.Find("Background/ShowAd").GetComponent<Button>();
            CleanCallBackListButton = transform.Find("Background/CleanCallBacksButton").GetComponent<Button>();
            LogsAdapter = transform.Find("Background/ScrollView").GetComponent<LogsViewAdapter>();
            mSpinnerBackground = transform.Find("Background/RequestAd/ProgressBackground").GetComponent<Image>();
            mSpinnerProgress = transform.Find("Background/RequestAd/ProgressBackground/Progress").GetComponent<Image>();
            mBackButton = transform.Find("Background/Header/BackButton").GetComponent<Image>();

            mRequestAdButton.onClick.AddListener(request);
            mShowAdButton.onClick.AddListener(show);
        }
    }


    /// <summary>
    /// Gets or sets the logs adapter.
    /// </summary>
    /// <value>The logs adapter.</value>
    public LogsViewAdapter LogsAdapter {
		get {
			return mLogsAdapter;
		}

		set {
			if (value != null) {
				mLogsAdapter = value;
				mLogsAdapter.OnFirstLogLine += () => mCleanCallbackListButton.interactable = true;
			}
		}
	}

    /// <summary>
    /// Gets or sets the "clean callback list" button.
    /// </summary>
    /// <value>The "clean callback list" button.</value>
	public Button CleanCallBackListButton {
		get {
			return mCleanCallbackListButton;
		}

		set {
			if (value != null) {
				this.mCleanCallbackListButton = value;
				mCleanCallbackListButton.interactable = false;
				mCleanCallbackListButton.onClick.AddListener (OnCleanCallbacksList);
			}
		}
	}

	/// <summary>
	/// Starts the request animation.
	/// </summary>
	public void startRequestAnimation() {
		setSpinnerState (true);
		setShowButtonState (false);
	}

	/// <summary>
	/// Resets the animation.
	/// </summary>
	public void resetAnimation() {
		setSpinnerState (false);
		setShowButtonState (false);
	}

	/// <summary>
	/// Call to hide the animation
	/// </summary>
	public void onAdAvailableAnimation() {
		setSpinnerState (false);
		setShowButtonState (true);
	}

    /// <summary>
    /// Sets the state of the spinner.
    /// </summary>
    /// <param name="state">makes the spinner active when set to <c>true</c> state. false otherwise</param>
	private void setSpinnerState(bool state) {
		this.mSpinnerBackground.enabled = state;
		this.mSpinnerProgress.enabled = state;
	}

    /// <summary>
    /// Sets the state of the show button.
    /// </summary>
    /// <param name="state">Shows the button if set to <c>true</c> state.</param>
	private void setShowButtonState(bool state) {
		mShowAdButton.interactable = state;
	}

    /// <summary>
    /// Invoked when we want to clean the callback list ("logs")
    /// </summary>
	private void OnCleanCallbacksList() {
		Debug.Log ("OnCleanCallbacksList");
		mCleanCallbackListButton.interactable = false;
		mLogsAdapter.clearLogList ();
	}

    /// <summary>
    /// Adds a log line (callback)
    /// </summary>
    /// <param name="log">Log.</param>
	public void addLog(String log) {
		this.mLogsAdapter.addLogLine (DateTime.Now.ToString ("HH:mm:ss") + " - " + log);
	}

    /// <summary>
    /// return if top toggle is selcted.
    /// </summary>
    /// <returns><c>true</c>, if top toggle selcted <c>false</c> otherwise.</returns>
	public bool isTopToggleSelcted() {
		if (mTopToggle != null) {
			return mTopToggle.isOn;
		}
		return false;
	}
}
