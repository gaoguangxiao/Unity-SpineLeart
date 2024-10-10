using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//桥接核心
public class BridgeCoreObject<T>
{
    public int callbackId;
    public string action;
    public int code;
    public string msg;
    //public string content;
    public T data;
}

//不解析data
public class BridgeObject
{
    public double callbackId;
    public int type;//类型
    public string action;
    public int code;
    public string msg;
    public string data;
}