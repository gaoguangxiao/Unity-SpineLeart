using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Examples;
using UnityEngine.UI;

// 家具升级
public class UpgradePannel : MonoBehaviour
{

    public ScollViewScript scollViewScript;

    // Start is called before the first frame update
    void Start()
    {

        //scollViewScript.OnDataScrollComplete += OnClickUpgrade;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 显示更新面板
    /// </summary>
    public void OnClickShowPannel()
    {
        gameObject.SetActive(true);
        //Debug.Log("添加家具：" + CurrentGoodModel.Name);
        //if (OnDataScrollComplete != null)
        //    OnDataScrollComplete(CurrentGoodModel);

    }

    //伙伴之家升级点击
    public void OnClickUpgrade()
    {
        gameObject.SetActive(false);

        UpgradeGoodsModel model = scollViewScript.CurrentGoodModel;
        //
        Debug.Log("添加家具：" + model.Name);

        if(model.Id == 3)
        {
            //SkeletonDataAsset asset = SpineAssetsManeger.Instance.GetSpineModel(1);
            //OnInitAsset(asset, CharacterFaShil,model.SpineName);
        }
    }


    void OnInitAsset(SkeletonDataAsset skeletonDataAsset, GameObject superGameObject, string spineName)
    {
        SkeletonAnimation sg = SkeletonAnimation.NewSkeletonAnimationGameObject(skeletonDataAsset);
        sg.transform.SetParent(superGameObject.transform, false);        
        sg.gameObject.name = superGameObject.name;
        sg.skeleton.SetSkin(spineName);
        sg.skeleton.SetSlotsToSetupPose();
    }
}