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
using UnityEngine.Diagnostics;

namespace Fyber
{
    /// <summary>
    /// FairBid wrapper for iOS and Android via Unity.
    /// Singleton that holds the configurations for the Fyber FairBid SDK.
    /// Provides convenience methods to configure and start the SDK.
    /// <para />
    /// Calling the <see cref="FairBid.Start(string)" /> method will start the FairBid SDK making all Fyber products available.
    /// <para />
    /// The method <see cref="FairBid.ConfigureForAppId(string)" /> provides a singleton instance of this class from which the default behaviour can be changed.
    /// <para />
    /// After starting the SDK, all parameters used to configure the FairBid SDK will remain immutable.
    /// <para />
    /// For more information, <see href="https://developers.fyber.com/docs/unity_sdk_setup_and_requirements">the official documentation</see>.
    /// </summary>
    public class FairBid : MonoBehaviour
    {
        public const string Version = "3.59.0";

        private const string CompatibleAndroidVersion = "3.59.0";
        private const string CompatibleIOSVersion = "3.59.0";

        private static FairBidListener fairBidListener;
        private static FairBid _instance;
        private bool initialized { get; set; }
        private int flags { get; set; }
        private string appId { get; set; }

        /// <summary>
        /// Start FairBid SDK, used in conjunction with <see cref="FairBid.ConfigureForAppId(string)" />.
        /// This needs to be called as early as possible in the application lifecycle.
        /// </summary>
        public void Start()
        {
            if (!initialized)
            {
                #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
                    #if UNITY_ANDROID
                        FairBidAndroid.Start(appId, flags);
                    #elif UNITY_IOS
                        FairBidIOS.SetPluginParams(Version, Application.unityVersion);
                        FairBidIOS.Start(appId, (flags & FLAG_DISABLE_AUTOMATIC_REQUESTING) == 0);
                    #endif
                #else
                    UnityEngine.Debug.LogError("Call received to start the FairBid SDK, but the SDK does not function in the editor. You must use a device/emulator to receive/test ads.");
                #endif

                FairBid.InitReceiver();
                Interstitial.InitReceiver();
                Rewarded.InitReceiver();
                Banner.InitReceiver();
                UserInfo.InitReceiver();
                Settings.InitReceiver();
                initialized = true;
            }
        }

        /// <summary>
        /// Disable the FairBid auto–requesting behaviour.
        /// This should be used in conjunction with <see cref="FairBid.ConfigureForAppId(string)" />.
        /// </summary>
        public FairBid DisableAutoRequesting()
        {
            if (!initialized)
            {
                flags |= FLAG_DISABLE_AUTOMATIC_REQUESTING;
            }

            return this;
        }

        /// <summary>
        /// This is Android-specific only API.
        /// Disable the usage of the advertising ID. It should be used when the app requires compliance with the Google Family Program.
        /// See https://support.google.com/googleplay/android-developer/answer/9893335 for more details (accessed 20.04.2022)
        /// This should be used in conjunction with <see cref="FairBid.ConfigureForAppId(string)" />.
        /// </summary>
        public FairBid DisableAdvertisingId()
        {
            if (!initialized)
            {
                flags |= FLAG_DISABLE_ADVERTISING_ID;
            }

            return this;
        }

        /// <summary>
        /// Mark the current user of the app a child in a given session. This should be used when the app requires compliance with COPPA.
        /// Setting this value to <b>true</b> results in notifying the integrated Advertising SDKs of the need to work with COPPA restrictions applied.
        /// This should be used in conjunction with <see cref="FairBid.ConfigureForAppId(string)" />.
        /// </summary>(
        public FairBid SetUserAChild(bool isChild)
        {
            if (!initialized)
            {
                #if UNITY_ANDROID
                if (isChild)
                {
                    flags |= FLAG_SET_USER_A_CHILD;
                }
                else
                {
                    flags &= ~FLAG_SET_USER_A_CHILD;
                }
                #elif !UNITY_EDITOR && UNITY_IOS
                    FairBidIOS.SetIsChildEnabled(isChild);
                #endif
            }

            return this;
        }

        /// <summary>
        /// Enables the FairBid internal logging mechanism, useful for debugging purposes while developing your app.
        /// This should be used in conjunction with <see cref="FairBid.ConfigureForAppId(string)" />.
        /// </summary>
        public FairBid EnableLogs()
        {
            if (!initialized)
            {
                #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
                #if UNITY_ANDROID
                FairBidAndroid.ShowDebugLogs();
                #elif UNITY_IOS
                        FairBidIOS.ShowDebugLogs();
                #endif
                #endif
            }

            return this;
        }

        /// <summary>
        /// Start FairBid SDK. This needs to be called as early as possible in the application lifecycle.
        /// </summary>
        /// <param name="appId">The publisher app id.</param>
        public static void Start(string appId)
        {
            ConfigureForAppId(appId).Start();
        }

        /// <summary>
        /// Creates and returns a Singleton instance of this class.
        /// <para />
        /// After starting the SDK, it will not be possible to change the appId.
        /// </summary>
        /// <returns>A singleton instance of this class.</returns>
        /// <param name="appId">The publisher app id.</param>
        public static FairBid ConfigureForAppId(string appId)
        {
            CheckNativeSDKCompatibility();

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
        /// Shows the Test Suite view presenting you with all the mediation configuration for this specific appId
        /// Note - FairBid SDK must be started first in order to show meaningful information
        /// </summary>
        public static void ShowTestSuite()
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
            #if UNITY_ANDROID
            FairBidAndroid.ShowMediationTestSuite();
            #elif UNITY_IOS
                    FairBidIOS.ShowMediationTestSuite();
            #endif
            #else
                UnityEngine.Debug.LogWarning("Call received to show the FairBid SDK test suite, but the SDK does not function in the editor. You must use a device/emulator to use the test suite.");
            #endif
        }


        /// <summary>
        /// Sets the <see cref="FairBidListener" /> that will be notified about Mediated network start status events
        /// </summary>
        public static void SetFairBidListener(FairBidListener listener)
        {
            FairBid.fairBidListener = listener;
        }

        #region Internal methods

        // private const int FLAG_NO_OPTIONS = 0 << 0; // 0
        private const int FLAG_DISABLE_AUTOMATIC_REQUESTING = 1 << 0; // 1
        private const int FLAG_DISABLE_ADVERTISING_ID = 1 << 1; // 2
        private const int FLAG_SET_USER_A_CHILD = 1 << 2; // 4

        internal static void InitReceiver()
        {
            if (_instance == null)
            {
                GameObject receiverObject = new GameObject("FairBid");
                DontDestroyOnLoad(receiverObject);
                _instance = receiverObject.AddComponent<FairBid>();
            }
        }

        private static void CheckNativeSDKCompatibility()
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
            #if UNITY_ANDROID
            if (FairBidAndroid.SDKVersion() != CompatibleAndroidVersion)
            {
                UnityEngine.Debug.LogError(String.Format(
                    "Invalid version of FairBid SDK for {0} integrated. Expected {1} found {2}", "Android",
                    CompatibleAndroidVersion, FairBidAndroid.SDKVersion()));
                UnityEngine.Diagnostics.Utils.ForceCrash(UnityEngine.Diagnostics.ForcedCrashCategory.FatalError);
            }
            #elif UNITY_IOS
                    if (FairBidIOS.SDKVersion() != CompatibleIOSVersion) {
                        UnityEngine.Debug.LogError(String.Format("Invalid version of FairBid SDK for {0} integrated. Expected {1} found {2}", "iOS", CompatibleIOSVersion, FairBidIOS.SDKVersion()));
                        UnityEngine.Diagnostics.Utils.ForceCrash(UnityEngine.Diagnostics.ForcedCrashCategory.FatalError);
                    }
            #endif
            #endif
        }

        public void InvokeCallback(string message)
        {
            CallbackInfo callbackInfo = CallbackInfo.FromJson(message);
            FairBid.InvokeCallback(callbackInfo);
        }

        private static void InvokeCallback(CallbackInfo callbackInfo)
        {
            if (FairBid.fairBidListener == null)
            {
                return;
            }

            switch (callbackInfo.callback)
            {
                case CallbackInfo.CallbackMediationStarted:
                    FairBid.fairBidListener.OnMediationStarted();
                    return;
                case CallbackInfo.CallbackMediationFailedToStart:
                    FairBid.fairBidListener.OnMediationFailedToStart(callbackInfo.error, callbackInfo.code);
                    return;
            }

            string network = callbackInfo.network;
            if (network.Length == 0)
            {
                return;
            }

            switch (callbackInfo.callback)
            {
                case CallbackInfo.CallbackNetworkStarted:
                    FairBid.fairBidListener.OnNetworkStarted(MediatedNetworkFromString(network));
                    break;
                case CallbackInfo.CallbackNetworkFailedToStart:
                    FairBid.fairBidListener.OnNetworkFailedToStart(MediatedNetworkFromString(network),
                        callbackInfo.error);
                    break;
            }
        }

        internal static MediatedNetwork MediatedNetworkFromString(string network)
        {
            switch (network)
            {
                case "ADMOB":
                    return MediatedNetwork.ADMOB;
                case "APPLOVIN":
                    return MediatedNetwork.APPLOVIN;
                case "BIDMACHINE":
                    return MediatedNetwork.BID_MACHINE;
                case "BIGO_ADS":
                    return MediatedNetwork.BIGO_ADS;
                case "CHARTBOOST":
                    return MediatedNetwork.CHARTBOOST;
                case "META_AUDIENCE_NETWORK":
                    return MediatedNetwork.META_AUDIENCE_NETWORK;
                case "GOOGLE_AD_MANAGER":
                    return MediatedNetwork.GAM;
                case "HYPRMX":
                    return MediatedNetwork.HYPRMX;
                case "INMOBI":
                    return MediatedNetwork.INMOBI;
                case "IRONSOURCE":
                    return MediatedNetwork.IRONSOURCE;
                case "MINTEGRAL":
                    return MediatedNetwork.MINTEGRAL;
                case "MYTARGET":
                    return MediatedNetwork.MYTARGET;
                case "OGURY":
                    return MediatedNetwork.OGURY;
                case "PANGLE":
                    return MediatedNetwork.PANGLE;
                case "UNITYADS":
                    return MediatedNetwork.UNITYADS;
                case "VERVE":
                    return MediatedNetwork.VERVE;
                case "LIFTOFF_MONETIZE":
                    return MediatedNetwork.LIFTOFF_MONETIZE;
            }

            return Fyber.MediatedNetwork.UNKNOWN;
        }

        #endregion
    }

    #region Platform-specific translations

    #if UNITY_IOS
    public class FairBidIOS : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void fyb_sdk_set_plugin_params(string pluginVersion, string frameworkVersion);
        
        [DllImport("__Internal")]
        private static extern void fyb_sdk_start_app(string publisher_id, bool autoRequestEnabled);

        [DllImport("__Internal")]
        private static extern void fyb_sdk_show_test_suite();

        [DllImport("__Internal")]
        private static extern void fyb_sdk_show_debug_logs();

        [DllImport("__Internal")]
        private static extern string fyb_sdk_version();

        [DllImport("__Internal")]
        private static extern void fyb_sdk_set_is_child(bool isChild);

        public static void SetPluginParams(string pluginVersion, string frameworkVersion)
        {
            fyb_sdk_set_plugin_params(pluginVersion, frameworkVersion);
        }

        public static void Start(string publisher_id, bool autoRequestEnabled = true)
        {
            fyb_sdk_start_app(publisher_id, autoRequestEnabled);
        }

        public static void SetIsChildEnabled(bool isChild = false)
        {
            fyb_sdk_set_is_child(isChild);
        }

        public static void ShowMediationTestSuite()
        {
            fyb_sdk_show_test_suite();
        }

        public static void ShowDebugLogs()
        {
            fyb_sdk_show_debug_logs();
        }

        public static String SDKVersion()
        {
            return fyb_sdk_version();
        }
    }
    #endif

    #if UNITY_ANDROID
    public class FairBidAndroid : MonoBehaviour
    {
        public static void Start(string appId, int options = 0)
        {
            if (Application.platform != RuntimePlatform.Android) return;

            string frameworkParams = "{\"plugin_sdk_version\":\"" + FairBid.Version +
                                     "\",\"plugin_framework_version\":\"" + Application.unityVersion + "\"}";
            AndroidJNIHelper.debug = false;
            using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
            {
                jc.CallStatic("start", appId, options, frameworkParams);
            }
        }

        public static void ShowMediationTestSuite()
        {
            if (Application.platform != RuntimePlatform.Android) return;

            AndroidJNIHelper.debug = false;
            using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
            {
                jc.CallStatic("showNetworkActivity");
            }
        }

        public static void ShowDebugLogs()
        {
            if (Application.platform != RuntimePlatform.Android) return;
            using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
            {
                jc.CallStatic("showDebugLogs");
            }
        }

        public static String SDKVersion()
        {
            if (Application.platform != RuntimePlatform.Android) return null;
            using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.FairBid"))
            {
                return jc.GetStatic<string>("SDK_VERSION");
            }
        }
    }
    #endif

    #endregion
}