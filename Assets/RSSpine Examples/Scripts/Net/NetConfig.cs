using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class NetConfig : MonoBase
{
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        NetManager.Instance.Register(this);

        //主动token相关
        Dictionary<string, object> paramsDicts = new Dictionary<string, object>();
        paramsDicts.Add("key", "access_token");
        Message message = new Message(MessageType.Type_plug, MessageType.getStorage, paramsDicts);
        BridgeScript.Instance.CallApp(message);
    }

   
    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ReceiveMessage(Message message)
    {
        //Debug.Log("NetConfig Receive: " + message.Content);
        if (message.Command == MessageType.getStorage)
        {
            //保存token消息
            GetStorageModel getStorageModel = JsonConvert.DeserializeObject<GetStorageModel>(message.Content);
            //Debug.Log("getStorageModel.value：" + getStorageModel.value);
            if (getStorageModel.key == GetStorageModel.StorageKey.AccessToken)
            {
                NetManager.Instance.SetToken(getStorageModel.value);

                //刷新用户信息
                MC.Instance.SendCustomMessage(new Message(MessageType.Type_UI,MessageType.UI_RefreshData,""));                    
            }
        }
    }
}

