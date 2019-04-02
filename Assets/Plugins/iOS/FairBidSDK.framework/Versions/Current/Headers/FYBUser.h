//
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
//

#import <Foundation/Foundation.h>

@class CLLocation;

typedef NS_ENUM(NSUInteger, FYBUserGender) {
    FYBUserGenderUnknown = 0,
    FYBUserGenderMale,
    FYBUserGenderFemale,
    FYBUserGenderOther
};

typedef NS_ENUM(NSUInteger, FYBUserMaritalStatus) {
    FYBUserMaritalStatusUnknown = 0,
    FYBUserMaritalStatusSingle,
    FYBUserMaritalStatusMarried
};

typedef NS_ENUM(NSUInteger, FYBUserEducation) {
    FYBUserEducationUnknown = 0,
    /// stopped after / still attending 1st-8th grades
    FYBUserEducationGradeSchool,
    /// stopped after / still attending 9th-12th grades
    FYBUserEducationHighSchoolUnfinished,
    /// stopped after high school and is not attending college
    FYBUserEducationHighSchoolFinished,
    /// stopped / still attending college with no college degree finished
    FYBUserEducationCollegeUnfinished,
    /// already achieved an Associate degree
    FYBUserEducationAssociateDegree,
    /// already achieved a Bachelor's degree
    FYBUserEducationBachelorsDegree,
    /// already achieved a graduate-level degree (i.e.: Master's)
    FYBUserEducationGraduateDegree,
    /// already achieved a post-graduate degree (i.e.: Doctorate)
    FYBUserEducationPostGraduateDegree
};


/**
 *  Set the properties on this class to pass information about the user to each of the mediated ad networks.
 * 
 *  Setting any/all of these values is optional. Many ad networks will use this information to serve better-targetted ads.
 */
@interface FYBUser : NSObject

/**
 *  The user's current location.
 *
 *  Networks who use this information: AdColony, AdMob, MoPub
 */
@property (nonatomic, strong, nullable) CLLocation *location;

/**
 *  The user's birthdate.
 *
 *  Some networks will only use the age / birth year / age range of the user, and some will use the full birthdate, so you can set this as accurately as possible and we'll give what we can to each network that asks for it. For instance, if you only know that a user is 25, you can set the birthdate to 25 years from today and that will be sufficient.
 *
 *  Networks who use this information: AdColony, AdMob
 */
@property (nonatomic, strong, nullable) NSDate *birthDate;

/*
 * A list of the user's interests.
 *
 *  Networks who use this information: AdColony, InMobi
 */
@property (nonatomic, strong, nullable) NSArray<NSString *> *interests;

/*
 *  The user's gender.
 *
 *  Networks who use this information: AdColony, AdMob, Fyber, InMobi
 */
@property (nonatomic) FYBUserGender gender;


/**
 *  The user's Postal/ZIP code.
 *
 *  Networks who use this information: AdColony, Fyber, InMobi
 */
@property (nonatomic, strong, nullable) NSString *postalCode;

/**
 *  The user's annual household income.
 *
 *  Networks who use this information: AdColony
 */
@property (nonatomic, strong, nullable) NSNumber *householdIncome;

/**
 *  The user's marital status.
 *
 *  Networks who use this information: AdColony
 */
@property (nonatomic) FYBUserMaritalStatus maritalStatus;

/**
 *  The user's highest-finished education level.
 *
 *  Networks who use this information: AdColony, InMobi
 */
@property (nonatomic) FYBUserEducation educationLevel;

@property(nonatomic, assign) BOOL gdprConsent;

@property(nonatomic, copy, nullable) NSDictionary<NSString *, NSString *> *gdprConsentData;

- (void)clearGDPRConsent;

@end
