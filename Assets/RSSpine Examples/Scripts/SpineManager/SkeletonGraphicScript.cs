using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickType;
using System;


namespace Spine.Unity.Examples
{
    public class SkeletonGraphicScript : MonoBehaviour
    {
        public SkeletonDataAsset skeletonDataAsset;


        SkeletonAnimation sg;

        //[SpineAnimation(dataField: "skeletonDataAsset")]
        string startingAnimation;

        //[SpineSkin(dataField: "skeletonDataAsset")]
        string startingSkin = "moren";

        public Material skeletonGraphicMaterial;

        //资产的动作名称
        public ActionList[] animationNames = { };
        //skin list
        public SkinList[] skinNames = { };

        //SkeletonGraphic sg;
    
        //播放速度
        public float TimeScale = 1.0f;

        //If true, the animation will be applied in reverse. Events are not fired when an animation is applied in reverse.</summary>
        public bool Reverse = true;

        //true，朝右，false朝左
        public bool ReverseX = true;

        public Spine.AnimationState spineAnimationState;

        //
        Skin characterSkin;

        public Action<int> OnSpineLoadComplete;

        private void Start()
        {
            if (skeletonDataAsset != null)
            {
                //
                StartCoroutine(AddSpineStart(skeletonDataAsset, startingAnimation, startingSkin, "001"));
            }
        }

        /// <summary>
        /// 重置皮肤
        /// </summary>
        public void ResetSkin()
        {
            UpdateSpineSKin("moren");

        }

        /// <summary>
        /// 翻转Sleketon
        /// </summary>
        /// <param name="value"></param>
        public void UpdateReverseX(bool value)
        {
            if (value == ReverseX) return;

            Skeleton skeleton = sg.Skeleton;
            ReverseX = value;
            skeleton.ScaleX = -skeleton.ScaleX;
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
        public void MixAndMatchSpineSkin(string skinName)
        {
            var skeletonData = sg.Skeleton.Data;

            Skin resultCombinedSkin = new Skin("character-combined");
            //Adds a new skin to the previous skin
            resultCombinedSkin.AddSkin(characterSkin);

            //组合皮肤追加皮肤
            resultCombinedSkin.AddSkin(skeletonData.FindSkin(skinName));

            UpdateCombinedSkin(resultCombinedSkin);
        }

        /// <summary>
        /// 更换spine资产对象
        /// </summary>
        /// <param name="datum"></param>
        public void UpdateSpineDataAsset(Datum datum)
        {
            //SkeletonDataAsset asset = SpineAssetsManeger.Instance.GetSpineModelV2(datum);
            SkeletonDataAsset asset = SpineAssetsManeger.Instance.GetSpineModelV3(datum);

            //移除旧spine
            DestroySpine();

            SkeletonData skeletonData = asset.GetSkeletonData(false);

            InitSkinSkeletonData(skeletonData);

            //创建新的spine实例
            if (asset) StartCoroutine(AddSpineStart(asset, startingAnimation,startingSkin, datum.Name));
        }

        public void SetSpineDataAsset(SkeletonDataAsset asset) {

            Debug.Log("spine load finish");

            //移除旧spine
            DestroySpine();

            SkeletonData skeletonData = asset.GetSkeletonData(false);

            InitSkinSkeletonData(skeletonData);

            //创建新的spine实例
            if (asset) StartCoroutine(AddSpineStart(asset, startingAnimation, startingSkin, "001"));
        }


        //手动访问骨骼变换
        void BoneRotation()
        {
            //或获取骨骼名称
            //sg.skeleton.FindBone();

        }

        /// <summary>
        /// 设置动画
        /// </summary>
        /// <param name="AnimationName">动画名字</param>
        /// <param name="loop"></param>
        public void PlayAnimationName(string AnimationName, bool loop = false)
        {
            TrackEntry entry = spineAnimationState.SetAnimation(0, AnimationName, loop);
            //Set play order, positive order or reverse order
            entry.Reverse = Reverse;
            //Set playback speed
            entry.TimeScale = TimeScale;

           
        }

        void PlayAnimationNameV2(string AnimationName, bool loop = false)
        {
            TrackEntry entry = spineAnimationState.AddAnimation(0, AnimationName, loop,0.1f);

            //Set play order, positive order or reverse order
            entry.Reverse = Reverse;
            //Set playback speed
            entry.TimeScale = TimeScale;

            //spineAnimationState.AddEmptyAnimation()
            //spineAnimationState.ClearTrack();
            //spineAnimationState.ClearTracks();
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
            if (animationNames.Length > 0)
                startingAnimation = animationNames[0].Name;

            //Get the spine animation skin
            List<SkinList> sks = new List<SkinList>();
            foreach (var skin in skeletonData.Skins)
            {
                SkinList skinList = new SkinList();
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

            //Skin first as the default implementation
            if (skinNames.Length > 0)
                startingSkin = skinNames[0].Name;

        }

        IEnumerator AddSpineStart(SkeletonDataAsset skeletonDataAsset,
            string startingAnimation,
            string skinName,
            string gameObjectName)
        {
            //sg = SkeletonGraphic.NewSkeletonGraphicGameObject(skeletonDataAsset, this.transform, skeletonGraphicMaterial); // Spawn a new SkeletonGraphic GameObject.

            sg = SkeletonAnimation.NewSkeletonAnimationGameObject(skeletonDataAsset);
            sg.transform.SetParent(this.transform, false);

            Skeleton skeleton = sg.Skeleton;
            sg.gameObject.name = gameObjectName;
            spineAnimationState = sg.AnimationState;
            //sg.layoutScaleMode = SkeletonGraphic.LayoutMode.FitInParent;
            // Extra Stuff
            sg.Initialize(false);

            if (!ReverseX)
            {
                skeleton.ScaleX = -skeleton.ScaleX;
            }

            //监听动画完成和结束
            //spineAnimationState.Start += OnSpineAnimationStart;
            //spineAnimationState.Interrupt += OnSpineAnimationInterrupt;
            //spineAnimationState.End += OnSpineAnimationEnd;
            //spineAnimationState.Dispose += OnSpineAnimationDispose;
            //spineAnimationState.Complete += OnSpineAnimationComplete;

            //改变缩放比例
            //skeleton.ScaleX = 0.5f;
            //skeleton.ScaleY = 0.5f;

            //获取事件
                    //[SpineEvent(dataField: "sg", fallbackToTextField: true)]
        //public string eventName;

            UpdateCharaterSkin(skinName);
            UpdateCombinedSkin(characterSkin);

            spineAnimationState.SetAnimation(0, startingAnimation, true);

            Debug.Log("spine load finish");

            yield return true;// Pretend stuff is happening.

            if (OnSpineLoadComplete != null) OnSpineLoadComplete(0);
        }

        private void DestroySpine()
        {
            if (sg != null)
                Destroy(sg.gameObject);
        }

        void UpdateCharaterSkin(string skinName)
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

        void InitSkeletonGraphic()
        {

        }

        //Add your implementation code here to react to start events
        public void OnSpineAnimationStart(TrackEntry trackEntry)
        {

        }

        public void OnSpineAnimationInterrupt(TrackEntry trackEntry)
        {
            // Add your implementation code here to react to interrupt events
        }
        public void OnSpineAnimationEnd(TrackEntry trackEntry)
        {
            // Add your implementation code here to react to end events
        }
        public void OnSpineAnimationDispose(TrackEntry trackEntry)
        {
            // Add your implementation code here to react to dispose events
        }
        public void OnSpineAnimationComplete(TrackEntry trackEntry)
        {
            // Add your implementation code here to react to complete events
            Debug.Log("Add your implementation code here to react to complete events");
        }
    }


    //void InitSkeletonAnimation()
    //   {
    //	//SkeletonAnimation具备
    //	//sg.transform.localPosition = Random.insideUnitCircle * 6f;
    //	sg.transform.SetParent(this.transform, false);
    //}

}