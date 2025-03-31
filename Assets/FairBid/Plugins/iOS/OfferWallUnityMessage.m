//
//  OfferWallUnityMessage.m
//  DT FairBid
//
//  Copyright © 2023 Fyber. All rights reserved.
//

#import "OfferWallUnityMessage.h"
#import <FairBidSDK/FairBidSDK-Swift.h>

static const char* UNITY_OFW_CLASS = "OfferWallMessageReceiver";

extern void UnitySendMessage(const char *, const char *, const char *);
NSString *NSStringFromError(OFWErrorCode errorCode);

@interface NSDictionary (FairBidUnity)

- (NSString *)fyb_stringRepresentation;

@end

@interface OfferWallUnityMessage (private)

@property (nonatomic, readonly) NSString *methodName;

@end

@implementation OfferWallUnityMessage

- (instancetype)initWithType:(OfferWallUnityMessageType)type payload:(NSString *)payload
{
    self = [super init];
    if (self) {
        _messageType = type;
        _payload = payload;
    }
    return self;
}

- (instancetype)initWithType:(OfferWallUnityMessageType)type payloadDictionary:(NSDictionary *)payload
{
    return [self initWithType:type payload:[payload fyb_stringRepresentation]];
}

+ (OfferWallUnityMessage *)showMessage:(NSString *)placementId {
    return [[OfferWallUnityMessage alloc] initWithType:OfferWallUnityMessageTypeShow payload:placementId];
}

+ (OfferWallUnityMessage *)showErrorMessage:(NSString *)placementId error:(OFWError *)error
{
    NSDictionary *payload = @{
        @"PlacementId": placementId ?: [NSNull null],
        @"Error": NSStringFromError(error.code)
    };

    return [[OfferWallUnityMessage alloc] initWithType:OfferWallUnityMessageTypeShowError payloadDictionary:payload];
}


+ (OfferWallUnityMessage *)closeMessage:(NSString *)placementId {
    return [[OfferWallUnityMessage alloc] initWithType:OfferWallUnityMessageTypeClose payload:placementId];
}

+ (OfferWallUnityMessage *)vcsSuccessMessage:(OFWVirtualCurrencyResponse *) response {
    NSDictionary *payload = @{
        @"DeltaOfCoins": @(response.deltaOfCoins),
        @"LatestTransactionId": response.latestTransactionId ?: [NSNull null],
        @"CurrencyId": response.currencyId ?: [NSNull null],
        @"CurrencyName": response.currencyName ?: [NSNull null],
        @"IsDefault": @(response.isDefault)
    };

    return [[OfferWallUnityMessage alloc] initWithType:OfferWallUnityMessageTypeVCSSuccess payloadDictionary:payload];
}

+ (OfferWallUnityMessage *)vcsErrorMessage:(OFWVirtualCurrencyErrorResponse *) response {
    NSDictionary *payload = @{
        @"Error": NSStringFromError(response.error.code),
        @"ServerErrorMessage": response.serverErrorMessage ?: [NSNull null],
        @"CurrencyId": response.currencyId ?: [NSNull null]
    };

    return [[OfferWallUnityMessage alloc] initWithType:OfferWallUnityMessageTypeVCSError payloadDictionary:payload];
}

- (void)send
{
    const char *method = [self.methodName cStringUsingEncoding:NSUTF8StringEncoding];
    const char *payload = [(self.payload ?: @"") cStringUsingEncoding:NSUTF8StringEncoding];
    UnitySendMessage(UNITY_OFW_CLASS, method, payload);
}

- (NSString *)methodName {
    switch (self.messageType) {
        case OfferWallUnityMessageTypeShow:
            return @"DispatchShowCallback";
        case OfferWallUnityMessageTypeClose:
            return @"DispatchClosedCallback";
        case OfferWallUnityMessageTypeShowError:
            return @"DispatchFailedToShowCallback";
        case OfferWallUnityMessageTypeVCSSuccess:
            return @"DispatchVirtualCurrencySuccessfulResponseCallback";
        case OfferWallUnityMessageTypeVCSError:
            return @"DispatchVirtualCurrencyErrorResponseCallback";
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

NSString *NSStringFromError(OFWErrorCode errorCode) {
    switch (errorCode) {
        case OFWErrorCodeNotStarted:
            return @"SDK_NOT_STARTED";
        case OFWErrorCodeNoNetworkConnection:
            return @"CONNECTION_ERROR";
        case OFWErrorCodeInvalidVirtualCurrencyResponse:
            return @"INVALID_VIRTUAL_CURRENCY_RESPONSE";
        case OFWErrorCodeInvalidVirtualCurrencyResponseSignature:
            return @"INVALID_VIRTUAL_CURRENCY_RESPONSE_SIGNATURE";
        case OFWErrorCodeVirtualCurrencyServerReturnedError:
            return @"VIRTUAL_CURRENCY_SERVER_RETURNED_ERROR";
        case OFWErrorCodeSecurityTokenProvided:
            return @"SECURITY_TOKEN_NOT_PROVIDED";
        case OFWErrorCodeInternal:
        default:
            return @"UNKNOWN_ERROR";
    }
}