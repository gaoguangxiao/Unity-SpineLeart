//
//  iOSBridgeKit.m
//  UnityToOC
//
//  Created by 高广校 on 2024/8/15.
//

#import "iOSBridgeKit.h"

static iOSBridgeKit * _instance = nil;

@interface iOSBridgeKit()

//用于unity的回调指针
//使用函数指针方式进行回调，可以传参
@property (nonatomic, assign) CallbackDelegate backDelegate;

@end

@implementation iOSBridgeKit

+ (iOSBridgeKit *)instance
{
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _instance = [[iOSBridgeKit alloc] init];
    });
    return _instance;
}

//注册事件
- (void)reiegerBridge:(CallbackDelegate)callback {
    _backDelegate = callback;
}


//typedef void(^)
//收到事件
- (void)didReceiveMessage:(NSString *)name body:(NSString *)body callBack:(CallbackDelegate)callBack{
    //执行一些操作name就是方法
    NSLog(@"name: %@",name);
    
    NSLog(@"body: %@",body);
    //    [NSString stringWithUTF8String:name]
    NSString *string = @"Hello, World!";
    const char *cString = [string UTF8String];
    callBack(cString);
}

- (void)didReceiveMessage:(NSString *)body{
    //执行一些操作name就是方法
//    NSLog(@"name: %@",name);
    NSLog(@"body: %@",body);
    //    [NSString stringWithUTF8String:name]
    NSString *string = @"Hello, World!";
    const char *cString = [string UTF8String];
//    callBack(cString);
    self.backDelegate(cString);
}

//包含code msg data等字段
- (void)callOtherPlatform:(NSString *)content {
    //unity中有一个监听交互的游戏对象
    // 假设在Unity中有一个名为"GameObjectName"的对象，并且该对象附加了名为UnityMessageReceiver的脚本
//    UnitySendMessage("PlugConfig", "ReceiveMessageFromiOS",content);
}

//2、用`extern "C"`声明，可以被C#调用
extern "C" {
   
    //无参无返回
    void callAppMessage() {
        // [iOSBridgeKit.instance didReceiveMessage:[NSString stringWithUTF8String:name]
                                            // body:[NSString stringWithUTF8String:body]];
    }
    
    
    //发送 回调函数是指通过函数指针调用的函数，把函数的指针地址作为参数参数传递给另一个函数，指针用来调用其指向的函数时。回调函数是特定事件发生时由另一方调用
//    public delegate void CallbackDelegate(string message);
//    typedef void (*BridgeCallback)(char);
    void didReceiveMessage(char *body) {
        [iOSBridgeKit.instance didReceiveMessage:[NSString stringWithUTF8String:body]];
    }
    
    //
//    void didOnlyReceiveMessage(const char *name, const char *body,void(*CallbackDelegate)(const char *body)) {
//        [iOSBridgeKit.instance didReceiveMessage:[NSString stringWithUTF8String:name]
//                                            body:[NSString stringWithUTF8String:body]];
//    }
    
    //接受事件
    //C#函数的函数指针
    typedef void (*cs_callback)(char);
    void registerCallBackDelegate(CallbackDelegate callback) {
        [iOSBridgeKit.instance reiegerBridge:callback];
    }
}

@end
