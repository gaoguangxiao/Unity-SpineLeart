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

    //右不可移动点
    public GameObject CharaterLeftEndCentter;

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

    //角色开始移动时位置记录
    private Vector3 lastCharaterVector;

    //角色移动参照物
    public Transform accumulationIndicatorObject;

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

        //Vector3 ve
        //GetScreenPointByWorld(CharaterGameobject.transform.position)
        //屏幕左边x为0
        //accumulationIndicatorObject.transform = 
        //accumulationDistance = new Vector3(0, 0, 0);
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
        if (Input.touches.Length > 0)
        {
            Touch touch = Input.touches[0];
            //获取点击点【0~屏幕宽度】
            Debug.Log("touch is: " + touch.position + Screen.height);
            if (touch.position.y >= Screen.height/2) return;
            

            Vector3 chaVector = GetScreenPointByWorld(CharaterGameobject.transform.position);
            //Debug.Log("chaVector" + chaVector);

            //角色跟随相机的位置
            Vector3 skeletonAccumulationVector3 = GetScreenPointByWorld(accumulationIndicatorObject.position);
            //Debug.Log("skeletonAccumulationVector3 is : " + skeletonAccumulationVector3);
            //Debug.Log("SkeletonCamera.SkeletonCamera is : " + accumulationIndicatorObject.position);

            //获取点击点相对于角色位移，屏幕点击点相对于主摄像机的点击位置。
            Vector3 touchVector2D = new Vector3(
                skeletonAccumulationVector3.x + touch.position.x,
                skeletonAccumulationVector3.y + touch.position.y,
                skeletonAccumulationVector3.z);

            //Debug.Log("touchVector2D is : " + touchVector2D);
            //角色是否处于最右边
            //if (chaVector.x >= Screen.width / 2)
            //{
            moveDistance = new Vector3();

            //点击点相对于角色位移
            targeDistance = touchVector2D - chaVector;
  
            Debug.Log("targeDistance" + targeDistance);
            // 判断角色移动方向
            IdleStatus = targeDistance.x > 0 ? 1 : -1;
            ////方向
            skeletonGraphicScript.UpdateReverseX(IdleStatus != 1);
            skeletonGraphicScript.PlayAnimationName("zoulu", true);
        }
        
         if (IdleStatus == 0)
        {
            if (isIdle) return;
            Debug.Log("playaniam");
            skeletonGraphicScript.PlayAnimationName("animation");
            isIdle = true;

        } else if (IdleStatus != 0)
        {
            isIdle = false;

            ModeSkeObj(IdleStatus == -1 ? Vector3.left : Vector3.right);

        }
    }

    //右
    void AddDistance(Vector3 v1, Vector3 v2)
    {
        Vector3 distance = v1 - v2;
        moveDistance += distance;
    }

    void ModeSkeObj(Vector3 vector)
    {
        Vector3 chaVertor = GetScreenPointByWorld(CharaterGameobject.transform.position);
        //Debug.Log("chaVertor" + chaVertor + "targeDistance : " + targeDistance + "moveDistance is : " + moveDistance);

        Vector3 lE = GetScreenPointByWorld(CharaterLeftEndCentter.transform.position);
        Vector3 rE = GetScreenPointByWorld(CharaterRightEndCentter.transform.position);
        //Debug.Log("lE" + lE + "rE" + rE);
        //角色移动屏幕边缘
        if (vector.x < 0 && chaVertor.x <= lE.x || vector.x > 0 && chaVertor.x >= rE.x)
        {
            IdleStatus = 0;
            return;
        }

        //角色移动结束
        if (vector.x > 0 && moveDistance.x >= targeDistance.x || vector.x < 0 && moveDistance.x <= targeDistance.x)
        {
            IdleStatus = 0;
            return;
        }
        //角色基本速度
        float baseSpeed = MoveSpeed * Time.deltaTime;

        Vector3 rc = GetScreenPointByWorld(CharaterRightCenter.transform.position);
        Vector3 lc = GetScreenPointByWorld(CharaterLeftCenter.transform.position);

        //角色移动屏幕边缘至左半边
        if (chaVertor.x >= lc.x && chaVertor.x <= rc.x) //当角色移动屏幕中心点时，控制相机跟随
        {
            SkeletonCamera.depth = 0;
            Transform cameraTransform = SkeletonCamera.transform;
            cameraTransform.transform.Translate(vector * baseSpeed);
            accumulationIndicatorObject.Translate(vector * baseSpeed);
        }

        //移动角色
        CharaterGameobject.transform.Translate(vector * baseSpeed);

        //累计距离
        Vector3 newVC = GetScreenPointByWorld(CharaterGameobject.transform.position);
        AddDistance(newVC, chaVertor);
    }
}
