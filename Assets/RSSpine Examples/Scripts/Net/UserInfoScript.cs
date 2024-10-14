using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using Newtonsoft.Json;

public class UserInfoScript : NetBaseScript<UserData>
{
    
    // Start is called before the first frame update
    void Start()
    {
        NetManager.Instance.Register(this);
    }

    public override void RefreshData()
    {
        StartCoroutine(GetRequestData(NetManager.Instance.GetHost() + "/wap/api/user/info"));
    }

    IEnumerator GetRequestData(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("token", NetManager.Instance.GetToken());
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            string receive = request.downloadHandler.text;
            Debug.Log(request.downloadHandler.text);
            rSResponse = JsonConvert.DeserializeObject<RSResponseV2<UserData>>(receive);
            //Debug.Log("rSResponse.data: " + rSResponse.Data);
            //Debug.Log("DressUpContent" + rSResponse.Data.DressUpContent);
            //Debug.Log("FaceContent lenth:" + rSResponse.Data.FaceContent.Length);
            OnDataLoadComplete(rSResponse.Data);
        }
    }

    public override void ReceiveMessage(Message message)
    {
        //Debug.Log("UserInfoScript-ReceiveMessage:" + message.Command + message.Content);
        if (message.Command == MessageType.UI_RefreshData)
        {
            RefreshData();
        }
    }
}

public partial class UserData
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("userNo")]
    public string UserNo { get; set; }

    [JsonProperty("phone")]
    public string Phone { get; set; }

    [JsonProperty("nickname")]
    public string Nickname { get; set; }

    [JsonProperty("avatar")]
    public Uri Avatar { get; set; }

    [JsonProperty("gender")]
    public long Gender { get; set; }

    [JsonProperty("registeredTime")]
    public DateTimeOffset RegisteredTime { get; set; }

    [JsonProperty("status")]
    public long Status { get; set; }

    [JsonProperty("type")]
    public long Type { get; set; }

    [JsonProperty("level")]
    public long Level { get; set; }

    [JsonProperty("isVip")]
    public long IsVip { get; set; }

    [JsonProperty("vipExpireTime")]
    public DateTimeOffset VipExpireTime { get; set; }

    [JsonProperty("vipType")]
    public long VipType { get; set; }

    [JsonProperty("birthday")]
    public string Birthday { get; set; }

    [JsonProperty("age")]
    public long Age { get; set; }

    [JsonProperty("coinCount")]
    public long CoinCount { get; set; }

    [JsonProperty("diamondCount")]
    public long DiamondCount { get; set; }

    [JsonProperty("isInWhiteList")]
    public long IsInWhiteList { get; set; }

    [JsonProperty("isInitFinish")]
    public long IsInitFinish { get; set; }

    [JsonProperty("buddyLevel")]
    public long BuddyLevel { get; set; }

    [JsonProperty("dressUpContent")]
    public DressUpContent DressUpContent { get; set; }

    [JsonProperty("faceContent")]
    public FaceContent[] FaceContent { get; set; }

    [JsonProperty("activeDay")]
    public long ActiveDay { get; set; }

    [JsonProperty("geo")]
    public Geo Geo { get; set; }

    [JsonProperty("visitContent")]
    public string VisitContent { get; set; }

    [JsonProperty("riseUserPhone")]
    public string RiseUserPhone { get; set; }

    [JsonProperty("riseUserName")]
    public string RiseUserName { get; set; }

    [JsonProperty("isPay")]
    public long IsPay { get; set; }

    [JsonProperty("setting")]
    public Dictionary<string, long> Setting { get; set; }
}

public partial class DressUpContent
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("spineName")]
    public string SpineName { get; set; }

    [JsonProperty("image")]
    public Uri Image { get; set; }

    [JsonProperty("shineValue")]
    public long ShineValue { get; set; }
}

public partial class FaceContent
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("spineName")]
    public string SpineName { get; set; }

    [JsonProperty("type")]
    public long Type { get; set; }
}

public partial class Geo
{
    [JsonProperty("userId")]
    public long UserId { get; set; }

    [JsonProperty("provinceId")]
    public long ProvinceId { get; set; }

    [JsonProperty("province")]
    public string Province { get; set; }

    [JsonProperty("cityId")]
    public long CityId { get; set; }

    [JsonProperty("city")]
    public string City { get; set; }
}