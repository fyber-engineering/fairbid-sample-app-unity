//
// FairBid Unity SDK
//
// Copyright (c) 2023 Digital Turbine. All rights reserved.
//
#nullable enable

namespace DigitalTurbine.OfferWall
{
    abstract class NativeOfferWallBridge
    {
        public abstract void SetPluginParams(string pluginVersion, string frameworkVersion);
        public abstract void StartSDK(string appId, string? token = null, bool disableAdvertisingId = false);
        public abstract void Show(OfferWallShowOptions offerWallShowOptions, string? placementId);
        public abstract void RequestVirtualCurrency(VirtualCurrencyRequestOptions vcOptions);
        public abstract void SetUserId(string? userId);
        public abstract string? GetUserId();
        public abstract void SetConsent(OfferWallPrivacyConsent consent);
        public abstract void RemoveConsent(OfferWallPrivacyStandard privacyStandard);
        public abstract void SetLogLevel(LogLevel level);

        public virtual void Log(string message)
        {
        }
    }

    /**
     * The enum declaring supported logging levels in OfferWall SDK. 
     */
    public enum LogLevel
    {
        Verbose, Debug, Info, Warning, Error, Off
    }
}