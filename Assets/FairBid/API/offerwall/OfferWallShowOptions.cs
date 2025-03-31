//
// FairBid Unity SDK
//
// Copyright (c) 2023 Digital Turbine. All rights reserved.
//
#nullable enable

using System.Collections.Generic;
using System.Text;

namespace DigitalTurbine.OfferWall
{
    /**
     * The class declaring the parameters that configure the offer wall display.
     */
    public struct OfferWallShowOptions
    {
        /**
         * Denotes whether the offer wall should be closed after entering a single offer.
         * If none provided in the constructor, then <i>false</i> value will be used.
         */
        public bool CloseOnRedirect { get; }
        
        /**
         * Defines the custom parameters that will be added to the offer wall request. 
         * If none provided in the constructor, then <i>null</i> value will be used.
         */
        public Dictionary<string, string>? CustomParams { get; }

        /**
         * The constructor for the ShowOptions class.
         * 
         * <param name="closeOnRedirect">denotes whether the offer wall should be closed after entering a single offer. If none provided, then <i>false</i> value will be used.</param>
         * <param name="customParams">defines the custom parameters that will be added to the offer wall request. If none provided then <i>null</i> value will be used.</param>
         */
        public OfferWallShowOptions(bool closeOnRedirect = false, Dictionary<string, string>? customParams = null)
        {
            CloseOnRedirect = closeOnRedirect;
            CustomParams = customParams;
        }

        internal string ToJsonString()
        {
            // Serialize the custom params dictionary to a string
            StringBuilder jsonString = new StringBuilder();
            if (CustomParams != null)
            {
                jsonString.Append("{");
                foreach (var pair in CustomParams)
                {
                    jsonString.Append($"  \"{pair.Key}\": \"{pair.Value}\",");
                }

                jsonString.Remove(jsonString.Length - 1, 1);
                jsonString.Append("}");
            }
            var dictionaryJsonObject = jsonString.Length == 0 ? "null" : jsonString.ToString();
            
            StringBuilder showOptionsJson = new StringBuilder();
            showOptionsJson.Append("{");
            showOptionsJson.Append($" \"CloseOnRedirect\": {CloseOnRedirect.ToString().ToLower()}, ");
            showOptionsJson.Append($" \"CustomParams\": {dictionaryJsonObject} ");
            showOptionsJson.Append("}");

            return showOptionsJson.ToString();
        }
    }
}