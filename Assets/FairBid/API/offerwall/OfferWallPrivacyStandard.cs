//
// FairBid Unity SDK
//
// Copyright (c) 2023 Digital Turbine. All rights reserved.
//
#nullable enable

namespace DigitalTurbine.OfferWall
{
    /**
     * An enum declaring privacy standards supported by the OfferWall SDK.
     */
    public enum OfferWallPrivacyStandard
    {
        Ccpa,
        Gdpr
    }

    public class OfferWallPrivacyConsent
    {
        /**
         * A class representing the GDPR consent standard.  
         */
        public class Gdpr : OfferWallPrivacyConsent
        {
            public bool ConsentGiven { get; private set; }

            /**
             * The GDPR consent class constructor.
             *
             * <param name="consentGiven">denotes whether the user gave consent or not.</param>
             */
            public Gdpr(bool consentGiven)
            {
                ConsentGiven = consentGiven;
            }
        }

        /**
         * A class representing the CCPA consent standard.
         */
        public class Ccpa : OfferWallPrivacyConsent
        {
            public string PrivacyString;

            /**
             * The CCPA consent class constructor.
             *
             * <param name="privacyString">a respective CCPA privacy string denoting proper user consent.</param>
             */
            public Ccpa(string privacyString)
            {
                PrivacyString = privacyString;
            }
        }
    };
}