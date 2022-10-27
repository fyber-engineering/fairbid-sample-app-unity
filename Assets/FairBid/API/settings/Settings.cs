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
    /// Class defining the FairBidSDK settings.
    /// </summary>
	public class Settings : MonoBehaviour
	{
		private static Settings _instance;
		
		public static void SetMuted(Boolean isMuted)
		{
			#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
				#if UNITY_ANDROID
					SettingsAndroid.SetMuted(isMuted);
				#elif UNITY_IPHONE
					SettingsIOS.SetMuted(isMuted);
				#endif
			#else
			     UnityEngine.Debug.LogWarning("Call received to set muted, but the SDK does not function in the editor. You must use a device/emulator to mute.");
			#endif
		}

		#region Internal methods
		
		static internal void InitReceiver()
		{
			if (_instance == null)
			{
				GameObject receiverObject = new GameObject("FairBidSettings");
				DontDestroyOnLoad(receiverObject);
				_instance = receiverObject.AddComponent<Settings>();
			}
		}

		#endregion

	}	

#region Platform-specific translations
#if UNITY_IPHONE && !UNITY_EDITOR 
	public class SettingsIOS : MonoBehaviour
	{
		[DllImport ("__Internal")]
		private static extern void fyb_settings_set_muted(Boolean isMuted);
		public static void SetMuted(Boolean isMuted)
		{
			fyb_settings_set_muted(isMuted);		
		}
	}
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	public class SettingsAndroid : MonoBehaviour
	{
		public static void SetMuted(Boolean isMuted)
		{
			if (Application.platform != RuntimePlatform.Android) return;

            AndroidJNIHelper.debug = false;
            using (AndroidJavaClass javaClass = new AndroidJavaClass("com.fyber.FairBid$Settings")) {
				javaClass.CallStatic("setMuted", isMuted); /// according to https://github.com/SponsorPay/fairbid-android-sdk/pull/1890/files#diff-e465e8cbbd863a6bc3e37c1b37a57407R58
			}
		}
	}
#endif
#endregion
}
