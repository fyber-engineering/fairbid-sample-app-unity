//
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
//

#import <CoreLocation/CLLocation.h>
#import <FairBidSDK/FairBid.h>
#import "FairBidUnityAdDelegate.h"

extern void UnitySendMessage(const char *, const char *, const char *);

static NSString * const FYBPluginName        = @"unity3d";
static NSString * const FYBInterstitialClass = @"FairBidInterstitial";
static NSString * const FYBRewardedClass     = @"FairBidRewarded";
static NSString * const FYBBannerClass       = @"FairBidBanner";


static FairBidUnityInterstitialDelegate *gInterstitialDelegate = nil;
static FairBidUnityRewardedDelegate *gRewardedDelegate = nil;
static FairBidUnityBannerDelegate *gBannerDelegate = nil;

static FYBOptions *startOptions = [[FYBOptions alloc] init];

extern "C" {

#pragma mark - Starting the SDK

    NSString* nil_or_string(const char *characters) {
        return (characters == NULL) ? nil : [NSString stringWithUTF8String:characters];
    }

    void fyb_sdk_set_plugin_version(const char *pluginVersion) {
        NSString *pluginVersionString = nil_or_string(pluginVersion);
        startOptions.pluginVersion = pluginVersionString;
    }

    void fyb_sdk_start_app(const char *publisher_id, BOOL autoRequestingEnabled) {
        static dispatch_once_t onceToken;
        dispatch_once(&onceToken, ^{
            NSString *publisherID = nil_or_string(publisher_id);

            startOptions.framework = FYBPluginName;
            startOptions.autoRequestingEnabled = autoRequestingEnabled;

            [FairBid startWithAppId:publisherID options:startOptions];

            gRewardedDelegate = [[FairBidUnityRewardedDelegate alloc] initWithKlassName:FYBRewardedClass];
            [FYBRewarded setDelegate:gRewardedDelegate];

            gInterstitialDelegate = [[FairBidUnityInterstitialDelegate alloc] initWithKlassName:FYBInterstitialClass];
            [FYBInterstitial setDelegate:gInterstitialDelegate];

            gBannerDelegate = [[FairBidUnityBannerDelegate alloc] initWithKlassName:FYBBannerClass];
            [FYBBanner setDelegate:gBannerDelegate];
            gBannerDelegate.bannerView = nil;
        });
    }


#pragma mark - Interstitial Ads

    void fyb_sdk_fetch_interstitial(const char *placementName) {
        [FYBInterstitial request:nil_or_string(placementName)];
    }

    void fyb_sdk_show_interstitial(const char *placementName) {
        [FYBInterstitial show:nil_or_string(placementName)];
    }

    bool fyb_sdk_interstitial_is_available(const char *placementName) {
        return [FYBInterstitial isAvailable:nil_or_string(placementName)];
    }

    void fyb_sdk_stop_requesting_interstitial(const char *placementName) {
        [FYBInterstitial stopRequesting:nil_or_string(placementName)];
    }

#pragma mark - Rewarded Ads

    void fyb_sdk_fetch_rewarded(const char *placementName) {
        [FYBRewarded request:nil_or_string(placementName)];
    }

    void fyb_sdk_show_rewarded(const char *placementName) {
        [FYBRewarded show:nil_or_string(placementName)];
    }

    bool fyb_sdk_rewarded_is_available(const char *placementName) {
        return [FYBRewarded isAvailable:nil_or_string(placementName)];
    }

    void fyb_sdk_stop_requesting_rewarded(const char *placementName) {
        [FYBRewarded stopRequesting:nil_or_string(placementName)];
    }

#pragma mark - Banner Ads

    void fyb_sdk_show_banner(const char *position, const char *placementName) {
        if (!gBannerDelegate.bannerView) {
            FYBBannerPosition pos = FYBBannerPositionBottom;
            NSString *positionStr = nil_or_string(position);
            if ([positionStr isEqualToString:@"top"]) {
                pos = FYBBannerPositionTop;
            }

            FYBBannerOptions *options = [[FYBBannerOptions alloc] init];
            options.placementName = nil_or_string(placementName);

            [FYBBanner placeBannerInView:nil
                                position:pos
                                 options:options];
        } else {
            // Unhide the banner
            [gBannerDelegate.bannerView setHidden:NO];
        }
    }

    void fyb_sdk_hide_banner(void) {
        if (gBannerDelegate.bannerView) {
            [gBannerDelegate.bannerView setHidden:YES];
        } else {
            NSLog(@"Can't hide banner, there is no banner ad currently loaded.");
        }
    }

    void fyb_sdk_destroy_banner(void) {
        if (gBannerDelegate.bannerView) {
            [gBannerDelegate.bannerView removeFromSuperview];
            gBannerDelegate.bannerView = nil;

        } else {
            NSLog(@"Can't destroy banner, there is no banner ad currently loaded.");
        }
    }


#pragma mark - Test Suite

    void fyb_sdk_show_test_suite(void) {
        [FairBid presentTestSuite];
    }


#pragma mark - GDPR

    void fyb_sdk_set_gdpr_consent(BOOL isGdprConsentGiven) {
        FairBid.user.gdprConsent = isGdprConsentGiven;
    }

    void fyb_sdk_set_gdpr_consent_data(const char * gdprConsentDataAsJsonString) {
        NSDictionary<NSString *, NSString *> *gdprConsentData = nil;
        if (gdprConsentDataAsJsonString != NULL) {
            NSData *data = [[NSString stringWithUTF8String:gdprConsentDataAsJsonString] dataUsingEncoding:NSUTF8StringEncoding];
            gdprConsentData = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
        }
        FairBid.user.gdprConsentData = gdprConsentData;
    }

    void fyb_sdk_clear_gdpr_consent_data() {
         [FairBid.user clearGDPRConsent];
    }


#pragma mark - Debugging

    void fyb_sdk_show_debug_logs(void) {
        startOptions.logLevel = FYBLogLevelVerbose;
    }

    void fyb_sdk_hide_debug_logs(void) {
        startOptions.logLevel = FYBLogLevelSilent;
    }

    void fyb_sdk_show_third_party_debug_logs(void) {
        startOptions.thirdPartyLoggingEnabled = YES;
    }

    void fyb_sdk_hide_third_party_debug_logs(void) {
        startOptions.thirdPartyLoggingEnabled = NO;
    }


#pragma mark - Demographics Setters

    void fyb_demo_set_gender(const char * genderChar) {
        NSString *gender = nil_or_string(genderChar);

        FYBUserGender userGender = FYBUserGenderUnknown;
        if ([gender isEqualToString:@"MALE"]) {
            userGender = FYBUserGenderMale;
        } else if ([gender isEqualToString:@"FEMALE"]) {
            userGender = FYBUserGenderFemale;
        } else if ([gender isEqualToString:@"OTHER"]) {
            userGender = FYBUserGenderOther;
        }
        FairBid.user.gender = userGender;
    }

    void fyb_demo_set_location(float latitude, float longitude, float horizontalAccuracy, float verticalAccuracy, float altitude, double timestamp) {
        CLLocation *location = [[CLLocation alloc] initWithCoordinate:CLLocationCoordinate2DMake(latitude, longitude) altitude:altitude horizontalAccuracy:horizontalAccuracy verticalAccuracy:verticalAccuracy timestamp:[NSDate dateWithTimeIntervalSince1970:timestamp]];
        FairBid.user.location = location;
    }

    void fyb_demo_set_postal_code(const char * postalCodeChar) {
        NSString *postalCode = nil_or_string(postalCodeChar);
        FairBid.user.postalCode = postalCode;
    }

    void fyb_demo_set_household_income(int householdIncome) {
        FairBid.user.householdIncome = @(householdIncome);
    }

    void fyb_demo_set_marital_status(const char * maritalStatusChar) {
        NSString *maritalStatus = nil_or_string(maritalStatusChar);

        FYBUserMaritalStatus status = FYBUserMaritalStatusUnknown;
        if ([maritalStatus isEqualToString:@"SINGLE"]) {
            status = FYBUserMaritalStatusSingle;
        } else if ([maritalStatus isEqualToString:@"MARRIED"]) {
            status = FYBUserMaritalStatusMarried;
        }
        FairBid.user.maritalStatus = status;
    }

    void fyb_demo_set_education_level(const char * educationLevelChar) {
        NSString *educationLevel = nil_or_string(educationLevelChar);

        FYBUserEducation education = FYBUserEducationUnknown;
        if ([educationLevel isEqualToString:@"GRADE_SCHOOL"]) {
            education = FYBUserEducationGradeSchool;
        } else if ([educationLevel isEqualToString:@"HIGH_SCHOOL_UNFINISHED"]) {
            education = FYBUserEducationHighSchoolUnfinished;
        } else if ([educationLevel isEqualToString:@"HIGH_SCHOOL_FINISHED"]) {
            education = FYBUserEducationHighSchoolFinished;
        } else if ([educationLevel isEqualToString:@"COLLEGE_UNFINISHED"]) {
            education = FYBUserEducationCollegeUnfinished;
        } else if ([educationLevel isEqualToString:@"ASSOCIATE_DEGREE"]) {
            education = FYBUserEducationAssociateDegree;
        } else if ([educationLevel isEqualToString:@"BACHELORS_DEGREE"]) {
            education = FYBUserEducationBachelorsDegree;
        } else if ([educationLevel isEqualToString:@"GRADUATE_DEGREE"]) {
            education = FYBUserEducationGraduateDegree;
        } else if ([educationLevel isEqualToString:@"POSTGRADUATE_DEGREE"]) {
            education = FYBUserEducationPostGraduateDegree;
        }

        FairBid.user.educationLevel = education;
    }

    void fyb_demo_set_birth_date(const char * yyyyMMdd_dateChar) {
        __block NSDateFormatter *dateFormat;
        static dispatch_once_t onceToken;
        dispatch_once(&onceToken, ^{
            dateFormat = [[NSDateFormatter alloc] init];
            [dateFormat setDateFormat:@"yyyy/MM/dd"];
        });

        NSDate *parsedDate = nil;
        if (yyyyMMdd_dateChar != NULL) {
            parsedDate = [dateFormat dateFromString:nil_or_string(yyyyMMdd_dateChar)];
        }

        FairBid.user.birthDate = parsedDate;
    }
}
