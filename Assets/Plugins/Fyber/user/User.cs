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
using System.Linq;

namespace Fyber {
    /// <summary>
    /// Use this class to pass information about a user to mediated ad networks that want the data. This kind of data is optional but can improve ad revenues.
    /// </summary>
    public class User : MonoBehaviour {

        private static User _instance;

        /// <summary>
        /// Set the gender of the user, if known, using the provided enum.
        /// </summary>
        public static void SetGender(Gender gender) {
            if (System.Enum.IsDefined(typeof(Gender), gender)) {
                #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                    #if UNITY_ANDROID
                        UserAndroid.SetUserGender(gender.ToString());
                    #elif UNITY_IPHONE
                        UserIOS.SetUserGender(gender.ToString());
                    #endif
                #else
                #endif
            }
        }

        /// <summary>
        /// Set the location of the user, if known. The required parameters match the parameters provided by the UnityEngine `LocationInfo` struct - see `https://docs.unity3d.com/ScriptReference/LocationInfo.html` for more information.
        /// </summary>
        public static void SetLocation(float latitude, float longitude, float horizontalAccuracy, float verticalAccuracy, float altitude, double timestamp) {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    UserAndroid.SetUserLocation(latitude, longitude, horizontalAccuracy, verticalAccuracy, altitude, timestamp);
                #elif UNITY_IPHONE
                    UserIOS.SetUserLocation(latitude, longitude, horizontalAccuracy, verticalAccuracy, altitude, timestamp);
                #endif
            #else
            #endif
        }

        /// <summary>
        /// Set the postal code (i.e.: the ZIP code in the US) of the user, if known. This is an alternative to setting the exact location but can provide similar benefits to ad revenues / targeting.
        /// </summary>
        public static void SetPostalCode(string postalCode) {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    UserAndroid.SetUserPostalCode(postalCode);
                #elif UNITY_IPHONE
                    UserIOS.SetUserPostalCode(postalCode);
                #endif
            #else
            #endif
        }

        /// <summary>
        /// Set the household income of the user, if known.
        /// </summary>
        public static void SetHouseholdIncome(int householdIncome) {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    UserAndroid.SetUserHouseholdIncome(householdIncome);
                #elif UNITY_IPHONE
                    UserIOS.SetUserHouseholdIncome(householdIncome);
                #endif
            #else
            #endif
        }

        /// <summary>
        /// Set the marital status of the user, if known, using the provided enum.
        /// </summary>
        public static void SetMaritalStatus(MaritalStatus maritalStatus) {
            if (System.Enum.IsDefined(typeof(MaritalStatus), maritalStatus)) {
                #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                    #if UNITY_ANDROID
                        UserAndroid.SetUserMaritalStatus(maritalStatus.ToString());
                    #elif UNITY_IPHONE
                        UserIOS.SetUserMaritalStatus(maritalStatus.ToString());
                    #endif
                #else
                #endif
            }
        }

        /// <summary>
        /// Set the highest education level already achieved by the user, if known, using the provided enum.
        /// </summary>
        public static void SetEducationLevel(EducationLevel educationLevel) {
            if (System.Enum.IsDefined(typeof(EducationLevel), educationLevel)) {
                #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                    #if UNITY_ANDROID
                        UserAndroid.SetUserEducationLevel(educationLevel.ToString());
                    #elif UNITY_IPHONE
                        UserIOS.SetUserEducationLevel(educationLevel.ToString());
                    #endif
                #else
                #endif
            }
        }

        /// <summary>
        /// Set the birth date of the user, if known, using this format: `YYYY/MM/DD`. Example: `2000/12/31` for December 31st, 2000.
        /// </summary>
        public static void SetBirthDate(string yyyyMMdd_date) {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    UserAndroid.SetUserBirthDate(yyyyMMdd_date);
                #elif UNITY_IPHONE
                    UserIOS.SetUserBirthDate(yyyyMMdd_date);
                #endif
            #else
            #endif
        }

        /// <summary>
        /// Sets User's consent under GDPR. Fyber will only be able to show targeted advertising if the user consented. Only call this method if the user explicitly gave or denied consent.
        /// </summary>
        /// <param name="isGdprConsentGiven">true if user gave consent to receive targeted advertisement, false otherwise</param>
        public static void SetGdprConsent(Boolean isGdprConsentGiven)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    UserAndroid.SetGdprConsent(isGdprConsentGiven);
                #elif UNITY_IPHONE
                    UserIOS.SetGdprConsent(isGdprConsentGiven);
                #endif
            #else
                UnityEngine.Debug.LogWarning("Call received to set the GDPR consent, but the SDK does not function in the editor. You must use a device/emulator to set the GDPR consent.");
            #endif
        }

        /// <summary>
        /// Sets User's consent data under GDPR. FairBid SDK will use this information to provide optimal targeted advertising without infringing GDPR.
        /// </summary>
        /// <param name="gdprConsentData">A Dictionary of key-value pairs containing GDPR related information</param>
        static public void SetGdprConsentData(Dictionary<string, string> gdprConsentData)
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                string gdprConsentDataAsJsonString = null;
                if (gdprConsentData != null)
                {
                    Dictionary<string, string> validatedGdprConsentData = new Dictionary<string, string>();
                    foreach(KeyValuePair<string, string> entry in gdprConsentData)
                    {
                        if (entry.Value != null)
                        {
                            validatedGdprConsentData.Add(entry.Key, entry.Value);
                        }
                    }
                    gdprConsentDataAsJsonString = GetGdprConsentDataAsJsonString(validatedGdprConsentData);
                }

                #if UNITY_ANDROID
                    UserAndroid.SetGdprConsentData(gdprConsentDataAsJsonString);
                #elif UNITY_IPHONE
                    UserIOS.SetGdprConsentData(gdprConsentDataAsJsonString);
                #endif
            #else
                UnityEngine.Debug.LogWarning("Call received to set the GDPR consent data, but the SDK does not function in the editor. You must use a device/emulator to set the GDPR consent data.");
            #endif
        }

        /// <summary>
        /// Clears all GDPR related information. This means removing any GDPR consent Data and restoring the GDPR consent to "unknown"
        /// </summary>
        static public void ClearGdprConsentData()
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
                #if UNITY_ANDROID
                    UserAndroid.ClearGdprConsentData();
                #elif UNITY_IPHONE
                    UserIOS.ClearGdprConsentData();
                #endif
            #else
                UnityEngine.Debug.LogWarning("Call received to clear the GDPR consent data, but the SDK does not function in the editor. You must use a device/emulator to set the GDPR consent data.");
            #endif
        }


        #region Internal methods

        static internal void InitReceiver(){
            if (_instance == null) {
                GameObject receiverObject = new GameObject("FairBidUser");
                DontDestroyOnLoad(receiverObject);
                _instance = receiverObject.AddComponent<User>();
            }
        }

        // Within Unity's .NET framework we don't have a stock solution for converting objets to Json so we need to implement a custom solution
        static private string GetGdprConsentDataAsJsonString(Dictionary<string, string> gdprConsentData)
        {
            var entries = gdprConsentData.Select(d =>
                string.Format("\"{0}\": \"{1}\"", d.Key, d.Value)
            );
            return "{" + string.Join(",", entries.ToArray()) + "}";
        }

        #endregion

    }

    #region Platform-specific translations
    #if UNITY_IPHONE && !UNITY_EDITOR
    public class UserIOS : MonoBehaviour {

        [DllImport ("__Internal")]
        private static extern void fyb_demo_set_gender(string gender);
        public static void SetUserGender(string gender) {
            fyb_demo_set_gender(gender);
        }

        [DllImport ("__Internal")]
        private static extern void fyb_demo_set_location(float latitude, float longitude, float horizontalAccuracy, float verticalAccuracy, float altitude, double timestamp);
        public static void SetUserLocation(float latitude, float longitude, float horizontalAccuracy, float verticalAccuracy, float altitude, double timestamp) {
            fyb_demo_set_location(latitude, longitude, horizontalAccuracy, verticalAccuracy, altitude, timestamp);
        }

        [DllImport ("__Internal")]
        private static extern void fyb_demo_set_postal_code(string postalCode);
        public static void SetUserPostalCode(string postalCode) {
            fyb_demo_set_postal_code(postalCode);
        }

        [DllImport ("__Internal")]
        private static extern void fyb_demo_set_household_income(int householdIncome);
        public static void SetUserHouseholdIncome(int householdIncome) {
            fyb_demo_set_household_income(householdIncome);
        }

        [DllImport ("__Internal")]
        private static extern void fyb_demo_set_marital_status(string maritalStatus);
        public static void SetUserMaritalStatus(string maritalStatus) {
            fyb_demo_set_marital_status(maritalStatus);
        }

        [DllImport ("__Internal")]
        private static extern void fyb_demo_set_education_level(string educationLevel);
        public static void SetUserEducationLevel(string educationLevel) {
            fyb_demo_set_education_level(educationLevel);
        }

        [DllImport ("__Internal")]
        private static extern void fyb_demo_set_birth_date(string yyyyMMdd_date);
        public static void SetUserBirthDate(string yyyyMMdd_date) {
            fyb_demo_set_birth_date(yyyyMMdd_date);
        }

        [DllImport ("__Internal")]
        private static extern void fyb_sdk_set_gdpr_consent(Boolean isGdprConsentGiven);
        public static void SetGdprConsent(Boolean isGdprConsentGiven)
        {
          fyb_sdk_set_gdpr_consent(isGdprConsentGiven);
        }

        [DllImport ("__Internal")]
        private static extern void fyb_sdk_set_gdpr_consent_data(string gdprConsentData);
        public static void SetGdprConsentData(string gdprConsentData)
        {
          fyb_sdk_set_gdpr_consent_data(gdprConsentData);
        }

        [DllImport ("__Internal")]
        private static extern void fyb_sdk_clear_gdpr_consent_data();
        public static void ClearGdprConsentData()
        {
          fyb_sdk_clear_gdpr_consent_data();
        }

    }
    #endif

    #if UNITY_ANDROID && !UNITY_EDITOR
    public class UserAndroid : MonoBehaviour {

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

        public static void SetGdprConsentData(String gdprConsentData)
        {
          if (Application.platform != RuntimePlatform.Android) return;

          AndroidJNIHelper.debug = false;
          using (AndroidJavaClass javaClass = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
          {
            javaClass.CallStatic("setGdprConsentData", gdprConsentData);
          }
        }

        public static void ClearGdprConsentData()
        {
          if (Application.platform != RuntimePlatform.Android) return;

          AndroidJNIHelper.debug = false;
          using (AndroidJavaClass javaClass = new AndroidJavaClass("com.fyber.fairbid.sdk.extensions.unity3d.UnityHelper"))
          {
            javaClass.CallStatic("clearGdprConsentData");
          }
        }

    }
    #endif
    #endregion
}
