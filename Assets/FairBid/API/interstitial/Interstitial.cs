//
// FairBid Unity SDK
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

namespace Fyber
{
    /// <summary>
    /// Class responsible for managing Interstitial ads.
    /// </summary>
    public class Interstitial : MonoBehaviour
    {

        private static InterstitialListener interstitialListener;
        private static Interstitial _instance;

        /// <summary>
        /// Requests an ad from the ad server.
        /// </summary>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        /// <seealso cref="InterstitialListener.OnAvailable(string)" />
        /// <seealso cref="InterstitialListener.OnUnavailable(string)" />
        public static void Request(string placementId)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    InterstitialAndroid.Fetch(placementId);
                #elif UNITY_IPHONE
                    InterstitialIOS.Fetch(placementId);
                #endif
            #else
                UnityEngine.Debug.LogWarning("Call received to fetch an Interstitial, but the SDK does not function in the editor. You must use a device/emulator to fetch/show ads.");
            #endif
        }

        /// <summary>
        /// Displays an ad. If the placement has not been requested before, no ad will be shown
        /// </summary>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        /// <seealso cref="InterstitialListener.OnShow(string)" />
        /// <seealso cref="InterstitialListener.OnShowFailure(string)" />
        public static void Show(string placementId)
        {
            Show(placementId, null);
        }


        /// <summary>
        /// Displays an ad. If the placement has not been requested before, no ad will be shown
        /// </summary>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        /// <param name="options">The options for the ad.</param>
        /// <seealso cref="InterstitialListener.OnShow(string)" />
        /// <seealso cref="InterstitialListener.OnShowFailure(string)" />
        public static void Show(string placementId, ShowOptions options)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)

                string optionsString = null;
                if (options != null && options.CustomParameters != null)
                {
                    optionsString = Utils.DictToJson(options.CustomParameters);
                }

                #if UNITY_ANDROID
                    InterstitialAndroid.Show(placementId, optionsString);
                #elif UNITY_IPHONE
                    InterstitialIOS.Show(placementId, optionsString);
                #endif
            #else
                UnityEngine.Debug.LogWarning("Call received to show an Interstitial, but the SDK does not function in the editor. You must use a device/emulator to fetch/show ads.");
            #endif
        }


        /// <summary>
        /// Checks if an ad is immediately available to show.
        /// </summary>
        /// <returns><c>true</c> if the ad is completely loaded and ready to be shown.</returns>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        /// <seealso cref="InterstitialListener.OnAvailable(string)" />
        /// <seealso cref="InterstitialListener.OnUnavailable(string)" />
        public static bool IsAvailable(string placementId)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    return InterstitialAndroid.IsAvailable(placementId);
                #elif UNITY_IPHONE
                    return InterstitialIOS.IsAvailable(placementId);
                #endif
            #else
                return false;
            #endif
        }

        /// <summary>
        /// Returns revenue and demand source information associated to the ad, if one is available.
        /// </summary>
        /// <return> An instance of <see cref="ImpressionData"/> if an ad is available, otherwise <c>null</c>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        /// <seealso cref="ImpressionData" />
        public static ImpressionData GetImpressionData(string placementId)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    return InterstitialAndroid.GetImpressionData(placementId);
                #elif UNITY_IPHONE
                    return InterstitialIOS.GetImpressionData(placementId);
                #endif
            #else
                return null;
            #endif
        }

        /// <summary>
        /// Registers an ad callback to notify about every lifecycle events of an interstitial ad.
        /// </summary>
        /// <param name="listener">The listener which implements <see cref="InterstitialListener" />.</param>
        /// <seealso cref="InterstitialListener" />
        public static void SetInterstitialListener(InterstitialListener listener)
        {
            Interstitial.interstitialListener = listener;
        }

        /// <summary>
        /// Enables the auto–requesting behaviour for a given placement.
        /// </summary>
        /// <param name="placementId">The placement id for which the auto–requesting should be enabled.</param>
        /// <seealso cref="FairBid.DisableAutoRequesting" />
        /// <seealso cref="Interstitial.DisableAutoRequesting" />
        public static void EnableAutoRequesting(string placementId)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    InterstitialAndroid.EnableAutoRequesting(placementId);
                #elif UNITY_IPHONE
                    InterstitialIOS.EnableAutoRequesting(placementId);
                #endif
            #else
                UnityEngine.Debug.LogWarning("Call received to enable auto-requesting an Interstitial, but the SDK does not function in the editor. You must use a device/emulator.");
            #endif
        }

        /// <summary>
        /// Disables auto–requesting for the given placement.
        /// </summary>
        /// <param name="placementId">The placement id for which the auto–requesting should be disabled.</param>
        /// <seealso cref="FairBid.DisableAutoRequesting" />
        /// <seealso cref="Interstitial.EnableAutoRequesting" />
        public static void DisableAutoRequesting(string placementId)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    InterstitialAndroid.DisableAutoRequesting(placementId);
                #elif UNITY_IPHONE
                    InterstitialIOS.DisableAutoRequesting(placementId);
                #endif
            #else
                UnityEngine.Debug.LogWarning("Call received to disable auto-requesting an Interstitial, but the SDK does not function in the editor. You must use a device/emulator.");
            #endif
        }

        /// <summary>
        /// The amount of Interstitial impressions for this session
        /// </summary>
        /// <returns>the amount of impressions</returns>
        public static int GetImpressionDepth()
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    return InterstitialAndroid.GetImpressionDepth();
                #elif UNITY_IPHONE
                    return InterstitialIOS.GetImpressionDepth();
                #endif
            #else
                return 0;
            #endif
        }

        /// <summary>
        /// Summary of what +notifyLoss:reason: does
        /// <param name="placementId">The placement id for notification loss.</param>
        /// <param name="reason">LossNotificationReason reason type <see cref="LossNotificationReason" />.</param>
        /// </summary>
        public static void NotifyLoss(string placementId, LossNotificationReason reason)
        {
            if (System.Enum.IsDefined(typeof(LossNotificationReason), reason))
            {
                #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                    #if UNITY_ANDROID
                        InterstitialAndroid.NotifyLoss(placementId, reason);
                    #elif UNITY_IPHONE
                        InterstitialIOS.NotifyLoss(placementId, reason);
                    #endif
                #else
                    UnityEngine.Debug.LogWarning("Call received to notify loss on Interstitial, but the SDK does not function in the editor. You must use a device/emulator.");
                #endif
            }
        }

        #region Internal methods
        internal static void InitReceiver()
        {
            if (_instance == null)
            {
                GameObject receiverObject = new GameObject("FairBidInterstitial");
                DontDestroyOnLoad(receiverObject);
                _instance = receiverObject.AddComponent<Interstitial>();
            }
        }

        public void InvokeCallback(string message)
        {
            CallbackInfo callbackInfo = CallbackInfo.FromJson(message);
            Interstitial.InvokeCallback(callbackInfo);
        }

        private static void InvokeCallback(CallbackInfo callbackInfo)
        {
            if (Interstitial.interstitialListener != null)
            {
                string placementId = callbackInfo.placement_id;
                switch (callbackInfo.callback)
                {
                    case CallbackInfo.CallbackShow:
                        Interstitial.interstitialListener.OnShow(placementId, callbackInfo.impressionData);
                        break;
                    case CallbackInfo.CallbackFailed:
                        Interstitial.interstitialListener.OnShowFailure(placementId, callbackInfo.impressionData);
                        break;
                    case CallbackInfo.CallbackAvailable:
                        Interstitial.interstitialListener.OnAvailable(placementId);
                        break;
                    case CallbackInfo.CallbackUnavailable:
                        Interstitial.interstitialListener.OnUnavailable(placementId);
                        break;
                    case CallbackInfo.CallbackClick:
                        Interstitial.interstitialListener.OnClick(placementId);
                        break;
                    case CallbackInfo.CallbackHide:
                        Interstitial.interstitialListener.OnHide(placementId);
                        break;
                    case CallbackInfo.CallbackRequestStart:
                        Interstitial.interstitialListener.OnRequestStart(placementId);
                        break;
                    default:
                        Console.WriteLine("Unknown callback for Interstitial");
                        break;
                }
            }
        }

        #endregion
    }

    #region Platform-specific translations
#if UNITY_IPHONE && !UNITY_EDITOR
  public class InterstitialIOS
  {
    [DllImport ("__Internal")]
    private static extern void fyb_sdk_show_interstitial(string placementId, string optionsString);
    [DllImport ("__Internal")]
    private static extern void fyb_sdk_fetch_interstitial(string placementId);
    [DllImport ("__Internal")]
    private static extern bool fyb_sdk_interstitial_is_available(string placementId);
    [DllImport ("__Internal")]
    private static extern string fyb_sdk_interstitial_get_impression_data(string placementId);
    [DllImport ("__Internal")]
    private static extern void fyb_sdk_interstitial_enable_auto_requesting(string placementId);
    [DllImport ("__Internal")]
    private static extern void fyb_sdk_interstitial_disable_auto_requesting(string placementId);
    [DllImport ("__Internal")]
    private static extern int fyb_sdk_impression_depth_interstitial();
    [DllImport ("__Internal")]
    private static extern void fyb_sdk_interstitial_notifyLoss(string placementId, string reason);

    public static void Show(string placementId, string optionsString)
    {
        fyb_sdk_show_interstitial(placementId, optionsString);
    }

    public static void Fetch(string placementId)
    {
        fyb_sdk_fetch_interstitial(placementId);
    }

    public static bool IsAvailable(string placementId)
    {
        return fyb_sdk_interstitial_is_available(placementId);
    }

    public static ImpressionData GetImpressionData(string placementId)
    {
        string impressionDataString = fyb_sdk_interstitial_get_impression_data(placementId);
        if (impressionDataString != null) {
          return new ImpressionData(impressionDataString);
        }
        return null;
    }

    public static void EnableAutoRequesting(string placementId)
    {
        fyb_sdk_interstitial_enable_auto_requesting(placementId);
    }

    public static void DisableAutoRequesting(string placementId)
    {
        fyb_sdk_interstitial_disable_auto_requesting(placementId);
    }

    public static int GetImpressionDepth()
    {
        return fyb_sdk_impression_depth_interstitial();
    }

    public static void NotifyLoss(string placementId, LossNotificationReason reason)
    {
        fyb_sdk_interstitial_notifyLoss(placementId, reason.ToString());
    }
  }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
  public class InterstitialAndroid
  {

    public static void Show(string placementId, string optionsString)
    {
      if(Application.platform != RuntimePlatform.Android) return;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
      {
        jc.CallStatic("showInterstitial", placementId, optionsString);
      }
    }


    public static void Fetch(string placementId)
    {
      if(Application.platform != RuntimePlatform.Android) return;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.ads.Interstitial"))
      {
        jc.CallStatic("request", placementId);
      }
    }

    public static void EnableAutoRequesting(string placementId)
    {
      if(Application.platform != RuntimePlatform.Android) return;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.ads.Interstitial"))
      {
        jc.CallStatic("enableAutoRequesting", placementId);
      }
    }

    public static void DisableAutoRequesting(string placementId)
    {
      if(Application.platform != RuntimePlatform.Android) return;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.ads.Interstitial"))
      {
        jc.CallStatic("disableAutoRequesting", placementId);
      }
    }

    public static Boolean IsAvailable(string placementId)
    {
      if(Application.platform != RuntimePlatform.Android) return false;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.ads.Interstitial"))
      {
        return jc.CallStatic<Boolean>("isAvailable", placementId);
      }
    }

    public static ImpressionData GetImpressionData(string placementId)
    {
      if(Application.platform != RuntimePlatform.Android) return null;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
      {
        string impressionDataString = jc.CallStatic<string>("getImpressionData", "interstitial", placementId);
        if (impressionDataString != null) {
          return new ImpressionData(impressionDataString);
        }
        return null;
      }
    }

    public static int GetImpressionDepth()
    {
      if(Application.platform != RuntimePlatform.Android) return 0;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.ads.Interstitial"))
      {
        return jc.CallStatic<int>("getImpressionDepth");
      }
    }

    public static void NotifyLoss(string placementId, LossNotificationReason reason)
    {
      if(Application.platform != RuntimePlatform.Android) return;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
      {
        jc.CallStatic("notifyLoss", "INTERSTITIAL", placementId, reason.ToString());
      }
    }
  }
#endif
    #endregion
}
