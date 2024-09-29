using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity.Examples;
using QuickType;

public class EnemyModel
{
    //角色名称
    public string name;

    //最大血量
    public float MaxBlood = 1;

    //当前血量
    public float CurrentBlood = 0;

    //奔跑速度
    public float speed = 1.0f;

    public bool death = false;
}

//普通角色

//策略智者

//会飞的巴布

//NPC管理
public class EnemyManager : MonoBehaviour
{
    //角色脚本
    public SkeletonControlScript skeletonControlScript;

    //收集脸部特征
    List<string> tfls = new List<string>();//发型
    List<string> mmls = new List<string>();//眉毛
    List<string> eyels = new List<string>();//眼睛
    List<string> nosels = new List<string>();//鼻子
    List<string> skinColorls = new List<string>();//肤色
    List<string> tzs = new List<string>();//套装
    List<string> roles = new List<string>();//角色

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Refresh(SkeletonControlScript script)
    {
        skeletonControlScript = script;

        var SkinList = skeletonControlScript.skinNames;
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


    public EnemyModel RandomAll()
    {

        EnemyModel enemy = new EnemyModel();
        //生成角色
        string str = RandomRole();

        enemy.name = "str";

        //判断角色类型
        if (str == "moren")
        {
            enemy.MaxBlood = 2;

            RandomHair();
            RandomEyes();
            RandomTZ();
        }
        else if (str == "celuezhizhe")
        {
            enemy.MaxBlood = 4;
        }
        else if (str == "cihuiyongzhe")
        {
            enemy.MaxBlood = 6;
        }
        return enemy;
    }

    public string RandomRole()
    {
        int index = Random.Range(0, roles.Count - 1);
        string tf = roles[index];
        skeletonControlScript.InitSpineSKin(tf);
        return tf;
    }


    //发型随机
    public void RandomHair()
    {
        int index = Random.Range(0, tfls.Count - 1);
        string tf = tfls[index];
        skeletonControlScript.UpdateMatchSpineSkin(tf);
    }

    //眼睛随机
    public void RandomEyes()
    {
        int index = Random.Range(0, eyels.Count - 1);
        string tf = eyels[index];
        skeletonControlScript.UpdateMatchSpineSkin(tf);
    }

    //套装随机
    public void RandomTZ()
    {
        int index = Random.Range(0, tzs.Count - 1);
        string tf = tzs[index];
        skeletonControlScript.UpdateMatchSpineSkin(tf);
    }
}
