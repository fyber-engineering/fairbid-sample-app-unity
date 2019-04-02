//
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
//

#import <UIKit/UIKit.h>
#import "FYBBannerOptions.h"

/**
 *  A view containing a mediated banner ad.
 */
@interface FYBBannerView : UIView

- (instancetype)init NS_UNAVAILABLE;
- (instancetype)initWithCoder:(NSCoder *)aDecoder NS_UNAVAILABLE;
- (instancetype)initWithFrame:(CGRect)frame NS_UNAVAILABLE;

@property (nonatomic, readonly, copy) FYBBannerOptions *options;

@end
