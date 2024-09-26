using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity {

    public class SkeletonBoneScript : MonoBehaviour
    {
        public SkeletonAnimation skeletonAnimation;

        [SpineBone(dataField: "skeletonAnimation")]
        public string boneName;

        public Camera cam;


        Bone bone;

        //尾巴摇动 看向

        // Start is called before the first frame update
        void Start()
        {
            bone = skeletonAnimation.Skeleton.FindBone(boneName);
            Debug.Log("bone is:" + bone);

            //使用Spine GameObject Transform获取骨骼的Unity World位置。UpdateWorldTransform需要被调用，以返回正确的、更新的值
            Vector3 worldPosition = bone.GetWorldPosition(skeletonAnimation.transform);
            Debug.Log("worldPosition is:" + worldPosition);
        }

        // Update is called once per frame
        void Update()
        {
            //Vector3 mousePosition = Input.mousePosition;

            ////Debug.Log("mousePosition is:" + mousePosition);

            //Vector3 worldMousePosition = cam.ScreenToWorldPoint(mousePosition);
            //Debug.Log("worldMousePosition is:" + worldMousePosition);

            //Vector3 skeletonSpacePoint = skeletonAnimation.transform.InverseTransformPoint(worldMousePosition);
            //Debug.Log("skeletonSpacePoint is:" + skeletonSpacePoint);
            //skeletonSpacePoint.x *= skeletonAnimation.Skeleton.ScaleX;
            //skeletonSpacePoint.y *= skeletonAnimation.Skeleton.ScaleY;
            //bone.SetLocalPosition(skeletonSpacePoint);
        }
    }
}