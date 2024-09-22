using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity.Examples;

//挂在角色物体，控制角色和背景移动
public class SkeletonMoveScript : MonoBehaviour
{
    public float MoveSpeed = 1.0f;

    //人物对象
    public GameObject CharaterGameobject;

    //人物相机
    public Camera SkeletonCamera;

    //人物左移动屏幕中心
    public GameObject CharaterLeftCenter;

    //人物右中心
    public GameObject CharaterRightCenter;

    //右不可移动点
    public GameObject CharaterRightEndCentter;

    //人物移动 -1左 0不移动 1右
    private int IdleStatus = 0;

    //目标点
    //private Vector3 targeVector;

    //角色累计
    private Vector3 accumulationDistance;

    //角色待移动距离
    private Vector3 targeDistance;

    //已经移动距离
    private Vector3 moveDistance;

    //人物创建spine脚本
    private SkeletonGraphicScript skeletonGraphicScript;

    //人物背景
    public GameObject bgGameObject;

    private bool isIdle;

    private void Awake()
    {
        skeletonGraphicScript = transform.GetComponent<SkeletonGraphicScript>();
        //skeletonGraphicScript.OnSpineLoadComplete = OnSpineLoadComplete;
    }

    // Start is called before the first frame update
    void Start()
    {
        //
        //CharaterGameobject = this.gameObject;

        //Debug.Log("bgGameObject is : " + bgGameObject);
    }

    /// <summary>
    /// 3d坐标的游戏物体转移到2d屏幕
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    Vector3 GetScreenPointByWorld(Vector3 vector)
    {

        Vector3 dVector = Camera.main.WorldToScreenPoint(vector);
        //Camera.current
        return dVector;
    }


    //2D坐标转换3d显示
    Vector3 Get3DScreenPointByVector2(Vector3 vector)
    {
        //Camera cam = Camera.main;
        Vector3 dVector = Camera.main.ScreenToWorldPoint(vector);
        //Vector3 dVector = cam.ScreenToWorldPoint(new Vector3(vector.x, vector.y, cam.nearClipPlane));
        return dVector;
    }


    // Update is called once per frame
    void Update()
    {
        //鼠标的点击
        //0左键，1右键，2滚轮
        if (IdleStatus == 0 && Input.touches.Length > 0)
        {
            Touch touch = Input.touches[0];
            //获取点击点【0~屏幕宽度】
            //

            Vector3 chaVector = GetScreenPointByWorld(CharaterGameobject.transform.position);
            Debug.Log("chaVector" + chaVector);

            //角色是否处于最右边
            if (chaVector.x >= Screen.width / 2)
            {
                moveDistance = new Vector3();

                if (touch.position.x > Screen.width / 2)
                {
                    //获取相对位移，点击坐标相对于人物位移
                    targeDistance = new Vector3(touch.position.x - Screen.width / 2, touch.position.y, 0);
                    //移动到最右边了，该角色移动 点击点是屏幕 - 角色坐标
                    //获取点击点转移3d
                    Vector3 touchFVector3D = Get3DScreenPointByVector2(touch.position);
                    //转移到和角色
                    Vector3 touchFVector2D = GetScreenPointByWorld(touchFVector3D);
                    Debug.Log("touchFVector2D is:" + touchFVector2D + "touchFVector3D is" + touchFVector3D);
                }
                else //
                {
                    targeDistance = new Vector3(touch.position.x - Screen.width / 2, touch.position.y, 0);
                }

            }
            else
            {
                //
                moveDistance = new Vector3();

                Vector3 vector = new Vector3(touch.position.x, touch.position.y, 0);
                //获取相对位移
                targeDistance = vector - chaVector;
            }

            Debug.Log("targeDistance" + targeDistance);


            //return;
            //获取角色 3d到2d屏幕，xy一样，当z值不一样，会导致在2d界面xy也不一致，因此相对位置需要移到2d计算。

            //Vector3 chaVector = CharaterGameobject.transform.position;

            ////
            //moveDistance = new Vector3();
            ////获取相对位移
            //targeDistance = targeVector - chaVector;

            //Debug.Log("targeDistance is：" + targeDistance);

            //Debug.Log("CharaterGameobject.x is" + vector);
            // 判断角色移动方向
            IdleStatus = targeDistance.x > 0 ? 1 : -1;
            ////方向
            skeletonGraphicScript.UpdateReverseX(IdleStatus != 1);
            skeletonGraphicScript.PlayAnimationName("zoulu", true);
        }



        if (IdleStatus != 0)
        {
            isIdle = false;
            //Debug.Log("IdleStatus is: " + IdleStatus);
            ModeSkeObj(IdleStatus == -1 ? Vector3.left : Vector3.right);

        }
        else if (IdleStatus == 0)
        {
            if (isIdle) return;
            Debug.Log("playaniam");
            skeletonGraphicScript.PlayAnimationName("animation");
            isIdle = true;
        }
    }

    //右
    void AddDistance(Vector3 v1, Vector3 v2)
    {
        moveDistance += v1 - v2;
        accumulationDistance += moveDistance;
    }

    //左
    void AddDistanceV1(Vector3 v1, Vector3 v2)
    {
        moveDistance += v1 - v2;
        accumulationDistance -= moveDistance;
    }

    void ModeSkeObj(Vector3 vector)
    {
        Vector3 chaVertor = GetScreenPointByWorld(CharaterGameobject.transform.position);
        Debug.Log("chaVertor" + chaVertor + "targeDistance : " + targeDistance + "moveDistance is : " + moveDistance);
        if (vector.x > 0 && moveDistance.x >= targeDistance.x)
        {
            IdleStatus = 0;
        }

        //if (vector.x < 0 && chaVertor.x <= targeVector.x)
        //{
        //    IdleStatus = 0;
        //}

        //Debug.Log("chaVertor is：" + chaVertor);
        //角色基本速度
        float baseSpeed = MoveSpeed * Time.deltaTime;

        //累计 人物移动
        //Vector3 chaDistance = new Vector3(baseSpeed, chaVertor.y, chaVertor.z);




        if (vector.x > 0)
        {
            CharaterGameobject.transform.Translate(vector * baseSpeed);

            Vector3 rc = GetScreenPointByWorld(CharaterRightCenter.transform.position);
            //Vector3 re = GetScreenPointByWorld(CharaterRightEndCentter.transform.position);
            //Debug.Log("re is" + re);
            //if (chaVertor.x > re.x) return;

            //角色移动屏幕边缘至左半边
            if (chaVertor.x >= Screen.width / 2 && chaVertor.x <= rc.x)
            {

                SkeletonCamera.depth = 0;
                Transform cameraTransform = SkeletonCamera.transform;
                cameraTransform.transform.Translate(vector * baseSpeed);
            }

            //if (chaVertor.x < Screen.width / 2 && chaVertor.x <= CharaterRightCenter.transform.position.x)
            //{
            //    //Vector3 oldC = GetScreenPointByWorld(CharaterGameobject.transform.position);
            //    //CharaterGameobject.transform.Translate(vector * baseSpeed);
            //    //
            //    //
            //    SkeletonCamera.depth = 0;
            //    Transform cameraTransform = SkeletonCamera.transform;
            //    cameraTransform.transform.Translate(vector * baseSpeed);
            //}
            else //移动相机跟随人物移动
            {


                //SkeletonCamera.depth = -3;


                //移
            }

            //
            Vector3 newVC = GetScreenPointByWorld(CharaterGameobject.transform.position);
            //距离累计
            AddDistance(newVC, chaVertor);
            //Debug.Log("newVC is : " + newVC);
            //lastChaVertor = newVC;
        }
        //角色移动左半边至屏幕边缘
        if (vector.x < 0)
        {
            if (chaVertor.x > 20 && chaVertor.x < Screen.width / 2)
            {
                //CharaterGameobject.transform.Translate(vector * baseSpeed);
            }
            else
            {
                //
                //bgGameObject.transform.Translate(Vector3.right * baseSpeed);
            }

        }
    }
}
