//
// FairBid Unity SDK
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
namespace Fyber
{
    /// <summary>
    /// Defines Loss Notification Reason.
    /// Unknown.
    /// Lost on price.
    /// Impression opportunity expired.
    /// Filtered Advertiser.
    /// Filtered Network.
    /// </summary>
    public enum LossNotificationReason
    {
        Unknown = 0,
        LostOnPrice = 1,
        ImpressionOpportunityExpired = 2,
        FilteredAdvertiser = 3,
        FilteredNetwork = 4
    }
}
