//
// FairBid Unity SDK
//
// Copyright (c) 2023 Digital Turbine. All rights reserved.
//
#nullable enable

using UnityEngine;

namespace DigitalTurbine.OfferWall
{
    public class OfferWallCallbackInfo
    {
        public const string CallbackShowError = "show_error";
        public const string CallbackShow = "show";
        public const string CallbackClose = "close";
        public const string CallbackVcsSuccess = "vcs_success";
        public const string CallbackVcsError = "vcs_error";

        public string? MessageType = null;
        public string? PlacementId = null;
        public string? OfferWallError = null;

        public double? DeltaOfCoins;
        public string? LatestTransactionId;
        public string? CurrencyId;
        public string? CurrencyName;
        public bool IsDefault = false;

        public string? ServerErrorMessage = null;
            
        public static OfferWallCallbackInfo FromJson(string jsonString)
        {
            var callbackInfo = JsonUtility.FromJson<OfferWallCallbackInfo>(jsonString);
            return callbackInfo;
        }
    }
}