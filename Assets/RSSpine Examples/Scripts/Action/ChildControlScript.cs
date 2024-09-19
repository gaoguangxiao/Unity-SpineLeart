using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickType;
using Spine.Unity.Examples;
using System.Linq;

public class ChildControlScript : MonoBehaviour
{
    //动作列表
    public GameObject ActionPrefab;
    private CreateButton ActionGamePannel;

    //全身列表
    public GameObject AllSKinPrefab;
    private CreateButton AllSkinGamePannel;

    //部分皮肤
    public GameObject PartSKinPrefab;
    private CreateButton PartSkinGamePannel;

    //角色脚本
    public SkeletonGraphicScript skeletonGraphicScript;
    //角色控制
    public ActionControl actionControl;//动作

    //皮肤列表
    private SkinList[] Skins;

    private void Awake()
    {
        ActionGamePannel = ActionPrefab.GetComponent<CreateButton>();
        ActionGamePannel.action = OnActionClick;

        AllSkinGamePannel = AllSKinPrefab.GetComponent<CreateButton>();
        AllSkinGamePannel.action = OnSkinClick;

        PartSkinGamePannel = PartSKinPrefab.GetComponent<CreateButton>();
        PartSkinGamePannel.action = OnSkinPartClick;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 动作面板点击
    /// </summary>
    /// <param name="name"></param>
    void OnActionClick(string name)
    {
        skeletonGraphicScript.TimeScale = actionControl.aniSpeed;
        skeletonGraphicScript.Reverse = actionControl.AniReverse;

        //播放spine 动画
        skeletonGraphicScript.PlayAnimationName(name, actionControl.IsLoop);
    }

    /// <summary>
    /// 皮肤点击
    /// </summary>
    /// <param name="name">皮肤名称</param>
    void OnSkinClick(string name)
    {
        //直接更换皮肤 或者显示子皮肤

        //{moren,moren_touxiang}

        //danci/kong、danci/ci、tupian/nainiu

        //kong
        Debug.Log("OnSkinClick skin name is:" + name);

        int skinComand = 0;//0：角色更换全部皮肤、1：显示部分皮肤列表、2：角色更换部分皮肤
        //在当前皮肤列表查找是否有
        foreach (var skin in Skins)
        {
            if (skin.AllName != null && skin.AllName.Equals(name))
            {
                skinComand = 1;
                break;
            }

            if (skin.SubName != null && skin.SubName.Equals(name))
            {
                skinComand = 2;
                break;
            }
        }

        if (skinComand == 0)
        {
            skeletonGraphicScript.UpdateSpineSKin(name);
        }
        else if (skinComand == 1)
        {
            PartSKinPrefab.SetActive(true);
            PartSkinGamePannel.title.text = "部分部位名称：";
            PartSkinGamePannel.CreateSkin(GetSkinAll(true, 1, name));
        }
        else
        {
            //更换皮肤
            //skeletonGraphicScript.UpdateSpineSKin(name);
        }
    }

    void OnSkinPartClick(string name)
    {
        //查找后依赖皮肤全称
        Debug.Log("update part skin" + name);
        SkinList skin = GetSKin(name);
        Debug.Log("update part skin" + name + "all skin is: " + skin.Name);
        skeletonGraphicScript.MixAndMatchSpineSkin(skin.Name);
    }

    public void CreateUI(Datum datum, int type)
    {

        Skins = datum.SkinList;

        //动作面板
        ActionPrefab.SetActive(type == 0);
        //显示全皮肤
        AllSKinPrefab.SetActive(type == 1);
        PartSKinPrefab.SetActive(false);
        //清理UI
        //ActionGamePannel.DestroyActionUI();
        //AllSkinGamePannel.DestroyActionUI();
        //PartSkinGamePannel.DestroyActionUI();

        if (type == 0)
        {
            ActionGamePannel.CreateUI(datum, type);
        }
        else if (type == 1)
        {
            AllSkinGamePannel.title.text = "全皮肤或者部位名称：";
            AllSkinGamePannel.CreateSkin(GetSkinAll(true, 0));
        }

    }

    SkinList GetSKin(string name)
    {

        SkinList[] skinL = Skins.Where(s => s.Name.Contains(name)).ToArray();
       
        return skinL[0];
    }
    /// <summary>
    /// 皮肤列表
    /// </summary>
    /// <param name="isSplist">有分隔皮肤时，是否依旧用全皮肤名称</param>
    /// <param name="skinType">0：`/`前的皮肤名称、1：`/`后的皮肤名称，对`/`前皮肤有依赖，传入`/`前皮肤名称</param>
    /// <param name="prevSkin"></param>
    /// <returns></returns>
    SkinList[] GetSkinAll(bool isSplist, int skinType, string prevSkin = null)
    {
        List<SkinList> skinL = new List<SkinList>();
        foreach (SkinList skin in Skins)
        {
            if (isSplist)
            {
                string[] skins = skin.Name.Split("/");
                SkinList skin1 = new SkinList();
                if (skins.Length == 2)
                {
                    if (skinType == 0)
                    {
                        skin1.Name = skins[0];
                    }
                    else
                    {
                        if (prevSkin == null)
                        {
                            skin1.Name = skins[1];
                        }
                        else
                        {
                            //Depends on the previous skin name
                            if (skins[0].Equals(prevSkin))
                            {
                                skin1.Name = skins[1];
                            }
                        }
                    }
                    //}
                }
                else
                {
                    //分割失败-无前置依赖时，如果有前置依赖，不处理
                    if (prevSkin == null)
                    {
                        skin1.Name = skin.Name;
                    }
                }
                //当skinType为1时，skin1.name依赖前一个皮肤名称，会出现name为空的情况，增加过滤
                if (skin1.Name != null && !skinL.Exists(f => f.Name == skin1.Name))
                {
                    skinL.Add(skin1);
                }
            }
            else
            {
                skinL.Add(skin);
            }
        }
        return skinL.ToArray();
    }
}
