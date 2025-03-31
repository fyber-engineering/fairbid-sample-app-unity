
#import <Foundation/Foundation.h>

extern "C" {
    NSString* nil_or_string(const char *characters);
    char* createCStringCopy(const char* string);
}

@interface NSDictionary(FairBidUnity)

- (NSString *)fyb_stringRepresentation;

@end
