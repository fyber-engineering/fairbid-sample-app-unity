//
//  Copyright © 2023 Fyber. All rights reserved.
//

#import "OfferWallUnitySDK.h"
#import <Foundation/Foundation.h>
#import <FairBidSDK/FairBidSDK-Swift.h>
#import "OfferWallUnityDelegate.h"
#import "FairBidUnitySDK.h"

OFWLogLevel OFWLogLevel_from_c_string(const char *log_level);

static OfferWallUnityDelegate *gFairBidDelegate = nil;

@interface FYBOFWPluginParamsManager: NSObject
+ (void)setUnityFrameworkVersion:(NSString *)frameworkVersion sdkVersion:(NSString *)sdkVersion;
@end

extern "C" {

    void fyb_ofw_start(const char *app_id, const char *security_token) {
        static dispatch_once_t onceToken;
        dispatch_once(&onceToken, ^{
            NSString *appId = nil_or_string(app_id);
            NSString *securityToken = nil_or_string(security_token);

            gFairBidDelegate = [[OfferWallUnityDelegate alloc] init];


            OFWVirtualCurrencySettings *vcsSettings = [OFWVirtualCurrencySettings virtualCurrencySettingsWithSecurityToken:securityToken delegate:gFairBidDelegate];

            [OfferWall startWithAppId:appId delegate:gFairBidDelegate settings:vcsSettings completion:^(OFWError * _Nullable) {

            }];
        });
    }

    void fyb_ofw_show(const char *options, const char *placement_id) {
        NSString *placementId = nil_or_string(placement_id);
        NSData *data = [[NSString stringWithUTF8String:options] dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary<NSString *, id> *showOptionsDictionary = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];

        NSNumber *closeOnRedirect = showOptionsDictionary[@"CloseOnRedirect"] ?: @NO;
        NSDictionary *customParams = showOptionsDictionary[@"CustomParams"];
        if (customParams == [NSNull null]) {
            customParams = nil;
        }
        OFWShowOptions *showOptions = [OFWShowOptions optionsWithCloseOnRedirect:closeOnRedirect.boolValue
                                                                  viewController:nil
                                                                        animated:YES
                                                                    customParams:customParams];
        if (placementId) {
            [OfferWall showWithOptions:showOptions placementId:placementId];
        } else {
            [OfferWall showWithOptions:showOptions];
        }
    }

    void fyb_ofw_request_virtual_currency(BOOL toast_on_reward, const char * currency_id) {
        NSString *currencyId = nil_or_string(currency_id);
        OFWVirtualCurrencyRequestOptions *options = [OFWVirtualCurrencyRequestOptions optionsWithCurrencyId:currencyId toastOnReward:toast_on_reward];

        [OfferWall requestCurrencyWithOptions:options];
    }

    void fyb_ofw_set_gdpr_consent(BOOL consent) {
        OFWGDPRConsent gdprConsent = consent ? OFWGDPRConsentGiven : OFWGDPRConsentDenied;
        OFWDataUseConsentGDPR *consentObject = [OFWDataUseConsentGDPR dataUseConsentGDPRWithConsent:gdprConsent];
        [OfferWall setConsent:consentObject];
    }

    void fyb_ofw_clear_gdpr_consent() {
        [OfferWall removeConsentFor:OFWPrivacyStandard.GDPR];
    }

    void fyb_ofw_set_ccpa_consent(const char *consent_string) {
        NSString *consentString = nil_or_string(consent_string);
        OFWDataUseConsentCCPA *ccpaConsent = [OFWDataUseConsentCCPA dataUseConsentCCPAWithPrivacyString:consentString];
        [OfferWall setConsent:ccpaConsent];
    }

    void fyb_ofw_clear_ccpa_consent() {
        [OfferWall removeConsentFor:OFWPrivacyStandard.CCPA];
    }

    void fyb_ofw_set_user_id(const char *user_id) {
        NSString *userId = nil_or_string(user_id);
        [OfferWall setUserId:userId];
    }

    const char *fyb_ofw_get_user_id() {
        const char *user_id = [[OfferWall userId] cStringUsingEncoding:NSUTF8StringEncoding];
        return createCStringCopy(user_id);
    }

    void fyb_ofw_set_log_level(const char *log_level) {
        [OfferWall setLogLevel:OFWLogLevel_from_c_string(log_level)];
    }

    void fyb_ofw_set_plugin_params(const char *pluginVersion, const char *frameworkVersion) {
        [FYBOFWPluginParamsManager setUnityFrameworkVersion:nil_or_string(frameworkVersion) sdkVersion:nil_or_string(pluginVersion)];
    }
}

OFWLogLevel OFWLogLevel_from_c_string(const char *log_level) {
    NSString *logLevel = [nil_or_string(log_level) lowercaseString];
    if ([logLevel isEqualToString:@"verbose"]) {
        return OFWLogLevelVerbose;
    }
    if ([logLevel isEqualToString:@"debug"]) {
        return OFWLogLevelDebug;
    }
    if ([logLevel isEqualToString:@"info"]) {
        return OFWLogLevelInfo;
    }
    if ([logLevel isEqualToString:@"warning"]) {
        return OFWLogLevelWarn;
    }

    return OFWLogLevelOff;
}
