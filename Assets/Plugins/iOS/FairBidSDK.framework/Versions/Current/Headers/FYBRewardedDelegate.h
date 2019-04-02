//
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@protocol FYBRewardedDelegate <NSObject>

@optional

- (void)rewardedIsAvailable:(NSString *)placementName;

- (void)rewardedIsUnavailable:(NSString *)placementName;

- (void)rewardedDidShow:(NSString *)placementName;

- (void)rewardedDidFailToShow:(NSString *)placementName withError:(NSError *)error;

- (void)rewardedDidClick:(NSString *)placementName;

- (void)rewardedDidDismiss:(NSString *)placementName;

- (void)rewardedDidComplete:(NSString *)placementName userRewarded:(BOOL)userRewarded;

- (void)rewardedWillStartAudio;

- (void)rewardedDidFinishAudio;

@end

NS_ASSUME_NONNULL_END
