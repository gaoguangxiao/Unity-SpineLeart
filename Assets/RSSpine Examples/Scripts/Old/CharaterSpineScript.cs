using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickType;

namespace Spine.Unity.Examples
{
    public class CharaterSpineScript : MonoBehaviour
    {

        // Spine.AnimationState and Spine.Skeleton are not Unity-serialized objects. You will not see them as fields in the inspector.
        public Spine.AnimationState spineAnimationState;
        public Spine.Skeleton skeleton;

        SkeletonAnimation skeletonAnimation;

        // Start is called before the first frame update
        void Start()
        {
            skeletonAnimation = GetComponent<SkeletonAnimation>();
            spineAnimationState = skeletonAnimation.AnimationState;
        }

        public void PlayAnimationName(string AnimationName)
        {
            spineAnimationState.SetAnimation(0, AnimationName, false);
        }

        public void UpdateSpineDataAsset(Datum datum)
        {
            //移除旧spine
            //DestroySpine();

            SkeletonDataAsset asset = SpineAssetsManeger.Instance.GetSpineModelV2(datum);

            skeletonAnimation.skeletonDataAsset = asset;
            //
            //if (asset) StartCoroutine(AddSpineStart(asset, startingAnimation, datum.Name));
        }
    }
}