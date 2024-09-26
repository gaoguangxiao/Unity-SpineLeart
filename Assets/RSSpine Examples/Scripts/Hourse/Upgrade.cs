using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

//升级
namespace Spine.Unity.Examples
{
    public class Upgrade : MonoBehaviour
    {

        //家具-窗户-位置
        public GameObject FurnitureWindow;

        //可升级列表
        private CreateButton UpgradeGamePannel;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        //伙伴之家升级点击
        public void OnClickUpgrade()
        {

            //添加家具
            SkeletonDataAsset asset = SpineAssetsManeger.Instance.GetSpineModel(13);
            SkeletonAnimation sg = OnInitAsset(asset, FurnitureWindow);

            sg.AnimationState.SetAnimation(0, "chuanghu", true);
        }


        SkeletonAnimation OnInitAsset(SkeletonDataAsset skeletonDataAsset, GameObject superGameObject)
        {
            SkeletonAnimation sg = SkeletonAnimation.NewSkeletonAnimationGameObject(skeletonDataAsset);
            sg.transform.SetParent(superGameObject.transform, false);

            sg.gameObject.name = superGameObject.name;

            return sg;
            
        }
    }
}