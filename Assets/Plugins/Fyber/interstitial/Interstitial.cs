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
  /// Use this class to show interstitial ads. Depending on the network and your settings, these can be static or video ads.
  /// </summary>
  public class Interstitial : MonoBehaviour
  {

    private static InterstitialListener interstitialListener;
    private static Interstitial _instance;

    /// <summary>
    /// Shows an ad with the given options.
    /// </summary>
    /// <param name="placementName"> The placement to show the ad with</param>
    public static void Show(string placementName)
    {
      #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
      #if UNITY_ANDROID
      InterstitialAndroid.Show(placementName);
      #elif UNITY_IPHONE
      InterstitialIOS.Show(placementName);
      #endif
      #else
      UnityEngine.Debug.LogWarning("Call received to show an Interstitial, but the SDK does not function in the editor. You must use a device/emulator to fetch/show ads.");
      _instance.StartCoroutine(InvokeCallbackNextFrame("failed", placementName));
      #endif
    }

    /// <summary>
    /// Requests an ad for the given placement name.
    /// </summary>
    /// <param name="placementName">The placement name to request an ad for.</param>
    public static void Request(string placementName)
    {
      #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
      #if UNITY_ANDROID
      InterstitialAndroid.Fetch(placementName);
      #elif UNITY_IPHONE
      InterstitialIOS.Fetch(placementName);
      #endif
      #else
      UnityEngine.Debug.LogWarning("Call received to fetch an Interstitial, but the SDK does not function in the editor. You must use a device/emulator to fetch/show ads.");
      _instance.StartCoroutine(InvokeCallbackNextFrame("fetch_failed", placementName));
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
      return InterstitialAndroid.IsAvailable(placementName);
      #elif UNITY_IPHONE
      return InterstitialIOS.IsAvailable(placementName);
      #endif
      #else
      return false;
      #endif
    }

    /// <summary>
    /// Stops any automatic request for the given placement name.
    /// </summary>
    /// <param name="placementName">The placement name to stop the automatic request for.</param>
    public static void StopRequesting(string placementName)
    {
      #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
      #if UNITY_ANDROID
      InterstitialAndroid.StopRequesting(placementName);
      #elif UNITY_IPHONE
      InterstitialIOS.StopRequesting(placementName);
      #endif
      #else
      UnityEngine.Debug.LogWarning("Call received to stop requesting an Interstitial, but the SDK does not function in the editor. You must use a device/emulator to fetch/show ads.");
      _instance.StartCoroutine(InvokeCallbackNextFrame("fetch_failed", placementName));
      #endif
    }

    /// <summary>
    /// Sets the AdDisplayListener for interstitial ads, which will receive callbacks regarding the state of interstitial ads.
    /// </summary>
    public static void SetInterstitialListener(InterstitialListener listener)
    {
      Interstitial.interstitialListener = listener;
    }

    #region Internal methods
    internal static void InitReceiver(){
      if (_instance == null)
      {
        GameObject receiverObject = new GameObject("FairBidInterstitial");
        DontDestroyOnLoad(receiverObject);
        _instance = receiverObject.AddComponent<Interstitial>();
      }
    }

    public void SetCallback(string message)
    {
      string[] displayStateParams = message.Split(',');
      string callback = displayStateParams[0];
      string placementName = displayStateParams[1];
      Interstitial.SetCallbackStateAndPlacement(callback, placementName);
    }

    public static void SetCallbackStateAndPlacement(string callback, string placementName)
    {
      if (Interstitial.interstitialListener != null)
      {
        switch (callback)
        {
          case "show":
            Interstitial.interstitialListener.OnShow(placementName);
            break;
          case "failed":
            Interstitial.interstitialListener.OnShowFailure(placementName);
            break;
          case "available":
            Interstitial.interstitialListener.OnAvailable(placementName);
            break;
          case "fetch_failed":
            Interstitial.interstitialListener.OnUnavailable(placementName);
            break;
          case "click":
            Interstitial.interstitialListener.OnClick(placementName);
            break;
          case "hide":
            Interstitial.interstitialListener.OnHide(placementName);
            break;
          case "audio_starting":
            Interstitial.interstitialListener.OnAudioStart(placementName);
            break;
          case "audio_finished":
            Interstitial.interstitialListener.OnAudioFinish(placementName);
            break;
          default:
            Console.WriteLine("Unkown callback for Interstitial");
            break;
        }
      }
    }

    protected static IEnumerator InvokeCallbackNextFrame(string callback, string placementName)
    {
      yield return null; // wait a frame
      Interstitial.SetCallbackStateAndPlacement(callback, placementName);
    }
    #endregion
  }

  #region Platform-specific translations
  #if UNITY_IPHONE && !UNITY_EDITOR
  public class InterstitialIOS
  {
    [DllImport ("__Internal")]
    private static extern void fyb_sdk_show_interstitial(string placementName);
    [DllImport ("__Internal")]
    private static extern void fyb_sdk_fetch_interstitial(string placementName);
    [DllImport ("__Internal")]
    private static extern bool fyb_sdk_interstitial_is_available(string placementName);
    [DllImport ("__Internal")]
    private static extern void fyb_sdk_stop_requesting_interstitial(string placementName);

    public static void Show(string placementName)
    {
      fyb_sdk_show_interstitial(placementName);
    }

    public static void Fetch(string placementName)
    {
      fyb_sdk_fetch_interstitial(placementName);
    }

    public static bool IsAvailable(string placementName)
    {
      return fyb_sdk_interstitial_is_available(placementName);
    }

    public static void StopRequesting(string placementName)
    {
      fyb_sdk_stop_requesting_interstitial(placementName);
    }

  }
  #endif

  #if UNITY_ANDROID && !UNITY_EDITOR
  public class InterstitialAndroid
  {

    public static void Show(string placementName)
    {
      if(Application.platform != RuntimePlatform.Android) return;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
      {
        jc.CallStatic("showInterstitial", placementName);
      }
    }

    public static void Fetch(string placementName)
    {
      if(Application.platform != RuntimePlatform.Android) return;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.ads.Interstitial"))
      {
        jc.CallStatic("request", placementName);
      }
    }

    public static void StopRequesting(string placementName)
    {
      if(Application.platform != RuntimePlatform.Android) return;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.ads.Interstitial"))
      {
        jc.CallStatic("stopRequesting", placementName);
      }
    }

    public static Boolean IsAvailable(string placementName)
    {
      if(Application.platform != RuntimePlatform.Android) return false;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.ads.Interstitial"))
      {
        return jc.CallStatic<Boolean>("isAvailable", placementName);
      }
    }

  }
  #endif
  #endregion
}
