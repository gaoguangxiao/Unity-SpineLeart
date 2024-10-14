using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class UpgradeGoodsScript : NetBaseScript<UpgradeGoodsModel[]>
{

    // Start is called before the first frame update
    void Start()
    {
        //RefreshData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void RefreshData()
    {
        TextAsset jsonText = Resources.Load<TextAsset>("JSON/UpdateGoodsJSON");
        //Debug.Log("jsonText is" + jsonText);
        RSResponseV3<UpgradeGoodsModel> Response = JsonConvert.DeserializeObject<RSResponseV3<UpgradeGoodsModel>>(jsonText.text);
        //rSResponse = Response.Data;
        //Debug.Log("Response.Data.lenth is" + Response.Data.Length);
        OnDataLoadComplete(Response.Data);
    }
}
