//
// FairBid Unity SDK
//
// Copyright (c) 2023 Digital Turbine. All rights reserved.
//
#nullable enable

using System;
using System.Runtime.InteropServices;
using UnityEngine;

#if UNITY_IOS 

namespace DigitalTurbine.OfferWall
{

    internal class OfferWallIOS : NativeOfferWallBridge
    {
        [DllImport("__Internal")]
        private static extern void fyb_ofw_start(string app_id, string? security_token);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_show(string options, string? placementId);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_request_virtual_currency(bool toast_on_reward, string? currency_id);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_set_gdpr_consent(bool consent);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_clear_gdpr_consent();

        [DllImport("__Internal")]
        private static extern void fyb_ofw_set_ccpa_consent(string consent_string);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_clear_ccpa_consent();

        [DllImport("__Internal")]
        private static extern void fyb_ofw_set_log_level(string? log_level);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_set_user_id(string? user_id);

        [DllImport("__Internal")]
        private static extern string fyb_ofw_get_user_id();

        [DllImport("__Internal")]
        private static extern void fyb_ofw_set_plugin_params(string pluginVersion, string frameworkVersion);

        public override void SetPluginParams(string pluginVersion, string frameworkVersion)
        {
            fyb_ofw_set_plugin_params(pluginVersion, frameworkVersion);
        }

        public override void StartSDK(string appId, string? token = null, bool disableAdvertisingId = false)
        {
            fyb_ofw_start(appId, token);
        }

        public override void Show(OfferWallShowOptions offerWallShowOptions, string? placementId)
        {
            fyb_ofw_show(offerWallShowOptions.ToJsonString(), placementId);
        }

        public override void RequestVirtualCurrency(VirtualCurrencyRequestOptions vcOptions)
        {
            fyb_ofw_request_virtual_currency(vcOptions.ToastOnReward, vcOptions.CurrencyId);
        }

        public override string GetUserId()
        {
            return fyb_ofw_get_user_id();
        }

        public override void SetUserId(string? userId)
        {
            fyb_ofw_set_user_id(userId);
        }

        public override void SetConsent(OfferWallPrivacyConsent consent)
        {
            switch (consent)
            {
                case OfferWallPrivacyConsent.Gdpr gdpr:
                    fyb_ofw_set_gdpr_consent(gdpr.ConsentGiven);
                    break;
                case OfferWallPrivacyConsent.Ccpa ccpa:
                    fyb_ofw_set_ccpa_consent(ccpa.PrivacyString);
                    break;
            }
        }

        public override void RemoveConsent(OfferWallPrivacyStandard privacyStandard)
        {
            switch (privacyStandard)
            {
                case OfferWallPrivacyStandard.Ccpa:
                    fyb_ofw_clear_ccpa_consent();
                    break;
                case OfferWallPrivacyStandard.Gdpr:
                    fyb_ofw_clear_gdpr_consent();
                    break;
            }
        }

        public override void SetLogLevel(LogLevel level)
        {
            fyb_ofw_set_log_level(level.ToString());
        }
    }
}
#endif