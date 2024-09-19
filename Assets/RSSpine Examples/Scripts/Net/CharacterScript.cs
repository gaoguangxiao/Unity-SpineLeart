using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using QuickType;
using System;
using System.IO;
//using 
public class CharacterScript : MonoBehaviour
{

    RSResponse rSResponse;//响应数据

    //private static CharacterScript instance;

    //public static CharacterScript Instance;

    public Action OnDataLoadComplete;
    //public static CharacterScript Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance = new CharacterScript();
    //        }
    //        return instance;
    //    }
    //}

    //获取角色数据
    public Datum[] GetCharaterData()
    {
        return rSResponse.Data;
    }

    public Datum[] GetCustomData()
    {
        List<Datum> datums = new List<Datum>();

        Datum npc = new Datum();
        npc.Id = 100;
        npc.Name = "NPC";

        //动作列表
        List<ActionList> npcAc = new List<ActionList>();
        //表情列表
        List<ExpressionList> npcExp = new List<ExpressionList>();
        //皮肤列表
        List<SkinList> npcSkin = new List<SkinList>();
        foreach (Datum data in rSResponse.Data)
        {
            if (data.Id > 2 && data.Id < 8)
            {
                foreach (ActionList skin in data.ActionList)
                {
                    if (!npcAc.Exists(f => f.Name == skin.Name)) npcAc.Add(skin);
                }

                foreach (ExpressionList exp in data.ExpressionList)
                {
                    if (!npcExp.Exists(f => f.Name == exp.Name)) npcExp.Add(exp);
                }

                //"_touxiang";
                foreach (SkinList skin in data.SkinList)
                {
                    SkinList newSKin = new SkinList();
                    newSKin.Remark = skin.Remark;
                    newSKin.Name = skin.Name + "_touxiang";
                    npcSkin.AddRange(data.SkinList);
                    npcSkin.Add(newSKin);
                }
            }
        }
        //foreach (ActionList action in npcAc)
        //{
        //    Debug.Log("The NPCAction is: " + action.Name);
        //}
        //foreach (SkinList skin in npcSkin)
        //{
        //    Debug.Log("The NPCSkin is: " + skin.Name);
        //
        npc.ActionList = npcAc.ToArray();
        npc.ExpressionList = npcExp.ToArray();
        npc.SkinList = npcSkin.ToArray();
        datums.Add(npc);

        //
        //修改Navi、巴布皮肤
        //皮肤列表
        List<SkinList> nNPCSkinList = new List<SkinList>();
        SkinList nNPCSkin = new SkinList();
        nNPCSkin.Name = "moren";
        nNPCSkinList.Add(nNPCSkin);

        SkinList nNPCSkin1 = new SkinList();
        nNPCSkin1.Name = "moren_touxiang";
        nNPCSkinList.Add(nNPCSkin1);

        SkinList[] nNPCSkinLists = nNPCSkinList.ToArray();

        Datum nv = new Datum();
        nv.SkinList = nNPCSkinLists;
        datums.Add(nv);
        //bu
        Datum bb = GetBabuData(2);
        bb.SkinList = nNPCSkinLists;
        datums.Add(bb);

        //新babu
        Datum nbb = new Datum();
        nbb.Id = 200;
        nbb.Name = "新巴布";
        nbb.ActionList = bb.ActionList;
        nbb.ExpressionList = bb.ExpressionList;
        nbb.SkinList = nNPCSkinLists;
        datums.Add(nbb);

        //gebulin
        Datum gebulin = new Datum();
        gebulin.Id = 201;
        gebulin.Name = "哥布林4";
        datums.Add(gebulin);

        return datums.ToArray();
    }

    //V3
    public Datum[] GetCharacterData()
    {
        List<Datum> datums = new List<Datum>();

        Datum npc = new Datum();
        npc.Id = 100;
        npc.Name = "NPC";
        datums.Add(npc);

        Datum nv = new Datum();
        nv.Id = 1;
        nv.Name = "纳威";
        datums.Add(nv);
        //bu
        Datum bb = GetBabuData(2);
        datums.Add(bb);

        //新babu
        Datum nbb = new Datum();
        nbb.Id = 200;
        nbb.Name = "新巴布";
        datums.Add(nbb);

        //gebulin
        Datum gebulin = new Datum();
        gebulin.Id = 201;
        gebulin.Name = "哥布林4";
        datums.Add(gebulin);

        Datum bbcWord = new Datum();
        bbcWord.Id = 202;
        bbcWord.Name = "巴布吃单词";
        datums.Add(bbcWord);

        Datum nbbcWord = new Datum();
        nbbcWord.Id = 203;
        nbbcWord.Name = "新巴布吃单词";
        datums.Add(nbbcWord);

        return datums.ToArray();
    }

    //V4
    public Datum[] GetCharacterDataJSON()
    {
        var filePath = Application.dataPath + "/Resources/JSON/charaterJSON.json";

        //读取文件
        StreamReader str = File.OpenText(filePath);
        //string类型的数据常量
        string readData = "";
        //数据保存
        readData = str.ReadToEnd();

        str.Close();

        Debug.Log("readData: " + readData);
        //解析角色JSON.json
        RSResponse re = RSResponse.FromJson(readData);

        //对角色填充动作和皮肤
        //skeletonGraphicScript.UpdateSpineDataAsset(currentDatum);
        //currentDatum.ActionList = skeletonGraphicScript.animationNames;
        //currentDatum.SkinList = skeletonGraphicScript.skinNames;

        return RSResponse.FromJson(readData).Data;

    }

    //v5
    public Datum[] GetCharacterDataJSONV5()
    {
        TextAsset jsonText = Resources.Load<TextAsset>("JSON/charaterJSON");
        RSResponse re = RSResponse.FromJson(jsonText.text);
        return re.Data;
    }

    /// <summary>
    /// 提供角色数据
    /// </summary>
    /// <param name="Id">角色ID</param>
    /// <returns></returns>
    public Datum GetBabuData(long Id)
    {
        Datum babuData = new Datum();
        foreach (Datum data in rSResponse.Data)
        {
            if (data.Id == Id)
            {
                babuData = data;
                break;
            }
        }
        if (babuData == null)
        {
            Debug.Log("没有巴布数据");
            //return;
        }
        return babuData;
    }

    //private void Awake()
    //{
    //    Instance = this;
    //}

    // Start is called before the first frame update
    //private void Start()
    //{
    //RefreshCharacterData();
    //}

    /// <summary>
    /// 刷新角色数据
    /// </summary>
    public void RefreshCharacterData()
    {
        //Debug.Log("RefreshCharacterData");
        StartCoroutine(GetRequestChatConfig("https://gw.risekid.cn/wap/api/character"));
        //GetRequestChatConfigNew("https://gw.risekid.cn/wap/api/character");
    }

    IEnumerator GetRequestChatConfig(string url)
    {
        Debug.Log("GetRequestChatConfig");
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("token", "Bearer eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiIxIiwidXVpZCI6Iks4RWZEWVpEIn0.3P_W60eIb9UdABaYYva4H3e7Na-EMqe8MPGp55VRuxKKVR7Pmarc-5saPlhxG2xz7aDLMqothIinagIyy8piTA");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            string receive = request.downloadHandler.text;
            Debug.Log(request.downloadHandler.text);
            //解析数据并返回
            rSResponse = RSResponse.FromJson(receive);
            //打印次数
            Debug.Log("code is: " + rSResponse.Code);
            OnDataLoadComplete();
        }
    }
}
