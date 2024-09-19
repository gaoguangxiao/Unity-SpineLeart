using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickType;
using Spine.Unity;
using System;

//namespace Spine.Unity.Examples
//{
public class SpineAssetsManeger
{
    // 默认材质球
    //public Material materialPropertySource;

    // 假设骨骼文件名为 "Skeleton"，并且位于 Resources 文件夹内
    private string NaweiResourcePath = "Spine Skeletons";

    //资产
    private Dictionary<long, SkeletonDataAsset> SpinePrefabsDict;

    private static SpineAssetsManeger instance;

    public static SpineAssetsManeger Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SpineAssetsManeger();
            }
            return instance;
        }
    }

    //public SpineAssetsManeger()
    //{
    //    Debug.Log("SpineAssetsManeger init SpinePrefabsDict");

    //    SpinePrefabsDict = new Dictionary<long, SkeletonDataAsset>();
    //    //npc
    //    SpinePrefabsDict.Add(1, GetSkeletonDataAsset("/character/character_v1_SkeletonData"));
    //    //babu
    //    SpinePrefabsDict.Add(2, GetSkeletonDataAsset("/babu/lanlongbabu_v1_SkeletonData"));
    //    //新巴布
    //    SpinePrefabsDict.Add(3, GetSkeletonDataAsset("/babu/babu_v1_SkeletonData"));
    //    //navi
    //    SpinePrefabsDict.Add(4, GetSkeletonDataAsset("/nawei/nawei_v1_SkeletonData"));
    //    //哥布林
    //    SpinePrefabsDict.Add(5, GetSkeletonDataAsset("/gebulin/gebulin_v1_SkeletonData"));
    //    //巴布吃单词
    //    SpinePrefabsDict.Add(6, GetSkeletonDataAsset("/babuchi/babu_v1_SkeletonData"));
    //    //新巴布吃东西
    //    SpinePrefabsDict.Add(7, GetSkeletonDataAsset("/babu/babuchidongxi_v1_SkeletonData"));
    //    //生气机
    //    SpinePrefabsDict.Add(8, GetSkeletonDataAsset("/shengqiji/shengqiji_SkeletonData"));
    //    //茶壶
    //    SpinePrefabsDict.Add(9, GetSkeletonDataAsset("/chahu/shengqiji_SkeletonData"));
    //    //家-宝剑
    //    SpinePrefabsDict.Add(11, GetSkeletonDataAsset("/buddy/baojian_SkeletonData"));
    //    //家-壁炉
    //    SpinePrefabsDict.Add(12, GetSkeletonDataAsset("/buddy/baojian_SkeletonData"));
    //    //家-窗户
    //    SpinePrefabsDict.Add(13, GetSkeletonDataAsset("/buddy/chuanghu_SkeletonData"));
    //    Debug.Log("SpineAssetsManeger init finish,The Spine Count is: " + SpinePrefabsDict.Keys.Count);
    //}

    public SkeletonDataAsset GetSkeletonDataAsset(string path)
    {
        SkeletonDataAsset asset = Resources.Load<SkeletonDataAsset>(NaweiResourcePath + path);
        return asset;
    }

    public SkeletonDataAsset GetSpineModel(long Id)
    {
        //从spine管理器获取本地所有角色模型

        // 获取Resources文件夹下所有SkeletonDataAsset的路径
        //string skeletonDataAssetPaths = Resources.LoadAll<SkeletonDataAsset>("");


        // 遍历并打印每个SkeletonDataAsset的名称
        //foreach (var pathId in SpinePrefabsDict)
        //{
        //    Debug.Log("The Spines key is" + pathId);
        //}
        //Debug.Log("The Spines keys is: " + SpinePrefabsDict.Keys);

        SkeletonDataAsset asset = SpinePrefabsDict[Id];
        if (asset) return asset;
        //SkeletonDataAsset asset = Resources.Load<SkeletonDataAsset>(NaweiResourcePath);
        Debug.Log("GetSpineModel fail，id is: " + Id);
        return null;
    }

    //V2
    /// <summary>
    /// 通过数据获取
    /// </summary>
    /// <param name="datum"></param>
    /// <returns></returns>
    public SkeletonDataAsset GetSpineModelV2(Datum datum)
    {
        SkeletonDataAsset asset = Resources.Load<SkeletonDataAsset>(NaweiResourcePath + datum.Path);
        return asset;
    }

    //通过本地json、png、alts产生
    public SkeletonDataAsset GetSpineModelV3(Datum datum)
    {
        //Debug.Log("json: " + datum.JSON);
        //数据完善之后可以移除
        if(datum.JSON == null)
        {
            SkeletonDataAsset PathAsset = Resources.Load<SkeletonDataAsset>(NaweiResourcePath + datum.Path);
            return PathAsset;
        }

        if(datum.PNG.Length < 1)
        {
            Debug.Log("No png found");
            return null;
        }
        //Read atlas
        TextAsset atlasText = Resources.Load<TextAsset>(GetSkeletonPath(datum.Atlas));
        //Read image texture
        Texture2D[] textures = GetTexture2DByPath(datum.PNG);
        // 材质
        Material material = GetMaterialByPath(datum.PNG[0]);
        //if (materialPropertySource == null) {
            //return null;
        //}
        //在unity使用spine导出的资源时，需要将spine的图像集转换为unity可以识别的资源，在spine unity中可以使用`SpineAtlasAsset`加载`Atlas`文件，
        SpineAtlasAsset runtimeAtlasAsset = SpineAtlasAsset.CreateRuntimeInstance(atlasText, textures, material, true, null, true);
        Debug.Log("runtimeAtlasAsset: " + runtimeAtlasAsset);
       
        //Read json
        TextAsset skeletonJson = Resources.Load<TextAsset>(GetSkeletonPath(datum.JSON));
        SkeletonDataAsset asset = SkeletonDataAsset.CreateRuntimeInstance(skeletonJson, runtimeAtlasAsset, true);
        Debug.Log("SkeletonDataAsset is :", asset);
       
        return asset;
    }

    //通过图片路径获取Texture2D对象
    Texture2D[] GetTexture2DByPath(string[] paths)
    {
        List<Texture2D> texture2s = new List<Texture2D>();
        foreach (var path in paths)
        {
            Texture2D texture = Resources.Load<Texture2D>(GetSkeletonPath(path));
            texture2s.Add(texture);
        }
        return texture2s.ToArray();
    }

    Material GetMaterialByPath(string path)
    {
        Shader shader = Shader.Find("Spine/Skeleton");
        Material material = new Material(shader);
        //Using textures as materials
        material.mainTexture = Resources.Load<Texture2D>(GetSkeletonPath(path));

        return material;
    }

    string GetSkeletonPath(string path)
    {
        return NaweiResourcePath + path;
    }
}
//}