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
    /// Rewarded is used to show a fullscreen rewarded ad where a user must complete some task (e.g.
    /// watch a video until the end, perform some action, ...) to get a reward.
    /// </summary>
    public class Rewarded : MonoBehaviour
    {
        private static RewardedListener rewardedListener;
        private static Rewarded _instance;

        /// <summary>
        /// Requests an ad from the ad server.
        /// </summary>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        /// <seealso cref="RewardedListener.OnAvailable(string)" />
        /// <seealso cref="RewardedListener.OnUnavailable(string)" />
        public static void Request(string placementId)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    RewardedAndroid.Fetch(placementId);
                #elif UNITY_IPHONE
                    RewardedIOS.Fetch(placementId);
                #endif
            #else
                UnityEngine.Debug.LogWarning("Call received to fetch an Rewarded, but the SDK does not function in the editor. You must use a device/emulator to fetch/show ads.");
                CallbackInfo callback = CallbackInfo.ForPlacement(CallbackInfo.CallbackUnavailable, placementId);
                _instance.StartCoroutine(InvokeCallbackNextFrame(callback));
            #endif
        }

        /// <summary>
        /// Displays an ad. If the placement has not been requested before, no ad will be shown
        /// </summary>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        /// <seealso cref="RewardedListener.OnShow(string)" />
        /// <seealso cref="RewardedListener.OnShowFailure(string)" />
        /// <seealso cref="RewardedListener.OnCompletion(string, bool)" />
        public static void Show(string placementId)
        {
            Show(placementId, null);
        }

        /// <summary>
        /// Displays an ad. If the placement has not been requested before, no ad will be shown
        /// </summary>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        /// <param name="options">The options for rewarded ad.</param>
        /// <seealso cref="RewardedListener.OnShow(string)" />
        /// <seealso cref="RewardedListener.OnShowFailure(string)" />
        /// <seealso cref="RewardedListener.OnCompletion(string, bool)" />
        public static void Show(string placementId, RewardedOptions options)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)

                string optionsString = null;
                if (options != null && options.CustomParameters != null)
                {
                    optionsString = Utils.DictToJson(options.CustomParameters);
                }

                #if UNITY_ANDROID
                    RewardedAndroid.Show(placementId, optionsString);
                #elif UNITY_IPHONE
                    RewardedIOS.Show(placementId, optionsString);
                #endif
            #else
                UnityEngine.Debug.LogWarning("Call received to show an Rewarded, but the SDK does not function in the editor. You must use a device/emulator to fetch/show ads.");
                CallbackInfo callback = CallbackInfo.ForPlacement(CallbackInfo.CallbackFailed, placementId);
                _instance.StartCoroutine(InvokeCallbackNextFrame(callback));
            #endif
        }

        /// <summary>
        /// Checks if an ad is immediately available to show.
        /// </summary>
        /// <returns><c>true</c> if the ad is completely loaded and ready to be shown.</returns>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        /// <seealso cref="RewardedListener.OnAvailable(string)" />
        /// <seealso cref="RewardedListener.OnUnavailable(string)" />
        public static bool IsAvailable(string placementId)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    return RewardedAndroid.IsAvailable(placementId);
                #elif UNITY_IPHONE
                    return RewardedIOS.IsAvailable(placementId);
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
                  return RewardedAndroid.GetImpressionData(placementId);
              #elif UNITY_IPHONE
                  return RewardedIOS.GetImpressionData(placementId);
              #endif
          #else
              return null;
          #endif
        }

        /// <summary>
        /// Registers an ad callback to notify about every lifecycle events of a rewarded ad.
        /// </summary>
        /// <param name="listener">The listener which implements <see cref="RewardedListener" />.</param>
        /// <seealso cref="RewardedListener" />
        public static void SetRewardedListener(RewardedListener listener)
        {
            Rewarded.rewardedListener = listener;
        }

        /// <summary>
        /// Enables the auto–requesting behaviour for a given placement.
        /// </summary>
        /// <param name="placementId">The placement id for which the auto–requesting should be enabled.</param>
        /// <seealso cref="FairBid.DisableAutoRequesting" />
        /// <seealso cref="Rewarded.DisableAutoRequesting" />
        public static void EnableAutoRequesting(string placementId)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    RewardedAndroid.EnableAutoRequesting(placementId);
                #elif UNITY_IPHONE
                    RewardedIOS.EnableAutoRequesting(placementId);
                #endif
            #else
                UnityEngine.Debug.LogWarning("Call received to enable auto-requesting an Rewarded, but the SDK does not function in the editor. You must use a device/emulator.");
            #endif
        }

        /// <summary>
        /// Disables auto–requesting for the given placement.
        /// </summary>
        /// <param name="placementId">The placement id for which the auto–requesting should be disabled.</param>
        /// <seealso cref="FairBid.DisableAutoRequesting" />
        /// <seealso cref="Rewarded.EnableAutoRequesting" />
        public static void DisableAutoRequesting(string placementId)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    RewardedAndroid.DisableAutoRequesting(placementId);
                #elif UNITY_IPHONE
                    RewardedIOS.DisableAutoRequesting(placementId);
                #endif
            #else
                UnityEngine.Debug.LogWarning("Call received to disable auto-requesting an Rewarded, but the SDK does not function in the editor. You must use a device/emulator.");
            #endif
        }

        /// <summary>
        /// The amount of Rewarded impressions for this session
        /// </summary>
        /// <returns>the amount of impressions</returns>
        public static int GetImpressionDepth()
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    return RewardedAndroid.GetImpressionDepth();
                #elif UNITY_IPHONE
                    return RewardedIOS.GetImpressionDepth();
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
                        RewardedAndroid.NotifyLoss(placementId, reason);
                    #elif UNITY_IPHONE
                        RewardedIOS.NotifyLoss(placementId, reason);
                    #endif
                #else
                    UnityEngine.Debug.LogWarning("Call received to notify loss on Rewarded, but the SDK does not function in the editor. You must use a device/emulator.");
                #endif
            }
        }

        #region Internal methods
        public static void InitReceiver()
        {
            if (_instance == null)
            {
                GameObject receiverObject = new GameObject("FairBidRewarded");
                DontDestroyOnLoad(receiverObject);
                _instance = receiverObject.AddComponent<Rewarded>();
            }
        }

        public void InvokeCallback(string message)
        {
            CallbackInfo callbackInfo = CallbackInfo.FromJson(message);
            Rewarded.InvokeCallback(callbackInfo);
        }

        private static void InvokeCallback(CallbackInfo callbackInfo)
        {
            if (Rewarded.rewardedListener != null)
            {
                string placementId = callbackInfo.placement_id;
                switch (callbackInfo.callback)
                {
                    case CallbackInfo.CallbackShow:
                        Rewarded.rewardedListener.OnShow(placementId, callbackInfo.impressionData);
                        break;
                    case CallbackInfo.CallbackFailed:
                        Rewarded.rewardedListener.OnShowFailure(placementId, callbackInfo.impressionData);
                        break;
                    case CallbackInfo.CallbackAvailable:
                        Rewarded.rewardedListener.OnAvailable(placementId);
                        break;
                    case CallbackInfo.CallbackUnavailable:
                        Rewarded.rewardedListener.OnUnavailable(placementId);
                        break;
                    case CallbackInfo.CallbackClick:
                        Rewarded.rewardedListener.OnClick(placementId);
                        break;
                    case CallbackInfo.CallbackHide:
                        Rewarded.rewardedListener.OnHide(placementId);
                        break;
                    case CallbackInfo.CallbackRewardedComplete:
                        Rewarded.rewardedListener.OnCompletion(placementId, true);
                        break;
                    case CallbackInfo.CallbackRewardedNotComplete:
                        Rewarded.rewardedListener.OnCompletion(placementId, false);
                        break;
                    case CallbackInfo.CallbackRequestStart:
                        Rewarded.rewardedListener.OnRequestStart(placementId);
                        break;
                    default:
                        Console.WriteLine("Unknown callback for Rewarded");
                        break;
                }
            }
        }

        private static IEnumerator InvokeCallbackNextFrame(CallbackInfo callbackInfo)
        {
            yield return null; // wait a frame
            Rewarded.InvokeCallback(callbackInfo);
        }

        #endregion
    }

    public class RewardedOptions : ShowOptions
    {

        public RewardedOptions() : base() {}

        /// <summary>
        /// Initializes a RewardedOptions object with a Dictionary of custom params to be passed to Server Side Rewarding upon video completion.
        /// You need to pass this object to the Show API every time you want to pass these parameters to Server Side Rewarding.
        /// </summary>
        /// <param name="customParameters"> A Dictionary<string, string> of parameters to be passed to Server Side Rewarding upon a video completion.
        ///                             Length of keys and values of custom parameters combined MUST NOT exceed 4096 characters. If the limit is exceeded,
        ///                             a null value will be passed to Server Side Rewarding upon video completion.</param>
        public RewardedOptions(Dictionary<string, string> customParameters) : base(customParameters) {}

    }

    #region Platform-specific translations
#if UNITY_IPHONE && !UNITY_EDITOR
  public class RewardedIOS : MonoBehaviour
  {
    [DllImport ("__Internal")]
    private static extern void fyb_sdk_show_rewarded(string placementId, string optionsString);
    [DllImport ("__Internal")]
    private static extern void fyb_sdk_fetch_rewarded(string placementId);
    [DllImport ("__Internal")]
    private static extern bool fyb_sdk_rewarded_is_available(string placementId);
    [DllImport ("__Internal")]
    private static extern string fyb_sdk_rewarded_get_impression_data(string placementId);
    [DllImport ("__Internal")]
    private static extern void fyb_sdk_rewarded_enable_auto_requesting(string placementId);
    [DllImport ("__Internal")]
    private static extern void fyb_sdk_rewarded_disable_auto_requesting(string placementId);
    [DllImport ("__Internal")]
    private static extern int fyb_sdk_impression_depth_rewarded();
    [DllImport ("__Internal")]
    private static extern void fyb_sdk_rewarded_notifyLoss(string placementId, string reason);

    public static void Show(string placementId, string optionsString)
    {
      fyb_sdk_show_rewarded(placementId, optionsString);
    }

    public static void Fetch(string placementId)
    {
      fyb_sdk_fetch_rewarded(placementId);
    }

    public static bool IsAvailable(string placementId)
    {
      return fyb_sdk_rewarded_is_available(placementId);
    }

    public static ImpressionData GetImpressionData(string placementId)
    {
        string impressionDataString = fyb_sdk_rewarded_get_impression_data(placementId);
        if (impressionDataString != null) {
          return new ImpressionData(impressionDataString);
        }
        return null;
    }

    public static void EnableAutoRequesting(string placementId)
    {
        fyb_sdk_rewarded_enable_auto_requesting(placementId);
    }

    public static void DisableAutoRequesting(string placementId)
    {
        fyb_sdk_rewarded_disable_auto_requesting(placementId);
    }

    public static int GetImpressionDepth()
    {
        return fyb_sdk_impression_depth_rewarded();
    }

    public static void NotifyLoss(string placementId, LossNotificationReason reason)
    {
        fyb_sdk_rewarded_notifyLoss(placementId, reason.ToString());
    }
  }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
  public class RewardedAndroid : MonoBehaviour
  {

    public static void Show(string placementId, string optionsString)
    {
      if(Application.platform != RuntimePlatform.Android) return;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
      {
        jc.CallStatic("showRewarded", placementId, optionsString);
      }
    }

    public static void Fetch(string placementId)
    {
      if(Application.platform != RuntimePlatform.Android) return;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.ads.Rewarded"))
      {
        jc.CallStatic("request", placementId);
      }
    }

    public static void EnableAutoRequesting(string placementId)
    {
      if(Application.platform != RuntimePlatform.Android) return;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.ads.Rewarded"))
      {
        jc.CallStatic("enableAutoRequesting", placementId);
      }
    }

    public static void DisableAutoRequesting(string placementId)
    {
      if(Application.platform != RuntimePlatform.Android) return;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.ads.Rewarded"))
      {
        jc.CallStatic("disableAutoRequesting", placementId);
      }
    }

    public static Boolean IsAvailable(string placementId)
    {
      if(Application.platform != RuntimePlatform.Android) return false;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.ads.Rewarded"))
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
        string impressionDataString = jc.CallStatic<string>("getImpressionData", "rewarded", placementId);
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
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.ads.Rewarded"))
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
        jc.CallStatic("notifyLoss", "REWARDED", placementId, reason.ToString());
      }
    }

  }
#endif
    #endregion
}
