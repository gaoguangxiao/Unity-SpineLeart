using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;

public class DressNetScript : MonoBehaviour
{
    static string token = "Bearer eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJ0dlZxZDdIdSIsInV1aWQiOiIzMjYyNGEwNzBlYTg0ZDMzYjk5YWU4MzAxYmIyNzMxMyIsInRpbWVzdGFtcCI6MTcyNjE5NjYzODI2Nn0.DcTBqUhkEJsBm2pG-3K4uS4FFM8_j_RxJgVkRrrxQUwLePRYS2xV4Lhvrb7zGjodRCKkcbcZreQDXEllBCYU1g";

    RSResponseV2<DressData> rSResponse;//响应数据

    public Action<DressData> OnDataLoadComplete;

    // Start is called before the first frame update
    void Start()
    {
        RefreshDressData();
    }

    /// <summary>
    /// 刷新数据
    /// </summary>
    public void RefreshDressData()
    {
        StartCoroutine(GetRequestDress("https://gw.risekid.cn/wap/api/buddy/dress"));
    }

    IEnumerator GetRequestDress(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("token", DressNetScript.token);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            string receive = request.downloadHandler.text;
            Debug.Log(request.downloadHandler.text);
            rSResponse = JsonConvert.DeserializeObject<RSResponseV2<DressData>>(receive);
            //打印次数
            //Debug.Log("code is: " + rSResponse.Data);
            OnDataLoadComplete(rSResponse.Data);
        }
    }
}

public partial class DressData
{
    [JsonProperty("lockDressList")]
    public UnlockDressList[] LockDressList { get; set; }

    [JsonProperty("unlockDressList")]
    public UnlockDressList[] UnlockDressList { get; set; }
}

public partial class UnlockDressList
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; }

    [JsonProperty("level")]
    public long Level { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("propCondition")]
    public string PropCondition { get; set; }

    [JsonProperty("propCount")]
    public long PropCount { get; set; }

    [JsonProperty("propId")]
    public long PropId { get; set; }

    [JsonProperty("propName")]
    public string PropName { get; set; }

    [JsonProperty("propType")]
    public long PropType { get; set; }

    [JsonProperty("shineValue")]
    public long ShineValue { get; set; }

    [JsonProperty("spineName")]
    public string SpineName { get; set; }

    [JsonProperty("userPropCount")]
    public long UserPropCount { get; set; }
}