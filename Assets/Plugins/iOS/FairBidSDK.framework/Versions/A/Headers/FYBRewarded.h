//
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
//

#import <Foundation/Foundation.h>
#import "FYBRewardedDelegate.h"
#import "FYBShowOptions.h"

/**
 * FYBRewarded is responsible for requesting and showing Rewarded ads.
 * All methods on this class must be called from the main queue.
 */
@interface FYBRewarded : NSObject

/**
 * The delegate to receive the messages listed in the `FYBRewardedDelegate` protocol.
 */
@property (class, nonatomic, nullable) id <FYBRewardedDelegate> delegate;

/**
 * Requests a Rewarded ad for the given placement.
 *
 * @param placementName name describing the location or context for the ad to be shown.
 */
+ (void)request:(nonnull NSString *)placementName;

/**
 * Whether or not an ad is available to show for the given placement.
 *
 * @param placementName name describing the location or context for the ad to be shown.
 *
 *  @return YES if a Rewarded ad is available to be shown, NO otherwise
 */
+ (BOOL)isAvailable:(nonnull NSString *)placementName;

/**
 * Shows a Rewarded ad for a given placement, if available.
 *
 * @param placementName name describing the location or context for the ad to be shown.
 */
+ (void)show:(nonnull NSString *)placementName;

/** Shows a Rewarded ad with the given options.
 *
 * @param placementName name describing the location or context for the ad to be shown.
 *
 * @param options FYBShowOptions object containing properties for configuring how the ad is shown.
 */
+ (void)show:(nonnull NSString *)placementName options:(nonnull FYBShowOptions *)options;

+ (void)stopRequesting:(nonnull NSString *)placementName;

@end
