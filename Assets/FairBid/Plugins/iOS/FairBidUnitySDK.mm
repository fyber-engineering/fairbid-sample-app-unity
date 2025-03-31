//
//
// Copyright (c) 2021 Fyber. All rights reserved.
//
//

#import <CoreLocation/CLLocation.h>
#import <FairBidSDK/FairBidSDK.h>

#import "FairBidUnityAdDelegate.h"
#import "FairBidUnitySDK.h"

extern void UnitySendMessage(const char *, const char *, const char *);

static NSString * const FYBInterstitialClass = @"FairBidInterstitial";
static NSString * const FYBRewardedClass     = @"FairBidRewarded";
static NSString * const FYBBannerClass       = @"FairBidBanner";
static NSString * const FairBidDelegateClass = @"FairBid";


static FairBidUnityInterstitialDelegate *gInterstitialDelegate = nil;
static FairBidUnityRewardedDelegate *gRewardedDelegate = nil;
static FairBidUnityBannerDelegate *gBannerDelegate = nil;
static FairBidUnityFairBidDelegate *gFairBidDelegate = nil;

static FYBStartOptions *startOptions = [[FYBStartOptions alloc] init];

extern "C" {

#pragma mark - Utils

    NSString* nil_or_string(const char *characters) {
        return (characters == NULL) ? nil : [NSString stringWithUTF8String:characters];
    }

    char* createCStringCopy(const char* string) {
        if (string == NULL) {
            return NULL;
        }
        char* res = (char*)malloc(strlen(string) + 1);
        strcpy(res, string);
        return res;
    }

    FYBLossNotificationReason fyb_loss_reason(const char *reasonChar) {
        NSString *reasonString = nil_or_string(reasonChar);
        FYBLossNotificationReason reason = FYBLossNotificationReasonUnknown;
        if ([reasonString isEqualToString:@"LostOnPrice"]) {
            reason = FYBLossNotificationReasonLostOnPrice;
        } else if ([reasonString isEqualToString:@"ImpressionOpportunityExpired"]) {
            reason = FYBLossNotificationReasonImpressionOpportunityExpired;
        } else if ([reasonString isEqualToString:@"FilteredAdvertiser"]) {
            reason = FYBLossNotificationReasonFilteredAdvertiser;
        } else if ([reasonString isEqualToString:@"FilteredNetwork"]) {
            reason = FYBLossNotificationReasonFilteredNetwork;
        }
        return reason;
    }

#pragma mark - Starting the SDK

    void fyb_sdk_set_plugin_params(const char *pluginVersion, const char *frameworkVersion) {
        startOptions.pluginOptions = [[FYBPluginOptions alloc] init];
        startOptions.pluginOptions.pluginFramework = FYBPluginFrameworkUnity;
        startOptions.pluginOptions.pluginFrameworkVersion = nil_or_string(frameworkVersion);
        if (pluginVersion) {
            startOptions.pluginOptions.pluginSdkVersion = [NSString stringWithUTF8String:pluginVersion];
        }
    }

    void fyb_sdk_start_app(const char *publisher_id, BOOL autoRequestingEnabled) {
        static dispatch_once_t onceToken;
        dispatch_once(&onceToken, ^{
            NSString *publisherID = nil_or_string(publisher_id);

            startOptions.autoRequestingEnabled = autoRequestingEnabled;

            [FairBid startWithAppId:publisherID options:startOptions];
            gFairBidDelegate = [[FairBidUnityFairBidDelegate alloc] initWithKlassName:FairBidDelegateClass];
            [FairBid setDelegate:gFairBidDelegate];

            gRewardedDelegate = [[FairBidUnityRewardedDelegate alloc] initWithKlassName:FYBRewardedClass];
            [FYBRewarded setDelegate:gRewardedDelegate];

            gInterstitialDelegate = [[FairBidUnityInterstitialDelegate alloc] initWithKlassName:FYBInterstitialClass];
            [FYBInterstitial setDelegate:gInterstitialDelegate];

            gBannerDelegate = [[FairBidUnityBannerDelegate alloc] initWithKlassName:FYBBannerClass];
            [FYBBanner setDelegate:gBannerDelegate];
        });
    }

    const char * fyb_sdk_version() {
        NSString *version = [FairBid version];
        return createCStringCopy([version UTF8String]);
    }


#pragma mark - Interstitial Ads

    void fyb_sdk_fetch_interstitial(const char *placementId) {
        [FYBInterstitial request:nil_or_string(placementId)];
    }

    void fyb_sdk_show_interstitial(const char *placementId, const char *options) {
        FYBShowOptions *showOptions = [FYBShowOptions new];
        if (options != NULL) {
            NSData *data = [[NSString stringWithUTF8String:options] dataUsingEncoding:NSUTF8StringEncoding];
            NSDictionary<NSString *, NSString *> *customParams = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
            showOptions.customParameters = customParams;
        }

        [FYBInterstitial show:nil_or_string(placementId) options:showOptions];
    }

    bool fyb_sdk_interstitial_is_available(const char *placementId) {
        return [FYBInterstitial isAvailable:nil_or_string(placementId)];
    }

    const char* fyb_sdk_interstitial_get_impression_data(const char *placementId) {
        FYBImpressionData *impressionData = [FYBInterstitial impressionData:nil_or_string(placementId)];
        if (impressionData) {
            return createCStringCopy([impressionData.jsonString UTF8String]);
        }
        return NULL;
    }

    void fyb_sdk_interstitial_enable_auto_requesting(const char *placementId) {
        [FYBInterstitial enableAutoRequesting:nil_or_string(placementId)];
    }

    void fyb_sdk_interstitial_disable_auto_requesting(const char *placementId) {
        [FYBInterstitial disableAutoRequesting:nil_or_string(placementId)];
    }

    int fyb_sdk_impression_depth_interstitial() {
        return (int)FYBInterstitial.impressionDepth;
    }

    void fyb_sdk_interstitial_notifyLoss(const char *placementId, const char *reasonChar) {
        [FYBInterstitial notifyLoss:nil_or_string(placementId) reason:fyb_loss_reason(reasonChar)];
    }

#pragma mark - Rewarded Ads

    void fyb_sdk_fetch_rewarded(const char *placementId) {
        [FYBRewarded request:nil_or_string(placementId)];
    }

    void fyb_sdk_show_rewarded(const char *placementId, const char *options) {
        FYBShowOptions *showOptions = [FYBShowOptions new];
        if (options != NULL) {
            NSData *data = [[NSString stringWithUTF8String:options] dataUsingEncoding:NSUTF8StringEncoding];
            NSDictionary<NSString *, NSString *> *customParams = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
            showOptions.customParameters = customParams;
        }

        [FYBRewarded show:nil_or_string(placementId) options:showOptions];
    }

    bool fyb_sdk_rewarded_is_available(const char *placementId) {
        return [FYBRewarded isAvailable:nil_or_string(placementId)];
    }

    const char* fyb_sdk_rewarded_get_impression_data(const char *placementId) {
        FYBImpressionData *impressionData = [FYBRewarded impressionData:nil_or_string(placementId)];
        if (impressionData) {
            return createCStringCopy([impressionData.jsonString UTF8String]);
        }
        return NULL;
    }

    void fyb_sdk_rewarded_enable_auto_requesting(const char *placementId) {
        [FYBRewarded enableAutoRequesting:nil_or_string(placementId)];
    }

    void fyb_sdk_rewarded_disable_auto_requesting(const char *placementId) {
        [FYBRewarded disableAutoRequesting:nil_or_string(placementId)];
    }

    int fyb_sdk_impression_depth_rewarded() {
        return (int)FYBRewarded.impressionDepth;
    }

    void fyb_sdk_rewarded_notifyLoss(const char *placementId, const char *reasonChar) {
        [FYBRewarded notifyLoss:nil_or_string(placementId) reason:fyb_loss_reason(reasonChar)];
    }

#pragma mark - Banner Ads

    void fyb_sdk_show_banner(const char *placementId, const char *position, bool adaptive, const char *refreshMode) {
        FYBBannerAdViewPosition pos = FYBBannerAdViewPositionBottom;
        NSString *positionStr = nil_or_string(position);
        if ([positionStr isEqualToString:@"top"]) {
            pos = FYBBannerAdViewPositionTop;
        }

        FYBBannerOptions *options = [[FYBBannerOptions alloc] initWithPlacementId:nil_or_string(placementId) position: pos];
        options.adaptive = adaptive;

        NSString *refreshModeStr = nil_or_string(refreshMode);
        if ([refreshModeStr isEqualToString:@"AUTO"]) {
            options.refreshMode = FYBBannerRefreshModeAuto;
        } else if ([refreshModeStr isEqualToString:@"MANUAL"]) {
            options.refreshMode = FYBBannerRefreshModeManual;
        } else if ([refreshModeStr isEqualToString:@"OFF"]) {
            options.refreshMode = FYBBannerRefreshModeOff;
        }

        [FYBBanner showBannerInView:nil options:options];
    }

    void fyb_sdk_destroy_banner(const char *placementId) {
        [FYBBanner destroy:nil_or_string(placementId)];
    }

    void fyb_sdk_hide_banner(const char *placementId) {
        [FYBBanner hide:nil_or_string(placementId)];
    }

    void fyb_sdk_refresh_banner(const char *placementId) {
        [FYBBanner refresh:nil_or_string(placementId)];
    }

    int fyb_sdk_impression_depth_banner() {
        return (int)FYBBanner.impressionDepth;
    }



#pragma mark - Test Suite

    void fyb_sdk_show_test_suite(void) {
        [FairBid presentTestSuite];
    }

#pragma mark - LGPD

    void fyb_sdk_set_lgpd_consent(BOOL isLgpdConsentGiven) {
        FairBid.user.LGPDConsent = isLgpdConsentGiven;
    }

#pragma mark - GDPR

    void fyb_sdk_set_gdpr_consent(BOOL isGdprConsentGiven) {
        FairBid.user.GDPRConsent = isGdprConsentGiven;
    }

#pragma mark - IAB US Privacy (CCPA)

    void fyb_sdk_set_iab_us_privacy_string(const char * iabUsPrivacyString) {
        FairBid.user.IABUSPrivacyString = nil_or_string(iabUsPrivacyString);
    }

#pragma mark - Debugging

    void fyb_sdk_show_debug_logs(void) {
        startOptions.logLevel = FYBLoggingLevelVerbose;
    }

    void fyb_sdk_hide_debug_logs(void) {
        startOptions.logLevel = FYBLoggingLevelSilent;
    }

    void fyb_sdk_show_third_party_debug_logs(void) {
        startOptions.thirdPartyLoggingEnabled = YES;
    }

    void fyb_sdk_hide_third_party_debug_logs(void) {
        startOptions.thirdPartyLoggingEnabled = NO;
    }


#pragma mark - UserInfo

    void fyb_user_set_gender(const char * genderChar) {
        NSString *gender = nil_or_string(genderChar);

        FYBGender userGender = FYBGenderUnknown;
        if ([gender isEqualToString:@"MALE"]) {
            userGender = FYBGenderMale;
        } else if ([gender isEqualToString:@"FEMALE"]) {
            userGender = FYBGenderFemale;
        } else if ([gender isEqualToString:@"OTHER"]) {
            userGender = FYBGenderOther;
        }
        FairBid.user.gender = userGender;
    }

    void fyb_user_set_location(float latitude, float longitude, float horizontalAccuracy, float verticalAccuracy, float altitude, double timestamp) {
        CLLocation *location = [[CLLocation alloc] initWithCoordinate:CLLocationCoordinate2DMake(latitude, longitude) altitude:altitude horizontalAccuracy:horizontalAccuracy verticalAccuracy:verticalAccuracy timestamp:[NSDate dateWithTimeIntervalSince1970:timestamp]];
        FairBid.user.location = location;
    }

    void fyb_user_set_postal_code(const char * postalCodeChar) {
        NSString *postalCode = nil_or_string(postalCodeChar);
        FairBid.user.postalCode = postalCode;
    }

    void fyb_user_set_birth_date(const char * yyyyMMdd_dateChar) {
        NSDateFormatter *dateFormat = [[NSDateFormatter alloc] init];
        [dateFormat setDateFormat:@"yyyy/MM/dd"];

        NSDate *parsedDate = nil;
        if (yyyyMMdd_dateChar != NULL) {
            parsedDate = [dateFormat dateFromString:nil_or_string(yyyyMMdd_dateChar)];
        }

        FairBid.user.birthDate = parsedDate;
    }

    void fyb_user_set_id(const char * userIdChar) {
        NSString *userId = nil_or_string(userIdChar);
        FairBid.user.userId = userId;
    }

    #pragma mark - Settings

    void fyb_settings_set_muted(BOOL isMuted) {
        FairBid.settings.muted = isMuted;
    }

    #pragma mark - isChild

    void fyb_sdk_set_is_child(BOOL isChild) {
        startOptions.isChild = isChild;
    }
}
