//
// FairBid Unity SDK
//
// Copyright (c) 2023 Digital Turbine. All rights reserved.
//
namespace DigitalTurbine.OfferWall
{
    /**
     * Enum naming possible error in the OfferWall SDK.
     */
    public enum OfferWallError
    {
        // common to android & iOS, generic errors
        UNKNOWN_ERROR,
        SDK_NOT_STARTED,
        CONNECTION_ERROR,

        // common to android & iOS, vcs specific
        INVALID_VIRTUAL_CURRENCY_RESPONSE,
        INVALID_VIRTUAL_CURRENCY_RESPONSE_SIGNATURE,
        VIRTUAL_CURRENCY_SERVER_RETURNED_ERROR,
        SECURITY_TOKEN_NOT_PROVIDED,

        // android only generic errors
        DEVICE_NOT_SUPPORTED
    }
}