//
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
//

#import <Foundation/Foundation.h>

typedef NS_ENUM(NSUInteger, FYBLogLevel) {
    FYBLogLevelSilent,
    FYBLogLevelError,
    FYBLogLevelInfo,
    FYBLogLevelVerbose
};

@interface FYBOptions : NSObject

@property(nonatomic, assign) FYBLogLevel logLevel;
@property(nonatomic, assign) BOOL thirdPartyLoggingEnabled;
@property(nonatomic, assign) BOOL autoRequestingEnabled;

//To be moved to a private header or subclass
@property(nonatomic, copy, nullable) NSString *framework;
@property(nonatomic, copy, nullable) NSString *pluginVersion;

@end
