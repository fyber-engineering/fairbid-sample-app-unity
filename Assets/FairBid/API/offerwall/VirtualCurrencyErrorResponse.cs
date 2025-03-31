//
// FairBid Unity SDK
//
// Copyright (c) 2023 Digital Turbine. All rights reserved.
//

#nullable enable

namespace DigitalTurbine.OfferWall
{
    /**
     * A struct that encapsulates the information returned in a failed virtual currency request.
     */
    public struct VirtualCurrencyErrorResponse
    {
        public VirtualCurrencyErrorResponse(OfferWallError error, string serverErrorMessage, string currencyId)
        {
            this.Error = error;
            this.ServerErrorMessage = serverErrorMessage;
            this.CurrencyId = currencyId;
        }

        /**
         * The error indicating the reason of the failed request.
         *
         * <seealso cref="OfferWallError"/>
         */
        public OfferWallError Error { get; }

        /**
         * The error message returned by the server.
         */
        public string ServerErrorMessage { get; }

        /**
         * The currency ID from the request.
         */
        public string CurrencyId { get; }
    }
}