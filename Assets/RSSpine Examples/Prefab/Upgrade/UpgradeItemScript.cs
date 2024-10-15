using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class UpgradeGoodsModel
{
    [JsonProperty("Id")]
    public long Id { get; set; }

    [JsonProperty("goods")]
    public string Goods { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("spineName")]
    public string SpineName { get; set; }

    public bool IsSelected = false;//是否选中
}

public class UpgradeItemScript : MonoBehaviour
{
    public Image GoodsUrlBack;//物品图片

    public RectTransform backImageRectTransform;

    public Image GoodsUrl;//物品图片

    public Text GoodIndex;//物品索引

    public Text GoodType;//物品类型

    public Text GoodName;//物品名称

    public UpgradeGoodsModel LocalGoodsModel;

    //private void Start()
    //{
        
    //}

    public void SetGoods(UpgradeGoodsModel goodsModel)
    {
        LocalGoodsModel = goodsModel;

        GoodIndex.text = goodsModel.Id.ToString();

        GoodType.text = goodsModel.Type;

        GoodName.text = goodsModel.Name;

        ReloadData();

        if (string.IsNullOrEmpty(goodsModel.Goods)) return;
        StartCoroutine(LoadImage(goodsModel.Goods));
    }

    public void ReloadData()
    {
        //Debug.Log("backImageRectTransform is: " + backImageRectTransform);

        if (LocalGoodsModel.IsSelected)
        {
            backImageRectTransform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        }
        else
        {
            backImageRectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    IEnumerator LoadImage(string imageUrl)
    {
       
        WWW www = new WWW(imageUrl);

        yield return www;

        if (www != null && string.IsNullOrEmpty(www.error))
        {
            Texture2D texture = www.texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            if (sprite != null)
            {
                //加载本地图片-加载网络图片
                GoodsUrl.sprite = sprite;
            } else
                Debug.Log("load fail is：" + imageUrl);
        }
    }

}
