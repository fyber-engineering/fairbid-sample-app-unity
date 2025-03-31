//
// FairBid Unity SDK
//
// Copyright (c) 2023 Digital Turbine. All rights reserved.
//
#nullable enable

namespace DigitalTurbine.OfferWall
{
    /**
     * A struct that encapsulates the information returned in a successful virtual currency request.
     */
    public struct VirtualCurrencySuccessfulResponse
    {
        public VirtualCurrencySuccessfulResponse(double deltaOfCoins, string latestTransactionId, string currencyId,
            string currencyName, bool isDefault)
        {
            this.DeltaOfCoins = deltaOfCoins;
            this.LatestTransactionId = latestTransactionId;
            this.CurrencyId = currencyId;
            this.CurrencyName = currencyName;
            this.IsDefault = isDefault;
        }

        /**
         * The amount of coins that the user earned since the last request.
         */
        public double DeltaOfCoins { get; }
        
        /**
         * The identifier of the last successful transaction.
         */
        public string LatestTransactionId { get; }
        
        /**
         * The currency ID of a currency that was requested.
         */
        public string CurrencyId { get; }
        
        /**
         * The currency name of a currency that was requested.
         */
        public string CurrencyName { get; }
        
        /**
         * The value indicating whether the requested currency is default.
         */
        public bool IsDefault { get; }
    }
}
