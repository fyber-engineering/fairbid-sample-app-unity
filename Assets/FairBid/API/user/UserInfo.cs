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
    /// Class defining the application user. Used for tracking user segments.
    /// </summary>
    public class UserInfo : MonoBehaviour
    {
        private static UserInfo _instance;

        private static bool _isChild = false;
        public static bool IsChild { get { return _isChild; } private set { _isChild = value; } }

        /// <summary>
        /// Sets the gender of the user, if known, using the provided enum.
        /// </summary>
        public static void SetGender(Gender gender)
        {
            if (System.Enum.IsDefined(typeof(Gender), gender))
            {
                #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                    #if UNITY_ANDROID
                        UserInfoAndroid.SetUserGender(gender.ToString());
                    #elif UNITY_IPHONE
                        UserInfoIOS.SetUserGender(gender.ToString());
                    #endif
                #else
                #endif
            }
        }

        /// <summary>
        /// Sets the location of the user, if known.
        /// The required parameters match the parameters provided by the UnityEngine `LocationInfo` struct.
        /// See <see href="https://docs.unity3d.com/ScriptReference/LocationInfo.html">the docs</see> for more information.
        /// </summary>
        public static void SetLocation(float latitude, float longitude, float horizontalAccuracy, float verticalAccuracy, float altitude, double timestamp)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    UserInfoAndroid.SetUserLocation(latitude, longitude, horizontalAccuracy, verticalAccuracy, altitude, timestamp);
                #elif UNITY_IPHONE
                    UserInfoIOS.SetUserLocation(latitude, longitude, horizontalAccuracy, verticalAccuracy, altitude, timestamp);
                #endif
            #else
            #endif
        }

        /// <summary>
        /// Sets the postal code (i.e.: the ZIP code in the US) of the user, if known.
        /// This is an alternative to setting the exact location but can provide similar benefits to ad revenues / targeting.
        /// </summary>
        public static void SetPostalCode(string postalCode)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    UserInfoAndroid.SetUserPostalCode(postalCode);
                #elif UNITY_IPHONE
                    UserInfoIOS.SetUserPostalCode(postalCode);
                #endif
            #else
            #endif
        }

        /// <summary>
        /// Sets the birth date.
        /// </summary>
        /// <param name="yyyyMMdd_date">The date.</param>
        public static void SetBirthDate(string yyyyMMdd_date)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    UserInfoAndroid.SetUserBirthDate(yyyyMMdd_date);
                #elif UNITY_IPHONE
                    UserInfoIOS.SetUserBirthDate(yyyyMMdd_date);
                #endif
            #else
            #endif
        }

        /// <summary>
        /// Sets User's consent under GDPR. FairBid SDK will only be able to show targeted advertising if the user consented.
        /// Only call this method if the user explicitly gave or denied consent.
        /// </summary>
        /// <param name="isGdprConsentGiven"><c>true</c> if user gave consent to receive targeted advertisement, <c>false</c> otherwise</param>
        public static void SetGdprConsent(Boolean isGdprConsentGiven)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    UserInfoAndroid.SetGdprConsent(isGdprConsentGiven);
                #elif UNITY_IPHONE
                    UserInfoIOS.SetGdprConsent(isGdprConsentGiven);
                #endif
            #else
                UnityEngine.Debug.LogWarning("Call received to set the GDPR consent, but the SDK does not function in the editor. You must use a device/emulator to set the GDPR consent.");
            #endif
        }

        /// <summary>
        /// Sets User's consent string under GDPR. FairBid SDK will use this information to provide optimal targeted advertising without infringing GDPR
        /// </summary>
        /// <param name="gdprConsentString">the GDPR consent string</param>
        static public void SetGdprConsentString(string gdprConsentString)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    UserInfoAndroid.SetGdprConsentString(gdprConsentString);
                #elif UNITY_IPHONE
                    UserInfoIOS.SetGdprConsentString(gdprConsentString);
                #endif
            #else
                UnityEngine.Debug.LogWarning("Call received to set the GDPR consent string, but the SDK does not function in the editor. You must use a device/emulator to set the GDPR consent string.");
            #endif
        }

        /// <summary>
        /// Deprecated in v3.14.0, use SetGdprConsentString()
        /// </summary>
        [Obsolete("Deprecated in v3.14.0, use SetGdprConsentString()")]
        static public void ClearGdprConsent()
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    UserInfoAndroid.ClearGdprConsent();
                #elif UNITY_IPHONE
                    UserInfoIOS.ClearGdprConsent();
                #endif
            #else
                UnityEngine.Debug.LogWarning("Call received to clear the GDPR consent, but the SDK does not function in the editor. You must use a device/emulator to set the GDPR consent.");
            #endif
        }

        /// <summary>
        /// Sets User's privacy string under CCPA compliance. FairBid SDK will use this information to provide optimal targeted advertising without infringing CCPA
        /// </summary>
        /// <param name="iabUsPrivacyString">the IAB US privacy string</param>
        public static void SetIabUsPrivacyString(string iabUsPrivacyString)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    UserInfoAndroid.SetIabUsPrivacyString(iabUsPrivacyString);
                #elif UNITY_IPHONE
                    UserInfoIOS.SetIabUsPrivacyString(iabUsPrivacyString);
                #endif
            #else
                UnityEngine.Debug.LogWarning("Call received to set the IAB US privacy string, but the SDK does not function in the editor. You must use a device/emulator to set the IAB US privacy string.");
            #endif
        }

        /// <summary>
        /// Deprecated in v3.16.0, use SetIabUsPrivacyString()
        /// </summary>
        [Obsolete("Deprecated in v3.16.0, use SetIabUsPrivacyString()")]
        public static void ClearIabUsPrivacyString()
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    UserInfoAndroid.ClearIabUsPrivacyString();
                #elif UNITY_IPHONE
                    UserInfoIOS.ClearIabUsPrivacyString();
                #endif
            #else
                UnityEngine.Debug.LogWarning("Call received to clear the IAB US privacy, but the SDK does not function in the editor. You must use a device/emulator to clear the IAB US privacy.");
            #endif
        }

        /// <summary>
        /// Sets User's consent under LGPD. FairBid SDK will only be able to show targeted advertising if the user consented.
        /// Only call this method if the user explicitly gave or denied consent.
        /// </summary>
        /// <param name="isLgpdConsentGiven"><c>true</c> if user gave consent to receive targeted advertisement, <c>false</c> otherwise</param>
        public static void SetLgpdConsent(Boolean isLgpdConsentGiven)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    UserInfoAndroid.SetLgpdConsent(isLgpdConsentGiven);
                #elif UNITY_IPHONE
                    UserInfoIOS.SetLgpdConsent(isLgpdConsentGiven);
                #endif
            #else
                UnityEngine.Debug.LogWarning("Call received to set the LGPD consent, but the SDK does not function in the editor. You must use a device/emulator to set the GDPR consent.");
            #endif
        }

        /// <summary>
        /// Sets User's ID to be used by the SDK in Server Side Rewarding upon video completion for the remainder of the session.
        /// </summary>
        /// <param name="userId">A string  representing the User’s ID. This ID is used to identify the user being rewarded in Server Side Rewarding every time a video is completed.
        ///                      If the total number of chars in this ID surpasses 256, a null value will be passed to Server Side Rewarding upon video completion.</param>
        static public void SetUserId(string userId)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    UserInfoAndroid.SetUserId(userId);
                #elif UNITY_IPHONE
                    UserInfoIOS.SetUserId(userId);
                #endif
            #else
            #endif
        }

        /// <summary>
        /// FairBid SDK will pass this flag to mediated network SDKs, to provide optimal targeted advertising
        /// </summary>
        /// <param name="isChild"> Set value to <c>true</c> if the user is a child subject to COPPA, and <c>false</c> otherwise</param>
        static public void SetIsChild(Boolean isChild)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                IsChild = isChild;

                #if UNITY_IPHONE
                    UserInfoIOS.SetIsChild(isChild);
                #endif
            #endif
        }

        #region Internal methods

        static internal void InitReceiver()
        {
            if (_instance == null)
            {
                GameObject receiverObject = new GameObject("FairBidUserInfo");
                DontDestroyOnLoad(receiverObject);
                _instance = receiverObject.AddComponent<UserInfo>();
            }
        }

        #endregion

    }

    #region Platform-specific translations
#if UNITY_IPHONE && !UNITY_EDITOR
    public class UserInfoIOS : MonoBehaviour {

        [DllImport ("__Internal")]
        private static extern void fyb_user_set_gender(string gender);
        public static void SetUserGender(string gender) {
            fyb_user_set_gender(gender);
        }

        [DllImport ("__Internal")]
        private static extern void fyb_user_set_location(float latitude, float longitude, float horizontalAccuracy, float verticalAccuracy, float altitude, double timestamp);
        public static void SetUserLocation(float latitude, float longitude, float horizontalAccuracy, float verticalAccuracy, float altitude, double timestamp) {
            fyb_user_set_location(latitude, longitude, horizontalAccuracy, verticalAccuracy, altitude, timestamp);
        }

        [DllImport ("__Internal")]
        private static extern void fyb_user_set_postal_code(string postalCode);
        public static void SetUserPostalCode(string postalCode) {
            fyb_user_set_postal_code(postalCode);
        }

        [DllImport ("__Internal")]
        private static extern void fyb_user_set_birth_date(string yyyyMMdd_date);
        public static void SetUserBirthDate(string yyyyMMdd_date) {
            fyb_user_set_birth_date(yyyyMMdd_date);
        }

        [DllImport ("__Internal")]
        private static extern void fyb_sdk_set_lgpd_consent(Boolean isLgpdConsentGiven);
        public static void SetLgpdConsent(Boolean isLgpdConsentGiven)
        {
          fyb_sdk_set_lgpd_consent(isLgpdConsentGiven);
        }

        [DllImport ("__Internal")]
        private static extern void fyb_sdk_set_gdpr_consent(Boolean isGdprConsentGiven);
        public static void SetGdprConsent(Boolean isGdprConsentGiven)
        {
          fyb_sdk_set_gdpr_consent(isGdprConsentGiven);
        }

        [DllImport ("__Internal")]
        private static extern void fyb_sdk_set_gdpr_consent_string(string gdprConsentString);
        public static void SetGdprConsentString(string gdprConsentString)
        {
          fyb_sdk_set_gdpr_consent_string(gdprConsentString);
        }

        [DllImport ("__Internal")]
        private static extern void fyb_sdk_clear_gdpr_consent();
        public static void ClearGdprConsent()
        {
          fyb_sdk_clear_gdpr_consent();
        }

        [DllImport ("__Internal")]
        private static extern void fyb_sdk_set_iab_us_privacy_string(string iabUsPrivacyString);
        public static void SetIabUsPrivacyString(string iabUsPrivacyString)
        {
          fyb_sdk_set_iab_us_privacy_string(iabUsPrivacyString);
        }

        [DllImport ("__Internal")]
        private static extern void fyb_sdk_clear_iab_us_privacy_string();
        public static void ClearIabUsPrivacyString()
        {
          fyb_sdk_clear_iab_us_privacy_string();
        }

        [DllImport ("__Internal")]
        private static extern void fyb_user_set_id(string userId);
        public static void SetUserId(string userId)
        {
          fyb_user_set_id(userId);
        }

        [DllImport ("__Internal")]
        private static extern void fyb_user_set_is_child(Boolean isChild);
        public static void SetIsChild(Boolean isChild)
        {
          fyb_user_set_is_child(isChild);
        }
    }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
    public class UserInfoAndroid : MonoBehaviour {

        public static void SetUserGender(string gender) {
            if(Application.platform != RuntimePlatform.Android) return;

            AndroidJNIHelper.debug = false;
            using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper")) {
                jc.CallStatic("setUserGender", gender);
            }
        }

        public static void SetUserLocation(float latitude, float longitude, float horizontalAccuracy, float verticalAccuracy, float altitude, double timestamp) {
            if(Application.platform != RuntimePlatform.Android) return;

            AndroidJNIHelper.debug = false;
            using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper")) {
                jc.CallStatic("setUserLocation", latitude, longitude, horizontalAccuracy, verticalAccuracy, altitude, timestamp);
            }

        }

        public static void SetUserPostalCode(string postalCode) {

            if(Application.platform != RuntimePlatform.Android) return;

            AndroidJNIHelper.debug = false;
            using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper")) {
                jc.CallStatic("setUserPostalCode", postalCode);
            }
        }

        public static void SetUserHouseholdIncome(int householdIncome) {

            if(Application.platform != RuntimePlatform.Android) return;

            AndroidJNIHelper.debug = false;
            using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper")) {
                jc.CallStatic("setUserHouseholdIncome", householdIncome);
            }
        }

        public static void SetUserMaritalStatus(string maritalStatus) {

            if(Application.platform != RuntimePlatform.Android) return;

            AndroidJNIHelper.debug = false;
            using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper")) {
                jc.CallStatic("setUserMaritalStatus", maritalStatus);
            }
        }

        public static void SetUserEducationLevel(string educationLevel) {

            if(Application.platform != RuntimePlatform.Android) return;

            AndroidJNIHelper.debug = false;
            using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper")) {
                jc.CallStatic("setUserEducationLevel", educationLevel);
            }
        }

        public static void SetUserBirthDate(string yyyyMMdd_date) {

            if(Application.platform != RuntimePlatform.Android) return;

            AndroidJNIHelper.debug = false;
            using (AndroidJavaClass jc = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper")) {
                jc.CallStatic("setUserBirthDate", yyyyMMdd_date);
            }
        }

        public static void SetGdprConsent(Boolean isGdprConsentGiven)
        {
          if (Application.platform != RuntimePlatform.Android) return;

          AndroidJNIHelper.debug = false;
          using (AndroidJavaClass javaClass = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
          {
            javaClass.CallStatic("setGdprConsent", isGdprConsentGiven);
          }
        }

        public static void SetGdprConsentString(String gdprConsentString)
        {
          if (Application.platform != RuntimePlatform.Android) return;

          AndroidJNIHelper.debug = false;
          using (AndroidJavaClass javaClass = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
          {
            javaClass.CallStatic("setGdprConsentString", gdprConsentString);
          }
        }

        public static void ClearGdprConsent()
        {
          if (Application.platform != RuntimePlatform.Android) return;

          AndroidJNIHelper.debug = false;
          using (AndroidJavaClass javaClass = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
          {
            javaClass.CallStatic("clearGdprConsent");
          }
        }
    
        public static void SetIabUsPrivacyString(String iabUsPrivacyString)
        {
          if (Application.platform != RuntimePlatform.Android) return;

          AndroidJNIHelper.debug = false;
          using (AndroidJavaClass javaClass = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
          {
            javaClass.CallStatic("setIabUsPrivacyString", iabUsPrivacyString);
          }
        }

        public static void ClearIabUsPrivacyString()
        {
          if (Application.platform != RuntimePlatform.Android) return;

          AndroidJNIHelper.debug = false;
          using (AndroidJavaClass javaClass = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
          {
            javaClass.CallStatic("clearIabUsPrivacyString");
          }
        }

        public static void SetLgpdConsent(Boolean isLgpdConsentGiven)
        {
            if (Application.platform != RuntimePlatform.Android) return;

            AndroidJNIHelper.debug = false;
            using (AndroidJavaClass javaClass = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
            {
                javaClass.CallStatic("setLgpdConsent", isLgpdConsentGiven);
            }
        }

        static public void SetUserId(string userId)
        {
          if (Application.platform != RuntimePlatform.Android) return;

          AndroidJNIHelper.debug = false;
          using (AndroidJavaClass javaClass = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
          {
            javaClass.CallStatic("setUserId", userId);
          }
        }
    }
#endif
    #endregion
}
