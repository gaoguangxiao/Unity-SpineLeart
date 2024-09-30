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

    //开枪动画
    //骨骼
    //public Transform BoneTransform;

    public AnimationReferenceAsset aim, shoot;

    [Header("Gun")]
    public AudioSource gunSource;

    public AudioClip audioClip;

    /// <summary>
    /// 枪
    /// </summary>
    public FireGGX fireModel;
    //射击方向
    public Vector3 DirPosition;

    [Header("Balance")]

    //射击速度
    public float shootInterval = 0.12f;

    //上次开抢时间
    float lastShootTime;

    //子弹飞行速度
    public float BulletSpeed = 1.0f;

    //打枪回调
    public event System.Action FireEvent;

    /// <summary>
    /// 骨骼位置是否内部更新
    /// </summary>
    public bool EnableChangeBoneLocation;

    //骨骼，由`boneName`在sg获取的骨骼对象
    Spine.Bone bone;

    // Start is called before the first frame update
    void Start()
    {
        //获取骨骼
        bone = sg.Skeleton.FindBone(boneName);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.touches.Length > 0 && EnableChangeBoneLocation)
        //{
        //    Touch touch = Input.touches[0];
        //    Vector3 mousePosition = touch.position;
        //    Vector3 worldMousePosition = cam.ScreenToWorldPoint(mousePosition);
        //    Vector3 skeletonSpacePoint = sg.transform.InverseTransformPoint(worldMousePosition);
        //    skeletonSpacePoint.x *= sg.Skeleton.ScaleX;
        //    skeletonSpacePoint.y *= sg.Skeleton.ScaleY;
        //    bone.SetLocalPosition(skeletonSpacePoint);
        //}

        if (EnableChangeBoneLocation)
        {
            Vector3 worldMousePosition = DirPosition + transform.position;
            Vector3 skeletonSpacePoint = sg.transform.InverseTransformPoint(worldMousePosition);
            skeletonSpacePoint.x *= sg.Skeleton.ScaleX;
            skeletonSpacePoint.y *= sg.Skeleton.ScaleY;
            bone.SetLocalPosition(skeletonSpacePoint);
        }
    }

    public void UpdateBoneVector(Vector3 vector)
    {
        DirPosition = vector;
        //Vector3 worldMousePosition = vector + transform.position;
        //Vector3 skeletonSpacePoint = sg.transform.InverseTransformPoint(worldMousePosition);
        //skeletonSpacePoint.x *= sg.Skeleton.ScaleX;
        //skeletonSpacePoint.y *= sg.Skeleton.ScaleY;
        //bone.SetLocalPosition(skeletonSpacePoint);
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

            sg.AnimationState.Complete += AnimationComplete;

            aimTrack.Complete += AimTrackComplete;

            aimTrack.Complete += ShootTrackComplete;
        }
    }

    void AnimationComplete(Spine.TrackEntry track)
    {
        if (track.Animation.Name == "aim")
        {
            //DidFireEvent();
        }
    }
    //
    void AimTrackComplete(Spine.TrackEntry track)
    {
        DidFireEvent();
    }

    void ShootTrackComplete(Spine.TrackEntry track)
    {
        //v();
    }

    void DidFireEvent()
    {
        if (fireModel)
        {

            float h = Mathf.FloorToInt(DirPosition.x);// 0.8 -> 1 最大整数
            if (h == 0)
                h = Mathf.CeilToInt(DirPosition.x);//最小整数,-0.8 -> -1

            float v = Mathf.FloorToInt(DirPosition.y);// 0.8 -> 1 最大整数
            if (v == 0)
                v = Mathf.CeilToInt(DirPosition.y);//最小整数,-0.8 -> -1
            v = 0;

            fireModel.fireDirection = new Vector3(h, v, 0);

            fireModel.BulletSpeed = BulletSpeed;
            fireModel.Fire();
        }
        if (FireEvent != null) FireEvent();
    }
}
