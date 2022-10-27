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
    /// Class responsible for managing Banner ads.
    /// </summary>
    public class Banner : MonoBehaviour
    {
        private static BannerListener bannerListener;
        private static Banner _instance;

        /// <summary>
        /// Displays a banner identified by the placement name
        /// </summary>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        public static void Show(string placementId)
        {
            Banner.Show(placementId, null);
        }

        /// <summary>
        /// Displays a banner identified by the placement name
        /// </summary>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        /// <param name="showOptions">The <see cref="BannerOptions" /> to be used for this display call</param>
        public static void Show(string placementId, BannerOptions showOptions)
        {
            if (showOptions == null)
            {
                showOptions = new BannerOptions();
            }

            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    BannerAndroid.Show(placementId, showOptions);
                #elif UNITY_IPHONE
                    BannerIOS.Show(placementId, showOptions);
                #endif
            #else
                string message = "Call received to show an Banner, but the SDK does not function in the editor. You must use a device/emulator to fetch/show ads.";
                UnityEngine.Debug.LogWarning(message);
                
                _instance.StartCoroutine(InvokeCallbackNextFrame(CallbackInfo.ForError(placementId, message)));
            #endif
        }

        /// <summary>
        /// Destroys the banner identified by the placement name, if any
        /// </summary>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        public static void Destroy(string placementId)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    BannerAndroid.Destroy(placementId);
                #elif UNITY_IPHONE
                    BannerIOS.Destroy(placementId);
                #endif
            #else
            #endif
        }

        /// <summary>
        /// Sets the <see cref="BannerListener" /> that will be notified about Banner events
        /// </summary>
        public static void SetBannerListener(BannerListener listener)
        {
            Banner.bannerListener = listener;
        }

        /// <summary>
        /// The amount of Banner impressions for this session
        /// </summary>
        /// <returns>the amount of impressions</returns>
        public static int GetImpressionDepth()
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    return BannerAndroid.GetImpressionDepth();
                #elif UNITY_IPHONE
                    return BannerIOS.GetImpressionDepth();
                #endif
            #else
                return 0;
            #endif
        }

        #region Internal methods
        public static void InitReceiver()
        {
            if (_instance == null)
            {
                GameObject receiverObject = new GameObject("FairBidBanner");
                DontDestroyOnLoad(receiverObject);
                _instance = receiverObject.AddComponent<Banner>();
            }
        }

        public void InvokeCallback(string message)
        {
            CallbackInfo callbackInfo = CallbackInfo.FromJson(message);
            Banner.InvokeCallback(callbackInfo);
        }

        private static void InvokeCallback(CallbackInfo callbackInfo)
        {
            if (Banner.bannerListener != null)
            {
                string placementId = callbackInfo.placement_id;
                switch (callbackInfo.callback)
                {
                    case CallbackInfo.CallbackShow:
                        Banner.bannerListener.OnShow(placementId, callbackInfo.impressionData);
                        break;
                    case CallbackInfo.CallbackError:
                        Banner.bannerListener.OnError(placementId, callbackInfo.error);
                        break;
                    case CallbackInfo.CallbackClick:
                        Banner.bannerListener.OnClick(placementId);
                        break;
                    case CallbackInfo.CallbackLoad:
                        Banner.bannerListener.OnLoad(placementId);
                        break;
                    case CallbackInfo.CallbackRequestStart:
                        Banner.bannerListener.OnRequestStart(placementId);
                        break;
                    default:
                        Console.WriteLine("Unknown callback for Banner");
                        break;
                }
            }
        }

        private static IEnumerator InvokeCallbackNextFrame(CallbackInfo callbackInfo)
        {
            yield return null; // wait a frame
            Banner.InvokeCallback(callbackInfo);
        }
        #endregion
    }

    #region Platform-specific translations
#if UNITY_IPHONE && !UNITY_EDITOR
  public class BannerIOS : MonoBehaviour
  {
    [DllImport ("__Internal")]
    private static extern void fyb_sdk_show_banner(string position, string placementId);
    [DllImport ("__Internal")]
    private static extern bool fyb_sdk_destroy_banner(string placementId);
    [DllImport ("__Internal")]
    private static extern int fyb_sdk_impression_depth_banner();

    public static void Show(string placementId, BannerOptions showOptions)
    {
      fyb_sdk_show_banner(showOptions.position, placementId);
    }

    public static void Destroy(string placementId)
    {
      fyb_sdk_destroy_banner(placementId);
    }

    public static int GetImpressionDepth()
    {
        return fyb_sdk_impression_depth_banner();
    }
  }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
  public class BannerAndroid : MonoBehaviour
  {

    public static void Show(string placementId, BannerOptions showOptions)
    {
      if(Application.platform != RuntimePlatform.Android) return;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
      {
        jc.CallStatic("showBanner", placementId, showOptions.position);
      }
    }

    public static void Destroy(string placementId)
    {
      if(Application.platform != RuntimePlatform.Android) return;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.ads.Banner"))
      {
        jc.CallStatic("destroy", placementId);
      }
    }

    public static int GetImpressionDepth()
    {
      if(Application.platform != RuntimePlatform.Android) return 0;

      AndroidJNIHelper.debug = false;
      using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.ads.Banner"))
      {
        return jc.CallStatic<int>("getImpressionDepth");
      }
    }
  }
#endif
    #endregion
}
