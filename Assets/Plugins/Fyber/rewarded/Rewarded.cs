//
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

namespace Fyber
{
  /// <summary>
  /// Use this class to show rewarded ads.
  /// </summary>
  public class Rewarded : MonoBehaviour
  {

    private static RewardedListener rewardedListener;
    private static Rewarded _instance;

    /// <summary>
    /// Fetches an ad for the given placement name.
    /// </summary>
    /// <param name="placementName">The placement name to request an ad for.</param>
    public static void Request(string placementName)
    {
      #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
        #if UNITY_ANDROID
          RewardedAndroid.Fetch(placementName);
        #elif UNITY_IPHONE
          RewardedIOS.Fetch(placementName);
        #endif
      #else
        UnityEngine.Debug.LogWarning("Call received to fetch an Rewarded, but the SDK does not function in the editor. You must use a device/emulator to fetch/show ads.");
        _instance.StartCoroutine(InvokeCallbackNextFrame("fetch_failed", placementName));
      #endif
    }

    /// <summary>
    /// Shows an ad with the given options.
    /// </summary>
    /// <param name="placementName">The placement name to request an ad for.</param>
    public static void Show(string placementName)
    {
      // TODO should we take care of this here?
      // ValidatePlacement(placement, callback);

      #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
        #if UNITY_ANDROID
          RewardedAndroid.Show(placementName);
        #elif UNITY_IPHONE
          RewardedIOS.Show(placementName);
        #endif
      #else
        UnityEngine.Debug.LogWarning("Call received to show an Rewarded, but the SDK does not function in the editor. You must use a device/emulator to fetch/show ads.");
        _instance.StartCoroutine(InvokeCallbackNextFrame("failed", placementName));
      #endif
    }

    /// <summary>
    /// Returns whether or not an ad is available for the given placement name.
    /// </summary>
    /// <returns><c>true</c>, if an ad is available, <c>false</c> otherwise.</returns>
    public static bool IsAvailable(string placementName)
    {
      #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
        #if UNITY_ANDROID
          return RewardedAndroid.IsAvailable(placementName);
        #elif UNITY_IPHONE
          return RewardedIOS.IsAvailable(placementName);
        #endif
      #else
        return false;
      #endif
    }

    /// <summary>
    /// Stops any ongoing automatic request for the given placement name.
    /// </summary>
    /// <param name="placementName">The placement name to stop the automatic request for.</param>
    public static void StopRequesting(string placementName)
    {
      #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
        #if UNITY_ANDROID
          RewardedAndroid.StopRequesting(placementName);
        #elif UNITY_IPHONE
          RewardedIOS.StopRequesting(placementName);
        #endif
      #else
        UnityEngine.Debug.LogWarning("Call received to stop requesting an Rewarded, but the SDK does not function in the editor. You must use a device/emulator to fetch/show ads.");
        _instance.StartCoroutine(InvokeCallbackNextFrame("fetch_failed", placementName));
      #endif
    }

    /// <summary>
    /// Sets the rewardedListener for rewarded ads, which will receive callbacks regarding the state of rewarded ads.
    /// </summary>
    public static void SetRewardedListener(RewardedListener listener)
    {
      Rewarded.rewardedListener = listener;
    }

    #region Internal methods
    public static void InitReceiver(){
      if (_instance == null)
      {
        GameObject receiverObject = new GameObject("FairBidRewarded");
        DontDestroyOnLoad(receiverObject);
        _instance = receiverObject.AddComponent<Rewarded>();
      }
    }

    // private static void ValidatePlacement(string placement, NetworkCallback callback)
    // {
    //   if (string.IsNullOrEmpty(placement))
    //   {
    //     // TODO will this crash if the sdk was not started?s
    //     _instance.StartCoroutine(InvokeCallbackNextFrame(callback, placement));
    //   }
    // }

    public void SetCallback(string message)
    {
      string[] displayStateParams = message.Split(',');
      string callback = displayStateParams[0];
      string placementName = displayStateParams[1];
      Rewarded.SetCallbackStateAndPlacement(callback, placementName);
    }

    public static void SetCallbackStateAndPlacement(string callback, string placementName)
    {
      if (Rewarded.rewardedListener != null)
      {
        switch (callback)
        {
        case "show":
          Rewarded.rewardedListener.OnShow(placementName);
          break;
        case "failed":
          Rewarded.rewardedListener.OnShowFailure(placementName);
          break;
        case "available":
          Rewarded.rewardedListener.OnAvailable(placementName);
          break;
        case "fetch_failed":
          Rewarded.rewardedListener.OnUnavailable(placementName);
          break;
        case "click":
          Rewarded.rewardedListener.OnClick(placementName);
          break;
        case "hide":
          Rewarded.rewardedListener.OnHide(placementName);
          break;
        case "rewarded_result_complete":
          Rewarded.rewardedListener.OnCompletion(placementName, true);
          break;
        case "rewarded_result_incomplete":
          Rewarded.rewardedListener.OnCompletion(placementName, false);
          break;
        case "audio_starting":
          Rewarded.rewardedListener.OnAudioStart(placementName);
          break;
        case "audio_finished":
          Rewarded.rewardedListener.OnAudioFinish(placementName);
          break;
        default:
          Console.WriteLine("Unkown Rewarded callback");
          break;
        }
      }
    }

    protected static IEnumerator InvokeCallbackNextFrame(string callback, string placementName)
    {
      yield return null; // wait a frame
      Rewarded.SetCallbackStateAndPlacement(callback, placementName);
    }
    #endregion
  }

  #region Platform-specific translations
  #if UNITY_IPHONE && !UNITY_EDITOR
  public class RewardedIOS : MonoBehaviour
  {
    [DllImport ("__Internal")]
    private static extern void fyb_sdk_show_rewarded(string placementName);
    [DllImport ("__Internal")]
    private static extern void fyb_sdk_fetch_rewarded(string placementName);
    [DllImport ("__Internal")]
    private static extern bool fyb_sdk_rewarded_is_available(string placementName);
    [DllImport ("__Internal")]
    private static extern void fyb_sdk_stop_requesting_rewarded(string placementName);

    public static void Show(string placement)
    {
      fyb_sdk_show_rewarded(placement);
    }

    public static void Fetch(string placementName)
    {
      fyb_sdk_fetch_rewarded(placementName);
    }

    public static bool IsAvailable(string placementName)
    {
      return fyb_sdk_rewarded_is_available(placementName);
    }

    public static void StopRequesting(string placementName)
    {
      fyb_sdk_stop_requesting_rewarded(placementName);
    }
  }
  #endif

  #if UNITY_ANDROID && !UNITY_EDITOR
  public class RewardedAndroid : MonoBehaviour
  {

    public static void Show(string placement)
    {
      if(Application.platform != RuntimePlatform.Android) return;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
      {
        jc.CallStatic("showRewarded", placement);
      }
    }

    public static void Fetch(string placementName)
    {
      if(Application.platform != RuntimePlatform.Android) return;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.ads.Rewarded"))
      {
        jc.CallStatic("request", placementName);
      }
    }

    public static void StopRequesting(string placementName)
    {
      if(Application.platform != RuntimePlatform.Android) return;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.ads.Rewarded"))
      {
        jc.CallStatic("stopRequesting", placementName);
      }
    }

    public static Boolean IsAvailable(string placementName)
    {
      if(Application.platform != RuntimePlatform.Android) return false;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.ads.Rewarded"))
      {
        return jc.CallStatic<Boolean>("isAvailable", placementName);
      }
    }
  }
  #endif
  #endregion
}
