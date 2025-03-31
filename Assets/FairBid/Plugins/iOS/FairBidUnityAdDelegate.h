//
//
// Copyright (c) 2021 Fyber. All rights reserved.
//
//

#import <Foundation/Foundation.h>
#import <FairBidSDK/FYBInterstitialDelegate.h>
#import <FairBidSDK/FYBRewardedDelegate.h>
#import <FairBidSDK/FYBBannerDelegate.h>
#import <FairBidSDK/FairBidDelegate.h>

@interface FairBidUnityAdDelegate : NSObject

@property (nonatomic, strong, readonly, nonnull) NSString *klassName;

- (nonnull id)initWithKlassName:(nonnull NSString *)klassName;

@end

@interface FairBidUnityInterstitialDelegate : FairBidUnityAdDelegate <FYBInterstitialDelegate>

@end

@interface FairBidUnityRewardedDelegate : FairBidUnityAdDelegate <FYBRewardedDelegate>

@end

@interface FairBidUnityBannerDelegate : FairBidUnityAdDelegate <FYBBannerDelegate>

@end

@interface FairBidUnityFairBidDelegate : FairBidUnityAdDelegate <FairBidDelegate>

@end

@interface FYBImpressionData ()

- (nonnull NSDictionary *)toDictionary;

@end

@interface NSDictionary (FairBidUnity)

- (nullable NSString *)fyb_stringRepresentation;

@end
