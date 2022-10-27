//
// FairBid Unity SDK
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
namespace Fyber
{
    /// <summary>
    /// An Interface that contains the events triggered by an <see cref="Rewarded" />
    /// </summary>
    public interface RewardedListener
    {
        /// <summary>
        /// Called when an ad is shown to the user.
        /// </summary>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        /// <param name="impressionData">Revenue and demand source information associated to the ad.</param>
        void OnShow(string placementId, ImpressionData impressionData);

        /// <summary>
        /// Call when an ad is clicked on by the user. Expect the application to go into the background shortly after this callback is fired.
        /// </summary>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        void OnClick(string placementId);

        /// <summary>
        /// Called when an ad has been hidden.
        /// </summary>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        void OnHide(string placementId);

        /// <summary>
        /// Called when an ad failed to be shown. This can be due to a variety of reasons, such as
        /// an internal error in FairBid or no internet connection.
        /// </summary>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        /// <param name="impressionData">Revenue and demand source information associated to the ad.</param>
        void OnShowFailure(string placementId, ImpressionData impressionData);

        /// <summary>
        /// Called when an ad has been successfully fetched and is available to show.
        /// </summary>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        void OnAvailable(string placementId);

        /// <summary>
        /// Called when an ad has a placement has lost its fill.
        /// </summary>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        void OnUnavailable(string placementId);

        /// <summary>
        /// Called to notify about the user completion of a rewarded ad.
        /// </summary>
        /// <param name="placementId">The placement identifier for the particular ad.</param>
        /// <param name="userRewarded"> <c>true</c> if the user completed the ad, <c>false</c> otherwise</param>
        void OnCompletion(string placementId, bool userRewarded);

        /// <summary>
        /// Called when a rewarded ad is going to be requested.
        /// </summary>
        /// <param name="placementId">The identifier of the placement that was requested.</param>
        void OnRequestStart(string placementId);
    }
}
