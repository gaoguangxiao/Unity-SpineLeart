using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity.Examples
{
    public class BabuSpineScript : MonoBehaviour
    {

        [SpineAnimation] public string BaoxiongAnimationName;
        [SpineAnimation] public string BaoxiongXianzhiAnimationName;

        SkeletonAnimation skeletonAnimation;

        // Spine.AnimationState and Spine.Skeleton are not Unity-serialized objects. You will not see them as fields in the inspector.
        public Spine.AnimationState spineAnimationState;
        public Spine.Skeleton skeleton;

        // Start is called before the first frame update
        void Start()
        {
            skeletonAnimation = GetComponent<SkeletonAnimation>();
            //AnimationState保存了所有当前播放和队列中的动画状态，每一次update，`AnimationState`都会被更新
            spineAnimationState = skeletonAnimation.AnimationState;
            //skeletonAnimation.BeforeApply
            //skeletonAnimation.UpdateLocal
            //skeletonAnimation.UpdateComplete

            //一个Skeleton存储了对一个skeleton data资产的运用，而skeleton data又引用了多个atlas资产
            //通过Skeleton可以设置皮肤、附件、重置骨骼为setup pose、比例以及翻转整个skeleton.
            skeleton = skeletonAnimation.Skeleton;


            //动画队列空动画
            //spineAnimationState.ClearTracks();

            //动画时间
            spineAnimationState.End += OnSpineAnimationEnd;

            
        }

        public void OnSpineAnimationEnd(TrackEntry trackEntry)
        {
            // Add your implementation code here to react to end events
            Debug.Log("OnSpineAnimationEnd");
        }

        // your delegate method
        void AfterUpdateComplete(ISkeletonAnimation anim)
        {
            // this is called after animation updates have been completed.
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlayAnimationName(string AnimationName)
        {
            spineAnimationState.SetAnimation(0, AnimationName, false);
        }


        public void OnAnimationBaoxiong()
        {
            StartCoroutine(DoBaoxiong());
        }

        public void OnAnimationBaoxiongXianzhi()
        {
            StartCoroutine(DoBaoxiongXianzhi());
        }

        IEnumerator DoBaoxiong()
        {
            //设置一个动画，确定轨道索引、动画名称、是否循环动画
            spineAnimationState.SetAnimation(0, BaoxiongAnimationName, false);
            yield return true;
        }

        IEnumerator DoBaoxiongXianzhi()
        {
            spineAnimationState.SetAnimation(0, BaoxiongXianzhiAnimationName, false);
            yield return true;
        }
    }
}