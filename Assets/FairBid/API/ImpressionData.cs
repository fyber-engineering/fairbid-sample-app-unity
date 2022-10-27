using UnityEngine;
using UnityEngine.Scripting;

namespace Fyber
{
    /// <summary>
    /// Impression data contains detailed information for each impression. That includes which demand source served the ad,
    /// the expected or exact revenue associated with it as well as granular details that will allow you to analyse and,
    /// ultimately, optimize both your ad monetization and user acquisition strategies.
    /// </summary>
    [System.Serializable]
    public class ImpressionData
    {
        /// <summary>
        /// Accuracy of the netPayout value. May return one of the following:
        /// - UNDISCLOSED - When the netPayout is '0'.
        /// - PREDICTED - When Fyber's estimation of the impression value is based on historical data from non-programmatic mediated network's reporting APIs.
        /// - PROGRAMMATIC - When netPayout is the exact and committed value of the impression, available when impressions are won by programmatic buyers.
        /// </summary>
        public enum PriceAccuracy
        {
            Undisclosed = 0,
            Predicted = 1,
            Programmatic = 2
        }

        /// <summary>
        /// Type of the impression's placement.
        /// </summary>
        public enum PlacementType
        {
            Banner = 0,
            Interstitial = 1,
            Rewarded = 2
        }

        internal ImpressionData(string jsonString) : this(JsonUtility.FromJson<ImpressionDataJson>(jsonString)) {}

        internal ImpressionData(ImpressionDataJson json) {
            this.json = json;
            this.advertiserDomain = json.advertiser_domain;
            this.campaignId = json.campaign_id;
            this.countryCode = json.country_code;
            this.creativeId = json.creative_id;
            this.currency = json.currency;
            this.demandSource = json.demand_source;
            this.impressionDepth = json.impression_depth;
            this.impressionId = json.impression_id;
            this.netPayout = json.net_payout;
            this.networkInstanceId = json.network_instance_id;
            this.renderingSDK = json.rendering_sdk;
            this.renderingSDKVersion = json.rendering_sdk_version;
            this.priceAccuracy = json.price_accuracy;
            this.placementType = json.placement_type;
            this.variantId = json.variant_id;
        }

        /// <summary>
        /// Advertiser's domain when available. Used as an identifier for a set of campaigns for the same advertiser.
        /// </summary>
        [Preserve]
        public string advertiserDomain;
        
        /// <summary>
        /// Campaign ID when available used as an identifier for a specific campaign of a certain advertiser.
        /// </summary>
        [Preserve]
        public string campaignId;
        
        /// <summary>
        /// Country location of the ad impression (in ISO country code).
        /// </summary>
        [Preserve]
        public string countryCode;
        
        /// <summary>
        /// Creative ID when available. Used as an identifier for a specific creative of a certain campaign.
        /// This is particularly useful information when a certain creative is found to cause user experience issues.
        /// </summary>
        [Preserve]
        public string creativeId;
        
        /// <summary>
        /// Currency of the payout.
        /// </summary>
        [Preserve]
        public string currency;
        
        /// <summary>
        /// Demand Source name is the name of the buy-side / demand-side entity that purchased the impression.
        /// When mediated networks win an impression, you'll see the mediated network's name. When a DSP buying
        /// through the programmatic marketplace wins the impression, you'll see the DSP's name.
        /// </summary>
        [Preserve]
        public string demandSource;

        /// <summary>
        /// The amount of impressions in current session for the given Placement Type
        /// </summary>
        [Preserve]
        public int impressionDepth;

        /// <summary>
        /// A unique identifier for a specific impression.
        /// </summary>
        [Preserve]
        public string impressionId;

        /// <summary>
        /// Net payout for an impression. The value accuracy is returned in the priceAccuracy field.
        /// The value is provided in units returned in the currency field.
        /// </summary>
        [Preserve]
        public double netPayout;
        
        /// <summary>
        /// The mediated ad network's original Placement/Zone/Location/Ad Unit ID that you created in their dashboard.
        /// For ads shown by the Fyber Marketplace the networkInstanceId is the Placement ID you created in the Fyber console.
        /// </summary>
        [Preserve]
        public string networkInstanceId;
        
        /// <summary>
        /// Name of the SDK rendering the ad.
        /// </summary>
        [Preserve]
        public string renderingSDK;

        /// <summary>
        /// Version of the SDK rendering the ad.
        /// </summary>
        [Preserve]
        public string renderingSDKVersion;

        /// <summary>
        /// Accuracy of the netPayout value.
        /// </summary>
        [Preserve]
        public PriceAccuracy priceAccuracy;
        /// <summary>
        /// Type of the impression's placement.
        /// </summary>
        [Preserve]
        public PlacementType placementType;
        
        /// <summary>
        /// The variant id can represent a group in a Multi-Testing experiment.
        /// </summary>
        [Preserve]
        public string variantId;
        
        public override string ToString()
        {
            return JsonUtility.ToJson(this, true) + "\nJsonString: \n" + ToJsonString();
        }

        public string ToJsonString()
        {
            return JsonUtility.ToJson(json, true);
        }

        [Preserve]
        private ImpressionDataJson json;
    }

    [System.Serializable]
    internal class ImpressionDataJson
    {
        [Preserve]
        public string advertiser_domain;

        [Preserve]
        public string campaign_id = null;

        [Preserve]
        public string country_code = null;

        [Preserve]
        public string creative_id = null;

        [Preserve]
        public string currency = null;

        [Preserve]
        public string demand_source = null;

        [Preserve]
        public int impression_depth;

        [Preserve]
        public string impression_id = null;

        [Preserve]
        public double net_payout;

        [Preserve]
        public string network_instance_id = null;

        [Preserve]
        public string rendering_sdk = null;

        [Preserve]
        public string rendering_sdk_version = null;

        [Preserve]
        public ImpressionData.PriceAccuracy price_accuracy;

        [Preserve]
        public ImpressionData.PlacementType placement_type;

        [Preserve]
        public string variant_id = null;
    }
}
