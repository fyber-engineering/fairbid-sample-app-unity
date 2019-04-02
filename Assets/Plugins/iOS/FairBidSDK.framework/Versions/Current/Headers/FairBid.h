//
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>


#import "FYBBanner.h"
#import "FYBBannerDelegate.h"
#import "FYBBannerOptions.h"
#import "FYBBannerView.h"
#import "FYBInterstitial.h"
#import "FYBInterstitialDelegate.h"
#import "FYBOptions.h"
#import "FYBRewarded.h"
#import "FYBRewardedDelegate.h"
#import "FYBShowOptions.h"
#import "FYBUser.h"


#define SDK_VERSION @"2.0.0"


#if __has_feature(objc_modules)
@import AdSupport;
@import CoreGraphics;
@import CoreTelephony;
@import MediaPlayer;
@import QuartzCore;
@import StoreKit;
@import MobileCoreServices;
@import Security;
@import SystemConfiguration;
@import MessageUI;
@import CoreLocation;
#endif


/**
 *  A class with miscellaneous Heyzap Ads methods. All methods on this class must be called from the main queue.
 */
@interface FairBid : NSObject

+ (void)startWithAppId:(nonnull NSString *)appId;

+ (void)startWithAppId:(nonnull NSString *)appId options:(nonnull FYBOptions *)options;

+ (BOOL)isStarted;

/**
 *  Returns an `FYBUser` object that you can use to pass demographic information to third party SDKs.
 *
 *  @return An `FYBUser` object. Guaranteed to be non-nil after starting the SDK.
 */
+ (nonnull FYBUser *)user;

/**
 * Presents a view controller that displays integration information and allows fetch/show testing
 */
+ (void)presentTestSuite;

/**
 * Returns SDK version
 */
+ (nonnull NSString *)version;

@end
