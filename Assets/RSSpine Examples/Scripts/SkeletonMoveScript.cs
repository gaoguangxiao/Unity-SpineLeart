using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity.Examples;

//挂在角色物体，控制角色和背景移动
public class SkeletonMoveScript : MonoBehaviour
{
    public float MoveSpeed = 1.0f;

    //人物对象
    GameObject CharaterGameobject;

    //人物移动 -1左 0不移动 1右
    private int IdleStatus = 0;

    //目标点
    private Vector3 targeVector;

    //角色待移动距离
    private Vector3 targeDistance;

    //已经移动距离
    private Vector3 moveDistance;

    //人物创建spine脚本
    private SkeletonGraphicScript skeletonGraphicScript;

    //人物背景
    public GameObject bgGameObject;

    private void Awake()
    {
        skeletonGraphicScript = transform.GetComponent<SkeletonGraphicScript>();
        //skeletonGraphicScript.OnSpineLoadComplete = OnSpineLoadComplete;
    }

    // Start is called before the first frame update
    void Start()
    {
        //
        CharaterGameobject = this.gameObject;

        Debug.Log("bgGameObject is : " + bgGameObject);
    }

    /// <summary>
    /// 3d坐标的游戏物体转移到2d屏幕
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    Vector3 GetScreenPointByWorld(Vector3 vector)
    {
        Vector3 dVector = Camera.main.WorldToScreenPoint(vector);

        return dVector;
    }

    // Update is called once per frame
    void Update()
    {
        //鼠标的点击
        //0左键，1右键，2滚轮
        if (Input.touches.Length > 0)
        {
            Touch touch = Input.touches[0];
            //记录点击点
            targeVector = new Vector3(touch.position.x, touch.position.y, 0);
            //Debug.Log("touch.position" + touch.position);

            //获取角色 3d到2d屏幕
            Vector3 vector = GetScreenPointByWorld(CharaterGameobject.transform.position);

            //
            moveDistance = new Vector3();
            //获取相对位置
            targeDistance = targeVector - vector;

            Debug.Log("targeDistance is：" + targeDistance);

            //Debug.Log("CharaterGameobject.x is" + vector);
            // 判断角色移动方向
            IdleStatus = targeVector.x -  vector.x > 0 ? 1 : -1;
            ////方向
            skeletonGraphicScript.UpdateReverseX(IdleStatus != 1);
            skeletonGraphicScript.PlayAnimationName("zoulu", true);
        }        



        if (IdleStatus != 0)
        {
            //Debug.Log("IdleStatus is: " + IdleStatus);
            ModeSkeObj(IdleStatus == -1 ? Vector3.left : Vector3.right);

        } else if (IdleStatus == 0)
        {
            if (targeVector.x == -1) return;
            Debug.Log("playaniam");
            skeletonGraphicScript.PlayAnimationName("animation");
            targeVector.x = -1;
        }
    }

    void AddDistance(Vector3 v1, Vector3 v2)
    {
        moveDistance += v1 - v2;
    }

    void ModeSkeObj(Vector3 vector)
    {
        
        //Debug.Log("chaVertor.x" + chaVertor.x);

        //
        if (moveDistance.x >= targeDistance.x)
        {
            IdleStatus = 0;
        }

        Debug.Log("moveDistance is：" + moveDistance);
        //角色基本速度
        float baseSpeed = MoveSpeed * Time.deltaTime;
        // 计算3D移动距离转换为2D移动距离的比例
        //float halfFOV = 60 * 0.5f * Mathf.Deg2Rad; // 将视角范围转换为半角度，并从弧度转换为弧度
        //float distanceToScreenHeightRatio = Mathf.Tan(halfFOV) * 2; // 计算距离屏幕高度的比例
        //float moveDistance2D = moveDistance * distanceToScreenHeightRatio; // 将3D距离转换为2D距离

        // 输出转换后的2D移动距离
        //Debug.Log("2D Move Distance: " + moveDistance);

        //Debug.Log("vector3 is : " + vector3.x);

        //移动角色
        Vector3 chaVertor = GetScreenPointByWorld(CharaterGameobject.transform.position);

        if (vector.x > 0)
        {
            //角色移动屏幕边缘至左半边
            if (chaVertor.x < Screen.width / 2)
            {
                Vector3 oldC = GetScreenPointByWorld(CharaterGameobject.transform.position);
                CharaterGameobject.transform.Translate(vector * baseSpeed);
                Vector3 newVC = GetScreenPointByWorld(CharaterGameobject.transform.position);
                AddDistance(newVC,oldC);
                
            }
            else //移动背景画布
            {
                Vector3 oldC = GetScreenPointByWorld(bgGameObject.transform.position);
                //画布停止移动
                //if (oldC.x > oldC.x)
                //{
                //    IdleStatus = 0;
                //}
                //Debug.Log("背景坐标 is" + scVertor.x);
                //背景画布移动边缘控制
                if (oldC.x > -940)
                {
                    bgGameObject.transform.Translate(Vector3.left * baseSpeed);
                } else
                {
                    //
                }
                Vector3 newVC = GetScreenPointByWorld(bgGameObject.transform.position);
                AddDistance(oldC, newVC);
            }

        }
        //角色移动左半边至屏幕边缘
        if (vector.x < 0)
        {
            if (chaVertor.x > 20 && chaVertor.x < Screen.width/2)
            {
                CharaterGameobject.transform.Translate(vector * baseSpeed);
            }
            else
            {
                //
                bgGameObject.transform.Translate(Vector3.right * baseSpeed);
            }

        }
    }
}
