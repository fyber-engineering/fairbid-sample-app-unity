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
    internal class OfferWallMessageReceiver : MonoBehaviour
    {
        public void DispatchShowCallback(string? placementId)
        {
            OfferWall.Log($"DispatchShowCallback, message payload: {placementId}");
            OfferWall.OnOfferWallShownEvent(String.IsNullOrEmpty(placementId) ? null : placementId);
        }
        
        public void DispatchFailedToShowCallback(string? message)
        {
            OfferWall.Log($"DispatchFailedToShowCallback, message payload: {message}");
            var showErrorModel = JsonUtility.FromJson<ShowErrorModel>(message);
            var offerWallError = OfferWallErrorFromString(showErrorModel.Error);
            OfferWall.OnOfferWallFailedToShowEvent(showErrorModel.PlacementId, offerWallError);
        }

        public void DispatchClosedCallback(string? placementId)
        {
            OfferWall.Log($"DispatchClosedCallback, message payload: {placementId}");
            OfferWall.OnOfferWallClosedEvent(String.IsNullOrEmpty(placementId) ? null : placementId);
        }

        public void DispatchVirtualCurrencySuccessfulResponseCallback(string? message)
        {
            OfferWall.Log($"DispatchVirtualCurrencySuccessfulResponseCallback, message payload: {message}");
            var currencySuccessModel = JsonUtility.FromJson<CurrencySuccessModel>(message);
            var virtualCurrencySuccessfulResponse = new VirtualCurrencySuccessfulResponse(currencySuccessModel.DeltaOfCoins, currencySuccessModel.LatestTransactionId, currencySuccessModel.CurrencyId, currencySuccessModel.CurrencyName, currencySuccessModel.IsDefault);
            OfferWall.OnVirtualCurrencyResponseEvent(virtualCurrencySuccessfulResponse);
        }

        public void DispatchVirtualCurrencyErrorResponseCallback(string? message)
        {
            OfferWall.Log($"DispatchVirtualCurrencyErrorResponseCallback, message payload: {message}");
            var currencyErrorModel = JsonUtility.FromJson<CurrencyErrorModel>(message);
            var offerWallError = OfferWallErrorFromString(currencyErrorModel.Error);
            var virtualCurrencyErrorResponse = new VirtualCurrencyErrorResponse(offerWallError, currencyErrorModel.ServerErrorMessage, currencyErrorModel.CurrencyId);
            OfferWall.OnVirtualCurrencyErrorEvent(virtualCurrencyErrorResponse);
        }

        private OfferWallError OfferWallErrorFromString(string offerWallErrorAsString)
        {
            // TODO: check for exceptions and so on
            var offerWallError = (OfferWallError)Enum.Parse( typeof(OfferWallError), offerWallErrorAsString);
            return offerWallError;
        }
    }

    [Serializable]
    public class ShowErrorModel
    {
        public string? PlacementId;
        public string Error;
    }

    [Serializable]
    public class CurrencySuccessModel
    {
        public double DeltaOfCoins;
        public string LatestTransactionId;
        public string CurrencyId;
        public string CurrencyName;
        public bool IsDefault;
    }
    
    [Serializable]
    public class CurrencyErrorModel
    {        
        public string Error;
        public string? ServerErrorMessage;
        public string? CurrencyId;
    }
}
