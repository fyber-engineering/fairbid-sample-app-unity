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
  /// FairBid wrapper for iOS and Android via Unity. For more information, see https://developers.fyber.com/docs/unity_sdk_setup_and_requirements .
  /// </summary>
  public class FairBid : MonoBehaviour
  {
    public const string Version = "2.0.0";

    private static FairBid _instance;
    private bool initialized { get; set; }
    private int flags { get; set; }
    private string appId { get; set; }

    public void Start()
    {
      if (!initialized)
      {
        #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
          #if UNITY_ANDROID
            FairBidAndroid.Start(appId, flags);
          #elif UNITY_IPHONE
            FairBidIOS.SetPluginVersion(Version);
            FairBidIOS.Start(appId, (flags & FLAG_DISABLE_AUTOMATIC_REQUESTING) == 0);
          #endif
        #else
          UnityEngine.Debug.LogError("Call received to start the FairBid SDK, but the SDK does not function in the editor. You must use a device/emulator to receive/test ads.");
        #endif

        FairBid.InitReceiver();
        Interstitial.InitReceiver();
        Rewarded.InitReceiver();
        Banner.InitReceiver();
        User.InitReceiver();
        initialized = true;
      }
    }

    public FairBid DisableAutoRequesting()
    {
      if (!initialized)
      {
        flags |= FLAG_DISABLE_AUTOMATIC_REQUESTING;
      }
      return this;
    }

    public FairBid EnableLogs()
    {
      if (!initialized)
      {
        #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
          #if UNITY_ANDROID
            FairBidAndroid.ShowDebugLogs();
          #elif UNITY_IPHONE
            FairBidIOS.ShowDebugLogs();
          #endif
        #endif
      }
      return this;
    }

    public static void Start(string appId)
    {
      ConfigureForAppId(appId).Start();
    }

    public static FairBid ConfigureForAppId(string appId)
    {
      if (string.IsNullOrEmpty(appId))
      {
        throw new ArgumentException("App ID cannot be null nor empty");
      }

      if (_instance == null)
      {
        InitReceiver();
        _instance.appId = appId;
      }
      return _instance;
    }

    /// <summary>
    /// Shows the mediation test suite.
    /// </summary>
    public static void ShowTestSuite()

    {
      #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
        #if UNITY_ANDROID
          FairBidAndroid.ShowMediationTestSuite();
        #elif UNITY_IPHONE
          FairBidIOS.ShowMediationTestSuite();
        #endif
      #else
        UnityEngine.Debug.LogWarning("Call received to show the FairBid SDK test suite, but the SDK does not function in the editor. You must use a device/emulator to use the test suite.");
      #endif
    }

    #region Internal methods

    // private const int FLAG_NO_OPTIONS = 0 << 0; // 0
    private const int FLAG_DISABLE_AUTOMATIC_REQUESTING = 1 << 0; // 1

    internal static void InitReceiver()
    {
      if (_instance == null)
      {
        GameObject receiverObject = new GameObject("FairBid");
        DontDestroyOnLoad(receiverObject);
        _instance = receiverObject.AddComponent<FairBid>();
      }
    }

    #endregion
  }

  #region Platform-specific translations
  #if UNITY_IPHONE && !UNITY_EDITOR
  public class FairBidIOS : MonoBehaviour
  {
    [DllImport ("__Internal")]
    private static extern void fyb_sdk_set_plugin_version(string pluginVersion);

    [DllImport ("__Internal")]
    private static extern void fyb_sdk_start_app(string publisher_id, bool autoRequestEnabled);

    [DllImport ("__Internal")]
    private static extern void fyb_sdk_show_test_suite();

    [DllImport ("__Internal")]
    private static extern void fyb_sdk_show_debug_logs();


    public static void SetPluginVersion(string pluginVersion)
    {
      fyb_sdk_set_plugin_version(pluginVersion);
    }

    public static void Start(string publisher_id, bool autoRequestEnabled = true)
    {
      fyb_sdk_start_app(publisher_id, autoRequestEnabled);
    }

    public static void ShowMediationTestSuite()
    {
        fyb_sdk_show_test_suite();
    }

    public static void ShowDebugLogs()
    {
      fyb_sdk_show_debug_logs();
    }

  }
  #endif

  #if UNITY_ANDROID && !UNITY_EDITOR
  public class FairBidAndroid : MonoBehaviour
  {

    public static void Start(string appId, int options=0)
    {
      if(Application.platform != RuntimePlatform.Android) return;

      // string frameworkParams = string.Format("{\"plugin_version\":\"{0}\",\"framework_version\":\"{1}\"}", FairBid.Version, Application.version);
      string frameworkParams = "{\"plugin_version\":\"" +FairBid.Version+ "\",\"framework_version\":\""+Application.unityVersion+"\"}";
      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
      {
        jc.CallStatic("start", appId, options, frameworkParams);
      }
    }

    public static void ShowMediationTestSuite()
    {
      if(Application.platform != RuntimePlatform.Android) return;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
      {
        jc.CallStatic("showNetworkActivity");
      }
    }

    public static void ShowDebugLogs()
    {
      if(Application.platform != RuntimePlatform.Android) return;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
      {
        jc.CallStatic("showDebugLogs");
      }
    }

  }
  #endif
  #endregion
}
