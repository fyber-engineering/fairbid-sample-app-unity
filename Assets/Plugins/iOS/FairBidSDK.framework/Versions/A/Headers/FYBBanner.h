//
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "FYBBannerDelegate.h"
#import "FYBBannerOptions.h"

/**
 *  Locations where Heyzap can automatically place the banner.
 *
 *  See `placeBannerInView:position:options:` for details on how this interacts with top/bottom layout guides in iOS 8+.
 */
typedef NS_ENUM(NSUInteger, FYBBannerPosition) {
    /**
     *  Option for placing the banner at the top of the view.
     */
    FYBBannerPositionTop,
    /**
     *  Option for placing the banner at the bottom of the view.
     */
    FYBBannerPositionBottom
};

@interface FYBBanner : NSObject

@property (class, nonatomic, nullable) id <FYBBannerDelegate> delegate;

+ (void)placeBannerInView:(nullable UIView *)view
                 position:(FYBBannerPosition)position
                  options:(nonnull FYBBannerOptions *)options;

+ (void)requestWithOptions:(nonnull FYBBannerOptions *)options;

@end
