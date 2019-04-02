//
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
//

#import <Foundation/Foundation.h>
#import <FairBidSDK/FairBid.h>

@interface FairBidUnityAdDelegate : NSObject <FYBInterstitialDelegate, FYBRewardedDelegate, FYBBannerDelegate>

@property (nonatomic, strong) NSString *klassName;

- (id)initWithKlassName:(NSString *)klassName;
- (void)sendMessageForKlass:(NSString *)klass withMessage:(NSString *)message tag:(NSString *)tag;

@end

@interface FairBidUnityInterstitialDelegate : FairBidUnityAdDelegate <FYBInterstitialDelegate>

@end

@interface FairBidUnityRewardedDelegate : FairBidUnityAdDelegate <FYBRewardedDelegate>

@end

@interface FairBidUnityBannerDelegate : FairBidUnityAdDelegate <FYBBannerDelegate>

@property (nonatomic, strong) FYBBannerView *bannerView;

@end
