using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AOT;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Unity.VisualScripting;

public class BridgeScript
{
    public String gameObjectName = "MainEventConfig";
    //每次调用
    private static int callbackId = 0;
    //监听callback的返回
    private Dictionary<double, Message> listeners = new Dictionary<double, Message>();

    //定义c# 传递到原生的的委托代理，接收ios原生回传的值
    delegate void CallbackDelegate(string body);

    //注册回调代理
    [DllImport("__Internal")]
    private static extern void registerCallBackDelegate(CallbackDelegate callback);

    //有参无回调
    [DllImport("__Internal")]
    private static extern void didReceiveMessage(string body);

    /// <summary>
    /// C#向OC注册回调代理，unity向OC通信之前必须注册回调函数
    /// </summary>
    public static void CallRegisterCallBackDelegate()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            registerCallBackDelegate(HandleOnCallbackDelegate);
        }
    }

    private static AndroidJavaObject appBridge;

    private static BridgeScript instance;

    public static BridgeScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BridgeScript();
            }
            return instance;
        }
    }

    // unity 2 app
    public void CallApp(Message message)
    {
        callbackId++;
        // 组成bridge消息
        FireEvent(callbackId, message);
    }

    // app 2 unity
    public void postMessage(string body)
    {
        //BridgeCoreObject bridge = JsonConvert.DeserializeObject<BridgeCoreObject>(body);
        //BridgeCoreObject bridge = JsonUtility.FromJson<BridgeCoreObject>(body);
        bool isExitCallBack = IsExist(callbackId);
        //if (!isExitCallBack)
        //{
        //    //没有此回调，为其他平台主动通知，默认通知到UI管理，
        //    Message message = new Message(MessageType.Type_UI, bridge.action, body);
        //    MC.Instance.SendCustomMessage(message);, Action<Message> response
        //}
        //else
        //{
        //    Message message = new Message(MessageType.Type_plug, bridge.action, body);
        //    MC.Instance.SendCustomMessage(message);
        //    //移除回调记录
        //    removeEvent(bridge.callbackId);
        //}
    }

    // 判断回调是否存在
    private bool IsExist(double type)
    {
        return listeners.ContainsKey(type);
    }

    // 缓存事件
    private void AddEvent(double type, Message message)
    {
        listeners.Add(type, message);
    }

    // 缓存事件
    private void FireEvent(double type, Message message)
    {

        BridgeObject request = new BridgeObject();
        request.action = message.Command;
        request.callbackId = (int)callbackId;
        request.data = message.Data;
        Dictionary<string, object> paramsDicts = new Dictionary<string, object>();
        paramsDicts.Add("params", request);

        string body = JsonConvert.SerializeObject(paramsDicts);
        Debug.Log("unity Call App: " + body);

        if (!IsExist(type))
        {
            AddEvent(type, message);
        }

        //"params":request
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {

            didReceiveMessage(body);

        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            appBridge = new AndroidJavaObject("com.readadventure.unity.AppBridge");
            //appBridge.Call("callApp", gameObjectName, JsonConvert.SerializeObject(request));
        }
        else
        {
            if (message.Command == MessageType.getStorage) //其他平台处理
            {
                Dictionary<string, object> paramsResp = new Dictionary<string, object>();
                paramsResp.Add("key", "access_token");
                paramsResp.Add("value", "Bearer eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJCYXpoZkIxMCIsInV1aWQiOiI1ZDg2YThmYjhlNzU0YjVjOTlmZTQxOGViZjc3M2U0" +
                    "MCIsInRpbWVzdGFtcCI6MTcyODU0NjA1Njc5N30.IBJsvTBN7XyOMEHZEGkbQj_YH5kuHDpBpKYNCWI0xPR_-HrnuC0YdFLzP98tvvqS6MH6u3FlTsUSdxr8LdtTrg");
                Message messageparamsResp = new(MessageType.Type_UI, MessageType.getStorage, paramsResp);
                MC.Instance.SendCustomMessage(messageparamsResp);
                BridgeScript.Instance.RemoveEvent(type);
            }
        }
    }

    // 删除缓存事件
    private void RemoveEvent(double type)
    {
        bool isExitCallBack = IsExist(type);
        if (isExitCallBack)
        {
            listeners.Remove(type);
        }
    }

    //通过`MonoPInvokeCallback`向C注册
    [MonoPInvokeCallback(typeof(CallbackDelegate))]
    private static void HandleOnCallbackDelegate(string body)
    {
        Debug.Log("ios call back： " + body);
        BridgeObject bridge = JsonConvert.DeserializeObject<BridgeObject>(body); ;
        Debug.Log("bridge call back 1： " + bridge.data);
        var callBackMessage = BridgeScript.Instance.listeners[bridge.callbackId];
        if (callBackMessage == null)
        {
            Debug.Log("is no exit");
            //没有此回调，为其他平台主动通知，默认通知到UI管理
            Message message = new Message(MessageType.Type_UI, bridge.action, bridge.data);

            //获取原生端token相关信息
            Debug.Log("action为：" + bridge.action);
            // MC.Instance.SendCustomMessage(message);
        }
        else
        {
            //Debug.Log("is exit");

            //查找执行回调 解析参数
            Message message = new Message(MessageType.Type_UI, callBackMessage.Command, bridge.data);

            //Dictionary<string, object> paramsResp = new Dictionary<string, object>();
            //paramsResp.Add("key", "access_token");
            //paramsResp.Add("value", "Bearer eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJCYXpoZkIxMCIsInV1aWQiOiI1ZDg2YThmYjhlNzU0YjVjOTlmZTQxOGViZjc3M2U0" +
            //    "MCIsInRpbWVzdGFtcCI6MTcyODU0NjA1Njc5N30.IBJsvTBN7XyOMEHZEGkbQj_YH5kuHDpBpKYNCWI0xPR_-HrnuC0YdFLzP98tvvqS6MH6u3FlTsUSdxr8LdtTrg");
            //Message messageparamsResp = new(MessageType.Type_UI, MessageType.getStorage, paramsResp);
            //MC.Instance.SendCustomMessage(messageparamsResp);
            //BridgeScript.Instance.RemoveEvent(type);
            Debug.Log("is exit：" + message.Type + message.Command + message.Data);
            MC.Instance.SendCustomMessage(message);

            BridgeScript.Instance.RemoveEvent(bridge.callbackId);
        }
        //bridge.callbackId
        //通过callbackid找到
        //nsdi
        //actions[]
        //Debug.Log("ios call back");
    }
}