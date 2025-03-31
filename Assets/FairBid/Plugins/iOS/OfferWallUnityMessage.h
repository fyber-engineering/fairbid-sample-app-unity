//
//  OfferWallUnityMessage.h
//  DT FairBid
//
//  Copyright © 2023 Fyber. All rights reserved.
//

#import <Foundation/Foundation.h>

typedef NS_ENUM(NSInteger, OfferWallUnityMessageType) {
    OfferWallUnityMessageTypeShowError,
    OfferWallUnityMessageTypeShow,
    OfferWallUnityMessageTypeClose,
    OfferWallUnityMessageTypeVCSSuccess,
    OfferWallUnityMessageTypeVCSError
};

NS_ASSUME_NONNULL_BEGIN

@class OFWError;
@class OFWVirtualCurrencyResponse;
@class OFWVirtualCurrencyErrorResponse;
@interface OfferWallUnityMessage : NSObject

@property (nonatomic, copy, readonly) NSString* payload;
@property (nonatomic, assign, readonly) OfferWallUnityMessageType messageType;

- (void)send;

+ (OfferWallUnityMessage *)showMessage:(NSString *)placementId;
+ (OfferWallUnityMessage *)showErrorMessage:(NSString *)placementId error:(OFWError *)error;
+ (OfferWallUnityMessage *)closeMessage:(NSString *)placementId;
+ (OfferWallUnityMessage *)vcsSuccessMessage:(OFWVirtualCurrencyResponse *)response;
+ (OfferWallUnityMessage *)vcsErrorMessage:(OFWVirtualCurrencyErrorResponse *)response;

@end

NS_ASSUME_NONNULL_END
