//
//  iOSBridgeKit.h
//  UnityToOC
//
//  Created by 高广校 on 2024/8/15.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

//回调函数是指通过函数指针调用的函数，把函数的指针地址作为参数参数传递给另一个函数，指针用来调用其指向的函数时。回调函数是特定事件发生时由另一方调用
//使用函数指针方式进行回调，可以传参
//C#函数的函数指针
typedef void (*CallbackDelegate)(const char*);

@interface iOSBridgeKit : NSObject

+ (iOSBridgeKit *)instance;

@end

NS_ASSUME_NONNULL_END
