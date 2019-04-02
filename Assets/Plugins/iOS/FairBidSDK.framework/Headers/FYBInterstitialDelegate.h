//
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@protocol FYBInterstitialDelegate <NSObject>

@optional

- (void)interstitialIsAvailable:(NSString *)placementName;

- (void)interstitialIsUnavailable:(NSString *)placementName;

- (void)interstitialDidShow:(NSString *)placementName;

- (void)interstitialDidFailToShow:(NSString *)placementName withError:(NSError *)error;

- (void)interstitialDidClick:(NSString *)placementName;

- (void)interstitialDidDismiss:(NSString *)placementName;

- (void)interstitialWillStartAudio;

- (void)interstitialDidFinishAudio;

@end

NS_ASSUME_NONNULL_END
