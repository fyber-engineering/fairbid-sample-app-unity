//
// FairBid Unity SDK
//
// Copyright (c) 2023 Digital Turbine. All rights reserved.
//
#nullable enable

namespace DigitalTurbine.OfferWall
{
    /**
     * The class declaring the parameters that configure the virtual currency request.
     */
    public struct VirtualCurrencyRequestOptions
    {
        /**
         * Denotes whether the user should be presented a toast notification upon getting a reward. 
         */
        public bool ToastOnReward { get; }
        
        /**
         * Defines the currency identifier that should be used upon request.
         */
        public string? CurrencyId { get; }

        /**
         * The constructor for the VirtualCurrencyRequestOptions class.
         *
         * <param name="toastOnReward">optional boolean parameter denoting whether the user should be presented a toast notification upon getting a reward. If not provided, <i>true</i> will be used.</param>
         * <param name="currencyId">optional string parameter defining the currency ID that should be used for the request. If none provided, the default currency ID will be used.</param>
         */
        public VirtualCurrencyRequestOptions(bool toastOnReward = true, string? currencyId = null)
        {
            ToastOnReward = toastOnReward;
            CurrencyId = currencyId;
        }
    }
}