using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity.Examples;
using QuickType;

//添加皮肤游戏物体
public class SkinUpdateMono : MonoBehaviour
{
    public SkeletonControlScript skeletonGraphicScript;

    //角色脚本
    //public SkeletonGraphicScript skeletonGraphicScript;

    //角色穿戴网络脚本
    DressNetScript dressNetScript;

    public GameObject PartSkinPrefab;
    private CreateButton PartSkinGamePannel;

    //穿戴列表
    private UnlockDressList[] dressL;

    private void Awake()
    {

        //skeletonGraphicScript.OnSpineLoadComplete = OnSpineLoadComplete;

        dressNetScript = GetComponent<DressNetScript>();
        dressNetScript.OnDataLoadComplete = OnDataLoadComplete;

        PartSkinGamePannel = PartSkinPrefab.GetComponent<CreateButton>();
    }

    // Start is called before the first frame update
    void Start()
    {

        //skeletonGraphicScript.
        PartSkinGamePannel.actionSkin = OnSkinPartClick;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDataLoadComplete(DressData dress)
    {
        //Debug.Log("OnDataLoadComplete");
        List<UnlockDressList> dressLists = new List<UnlockDressList>();
        dressLists.AddRange(dress.UnlockDressList);
        dressLists.AddRange(dress.LockDressList);
   
        dressL = dressLists.ToArray();
        PartSkinGamePannel.CreateSkinV2(dressL);
    }

    void OnSpineLoadComplete(int index)
    {
        //Debug.Log("OnSpineLoadComplete");

        string skinName = GetSKin("moren");
        skeletonGraphicScript.UpdateSpineSKin(skinName);
        //Skins = skeletonGraphicScript.skinNames;
        //获取服装皮肤列表
        //skeletonGraphicScript.skinNames;
        //显示皮肤列表
        //PartSkinGamePannel.CreateSkinV2(GetSkinAll());
    }

    void OnSkinPartClick(GameObject obj)
    {
        //Debug.Log("update part skin" + obj.name);
        //obj.name
        //查找后依赖皮肤全称
        SkinObjScript skin = obj.GetComponent<SkinObjScript>();
        //Debug.Log("update part skin: " + skin.localDress.SpineName);
        string skinName = GetSKin(skin.localDress.SpineName);
        //Debug.Log("update part skinName is : " + skinName);
        skeletonGraphicScript.UpdateMatchSpineSkin(skinName);
    }


    string GetSKin(string spineName)
    {
        string _skinName = null;
        foreach (SkinList skin in skeletonGraphicScript.skinNames)
        {
            if (skin.Name.Contains(spineName))
            {
                //Debug.Log(skin.SubName);
                _skinName = skin.Name;
                break;
            }
        }
        return _skinName;
    }
    /// <summary>
    /// 获取其套装皮肤
    /// </summary>
    /// <returns></returns>
    //SkinList[] GetSkinAll()
    //{
    //    List<SkinList> skinL = new List<SkinList>();
    //    foreach (SkinList skin in skeletonGraphicScript.skinNames)
    //    {
    //        if (skin.AllName != null && skin.AllName.Equals("taozhuang"))
    //        {
    //            skinL.Add(skin);
    //        }
    //    }
    //    return skinL.ToArray();
    //}
}
