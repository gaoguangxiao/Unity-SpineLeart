using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class HourseUpgradeScript : MonoBehaviour
{
    public UserInfoScript userInfoScript;

    int CurrentBuddyLevel;
    // Start is called before the first frame update
    void Start()
    {
        userInfoScript.OnDataLoadComplete += OnUserDataLoadComplete;


        //transform.GetChild();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnUserDataLoadComplete(UserData userData)
    {

        CurrentBuddyLevel = userData.BuddyLevel;

        CurrentBuddyLevel = 30;

        Debug.Log("伙伴之家等级：" + CurrentBuddyLevel);

        //获取环境可加载等级名称
        int childCount = transform.childCount;
        Debug.Log("child count is: " + childCount);

        for (int i = 0; i < childCount; i++)
        {
            GameObject itemContent = transform.GetChild(i).gameObject;

            if (int.TryParse(itemContent.name, out int nameKey))
            {
                if (nameKey <= CurrentBuddyLevel)
                {
                    Debug.Log("level name：" + nameKey);
                    if (nameKey == 1) CreateCharaterSkeleton(4, itemContent);
                    else if (nameKey == 2) CreateBuddySkeleton(13, itemContent, "chuanghu");
                    else if (nameKey == 3) CreateCharaterSkeleton(1, itemContent, "pindufashi");
                    else if (nameKey == 4) CreateBuddySkeleton(12, itemContent, "bilu");
                    else if (nameKey == 5) CreateCharaterSkeleton(1, itemContent, "cihuiyongzhe");
                    else if (nameKey == 6) CreateBuddySkeleton(14, itemContent, "guanzi");
                    else if (nameKey == 7) CreateCharaterSkeleton(1, itemContent, "yufazhanglao");
                    else if (nameKey == 8) CreateBuddyIamge(nameKey, itemContent, "Frame 20");//普通沙发Frame 20
                    else if (nameKey == 9) CreateCharaterSkeleton(1, itemContent, "celuezhizhe");
                }
            }
            else
            {
                //Debug.Log("转换失败");
            }
            
        }

    }

    //
    void CreateBuddyIamge(int Id, GameObject posiotnPosition, string imageName)
    {
        //Debug.Log("imageName is: " + imageName);
        //new GameObject(Id).AddComponent
        SpriteRenderer sr = new GameObject(imageName).AddComponent<SpriteRenderer>();
        var path = "Static/jiaju/" + imageName;

        //Debug.Log("path is: " + path);

        Sprite sprite = Resources.Load<Sprite>(path);

        //Debug.Log("sprite is: " + sprite);

        sr.sprite = sprite;

        sr.transform.SetParent(posiotnPosition.transform, false);
    }

    /// <summary>
    /// 创建NPC角色
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="posiotnPosition"></param>
    /// <param name="skinName"></param>
    void CreateCharaterSkeleton(int Id, GameObject posiotnPosition, string skinName = "moren")
    {
        SkeletonDataAsset asset = SpineAssetsManeger.Instance.GetSpineModel(Id);
        OnInitAsset(asset, posiotnPosition, skinName);
    }

    /// <summary>
    /// 创建家具
    /// </summary>
    /// <param name="index"></param>
    /// <param name="posiotnPosition"></param>
    /// <param name="animaiionName"></param>
    void CreateBuddySkeleton(int Id, GameObject posiotnPosition, string animaiionName)
    {
        SkeletonDataAsset asset = SpineAssetsManeger.Instance.GetSpineModel(Id);
        SkeletonAnimation sg = OnInitAsset(asset, posiotnPosition);
        sg.AnimationState.SetAnimation(0, animaiionName, true);
    }

    SkeletonAnimation OnInitAsset(SkeletonDataAsset skeletonDataAsset, GameObject superGameObject, string spineName = "default")
    {
        SkeletonAnimation sg = SkeletonAnimation.NewSkeletonAnimationGameObject(skeletonDataAsset);
        sg.transform.SetParent(superGameObject.transform, false);
        sg.gameObject.name = superGameObject.name;

        sg.skeleton.SetSkin(spineName);
        sg.skeleton.SetSlotsToSetupPose();
        return sg;
    }
}
