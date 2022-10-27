//
//
// Copyright (c) 2021 Fyber. All rights reserved.
//
//

#import <FairBidSDK/FYBBannerOptions.h>
#import <FairBidSDK/FYBBannerAdView.h>

#import "FairBidUnityAdDelegate.h"

@implementation FairBidUnityAdDelegate

- (id)initWithKlassName:(NSString *)klassName {
    self = [super init];
    if (self) {
        _klassName = klassName;
    }

    return self;
}

- (void)sendMessageForKlass:(NSString *)klass callback:(NSString *)callback placementId:(NSString *)placementId  {
    [self sendMessageForKlass:klass callback:callback placementId:placementId error:nil impressionData:nil];
}

- (void)sendMessageForKlass:(NSString *)klass
                   callback:(NSString *)callback
                placementId:(NSString *)placementId
                      error:(NSString *)error
             impressionData:(NSDictionary *)impressionData {

    NSMutableDictionary *params = [[NSMutableDictionary alloc] init];
    params[@"callback"] = callback;
    params[@"placement_id"] = placementId;
    params[@"error"] = error;
    params[@"impression_data"] = impressionData;
    NSString *str = [params fyb_stringRepresentation];
    if (str.length > 0) {
        UnitySendMessage([klass UTF8String], "InvokeCallback", [str UTF8String]);
    }
}

- (void)sentMessageForKlass:(NSString *)klass 
            networkCallback:(NSString *)callback 
                    network:(NSString *)network 
                      error:(NSError *)error {
    NSMutableDictionary *params = [[NSMutableDictionary alloc] init];
    params[@"callback"] = callback;
    params[@"network"] = network;
    params[@"error"] = error.description;
    NSString *str = [params fyb_stringRepresentation];
    if (str.length > 0) {
        UnitySendMessage([klass UTF8String], "InvokeCallback", [str UTF8String]);
    }
}

@end

@implementation FairBidUnityInterstitialDelegate

#pragma mark - FYBInterstitialDelegate

- (void)interstitialIsAvailable:(NSString *)placementId {
    [self sendMessageForKlass:self.klassName callback:@"available" placementId:placementId];
}

- (void)interstitialIsUnavailable:(NSString *)placementId {
    [self sendMessageForKlass:self.klassName callback:@"unavailable" placementId:placementId];
}

- (void)interstitialDidShow:(nonnull NSString *)placementId impressionData:(nonnull FYBImpressionData *)impressionData {
    NSDictionary *impressionDataDictionary = [impressionData toDictionary];
    [self sendMessageForKlass:self.klassName callback:@"show" placementId:placementId error:nil impressionData:impressionDataDictionary];
}

- (void)interstitialDidClick:(NSString *)placementId {
    [self sendMessageForKlass:self.klassName callback:@"click" placementId:placementId];
}

- (void)interstitialDidDismiss:(NSString *)placementId {
    [self sendMessageForKlass:self.klassName callback:@"hide" placementId:placementId];
}

- (void)interstitialDidFailToShow:(nonnull NSString *)placementId withError:(NSError *)error impressionData:(nonnull FYBImpressionData *)impressionData {
    NSDictionary *impressionDataDictionary = [impressionData toDictionary];
    [self sendMessageForKlass:self.klassName callback:@"failed" placementId:placementId error:nil impressionData:impressionDataDictionary];
}

- (void)interstitialWillRequest:(nonnull NSString *)placementId {
    [self sendMessageForKlass:self.klassName callback:@"request_start" placementId:placementId];
}

@end

@implementation FairBidUnityRewardedDelegate

#pragma mark - FYBRewardedDelegate

- (void)rewardedIsAvailable:(NSString *)placementId {
    [self sendMessageForKlass:self.klassName callback:@"available" placementId:placementId];
}

- (void)rewardedIsUnavailable:(NSString *)placementId {
    [self sendMessageForKlass:self.klassName callback:@"unavailable" placementId:placementId];
}

- (void)rewardedDidShow:(NSString *)placementId impressionData:(FYBImpressionData *)impressionData {
    NSDictionary *impressionDataDictionary = [impressionData toDictionary];
    [self sendMessageForKlass:self.klassName callback:@"show" placementId:placementId error:nil impressionData:impressionDataDictionary];
}

- (void)rewardedDidClick:(NSString *)placementId {
    [self sendMessageForKlass:self.klassName callback:@"click" placementId:placementId];
}

- (void)rewardedDidDismiss:(NSString *)placementId {
    [self sendMessageForKlass:self.klassName callback:@"hide" placementId:placementId];
}

- (void)rewardedDidFailToShow:(NSString *)placementId withError:(nonnull NSError *)error impressionData:(nonnull FYBImpressionData *)impressionData {
    NSDictionary *impressionDataDictionary = [impressionData toDictionary];
    [self sendMessageForKlass:self.klassName callback:@"failed" placementId:placementId error:nil impressionData:impressionDataDictionary];
}

- (void)rewardedDidComplete:(NSString *)placementId userRewarded:(BOOL)userRewarded {
    NSString *message = userRewarded ? @"rewarded_result_complete" : @"rewarded_result_incomplete";
    [self sendMessageForKlass:self.klassName callback:message placementId:placementId];
}

- (void)rewardedWillRequest:(nonnull NSString *)placementId {
    [self sendMessageForKlass:self.klassName callback:@"request_start" placementId:placementId];
}

@end

@implementation FairBidUnityBannerDelegate

#pragma mark - FYBBannerDelegate

- (void)bannerDidLoad:(FYBBannerAdView *)banner {
    [self sendMessageForKlass:self.klassName callback:@"loaded" placementId:banner.options.placementId];
}

- (void)bannerDidFailToLoad:(NSString *)placementId withError:(NSError *)error {
    [self sendMessageForKlass:self.klassName callback:@"error" placementId:placementId error:[error description] impressionData:nil];
}

- (void)bannerDidShow:(FYBBannerAdView *)banner impressionData:(FYBImpressionData *)impressionData {
    NSDictionary *impressionDataDictionary = [impressionData toDictionary];
    [self sendMessageForKlass:self.klassName callback:@"show" placementId:banner.options.placementId error:nil impressionData:impressionDataDictionary];
}

- (void)bannerDidClick:(FYBBannerAdView *)banner {
    [self sendMessageForKlass:self.klassName callback:@"click" placementId:banner.options.placementId];
}

- (void)bannerWillRequest:(nonnull NSString *)placementId {
    [self sendMessageForKlass:self.klassName callback:@"request_start" placementId:placementId];
}

@end

@implementation FairBidUnityFairBidDelegate

- (void)networkStarted:(FYBMediatedNetwork)network {
    [self sentMessageForKlass:self.klassName 
              networkCallback:@"network_started" 
                      network:[self networkStringFromFYBMediatedNetwork:network] 
                        error:nil];
}

- (void)network:(FYBMediatedNetwork)network failedToStartWithError:(NSError *)error {
    [self sentMessageForKlass:self.klassName 
              networkCallback:@"network_failed_to_start" 
                      network:[self networkStringFromFYBMediatedNetwork:network]
                        error:error];
}

- (NSString *)networkStringFromFYBMediatedNetwork:(FYBMediatedNetwork)network {
    switch (network) {
        case FYBMediatedNetworkAdColony:
            return @"ADCOLONY";
        case FYBMediatedNetworkAdMob:
            return @"ADMOB";
        case FYBMediatedNetworkAppLovin:
            return @"APPLOVIN";
        case FYBMediatedNetworkChartboost:
            return @"CHARTBOOST";
        case FYBMediatedNetworkMeta:
            return @"META_AUDIENCE_NETWORK";
        case FYBMediatedNetworkHyprMX:
            return @"HYPRMX";
        case FYBMediatedNetworkInMobi:
            return @"INMOBI";
        case FYBMediatedNetworkIronSource:
            return @"IRONSOURCE";
        case FYBMediatedNetworkMintegral:
            return @"MINTEGRAL";
        case FYBMediatedNetworkMyTarget:
            return @"MYTARGET";
        case FYBMediatedNetworkOgury:
            return @"OGURY";
        case FYBMediatedNetworkPangle:
            return @"PANGLE";
        case FYBMediatedNetworkTapjoy:
            return @"TAPJOY";
        case FYBMediatedNetworkUnityAds:
            return @"UNITYADS";
        case FYBMediatedNetworkYahoo:
            return @"YAHOO";
        case FYBMediatedNetworkVerve:
            return @"VERVE";
        case FYBMediatedNetworkVungle:
            return @"VUNGLE";
        default:
            return @"UNKNOWN";
    }
}

@end

@implementation NSDictionary (FairBidUnity)

- (NSString *)fyb_stringRepresentation {
    if (![NSJSONSerialization isValidJSONObject:self]) {
        NSLog(@"Failed to convert dictionary to String: invalid object %@", self);
        return nil;
    }

    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:self
                                                       options:0
                                                         error:&error];
    if (!jsonData) {
        NSLog(@"Failed to convert dictionary to String: %@", error);
        return nil;
    }

    NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    return jsonString;
}

@end
