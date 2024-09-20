using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity.Examples;
using QuickType;
using UnityEngine.UI;

public class PartnerHouseScript : MonoBehaviour
{
    //人物对象
    public GameObject CharaterGameobject;

    //人物创建spine脚本
    private SkeletonGraphicScript skeletonGraphicScript;

    //移动速度
    public float MoveSpeed = 5.0f;

    //目标点
    private Vector3 targeVector;
    //人物移动 -1左 0不移动 1右
    private int IdleStatus = 0;

    //移动背景画布
    public ScrollRect BgScrollRect;

    private void Awake()
    {
        skeletonGraphicScript = CharaterGameobject.GetComponent<SkeletonGraphicScript>();
        skeletonGraphicScript.OnSpineLoadComplete = OnSpineLoadComplete;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnSpineLoadComplete(int index)
    {
        //Debug.Log("OnSpineLoadComplete");
        string skinName = GetSKin("moren");
        skeletonGraphicScript.UpdateSpineSKin(skinName);
    }

    // Update is called once per frame
    void Update()
    {
        //鼠标的点击
        //0左键，1右键，2滚轮
        if (Input.GetMouseButtonDown(0))
        {
            targeVector = Input.mousePosition;

            //Debug.Log("targeVector.x is" + targeVector.x);

            IdleStatus = targeVector.x >= CharaterGameobject.transform.position.x ? 1 : -1;

            //Debug.Log("IdleStatus.x is" + IdleStatus);

            //方向
            skeletonGraphicScript.UpdateReverseX(IdleStatus != 1);

            skeletonGraphicScript.PlayAnimationName("zoulu", true);           
        }

        if (IdleStatus != 0)
        {
            ModeSkeObj(IdleStatus == -1 ? Vector3.left : Vector3.right);

        } else if(IdleStatus == 0)
        {
            
            if (targeVector.x == -1) return;
            Debug.Log("playaniam");
            skeletonGraphicScript.PlayAnimationName("animation");
            targeVector.x = -1;
            //动画已经重置，
        }
    }

    void ModeSkeObj(Vector3 vector)
    {
        //结束条件
        if (IdleStatus == -1 && CharaterGameobject.transform.position.x <= targeVector.x)
        {
            IdleStatus = 0;
        }

        if (IdleStatus == 1 && CharaterGameobject.transform.position.x >= targeVector.x)
        {
            IdleStatus = 0;
        }

        if (IdleStatus == 0)
        {
            return;
        }
        CharaterGameobject.transform.Translate(vector * MoveSpeed * Time.deltaTime);

        //
        BgScrollRect.horizontalNormalizedPosition += 1.0f * Time.deltaTime * 1000;
    }

    string GetSKin(string spineName)
    {
        string _skinName = null;
        foreach (SkinList skin in skeletonGraphicScript.skinNames)
        {
            if (skin.Name.Contains(spineName))
            {
                //Debug.Log(skin.SubName);
                _skinName = skin.Name;
                break;
            }
        }
        return _skinName;
    }
}
