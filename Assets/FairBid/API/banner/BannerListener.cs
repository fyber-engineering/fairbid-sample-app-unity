//
// FairBid Unity SDK
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
namespace Fyber
{
    ///<summary>
    /// An Interface that contains the events triggered by a <see cref="Banner"/>
    /// </summary>
    public interface BannerListener
    {
        /// <summary>
        /// Called when the banner triggers an error.
        /// </summary>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        /// <param name="error">Error.</param>
        void OnError(string placementId, string error);

        /// <summary>
        /// Called when the banner is loaded.
        /// </summary>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        void OnLoad(string placementId);

        /// <summary>
        /// Called when the banner is shown.
        /// </summary>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        /// <param name="impressionData">Revenue and demand source information associated to the ad.</param>
        void OnShow(string placementId, ImpressionData impressionData);

        /// <summary>
        /// Called when the banner is clicked.
        /// </summary>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        void OnClick(string placementId);

        /// <summary>
        /// Called when a banner ad is going to be requested.
        /// </summary>
        /// <param name="placementId">The identifier of the placement that was requested.</param>
        void OnRequestStart(string placementId);
    }
}
