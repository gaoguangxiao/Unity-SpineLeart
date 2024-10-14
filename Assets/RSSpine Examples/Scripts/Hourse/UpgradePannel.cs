using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Examples;
using UnityEngine.UI;

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

    //public void OnClickUpgrade()
    //{
    //    Debug.Log("添加家具：" + CurrentGoodModel.Name);
    //    if (OnDataScrollComplete != null)
    //        OnDataScrollComplete(CurrentGoodModel);

    //}

    //伙伴之家升级点击
    public void OnClickUpgrade()
    {

        Debug.Log("添加家具：" + scollViewScript.CurrentGoodModel.Name);
        //添加家具
        //SkeletonDataAsset asset = SpineAssetsManeger.Instance.GetSpineModel(13);
        //SkeletonAnimation sg = OnInitAsset(asset, FurnitureWindow);

        //sg.AnimationState.SetAnimation(0, "chuanghu", true);
    }


    SkeletonAnimation OnInitAsset(SkeletonDataAsset skeletonDataAsset, GameObject superGameObject)
    {
        SkeletonAnimation sg = SkeletonAnimation.NewSkeletonAnimationGameObject(skeletonDataAsset);
        sg.transform.SetParent(superGameObject.transform, false);

        sg.gameObject.name = superGameObject.name;

        return sg;

    }
}