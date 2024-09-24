using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;
using Spine.Unity.Examples;

public class CharayerMoveScript : MonoBehaviour
{
    //public GameObject charaterGameobject;

    public float MoveSpeed = 1.0f;

    //角色待移动距离
    private Vector3 targeDistance;

    //已经移动距离
    private Vector3 moveDistance;

    //人物创建spine脚本
    public SkeletonGraphicScript skeletonGraphicScript;

    //人物移动 -1左 0不移动 1右
    private int IdleStatus = 0;

    //状态是否恢复
    private bool isIdle;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //{
        if (Input.GetMouseButtonDown(0))
        {
            //获取鼠标点击坐标，摄像机到点击点的射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //获取点击位置
            if (Physics.Raycast(ray, out hit))
            {
                //点击位置
                Vector3 vector3 = hit.point;

                Debug.Log("vector3" + vector3);

                Vector3 chaVector = transform.position;

                Debug.Log("chaVector" + chaVector);
                //agent.SetDestination(vector3);

                moveDistance = new Vector3();

                //点击点相对于角色位移
                targeDistance = vector3 - chaVector;

                Debug.Log("targeDistance" + targeDistance);
                // 判断角色移动方向
                IdleStatus = targeDistance.x > 0 ? 1 : -1;
                ////方向
                skeletonGraphicScript.UpdateReverseX(IdleStatus != 1);
                skeletonGraphicScript.PlayAnimationName("zoulu", true);
            }
        }

        if (IdleStatus == 0)
        {
            if (isIdle) return;
            Debug.Log("playaniam");
            skeletonGraphicScript.PlayAnimationName("animation");
            isIdle = true;

        }
        else if (IdleStatus != 0)
        {
            isIdle = false;
            //;
            //targeDistance.z > 0 ? Vector3.forward : Vector3.back;
            Vector3 dir = targeDistance.normalized;

            dir.y = 0;//不小于0
            ModeSkeObj(dir);
        }
        //float hei = Input.GetAxis("Horizontal");
        //float ver = Input.GetAxis("Vertical");

        ////Debug.Log("hei" + hei + player);
        ////创建方向向量
        //Vector3 dir = new Vector3(hei, 0, ver);

        //Debug.Log("dir" + dir);

        ////移动
        //player.SimpleMove(dir * 3);
    }

    void AddDistance(Vector3 v1, Vector3 v2)
    {
        Vector3 distance = v1 - v2;
        moveDistance += distance;
    }

    //
    void ModeSkeObj(Vector3 vector)
    {
        Vector3 chaVertor = transform.position;
        //Debug.Log("chaVertor" + chaVertor + "targeDistance : " + targeDistance + "moveDistance is : " + moveDistance);


        //Vector3 lE = GetScreenPointByWorld(CharaterLeftEndCentter.transform.position);
        //Vector3 rE = GetScreenPointByWorld(CharaterRightEndCentter.transform.position);
        ////Debug.Log("lE" + lE + "rE" + rE);
        ////角色移动屏幕边缘
        if (chaVertor.z < 0)
        {
            //vector.z = 0;
            //IdleStatus = 0;
            //return;
        }

        //角色移动结束
        if (vector.x > 0 && moveDistance.x >= targeDistance.x || vector.x < 0 && moveDistance.x <= targeDistance.x)
        {
            IdleStatus = 0;
            return;
        }
        //角色基本速度
        float baseSpeed = MoveSpeed * Time.deltaTime;

        //Vector3 rc = GetScreenPointByWorld(CharaterRightCenter.transform.position);
        //Vector3 lc = GetScreenPointByWorld(CharaterLeftCenter.transform.position);

        //角色移动屏幕边缘至左半边
        //if (chaVertor.x >= lc.x && chaVertor.x <= rc.x) //当角色移动屏幕中心点时，控制相机跟随
        //{
        //    SkeletonCamera.depth = 0;
        //    Transform cameraTransform = SkeletonCamera.transform;
        //    cameraTransform.transform.Translate(vector * baseSpeed);
        //    accumulationIndicatorObject.Translate(vector * baseSpeed);
        //}

        //移动角色
        transform.Translate(vector * baseSpeed);

        //累计距离
        Vector3 newVC = transform.position;
        AddDistance(newVC, chaVertor);
    }
}