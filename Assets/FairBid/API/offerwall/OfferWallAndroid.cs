//
// FairBid Unity SDK
//
// Copyright (c) 2023 Digital Turbine. All rights reserved.
//
#nullable enable

using System;
using UnityEngine;

namespace DigitalTurbine.OfferWall
{
    internal class OfferWallAndroid : NativeOfferWallBridge
    {
        private const string OfferWallUnityHelper = "com.fyber.fairbid.sdk.extensions.unity3d.OfferWallUnityHelper";
        
        public override void SetPluginParams(string pluginVersion, string frameworkVersion)
        {
            if (Application.platform != RuntimePlatform.Android) return;
            AndroidJNIHelper.debug = false;

            using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
            javaClass.CallStatic("setPluginParams", pluginVersion, frameworkVersion);
        }

        public override void StartSDK(string appId, string? token = null, bool disableAdvertisingId = false)
        {
            if (Application.platform != RuntimePlatform.Android) return;
            AndroidJNIHelper.debug = false;

            using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
            javaClass.CallStatic("start", appId, token, disableAdvertisingId);
        }

        public override void Show(OfferWallShowOptions offerWallShowOptions, string? placementId)
        {
            if (Application.platform != RuntimePlatform.Android) return;
            AndroidJNIHelper.debug = false;

            using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
            javaClass.CallStatic("show", offerWallShowOptions.ToJsonString(), placementId);
        }

        public override void RequestVirtualCurrency(VirtualCurrencyRequestOptions vcOptions)
        {
            if (Application.platform != RuntimePlatform.Android) return;
            AndroidJNIHelper.debug = false;

            using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
            javaClass.CallStatic("requestVirtualCurrency", vcOptions.ToastOnReward, vcOptions.CurrencyId);
        }

        public override string?GetUserId()
        {
            if (Application.platform != RuntimePlatform.Android) return null;
            AndroidJNIHelper.debug = false;

            using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
            return javaClass.CallStatic<string>("getUserId");
        }

        public override void SetUserId(string? userId)
        {
            if (Application.platform != RuntimePlatform.Android) return;
            AndroidJNIHelper.debug = false;

            using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
            javaClass.CallStatic("setUserId", userId);
        }

        public override void SetConsent(OfferWallPrivacyConsent consent)
        {
            if (Application.platform != RuntimePlatform.Android) return;
            AndroidJNIHelper.debug = false;

            using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
            switch (consent)
            {
                case OfferWallPrivacyConsent.Gdpr gdpr:
                    javaClass.CallStatic("setGdprConsent", gdpr.ConsentGiven);
                    break;
                case OfferWallPrivacyConsent.Ccpa ccpa:
                    javaClass.CallStatic("setCcpaConsent", ccpa.PrivacyString);
                    break;
            }
        }

        public override void RemoveConsent(OfferWallPrivacyStandard privacyStandard)
        {
            var clearConsentMethodName = privacyStandard switch
            {
                OfferWallPrivacyStandard.Ccpa => "clearCcpaConsent",
                OfferWallPrivacyStandard.Gdpr => "clearGdprConsent",
                _ => throw new ArgumentOutOfRangeException(nameof(privacyStandard), privacyStandard, null)
            };

            if (Application.platform != RuntimePlatform.Android) return;
            AndroidJNIHelper.debug = false;

            using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
            javaClass.CallStatic(clearConsentMethodName);
        }

        public override void SetLogLevel(LogLevel level)
        {
            if (Application.platform != RuntimePlatform.Android) return;
            AndroidJNIHelper.debug = false;

            using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
            javaClass.CallStatic("setLogLevel", level.ToString());
        }

        public override void Log(string message)
        {
            if (Application.platform != RuntimePlatform.Android) return;
            AndroidJNIHelper.debug = false;

            using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
            javaClass.CallStatic("log", message);
        }
    }
}