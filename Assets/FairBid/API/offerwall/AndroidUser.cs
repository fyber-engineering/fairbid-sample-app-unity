//
// FairBid Unity SDK
//
// Copyright (c) 2023 Digital Turbine. All rights reserved.
//
#nullable enable

using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID

namespace DigitalTurbine.OfferWall
{
    internal class AndroidUser : IUser
    {
        private const string OfferWallUnityHelper = "com.fyber.fairbid.sdk.extensions.unity3d.OfferWallUnityHelper";

        public int? Age
        {
            set
            {
                if (Application.platform != RuntimePlatform.Android) return;
                AndroidJNIHelper.debug = false;

                using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
                javaClass.CallStatic("setAge", value);
            }
        }

        public DateTime? Birthdate
        {
            set
            {
                if (Application.platform != RuntimePlatform.Android) return;
                AndroidJNIHelper.debug = false;

                using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
                javaClass.CallStatic("setBirthday", ((DateTimeOffset?)value)?.ToUnixTimeMilliseconds());
            }
        }

        public Gender? Gender
        {
            set
            {
                if (Application.platform != RuntimePlatform.Android) return;
                AndroidJNIHelper.debug = false;

                using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
                javaClass.CallStatic("setGender", value.ToString());
            }
        }

        public SexualOrientation? SexualOrientation
        {
            set
            {
                if (Application.platform != RuntimePlatform.Android) return;
                AndroidJNIHelper.debug = false;

                using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
                javaClass.CallStatic("setSexualOrientation", value.ToString());
            }
        }

        public Ethnicity? Ethnicity
        {
            set
            {
                if (Application.platform != RuntimePlatform.Android) return;
                AndroidJNIHelper.debug = false;

                using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
                javaClass.CallStatic("setEthnicity", value.ToString());
            }
        }

        [Obsolete("Setting User Location is no longer supported on Android. Calling this method will result in no action.")]
        public Location? Location
        {
            set
            {
                if (Application.platform != RuntimePlatform.Android) return;
                Debug.Log("Setting User Location is no longer supported on Android. No action has been performed.");
            }
        }

        public MaritalStatus? MaritalStatus
        {
            set
            {
                if (Application.platform != RuntimePlatform.Android) return;
                AndroidJNIHelper.debug = false;

                using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
                javaClass.CallStatic("setMaritalStatus", value.ToString());
            }
        }

        public int? NumberOfChildren
        {
            set
            {
                if (Application.platform != RuntimePlatform.Android) return;
                AndroidJNIHelper.debug = false;

                using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
                javaClass.CallStatic("setNumberOfChildren", value);
            }
        }

        public int? AnnualHouseholdIncome
        {
            set
            {
                if (Application.platform != RuntimePlatform.Android) return;
                AndroidJNIHelper.debug = false;

                using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
                javaClass.CallStatic("setAnnualHouseholdIncome", value);
            }
        }

        public Education? Education
        {
            set
            {
                if (Application.platform != RuntimePlatform.Android) return;
                AndroidJNIHelper.debug = false;

                using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
                javaClass.CallStatic("setEducation", value.ToString());
            }
        }

        public string Zipcode
        {
            set
            {
                if (Application.platform != RuntimePlatform.Android) return;
                AndroidJNIHelper.debug = false;

                using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
                javaClass.CallStatic("setZipcode", value);
            }
        }

        public string[] Interests
        {
            set
            {
                if (Application.platform != RuntimePlatform.Android) return;
                AndroidJNIHelper.debug = false;

                var enumerator = value.GetEnumerator();
                var interests = "";
                while (enumerator.MoveNext())
                {
                    interests += enumerator.Current + ",";
                }

                using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
                javaClass.CallStatic("setInterests", interests);
            }
        }

        public bool? Iap
        {
            set
            {
                if (Application.platform != RuntimePlatform.Android) return;
                AndroidJNIHelper.debug = false;

                using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
                javaClass.CallStatic("setIap", value);
            }
        }

        public float? IapAmount
        {
            set
            {
                if (Application.platform != RuntimePlatform.Android) return;
                AndroidJNIHelper.debug = false;

                using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
                javaClass.CallStatic("setIapAmount", value);
            }
        }

        public int? NumberOfSessions
        {
            set
            {
                if (Application.platform != RuntimePlatform.Android) return;
                AndroidJNIHelper.debug = false;

                using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
                javaClass.CallStatic("setNumberOfSessions", value);
            }
        }

        public long? PsTime
        {
            set
            {
                if (Application.platform != RuntimePlatform.Android) return;
                AndroidJNIHelper.debug = false;

                using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
                javaClass.CallStatic("setPsTime", value);
            }
        }

        public long? LastSession
        {
            set
            {
                if (Application.platform != RuntimePlatform.Android) return;
                AndroidJNIHelper.debug = false;

                using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
                javaClass.CallStatic("setLastSession", value);
            }
        }

        public ConnectionType? ConnectionType
        {
            set
            {
                if (Application.platform != RuntimePlatform.Android) return;
                AndroidJNIHelper.debug = false;

                using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
                javaClass.CallStatic("setConnectionType", value.ToString());
            }
        }

        public string? Device
        {
            set
            {
                if (Application.platform != RuntimePlatform.Android) return;
                AndroidJNIHelper.debug = false;

                using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
                javaClass.CallStatic("setDevice", value);
            }
        }

        public string AppVersion
        {
            set
            {
                if (Application.platform != RuntimePlatform.Android) return;
                AndroidJNIHelper.debug = false;

                using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
                javaClass.CallStatic("setAppVersion", value);
            }
        }

        public Dictionary<string, string?> CustomParameters
        {
            set
            {
                if (Application.platform != RuntimePlatform.Android) return;
                AndroidJNIHelper.debug = false;

                using var javaClass = new AndroidJavaClass(OfferWallUnityHelper);
                javaClass.CallStatic("setCustomParameters", JsonUtility.ToJson(value, false));
            }
        }
    }
}
#endif