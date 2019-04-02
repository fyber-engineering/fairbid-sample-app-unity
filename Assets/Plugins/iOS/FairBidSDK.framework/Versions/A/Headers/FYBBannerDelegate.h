//
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
//

#import <Foundation/Foundation.h>
#import <CoreGraphics/CGGeometry.h>

@class FYBBannerView;

NS_ASSUME_NONNULL_BEGIN

@protocol FYBBannerDelegate <NSObject>

@optional

- (void)bannerDidLoad:(FYBBannerView *)banner;

- (void)bannerDidFailToLoad:(NSString *)placementName withError:(NSError *)error;

- (void)bannerDidShow:(FYBBannerView *)banner;

- (void)bannerDidClick:(FYBBannerView *)banner;

- (void)bannerWillPresentModalView:(FYBBannerView *)banner;

- (void)bannerDidDismissModalView:(FYBBannerView *)banner;

- (void)bannerWillLeaveApplication:(FYBBannerView *)banner;

- (void)banner:(FYBBannerView *)banner didResizeToFrame:(CGRect)frame;

@end

NS_ASSUME_NONNULL_END
