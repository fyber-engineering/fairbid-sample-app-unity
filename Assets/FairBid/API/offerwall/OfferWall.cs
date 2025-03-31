//
// FairBid Unity SDK
//
// Copyright (c) 2023 Digital Turbine. All rights reserved.
//
#nullable enable

using UnityEngine;
using Fyber;

namespace DigitalTurbine.OfferWall
{
    /** <summary>
     * Digital Turbine OfferWall SDK wrapper for iOS and Android via Unity.
     * This is the entry-point class to interact with the DT OfferWall SDK.
     * <ul>
     * <li>The SDK start is done via <see cref="OfferWall.StartSDK"/> method.</li>
     * <li>The OfferWall show is done via <see cref="OfferWall.Show"/> method.</li>
     * <li>Requesting the virtual currency might be achieved through the <see cref="OfferWall.RequestCurrency"/> method.</li>
     * </ul>
     *
     * For managing the privacy consents, see <see cref="SetConsent"/> and <see cref="RemoveConsent"/> methods.
     * <p/>
     * Additionally, you can set the debug logging through the <see cref="SetLogLevel"/> method.
     * <p/>
     * Please check the respective method and class documentation to get more details about their usage. 
     * </summary>
     */
    public class OfferWall : MonoBehaviour
    {
        static OfferWall()
        {
            #if UNITY_ANDROID
            Bridge = new OfferWallAndroid();
            #elif UNITY_IOS
            Bridge = new OfferWallIOS();
            #endif
            
            var messageReceiver = new GameObject("OfferWallMessageReceiver");
            DontDestroyOnLoad(messageReceiver);
            MessageReceiver = messageReceiver.AddComponent<OfferWallMessageReceiver>();
        }

        private static readonly NativeOfferWallBridge Bridge;
        private static readonly OfferWallMessageReceiver MessageReceiver;

        private static string? _userId;
        /**
         * The property to set and read the custom user ID value. If none is set, the OfferWall SDK will autogenerate one.
         */
        public static string? UserId
        {
            get => Bridge.GetUserId() ?? _userId;
            set
            {
                Bridge.SetUserId(value);
                _userId = value;
            }
        }

        // OfferWall events

        /// The event data associated with the OfferWall show event.
        ///
        /// <param name="placementId">the placement ID that this offerwall was displayed for. If <i>null</i>, then the default placement was used.</param>
        public delegate void OfferWallShownCallback(string? placementId);

        /// The event to be triggered when the OfferWall has been displayed.
        public static event OfferWallShownCallback? OfferWallShownEvent;

        /// The event data associated with the OfferWall show failure event.
        ///
        /// <param name="placementId">the placement ID that this offerwall was displayed for. If <i>null</i>, then the default placement was used.</param>
        /// <param name="error">the reason of the show failure.</param>
        public delegate void OfferWallFailedToShowCallback(string? placementId, OfferWallError error);

        /// The event to be triggered when the OfferWall failed to show.
        public static event OfferWallFailedToShowCallback? OfferWallFailedToShowEvent;
        
        /// The event data associated with the OfferWall closed event.
        ///
        /// <param name="placementId">the placement ID that this offerwall was displayed for. If <i>null</i>, then the default placement was used.</param>
        public delegate void OfferWallClosedCallback(string? placementId);

        /// The event to be triggered when the OfferWall was closed.
        public static event OfferWallClosedCallback? OfferWallClosedEvent;

        // Virtual Currency events
        
        /// The event data associated with the successful virtual currency requests handler.
        /// 
        /// <param name="response"> the details of the successful virtual currency request. For more details, see the <see cref="VirtualCurrencySuccessfulResponse"/> struct.</param>
        public delegate void VirtualCurrencyResponseHandler(VirtualCurrencySuccessfulResponse response);

        /// The handler event for the successful virtual currency requests.
        public static event VirtualCurrencyResponseHandler? VirtualCurrencyResponseEvent;

        /// The event data associated with the failed virtual currency requests handler.
        /// 
        /// <param name="response"> the details of the failed virtual currency request. For more details, see the <see cref="VirtualCurrencyErrorResponse"/> struct.</param>
        public delegate void VirtualCurrencyErrorHandler(VirtualCurrencyErrorResponse response);

        /// The handler event for the failed virtual currency requests.
        public static event VirtualCurrencyErrorHandler? VirtualCurrencyErrorEvent;

        /**
         * The method used to start the OfferWall SDK.
         *
         * <param name="appId"> the app ID that the session should be started against to.</param>
         * <param name="token"> the optional string value of security token required to perform the virtual currency requests. If <i>null</i> or not provided, requesting for virtual currency will not be possible.</param>
         * <param name="disableAdvertisingId"> the optional boolean parameter denoting whether the usage of Google Advertising ID should be restricted. If none provided, then <i>false</i> is used. Applicable to Android only.</param>
         */
        public static void StartSDK(string appId, string? token = null, bool disableAdvertisingId = false)
        {
            #if !UNITY_EDITOR
            Bridge.SetPluginParams(FairBid.Version, Application.unityVersion);
            Bridge.StartSDK(appId, token, disableAdvertisingId);
            #endif
        }
        
        /**
         * The method used for displaying the OfferWall.
         *
         * After calling this method, either <see cref="OfferWall.OfferWallShownEvent"/> or <see cref="OfferWall.OfferWallFailedToShowEvent"/> event will be called.
         *
         * In case the OfferWall is shown, after closing it the <see cref="OfferWall.OfferWallClosedEvent"/>
         *
         * <param name="offerWallShowOptions">the parameters that configure this specific offerwall display. For more details, see <see cref="OfferWallShowOptions"/> struct.</param>
         * <param name="placementId">the optional string value for placement ID that the offerwall will be displayed for. If none provided or <i>null</i>, then the default placement will be used.</param>
         */
        public static void Show(OfferWallShowOptions offerWallShowOptions = new OfferWallShowOptions(), string? placementId = null)
        {
            #if !UNITY_EDITOR
            Bridge.Show(offerWallShowOptions, placementId);
            #endif
        }

        /**
         * The method used for requesting virtual currency.
         *
         * After calling this method, either <see cref="VirtualCurrencyResponseEvent"/> or <see cref="VirtualCurrencyErrorEvent"/> event will be called.
         *
         * <param name="options">an optional parameter of the request options. If none provided, then the default values will be used. For more details, see <see cref="VirtualCurrencyRequestOptions"/>.</param>
         */
        public static void RequestCurrency(VirtualCurrencyRequestOptions options = new VirtualCurrencyRequestOptions())
        {
            #if !UNITY_EDITOR
            Bridge.RequestVirtualCurrency(options);
            #endif
        }

        /**
         * The method for setting the user privacy consent.
         * For providing a consent please instantiate one of the following classes and provide it as the parameter:
         * <ul>
         * <li><see cref="OfferWallPrivacyConsent.Gdpr"/></li>
         * <li><see cref="OfferWallPrivacyConsent.Ccpa"/></li>
         * </ul>
         *
         * <param name="consent">the privacy consent that was explicitly given by the user.</param>
         *
         * <seealso cref="RemoveConsent"/>
         */
        public static void SetConsent(OfferWallPrivacyConsent consent)
        {
            #if !UNITY_EDITOR
            Bridge.SetConsent(consent);
            #endif
        }

        /**
         * The method for removing the user privacy consent for a given standard.
         *
         * <param name="privacyStandard">the privacy standard for which the consent should be removed.</param>
         *
         * <seealso cref="SetConsent"/>
         */
        public static void RemoveConsent(OfferWallPrivacyStandard privacyStandard)
        {
            #if !UNITY_EDITOR
            Bridge.RemoveConsent(privacyStandard);
            #endif
        }

        /**
         * The method used to set specific log level in the native OfferWall SDK.
         *
         * <param name="level">the minimum allowed log level for the console messages.</param>
         */
        public static void SetLogLevel(LogLevel level)
        {
            #if !UNITY_EDITOR
            Bridge.SetLogLevel(level);
            #endif
        }
        
        /**
         * The method used to log messages through the native OfferWall SDK.  
         */
        public static void Log(string message)
        {
            #if !UNITY_EDITOR
            Bridge.Log(message);
            #endif
        }

        internal static void OnOfferWallShownEvent(string? placementId)
        {
            OfferWallShownEvent?.Invoke(placementId);
        }

        internal static void OnOfferWallFailedToShowEvent(string? placementId, OfferWallError error)
        {
            OfferWallFailedToShowEvent?.Invoke(placementId, error);
        }

        internal static void OnOfferWallClosedEvent(string? placementId)
        {
            OfferWallClosedEvent?.Invoke(placementId);
        }

        internal static void OnVirtualCurrencyResponseEvent(VirtualCurrencySuccessfulResponse response)
        {
            VirtualCurrencyResponseEvent?.Invoke(response);
        }

        internal static void OnVirtualCurrencyErrorEvent(VirtualCurrencyErrorResponse response)
        {
            VirtualCurrencyErrorEvent?.Invoke(response);
        }
    }
}