using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//存储主App域名
public class NetManager: ManagerBase
{
    private static NetManager instance;

    public static NetManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NetManager();
                MC.Instance.Register(instance);
            }
            return instance;
        }
    }

    private string HostURL = "https://gateway-test.risekid.cn";

    public string GetHost()
    {
        return HostURL;
    }

    public void SetHost(string host)
    {
        HostURL = host;
    }


    //token信息
    private string Token;
    public string GetToken()
    {
        return Token;
    }

    public void SetToken(string token)
    {
        Token = token;
    }

    public override int GetMessageType()
    {
        return MessageType.Type_UI;
    }
}
