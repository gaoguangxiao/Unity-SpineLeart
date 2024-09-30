using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity.Examples;
using QuickType;
using Spine.Unity;

public class EnemyModel
{
    //角色名称
    public string name;

    //角色奖励分
    public int AwardScore = 2;

    //最大血量
    public float MaxBlood = 1;

    //当前血量
    public float CurrentBlood = 0;

    //奔跑速度
    public float speed = 1.0f;

    //是否死亡
    public bool death = false;
}

//普通角色

//策略智者

//会飞的巴布

//NPC管理
public class EnemyManager : MonoBehaviour
{
    //敌人数组
    //List<EnemyModel> enemyModels = new List<EnemyModel>();

    /// 分数面板
    public UILevelInfoScript levelInfoScript;

    ////分数
    //public float LocalScore = 0;

    //NPC角色
    SkeletonControlScript skeletonControV1;

    //巴布
    SkeletonControlScript skeletonControlV2;

    //收集脸部特征-V1具备
    List<string> tfls = new List<string>();//发型
    List<string> mmls = new List<string>();//眉毛
    List<string> eyels = new List<string>();//眼睛
    List<string> nosels = new List<string>();//鼻子
    List<string> skinColorls = new List<string>();//肤色
    List<string> tzs = new List<string>();//套装
    List<string> roles = new List<string>();//角色

    private void Start()
    {
        
    }

    public void Refresh(SkeletonControlScript script)
    {
        skeletonControV1 = script;
        var SkinList = script.skinNames;
        for (int i = 0; i < SkinList.Length; i++)
        {
            SkinList skin = SkinList[i];
            if (skin.Name.Contains("toufa")) tfls.Add(skin.Name);
            if (skin.Name.Contains("bizi")) nosels.Add(skin.Name);
            if (skin.Name.Contains("fuse")) skinColorls.Add(skin.Name);
            if (skin.Name.Contains("meimao")) mmls.Add(skin.Name);
            if (skin.Name.Contains("yan")) eyels.Add(skin.Name);
            if (skin.Name.Contains("taozhuang")) tzs.Add(skin.Name);
        }

        roles.Add("moren");
        roles.Add("celuezhizhe");
        roles.Add("cihuiyongzhe");
        roles.Add("langduqishi");
        roles.Add("pindufashi");
        roles.Add("yufazhanglao");
    }

    //根据关卡1生成不同角色
    //void CreateSpineV1(SkeletonAnimation skeletonAnimation)
    //{
    //    skeletonControV1 = gameObject.AddComponent<SkeletonControlScript>();

    //    SkeletonDataAsset skeletonDataAsset = SpineAssetsManeger.Instance.GetSpineModel(1);
    //    skeletonAnimation.skeletonDataAsset = skeletonDataAsset;
    //    skeletonAnimation.Initialize(true);//运行时初始化

    //    //spine动作皮肤绑定
    //    skeletonControV1.Refresh(skeletonAnimation);
    //    //将皮肤提取到本地列表
    //    Refresh(skeletonControV1);
    //}

    public EnemyModel RandomCreate()
    {
        //if(skeletonControV1 == null || skeletonControV1.skinNames == null)
        //{
        //    Debug.Log("初始化---脚本");
        //    CreateSpineV1(skeletonAnimation);
        //} else
        //{
        //    //更换spine资产数据
        //    skeletonAnimation.skeletonDataAsset = skeletonControV1.sg.skeletonDataAsset;
        //    skeletonAnimation.Initialize(true);//运行时初始化
        //}

        //skeletonAnimation.skeletonDataAsset = skeletonDataAsset;
        //skeletonAnimation.Initialize(true);//运行时初始化

        EnemyModel enemy = new EnemyModel();
        //生成角色
        string str = RandomRole();
        enemy.name = "str";

        //判断角色类型
        if (str == "moren")
        {
            enemy.MaxBlood = 2;
            enemy.AwardScore = 2;
            RandomHair();
            RandomEyes();
            RandomTZ();
        }
        else if (str == "celuezhizhe")
        {
            enemy.MaxBlood = 4;
            enemy.AwardScore = 8;
        }
        else if (str == "cihuiyongzhe")
        {
            enemy.MaxBlood = 8;
            enemy.AwardScore = 16;
        }
        else if (str == "langduqishi")
        {
            enemy.MaxBlood = 16;
            enemy.AwardScore = 32;
        }
        else if (str == "pindufashi")
        {
            enemy.MaxBlood = 32;
            enemy.AwardScore = 64;
        }
        else if (str == "yufazhanglao")
        {
            enemy.MaxBlood = 64;
            enemy.AwardScore = 128;
        }
        //enemyModels.Add(enemy);
        return enemy;
    }

    public void SkillEnemy(EnemyModel enemy,GameObject gameObject)
    {
        //增加分数
        if(levelInfoScript) levelInfoScript.UpdateScore(enemy.AwardScore);

        Destroy(gameObject);
        //enemyModels.Remove(enemy);
    }

    public string RandomRole()
    {
        int maxRoles = roles.Count - 1;//7关
        if (levelInfoScript != null)
        {
            maxRoles = levelInfoScript.Level - 1;
        }
        
        //Debug.Log("levelInfoScript.Level" + levelInfoScript.Level);
        if (maxRoles > roles.Count - 1)
        {
            maxRoles = 0;//无法生成小兵
            return "moren";
            //return "moren";
        }
        int index = Random.Range(0, maxRoles);
        string tf = roles[index];
        skeletonControV1.InitSpineSKin(tf);
        return tf;
    }


    //发型随机
    public void RandomHair()
    {
        int index = Random.Range(0, tfls.Count - 1);
        string tf = tfls[index];
        skeletonControV1.UpdateMatchSpineSkin(tf);
    }

    //眼睛随机
    public void RandomEyes()
    {
        int index = Random.Range(0, eyels.Count - 1);
        string tf = eyels[index];
        skeletonControV1.UpdateMatchSpineSkin(tf);
    }

    //套装随机
    public void RandomTZ()
    {
        int index = Random.Range(0, tzs.Count - 1);
        string tf = tzs[index];
        skeletonControV1.UpdateMatchSpineSkin(tf);
    }
}
