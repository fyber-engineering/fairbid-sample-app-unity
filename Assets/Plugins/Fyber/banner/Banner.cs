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
  /// Use this class to show banner ads.
  /// </summary>
  public class Banner : MonoBehaviour
  {

    private static BannerListener bannerListener;
    private static Banner _instance;


    public static void Show(string placementName)
    {
      Banner.Show(placementName, null);
    }

    /// <summary>
    /// Shows a banner ad with the given options.
    /// </summary>
    /// <param name="showOptions">The options with which to show the banner ad, or the defaults if <c>null</c> </param>
    public static void Show(string placementName, BannerOptions showOptions)
    {
      if (showOptions == null)
      {
        showOptions = new BannerOptions();
      }

      #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
        #if UNITY_ANDROID
          BannerAndroid.Show(placementName, showOptions);
        #elif UNITY_IPHONE
          BannerIOS.Show(placementName, showOptions);
        #endif
      #else
        string message = "Call received to show an Banner, but the SDK does not function in the editor. You must use a device/emulator to fetch/show ads.";
        UnityEngine.Debug.LogWarning(message);
        _instance.StartCoroutine(InvokeCallbackNextFrame("error", placementName + ":" + message));
      #endif
    }


    /// <summary>
    /// Hides the current banner ad, if there is one, from the view. The next call to ShowWithOptions will unhide the banner ad hidden by this method.
    /// </summary>
    public static void Hide(string placementName)
    {
      #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
        #if UNITY_ANDROID
          BannerAndroid.Hide(placementName);
        #elif UNITY_IPHONE
          BannerIOS.Hide(placementName);
        #endif
      #else
      #endif
    }

    /// <summary>
    /// Destroys the current banner ad, if there is one. The next call to ShowWithOptions() will create a new banner ad.
    /// </summary>
    public static void Destroy(string placementName)
    {
      #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
        #if UNITY_ANDROID
          BannerAndroid.Destroy(placementName);
        #elif UNITY_IPHONE
          BannerIOS.Destroy(placementName);
        #endif
      #else
      #endif
    }

    /// <summary>
    /// Sets the AdDisplayListener for banner ads, which will receive callbacks regarding the state of banner ads.
    /// </summary>
    public static void SetBannerListener(BannerListener listener)
    {
      Banner.bannerListener = listener;
    }

    #region Internal methods
    public static void InitReceiver(){
      if (_instance == null)
      {
        GameObject receiverObject = new GameObject("FairBidBanner");
        DontDestroyOnLoad(receiverObject);
        _instance = receiverObject.AddComponent<Banner>();
      }
    }

    public void SetCallback(string message)
    {
      string[] displayStateParams = message.Split(',');
      string callback = displayStateParams[0];
      string placementName = displayStateParams[1];
      Banner.SetCallbackStateAndPlacement(callback, placementName);
    }

    public static void SetCallbackStateAndPlacement(string callback, string placementName)
    {
      if (Banner.bannerListener != null)
      {
        switch (callback)
        {
          case "show":
            Banner.bannerListener.OnShow(placementName);
            break;
          case "error":
            string[] placementAndMessage = placementName.Split(':');
            Banner.bannerListener.OnError(placementAndMessage[0], placementAndMessage[1]);
            break;
          case "click":
            Banner.bannerListener.OnClick(placementName);
            break;
          case "loaded":
            Banner.bannerListener.OnLoad(placementName);
            break;
          default:
            Console.WriteLine("Unkown callback for Banner");
            break;
        }
      }
    }

    protected static IEnumerator InvokeCallbackNextFrame(string callback, string placementName)
    {
      yield return null; // wait a frame
      Banner.SetCallbackStateAndPlacement(callback, placementName);
    }
    #endregion
  }

  #region Platform-specific translations
  #if UNITY_IPHONE && !UNITY_EDITOR
  public class BannerIOS : MonoBehaviour
  {
    [DllImport ("__Internal")]
    private static extern void fyb_sdk_show_banner(string position, string placementName);
    [DllImport ("__Internal")]
    private static extern bool fyb_sdk_hide_banner();
    [DllImport ("__Internal")]
    private static extern bool fyb_sdk_destroy_banner();


    public static void Show(string placementName, BannerOptions showOptions)
    {
      fyb_sdk_show_banner(showOptions.position, placementName);
    }

    public static bool Hide(string placementName)
    {
      return fyb_sdk_hide_banner();
    }

    public static void Destroy(string placementName)
    {
      fyb_sdk_destroy_banner();
    }

  }
  #endif

  #if UNITY_ANDROID && !UNITY_EDITOR
  public class BannerAndroid : MonoBehaviour
  {

    public static void Show(string placementName, BannerOptions showOptions)
    {
      if(Application.platform != RuntimePlatform.Android) return;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
      {
        jc.CallStatic("showBanner", placementName, showOptions.position);
      }
    }

    public static void Hide(string placementName)
    {
      if(Application.platform != RuntimePlatform.Android) return;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.ads.Banner"))
      {
        jc.CallStatic("hide", placementName);
      }
    }

    public static void Destroy(string placementName)
    {
      if(Application.platform != RuntimePlatform.Android) return;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.ads.Banner"))
      {
        jc.CallStatic("destroy", placementName);
      }
    }

  }
  #endif
  #endregion
}
