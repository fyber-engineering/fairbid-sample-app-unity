//

//  Copyright © 2023 Fyber. All rights reserved.
//

#import "OfferWallUnityDelegate.h"
#import "OfferWallUnityMessage.h"

extern void UnitySendMessage(const char *, const char *, const char *);


@implementation OfferWallUnityDelegate

- (void)didShow:(NSString * _Nullable)placementId
{
    [[OfferWallUnityMessage showMessage:placementId] send];
}

- (void)didDismiss:(NSString * _Nullable)placementId
{
    [[OfferWallUnityMessage closeMessage:placementId] send];
}

- (void)didFailToShow:(NSString * _Nullable)placementId error:(OFWError * _Nonnull)error
{
    [[OfferWallUnityMessage showErrorMessage:placementId error:error] send];
}

- (void)didReceiveResponse:(OFWVirtualCurrencyResponse * _Nonnull)response
{
    [[OfferWallUnityMessage vcsSuccessMessage:response] send];
}

- (void)didFailWithError:(OFWVirtualCurrencyErrorResponse * _Nonnull)error
{
    [[OfferWallUnityMessage vcsErrorMessage:error] send];
}

@end
