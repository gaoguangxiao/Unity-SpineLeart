using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QuickType;
//struct

public class SkinObjScript : MonoBehaviour
{
    public Image SkinImage;

    public Text SkinTitie;

    public UnlockDressList localDress;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetSkin(SkinList skin)
    {

        //https://file.risekid.cn/app/skin-store/kaijia_1.png
        var path = "skin-store/" + skin.SubName;
        Sprite sprite = Resources.Load<Sprite>(path);
        if (sprite != null)
        {
            //加载本地图片-加载网络图片
            SkinImage.sprite = Resources.Load<Sprite>("skin-store/" + skin.SubName);
        } else
        {
            Debug.Log("load fail is：" + path);
        }
       

        SkinTitie.text = skin.Remark;
    }

    public void SetSkinV2(UnlockDressList dress)
    {

        localDress = dress;

        StartCoroutine(loadImage());
       
        SkinTitie.text = dress.Name;
    }

    IEnumerator loadImage()
    {
        WWW www = new WWW(localDress.Image);

        yield return www;

        if (www != null && string.IsNullOrEmpty(www.error))
        {
            Texture2D texture = www.texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            if (sprite != null)
            {
                //加载本地图片-加载网络图片
                SkinImage.sprite = sprite;
            }
            else
            {
                Debug.Log("load fail is：" + localDress.Image);
            }
        }

    }
}
