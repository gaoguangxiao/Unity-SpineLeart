using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickType;
namespace Spine.Unity.Examples
{
    public class SkeletonGraphicScript : MonoBehaviour
    {
        public SkeletonDataAsset LocalSkeletonDataAsset;

        [SpineAnimation(dataField: "skeletonDataAsset")]
        public string startingAnimation;

        [SpineSkin(dataField: "skeletonDataAsset")]
        public string startingSkin = "moren";

        public Material skeletonGraphicMaterial;

        //资产的动作名称
        public ActionList[] animationNames = { };
        //skin list
        public SkinList[] skinNames = { };

        //sping动画组件
        //public SkeletonAnimation SkeletonAnimationImmutable;

        SkeletonAnimation sg;

        //播放速度
        public float TimeScale = 1.0f;

        //If true, the animation will be applied in reverse. Events are not fired when an animation is applied in reverse.</summary>
        public bool Reverse = true;

        //true
        public bool ReverseX = true;

        public Spine.AnimationState spineAnimationState;

        //
        Skin characterSkin;
        //private Skeleton skeleton;

        private void Start()
        {
            StartCoroutine(AddSpineStart(LocalSkeletonDataAsset, startingAnimation, "001"));
        }

        public void ResetSkin()
        {
            UpdateSpineSKin("moren");

        }

        public void UpdateReverseX(bool value)
        {
            Skeleton skeleton = sg.Skeleton;
            ReverseX = value;
            skeleton.ScaleX = -skeleton.ScaleX;
        }

        public void UpdateSpineSKin(string skinName)
        {
            //Debug.Log("更换皮肤" + skinName);
            startingSkin = skinName;
            //skt.SetSkin();
            UpdateCharaterSkin();
            UpdateCombinedSkin(characterSkin);
        }

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

        void UpdateCharaterSkin()
        {
            //sg.Skeleton.SetSkin(startingSkin);
            //v2
            var skeletonData = sg.Skeleton.Data;
            characterSkin = new Skin("character-base");
            characterSkin.AddSkin(skeletonData.FindSkin(startingSkin));
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

        public void UpdateSpineDataAsset(Datum datum)
        {
            SkeletonDataAsset asset = SpineAssetsManeger.Instance.GetSpineModelV2(datum);
            //SkeletonDataAsset asset = SpineAssetsManeger.Instance.GetSpineModelV3(datum);

            //移除旧spine
            DestroySpine();
            //创建新的spine实例
            if (asset) StartCoroutine(AddSpineStart(asset, startingAnimation, datum.Name));
        }

        //public void UpdateSpineDataAssetV2(Datum datum)
        //{
    
        //    SkeletonDataAsset asset = SpineAssetsManeger.Instance.GetSpineModelV2(datum);

        //    skeletonAnimation.skeletonDataAsset = asset;

        //    //if (asset) StartCoroutine(AddSpineStart(asset, startingAnimation, datum.Name));
        //}

        public void PlayAnimationName(string AnimationName, bool loop = false)
        {
            TrackEntry entry = spineAnimationState.SetAnimation(0, AnimationName, loop);
            //Set play order, positive order or reverse order
            entry.Reverse = Reverse;
            //Set playback speed
            entry.TimeScale = TimeScale;
        }

        private void DestroySpine()
        {
            if (sg != null)
                Destroy(sg.gameObject);
        }

        IEnumerator AddSpineStart(SkeletonDataAsset skeletonDataAsset,
            string startingAnimation,
            string gameObjectName)
        {
            if (skeletonDataAsset == null) yield break;
            SkeletonData skeletonData = skeletonDataAsset.GetSkeletonData(false); // Preload SkeletonDataAsset.
                                                                                  //Gets the spine action name
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

            yield return true;// Pretend stuff is happening.
                              //sg = SkeletonGraphic.NewSkeletonGraphicGameObject(skeletonDataAsset, this.transform, skeletonGraphicMaterial); // Spawn a new SkeletonGraphic GameObject.

            sg = SkeletonAnimation.NewSkeletonAnimationGameObject(skeletonDataAsset);
            Skeleton skeleton = sg.Skeleton;

            sg.transform.SetParent(this.transform, false);

            //if (skeletonAnimation != null)
            //{
            //sa.Initialize(false);
            //sa.AnimationState.SetAnimation(0, spineAnimation, true);
            //}

            sg.gameObject.name = gameObjectName;
            spineAnimationState = sg.AnimationState;
            //sg.layoutScaleMode = SkeletonGraphic.LayoutMode.FitInParent;
            //sg.gameObject.transform.localScale = new Vector3(0.9,0.9,0.9);
            // Extra Stuff
            sg.Initialize(false);
            //Debug.Log("The spine skin set is" + startingSkin);
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

            UpdateCharaterSkin();
            UpdateCombinedSkin(characterSkin);

            spineAnimationState.SetAnimation(0, startingAnimation, true);

            Debug.Log("spine load finish");
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