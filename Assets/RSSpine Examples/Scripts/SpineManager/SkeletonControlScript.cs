using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickType;



//控制角色皮肤 动作 骨骼，外界传入对象
namespace Spine.Unity.Examples
{
    public class SkeletonControlScript : MonoBehaviour
    {
        public SkeletonAnimation sg;

        //记录角色之前皮肤效果
        Skin characterSkin;

        SkeletonData skeletonData;

        //action name
        public ActionList[] animationNames = { };

        //skin list
        public SkinList[] skinNames = { };

        // Start is called before the first frame update
        void Start()
        {
            skeletonData = sg.skeletonDataAsset.GetSkeletonData(false);

            InitSkinSkeletonData(skeletonData);

            //初始化默认皮肤
            characterSkin = new Skin("character-base");
            characterSkin.AddSkin(skeletonData.FindSkin(sg.initialSkinName));

            
        }

        /// <summary>
        /// 指定spine皮肤，会移除之前所有皮肤效果
        /// </summary>
        /// <param name="skinName"></param>
        public void UpdateSpineSKin(string skinName)
        {
            //Debug.Log("更换皮肤" + skinName);
            //startingSkin = skinName;
            //skt.SetSkin();
            UpdateCharaterSkin(skinName);
            UpdateCombinedSkin(characterSkin);
        }

        /// <summary>
        /// 混合皮肤，在之前皮肤的基础上叠加
        /// </summary>
        /// <param name="skinName"></param>
        public void UpdateMatchSpineSkin(string skinName)
        {

            Skin resultCombinedSkin = new Skin("character-combined");
            //Adds a new skin to the previous skin
            resultCombinedSkin.AddSkin(characterSkin);

            //组合皮肤追加皮肤
            resultCombinedSkin.AddSkin(skeletonData.FindSkin(skinName));

            UpdateCombinedSkin(resultCombinedSkin);
        }


        private void UpdateCharaterSkin(string skinName)
        {
            var skeletonData = sg.Skeleton.Data;
            characterSkin = new Skin("character-base");
            characterSkin.AddSkin(skeletonData.FindSkin(skinName));
        }

        void UpdateCombinedSkin(Skin resultCombinedSkin)
        {
            Skeleton skeleton = sg.Skeleton;
            //AddEquipmentSkinsTo(resultCombinedSkin);
            skeleton.SetSkin(resultCombinedSkin);
            skeleton.SetSlotsToSetupPose();
            //Record character skin, switch other skin will not overwrite
            characterSkin = resultCombinedSkin;
        }

        /// <summary>
        /// 初始化皮肤动画数据
        /// </summary>
        /// <param name="skeletonData"></param>
        void InitSkinSkeletonData(SkeletonData skeletonData)
        {
            List<ActionList> ans = new List<ActionList>();
            foreach (var animation in skeletonData.Animations)
            {
                ActionList action = new ActionList();
                action.Name = animation.Name;
                ans.Add(action);
            }
            animationNames = ans.ToArray();

            //animation first as the default implementation
            //if (animationNames.Length > 0)
            //startingAnimation = animationNames[0].Name;

            //Get the spine animation skin
            List<SkinList> sks = new List<SkinList>();
            foreach (var skin in skeletonData.Skins)
            {
                SkinList skinList = new SkinList();
                //Debug.Log("default skin is: " + skin.Name);
                //Filter 'default' display
                if (skeletonData.Skins.Count > 1)
                {
                    if (skin.Name != "default")
                    {
                        skinList.Name = skin.Name;

                        string[] skins = skinList.Name.Split("/");
                        if (skins.Length == 2)
                        {
                            skinList.AllName = skins[0];
                            skinList.SubName = skins[1];
                        }
                        //Debug.Log("The spine skin " + skin.Name);
                        sks.Add(skinList);
                    }
                }
                else
                {
                    //only one
                    skinList.Name = skin.Name;
                    sks.Add(skinList);
                }
            }
            skinNames = sks.ToArray();
        }

    }
}