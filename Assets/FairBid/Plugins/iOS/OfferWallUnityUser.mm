//
//  OfferWallUnityUser.m
//  DT FairBid
//
//  Copyright © 2023 Fyber. All rights reserved.
//

#import <FairBidSDK/FairBidSDK-Swift.h>
#import <FairBidSDK/FYBUser.h>
#import <FairBidSDK/FyberSDK.h>
#import <CoreLocation/CoreLocation.h>
#import "OfferWallUnityUser.h"
#import "FairBidUnitySDK.h"

@interface FYBUser (Unity)
- (NSString *)setDataWithKey:(NSString *)key value:(id)value;
@end

OFWUserGender OFWUserGenderFromString(NSString *gender) {
    if([gender isEqualToString:@"male"]) {
        return OFWUserGenderMale;
    }
    if([gender isEqualToString:@"female"]) {
        return OFWUserGenderFemale;
    }
    if([gender isEqualToString:@"other"]) {
        return OFWUserGenderOther;
    }

    return OFWUserGenderUndefined;
}

OFWUserSexualOrientation OFWUserSexualOrientationFromString(NSString *sexualOrientation) {
    if([sexualOrientation isEqualToString:@"heterosexual"]) {
        return OFWUserSexualOrientationHeterosexual;
    }
    if([sexualOrientation isEqualToString:@"homosexual"]) {
        return OFWUserSexualOrientationHomosexual;
    }
    if([sexualOrientation isEqualToString:@"bisexual"]) {
        return OFWUserSexualOrientationBisexual;
    }
    if([sexualOrientation isEqualToString:@"other"]) {
        return OFWUserSexualOrientationOther;
    }

    return OFWUserSexualOrientationUndefined;
}

OFWUserEthnicity OFWUserEthnicityFromString(NSString *ethnicity) {
    if([ethnicity isEqualToString:@"asian"]) {
        return OFWUserEthnicityAsian;
    }
    if([ethnicity isEqualToString:@"black"]) {
        return OFWUserEthnicityBlack;
    }
    if([ethnicity isEqualToString:@"hispanic"]) {
        return OFWUserEthnicityHispanic;
    }
    if([ethnicity isEqualToString:@"white"]) {
        return OFWUserEthnicityWhite;
    }
    if([ethnicity isEqualToString:@"other"]) {
        return OFWUserEthnicityOther;
    }

    return OFWUserEthnicityUndefined;
}

OFWUserMaritalStatus OFWUserMaritalStatusFromString(NSString *status) {
    if([status isEqualToString:@"single"]) {
        return OFWUserMaritalStatusSingle;
    }
    if([status isEqualToString:@"married"]) {
        return OFWUserMaritalStatusMarried;
    }
    if([status isEqualToString:@"divorced"]) {
        return OFWUserMaritalStatusDivorced;
    }
    if([status isEqualToString:@"relationship"]) {
        return OFWUserMaritalStatusRelationship;
    }
    if([status isEqualToString:@"engaged"]) {
        return OFWUserMaritalStatusEngaged;
    }

    return OFWUserMaritalStatusUndefined;
}

OFWUserEducation OFWUserEducationFromString(NSString *education) {
    if([education isEqualToString:@"highschool"]) {
        return OFWUserEducationHighSchool;
    }
    if([education isEqualToString:@"bachelor"]) {
        return OFWUserEducationBachelor;
    }
    if([education isEqualToString:@"master"]) {
        return OFWUserEducationMaster;
    }
    if([education isEqualToString:@"phd"]) {
        return OFWUserEducationPhd;
    }
    if([education isEqualToString:@"other"]) {
        return  OFWUserEducationOther;
    }

    return OFWUserEducationUndefined;
}

OFWUserConnectionType OFWUserConnectionTypeFromString(NSString *connecton) {
    if([connecton isEqualToString:@"wifi"]) {
        return OFWUserConnectionTypeWifi;
    }
    if([connecton isEqualToString:@"cellular"]) {
        return OFWUserConnectionTypeCellular;
    }

    return OFWUserConnectionTypeUndefined;
}

OFWUserDevice OFWUserDeviceFromString(NSString *connecton) {
    if([connecton isEqualToString:@"iphone"]) {
        return OFWUserDeviceIPhone;
    }
    if([connecton isEqualToString:@"ipad"]) {
        return OFWUserDeviceIPad;
    }

    return OFWUserDeviceUndefined;
}

extern "C" {
    void fyb_ofw_user_set_age(int age)
    {
        OFWUser.shared.age = age;
    }

    void fyb_ofw_user_set_birthdate(long timestamp)
    {
        NSDate *date = [NSDate dateWithTimeIntervalSince1970:timestamp];
        OFWUser.shared.birthdate = date;
    }

    void fyb_ofw_user_set_gender(const char *gender)
    {
        NSString *genderString = [nil_or_string(gender) lowercaseString];
        OFWUser.shared.gender = OFWUserGenderFromString(genderString);
    }

    void fyb_ofw_user_set_sexual_orientation(const char *sexual_orientation)
    {
        NSString *sexualOrientation = [nil_or_string(sexual_orientation) lowercaseString];
        OFWUser.shared.sexualOrientation = OFWUserSexualOrientationFromString(sexualOrientation);
    }

    void fyb_ofw_user_set_ethnicity(const char *ethnicity)
    {
        NSString *ethnicityString = [nil_or_string(ethnicity) lowercaseString];
        OFWUser.shared.ethnicity = OFWUserEthnicityFromString(ethnicityString);
    }

    void fyb_ofw_user_set_location(double lat, double lon)
    {
        OFWUser.shared.location = [[CLLocation alloc] initWithLatitude:lat longitude:lon];
    }

    void fyb_ofw_user_set_marital_status(const char *marital_status)
    {
        NSString *maritalStatus = [nil_or_string(marital_status) lowercaseString];
        OFWUser.shared.maritalStatus = OFWUserMaritalStatusFromString(maritalStatus);
    }

    void fyb_ofw_user_set_number_of_children(int children)
    {
        OFWUser.shared.numberOfChildren = children;
    }

    void fyb_ofw_user_set_annual_household_income(int income)
    {
        OFWUser.shared.annualHouseholdIncome = income;
    }

    void fyb_ofw_user_set_education(const char *ethnicity)
    {
        NSString *ethnicityString = [nil_or_string(ethnicity) lowercaseString];
        OFWUser.shared.ethnicity = OFWUserEthnicityFromString(ethnicityString);
    }

    void fyb_ofw_user_set_zip_code(const char *zip_code)
    {
        NSString *zipCode = nil_or_string(zip_code);
        OFWUser.shared.zipcode = zipCode;
    }

    void fyb_ofw_user_set_interests(const char *interests)
    {
        NSString *interestsString = nil_or_string(interests);
        OFWUser.shared.interests = [interestsString componentsSeparatedByString:@","];
    }

    void fyb_ofw_user_set_iap(bool iap)
    {
        OFWUser.shared.iap = iap;
    }

    void fyb_ofw_user_set_iap_amout(double iap_amout)
    {
        OFWUser.shared.iapAmount = iap_amout;
    }

    void fyb_ofw_user_set_number_of_session(double number_of_session)
    {
        OFWUser.shared.numberOfSessions = number_of_session;
    }

    void fyb_ofw_user_set_ps_time(double ps_time)
    {
        OFWUser.shared.psTime = ps_time;
    }

    void fyb_ofw_user_set_last_session(double last_session)
    {
        OFWUser.shared.lastSession = last_session;
    }

    void fyb_ofw_user_set_connection_type(const char *connection_type)
    {
        NSString *connectionType = [nil_or_string(connection_type) lowercaseString];
        OFWUser.shared.connectionType = OFWUserConnectionTypeFromString(connectionType);
    }

    void fyb_ofw_user_set_device(const char *device)
    {
        NSString *deviceString = nil_or_string(device);
        [[FyberSDK instance].user setDataWithKey:@"device" value:deviceString];
    }

    void fyb_ofw_user_set_app_version(const char *app_version)
    {
        NSString *appVersion = nil_or_string(app_version);
        OFWUser.shared.version = appVersion;
    }

    void fyb_ofw_user_set_custom_params(const char *custom_params)
    {
        NSData *data = [[NSString stringWithUTF8String:custom_params] dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary<NSString *, id> *customParams = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];

        OFWUser.shared.customParameters = customParams;
    }

    void fyb_ofw_user_clear_age()
    {
        OFWUser.shared.age = NSNotFound;
    }

    void fyb_ofw_user_clear_number_of_children()
    {
        OFWUser.shared.numberOfChildren = NSNotFound;
    }

    void fyb_ofw_user_clear_annual_income()
    {
        OFWUser.shared.annualHouseholdIncome = NSNotFound;
    }

    void fyb_ofw_user_clear_birthdate()
    {
        OFWUser.shared.birthdate = nil;
    }

    void fyb_ofw_user_clear_location()
    {
        OFWUser.shared.location = nil;
    }
}
