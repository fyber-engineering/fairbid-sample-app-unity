using UnityEngine;

namespace Fyber
{
    class CallbackInfo
    {
        public const string CallbackAvailable = "available";
        public const string CallbackUnavailable = "unavailable";
        public const string CallbackShow = "show";
        public const string CallbackFailed = "failed";
        public const string CallbackClick = "click";
        public const string CallbackHide = "hide";
        public const string CallbackError = "error";
        public const string CallbackLoad = "loaded";
        public const string CallbackRewardedComplete = "rewarded_result_complete";
        public const string CallbackRewardedNotComplete = "rewarded_result_incomplete";
        public const string CallbackRequestStart = "request_start";
        public const string CallbackNetworkStarted = "network_started";
        public const string CallbackNetworkFailedToStart = "network_failed_to_start";
        public const string CallbackMediationStarted = "mediation_started";
        public const string CallbackMediationFailedToStart = "mediation_failed_to_start";

        public string callback = null;
        public string placement_id = null;
        public string error = null;
        public string code = null;
        public string network = null;
        public string request_id = null;

        public ImpressionData impressionData => new ImpressionData(impression_data);

        [SerializeField]
        private ImpressionDataJson impression_data = null;

        public static CallbackInfo FromJson(string jsonString)
        {
            CallbackInfo callbackInfo = JsonUtility.FromJson<CallbackInfo>(jsonString);
            return callbackInfo;
        }

        public static CallbackInfo ForPlacement(string callback, string placementId)
        {
            CallbackInfo callbackInfo = new CallbackInfo()
            {
                callback = callback,
                placement_id = placementId
            };
            return callbackInfo;
        }

        public static CallbackInfo ForError(string placementId, string errorMessage)
        {
            CallbackInfo errorCallbackInfo = new CallbackInfo()
            {
                callback = CallbackInfo.CallbackError,
                placement_id = placementId,
                error = errorMessage
            };
            return errorCallbackInfo;
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
