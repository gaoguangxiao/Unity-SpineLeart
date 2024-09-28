using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CharaterGunScript : MonoBehaviour
{
    public Camera cam;

    public SkeletonAnimation sg;

    [SpineBone(dataField: "sg")]
    public string boneName;

    //骨骼
    //public Transform BoneTransform;

    public AnimationReferenceAsset aim, shoot;

    //骨骼，由`boneName`在sg获取的骨骼对象
    Spine.Bone bone;

    public AudioSource gunSource;

    public AudioClip audioClip;

    //射击方向
    public Vector3 DirPosition;

    [Header("Balance")]
    public float shootInterval = 0.12f;

    float lastShootTime;

    // Start is called before the first frame update
    void Start()
    {
        //获取骨骼
        bone = sg.Skeleton.FindBone(boneName);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touches.Length > 0)
        {
            //Touch touch = Input.touches[0];
            //Vector3 mousePosition = touch.position;
            //Vector3 worldMousePosition = cam.ScreenToWorldPoint(mousePosition);
            //Vector3 skeletonSpacePoint = sg.transform.InverseTransformPoint(worldMousePosition);
            //skeletonSpacePoint.x *= sg.Skeleton.ScaleX;
            //skeletonSpacePoint.y *= sg.Skeleton.ScaleY;
            //bone.SetLocalPosition(skeletonSpacePoint);

            //Touch touch = Input.touches[0];
            //Vector3 mousePosition = touch.position; 
        }

        Vector3 worldMousePosition = DirPosition + transform.position;
        Vector3 skeletonSpacePoint = sg.transform.InverseTransformPoint(worldMousePosition);
        skeletonSpacePoint.x *= sg.Skeleton.ScaleX;
        skeletonSpacePoint.y *= sg.Skeleton.ScaleY;
        bone.SetLocalPosition(skeletonSpacePoint);
    }

    //枪口
    public void PlayShoot()
    {

        float currentTime = Time.time;
        if (currentTime - lastShootTime > shootInterval)
        {
            lastShootTime = currentTime;

            //Debug.Log("PlayShoot is:" + bone);

            // Play the shoot animation on track 1.
            Spine.TrackEntry shootTrack = sg.AnimationState.SetAnimation(1, shoot, false);
            shootTrack.MixAttachmentThreshold = 1f;
            shootTrack.SetMixDuration(0f, 0f);
            sg.state.AddEmptyAnimation(1, 0.5f, 0.1f);

            //////// Play the aim animation on track 2 to aim at the mouse target.
            Spine.TrackEntry aimTrack = sg.AnimationState.SetAnimation(2, aim, false);
            aimTrack.MixAttachmentThreshold = 1f;
            aimTrack.SetMixDuration(0f, 0f);
            sg.state.AddEmptyAnimation(2, 0.5f, 0.1f);

            gunSource.clip = audioClip;
            gunSource.Play();
        }
    }
}
