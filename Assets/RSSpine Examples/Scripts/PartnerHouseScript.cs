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
    //public Scrollbar BgScrollView;

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

    

    void Update()
    {
        //鼠标的点击
        //0左键，1右键，2滚轮
        //if (Input.GetMouseButtonDown(0))
        //{
        //    targeVector = Input.mousePosition;

        //    // V1方法
        //    IdleStatus = targeVector.x >= CharaterGameobject.transform.position.x ? 1 : -1;
        //    //方向
        //    skeletonGraphicScript.UpdateReverseX(IdleStatus != 1);
        //    skeletonGraphicScript.PlayAnimationName("zoulu", true);
        //}


        //if (IdleStatus != 0)
        //{
        //    //Vector3.left为-1，Vector3.right为1
        //    ModeSkeObj(IdleStatus == -1 ? Vector3.left : Vector3.right);

        //}
        //else if (IdleStatus == 0)
        //{

        //    if (targeVector.x == -1) return;
        //    Debug.Log("playaniam");
        //    skeletonGraphicScript.PlayAnimationName("animation");
        //    targeVector.x = -1;
        //}
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

        //角色移动速度  100 * 1 * 0.02=
        Vector3 vector3 = vector * MoveSpeed * Time.deltaTime;
        Debug.Log("vector3 is : " + vector3.x);

        if (vector3.x > 0)
        {
            //角色移动屏幕边缘至左半边
            if (CharaterGameobject.transform.position.x < Screen.width / 2)
            {
                CharaterGameobject.transform.Translate(vector3);
            }
            else //移动背景画布
            {
                //Debug.Log("scroll bg");
                //BgScrollRect.velocity = new Vector2(vector.x * MoveSpeed * Time.deltaTime, 0f);

                // 获取角色的当前位置和ScrollView的边界
                Vector3 rolePos = CharaterGameobject.transform.position;

                Vector2 viewBounds = BgScrollRect.viewport.rect.size;//视图宽度
                Vector2 contentBounds = BgScrollRect.content.rect.size;//视图内容大小
                //Debug.Log("viewBounds is:" + viewBounds);
                //Debug.Log("contentBounds is:" + contentBounds);
                Debug.Log("contentBounds.x is:" + contentBounds.x);
                Debug.Log("viewBounds.x is:" + viewBounds.x);//2230

                // 点击位置和角色相对位置
                float roleTargetPosX = targeVector.x - rolePos.x;
                //Debug.Log("roleTargetPosX is:" + roleTargetPosX);
                // 计算角色位置相对于ScrollView内容的位置
                //float rolePosX = rolePos.x - (contentBounds.x * 0.5f);
                //Debug.Log("rolePosX is:" + rolePosX);
                // 根据角色的位置来滚动ScrollView
                float targetHorizontalNormalizedPosition = roleTargetPosX / contentBounds.x;
                Debug.Log("targetHorizontalNormalizedPosition is:" + targetHorizontalNormalizedPosition);
                // 限制targetHorizontalNormalizedPosition在0和1之间，确保不会超出内容边界
                targetHorizontalNormalizedPosition = Mathf.Clamp01(targetHorizontalNormalizedPosition);

                BgScrollRect.horizontalNormalizedPosition += vector3.x;
                // 平滑滚动到目标位置
                //BgScrollRect.horizontalNormalizedPosition = Mathf.MoveTowards(BgScrollRect.horizontalNormalizedPosition, targetHorizontalNormalizedPosition, MoveSpeed * Time.deltaTime);
            }

        }
        //角色移动左半边至屏幕边缘
        if (vector3.x < 0)
        {

            if (CharaterGameobject.transform.position.x > 20)
            {
                CharaterGameobject.transform.Translate(vector * MoveSpeed * Time.deltaTime);
            }
            else
            {
                //
            }

        }

        //让背景颜色移动velocity不受帧率影响
        //BgScrollRect.velocity = new Vector2(vector.x * MoveSpeed * Time.deltaTime, 0f);
        //Debug.Log("vector.x: " + vector.x);
        //Debug.Log("Time.deltaTime" + Time.deltaTime);
        //BgScrollRect.horizontalNormalizedPosition += vector3.x;
        //Debug.Log("BgScrollRect.horizontalNormalizedPosition is:" + BgScrollRect.horizontalNormalizedPosition);
        //Debug.Log("BgScrollRect" + BgScrollRect.velocity);

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
