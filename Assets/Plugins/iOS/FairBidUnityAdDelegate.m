//
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
//

#import "FairBidUnityAdDelegate.h"

@implementation FairBidUnityAdDelegate

- (id)initWithKlassName:(NSString *)klassName {
    self = [super init];
    if (self) {
        _klassName = klassName;
    }

    return self;
}

- (void)sendMessageForKlass:(NSString *)klass withMessage:(NSString *)message tag:(NSString *)tag {
    NSString *unityMessage = [NSString stringWithFormat: @"%@,%@", message, tag];
    UnitySendMessage([klass UTF8String], "SetCallback", [unityMessage UTF8String]);
}

@end

@implementation FairBidUnityInterstitialDelegate

#pragma mark - FYBInterstitialDelegate

- (void)interstitialIsAvailable:(nonnull NSString *)placementId {
    [self sendMessageForKlass:self.klassName withMessage:@"available" tag:placementId];
}

- (void)interstitialIsUnavailable:(nonnull NSString *)placementId {
    [self sendMessageForKlass:self.klassName withMessage:@"fetch_failed" tag:placementId];
}

- (void)interstitialDidShow:(nonnull NSString *)placementId {
    [self sendMessageForKlass:self.klassName withMessage:@"show" tag:placementId];
}

- (void)interstitialDidClick:(nonnull NSString *)placementId {
    [self sendMessageForKlass:self.klassName withMessage:@"click" tag:placementId];
}

- (void)interstitialDidDismiss:(nonnull NSString *)placementId {
    [self sendMessageForKlass:self.klassName withMessage:@"hide" tag:placementId];
}

- (void)interstitialDidFailToShow:(nonnull NSString *)placementId withError:(nonnull NSError *)error {
    [self sendMessageForKlass:self.klassName withMessage:@"failed" tag:placementId];
}

- (void)interstitialWillStartAudio {
    [self sendMessageForKlass:self.klassName withMessage:@"audio_starting" tag:@""];
}

- (void)interstitialDidFinishAudio {
    [self sendMessageForKlass:self.klassName withMessage:@"audio_finished" tag:@""];
}

@end

@implementation FairBidUnityRewardedDelegate

#pragma mark - FYBRewardedDelegate

- (void)rewardedIsAvailable:(nonnull NSString *)placementId {
    [self sendMessageForKlass:self.klassName withMessage:@"available" tag:placementId];
}

- (void)rewardedIsUnavailable:(nonnull NSString *)placementId {
    [self sendMessageForKlass:self.klassName withMessage:@"fetch_failed" tag:placementId];
}

- (void)rewardedDidShow:(nonnull NSString *)placementId {
    [self sendMessageForKlass:self.klassName withMessage:@"show" tag:placementId];
}

- (void)rewardedDidClick:(nonnull NSString *)placementId {
    [self sendMessageForKlass:self.klassName withMessage:@"click" tag:placementId];
}

- (void)rewardedDidDismiss:(nonnull NSString *)placementId {
    [self sendMessageForKlass:self.klassName withMessage:@"hide" tag:placementId];
}

- (void)rewardedDidFailToShow:(nonnull NSString *)placementId withError:(nonnull NSError *)error {
    [self sendMessageForKlass:self.klassName withMessage:@"failed" tag:placementId];
}

- (void)rewardedDidComplete:(nonnull NSString *)placementId userRewarded:(BOOL)userRewarded {
    NSString *message = userRewarded ? @"rewarded_result_complete" : @"rewarded_result_incomplete";
    [self sendMessageForKlass:self.klassName withMessage:message tag:placementId];
}

- (void)rewardedWillStartAudio {
    [self sendMessageForKlass:self.klassName withMessage:@"audio_starting" tag:@""];
}

- (void)rewardedDidFinishAudio {
    [self sendMessageForKlass:self.klassName withMessage:@"audio_finished" tag:@""];
}

@end

@implementation FairBidUnityBannerDelegate

#pragma mark - FYBBannerDelegate

- (void)bannerDidLoad:(nonnull FYBBannerView *)banner {
    if (!self.bannerView) {
        self.bannerView = banner;
        [self sendMessageForKlass:self.klassName withMessage:@"loaded" tag:banner.options.placementName];
    } else {
        [banner removeFromSuperview];
        NSLog(@"Requested a banner before the previous one was destroyed. Ignoring this request.");
    }
}

- (void)bannerDidFailToLoad:(nonnull NSString *)placementName withError:(nonnull NSError *)error {
    NSString *message = [NSString stringWithFormat:@"%@:%@", placementName, [error description]];
    [self sendMessageForKlass:self.klassName withMessage:@"error" tag:message];
}

- (void)bannerDidShow:(nonnull FYBBannerView *)banner {
    [self sendMessageForKlass:self.klassName withMessage:@"show" tag:banner.options.placementName];
    //FIXME: when firing multiple requests at the same time, only the first banner received should be displayed
}

- (void)banner:(nonnull FYBBannerView *)banner didResizeToFrame:(CGRect)frame {

}

- (void)bannerDidClick:(nonnull FYBBannerView *)banner {
    [self sendMessageForKlass:self.klassName withMessage:@"click" tag:banner.options.placementName];
}

- (void)bannerWillPresentModalView:(nonnull FYBBannerView *)banner {

}

- (void)bannerDidDismissModalView:(nonnull FYBBannerView *)banner {

}

- (void)bannerWillLeaveApplication:(nonnull FYBBannerView *)banner {

}

@end
