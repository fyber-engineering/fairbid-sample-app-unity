//
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

/** FYBShowOptions allows you to pass options to configure how ads are shown */
@interface FYBShowOptions : NSObject

/**
 *  @discussion A UIViewController that should present the ad being shown. If not specified the application's key window's root view controller is used.
 */
@property (nonatomic, weak, null_resettable) UIViewController *viewController;

@end
