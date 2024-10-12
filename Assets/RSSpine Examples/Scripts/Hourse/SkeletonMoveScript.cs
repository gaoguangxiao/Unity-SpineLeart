using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// 场景控制器脚本
namespace Spine.Unity.Examples
{
    //伙伴之家角色移动、挂在角色物体，控制角色和背景移动，角色控制器的输入
    public class SkeletonMoveScript : MonoBehaviour
    {
        public float MoveSpeed = 1.0f;

        //人物对象
        public GameObject CharaterGameobject;

        //人物相机
        public Camera SkeletonCamera;

        //摇杆
        //public FixedJoystick fixedJoystick;

        //左边原点
        public GameObject CharaterLeftEndCentter;

        //右不可移动点
        public GameObject CharaterRightEndCentter;

        //角色移动参照物
        public Transform accumulationIndicatorObject;

        //角色待移动距离
        private Vector3 targeDistance;

        //已经移动距离
        private Vector3 moveDistance;

        //人物创建spine脚本
        public CharaterMainScript skeletonGraphicScript;

        //脚步移动
        public CharaterFootSoundScript charaterFootSoundScript;

        //人物皮肤数据
        UserInfoScript userInfoScript;

        //人物皮肤控制
        public SkeletonControlScript skeletonControlScript;
        
        public Text CoinText; // 金币数量
        public Text DiamondText; //钻石数量
        //public CharaterGunScript charaterGunScript;
        //public CharaterCollisionScript charaterCollisionScript;

        //创建敌人
        //public CreateGameObject EnemyCreateGameObject;


        // Start is called before the first frame update
        void Start()
        {
            skeletonGraphicScript.EventAction += HandleAnimationStateEvent;
            skeletonGraphicScript.DeathEvent += OnDeathEvent;
            skeletonGraphicScript.ResurgenceEvent += OnResurgenceEvent;

            userInfoScript = GetComponent<UserInfoScript>();
            userInfoScript.OnDataLoadComplete += OnDataLoadComplete;
        }

        void OnDataLoadComplete(UserData userData)
        {

            CoinText.text = userData.CoinCount.ToString();

            DiamondText.text = userData.DiamondCount.ToString();

            //Debug.Log("userdata：" + userData.FaceContent);
            //查头发
            string toufa = "";
            string toufaColor = "";
            foreach (var item in userData.FaceContent)
            {
                //查找该皮肤全部名称 头发
                if (item.Type == 1)
                {
                    toufa = item.SpineName;
                }
                else if (item.Type == 6)
                {
                    toufaColor = item.SpineName;
                }
                else if (item.Type == 3)//眼
                {
                    string allSpinename = "yan" + "/" + item.SpineName;
                    skeletonControlScript.UpdateMatchSpineSkin(allSpinename);
                }
                else //其他部位
                {

                    string[] skins = item.SpineName.Split("_");
                    string prefix = "";
                    if (skins.Length >= 2)
                    {
                        prefix = skins[0];
                        string allSpinename = prefix + "/" + item.SpineName;
                        skeletonControlScript.UpdateMatchSpineSkin(allSpinename);
                    }
                }
            }
            skeletonControlScript.UpdateMatchSpineSkin("toufa/" + toufa + "_" + toufaColor);

            //衣服
            skeletonControlScript.UpdateMatchSpineSkin("taozhuang/" + userData.DressUpContent.SpineName);
           
        }
        void HandleAnimationStateEvent(string name)
        {
            if (skeletonGraphicScript.state == CharaterBodyState.Running)
            {
                charaterFootSoundScript.PlaySound();
            }
        }

        //玩家被击倒
        void OnDeathEvent()
        {
            //EnemyGameOBject.StopCreate();
            Debug.Log("OnDeathEvent");
        }

        void OnResurgenceEvent()
        {
            //炸起来
        }

        public void OnClickPlayShoot()
        {
            //charaterGunScript.DirPosition = vector;
            //charaterGunScript.PlayShoot();
        }

        public void OnClickPlayJump()
        {
            skeletonGraphicScript.PlayJump(JumpComeplete);
        }

        void JumpComeplete(TrackEntry trackEntry)
        {
            Debug.Log("JumpComeplete");
            //回归之前状态
            //skeletonGraphicScript.state = CharaterBodyState.Idle;
        }

        //}
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

        // Update is called once per frame
        void Update()
        {

//            float horizontal = 0;
//            float vertical = 0;
//#if UNITY_IOS && !UNITY_EDITOR
//            horizontal = fixedJoystick.Horizontal;
//            vertical = fixedJoystick.Vertical;
//#elif UNITY_EDITOR
//            horizontal = Input.GetAxis("Horizontal");
//            vertical = Input.GetAxis("Vertical");
//#endif

//            if (horizontal != 0)
//            {
//                //移动
//                MoveSkeObjV2(horizontal < 0 ? Vector3.left : Vector3.right);

//                //改变枪口方向 1、 -1
//                Vector3 vector = new Vector3(horizontal, vertical, 0);
//                charaterGunScript.UpdateBoneVector(vector);
//            }
//            else
//            {
//                skeletonGraphicScript.UpdatState(CharaterBodyState.Idle);
//            }

//            if (Input.GetKeyDown(KeyCode.J))
//            {
//                OnClickPlayShoot();
//            }

//            if (Input.GetKeyDown(KeyCode.K))
//            {
//                OnClickPlayJump();
//            }
//            return;

            //if (skeletonGraphicScript.state == CharaterBodyState.Death) return;

            //鼠标的点击
            //0左键，1右键，2滚轮
            if (Input.touches.Length > 0)
            {
                Touch touch = Input.touches[0];
                //获取点击点【0~屏幕宽度】
                //Debug.Log("touch is: " + touch.position + Screen.height);
                if (touch.position.y >= Screen.height - 200) return;

                Vector3 chaVector = GetScreenPointByWorld(CharaterGameobject.transform.position);

                //角色跟随相机的位置
                Vector3 skeletonAccumulationVector3 = GetScreenPointByWorld(accumulationIndicatorObject.position);

                //获取点击点相对于角色位移，屏幕点击点相对于主摄像机的点击位置。
                Vector3 touchVector2D = new Vector3(
                    skeletonAccumulationVector3.x + touch.position.x,
                    skeletonAccumulationVector3.y + touch.position.y,
                    skeletonAccumulationVector3.z);

                //点击点相对于角色位移
                targeDistance = touchVector2D - chaVector;

                //Debug.Log("targeDistance" + targeDistance + "state" + state);

                moveDistance = new Vector3();

                //射击
                //charaterGunScript.PlayShoot();
            }

            if (targeDistance.x != 0)
            {
                //skeletonGraphicScript.state = CharaterBodyState.Running;
                //方向
                //skeletonGraphicScript.UpdateReverseX(targeDistance.x < 0);
                //移动
                ModeSkeObj(targeDistance.x < 0 ? Vector3.left : Vector3.right);
            }
            else
            {
                skeletonGraphicScript.UpdatState(CharaterBodyState.Idle);
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

            skeletonGraphicScript.UpdatState(CharaterBodyState.Running);

            Vector3 chaVertor = GetScreenPointByWorld(CharaterGameobject.transform.position);
            //Debug.Log("chaVertor" + chaVertor + "targeDistance : " + targeDistance + "moveDistance is : " + moveDistance);

            Vector3 lE = GetScreenPointByWorld(CharaterLeftEndCentter.transform.position);
            Vector3 rE = GetScreenPointByWorld(CharaterRightEndCentter.transform.position);
            //Debug.Log("lE" + lE + "rE" + rE);
            //角色移动屏幕边缘
            if (vector.x < 0 && chaVertor.x <= lE.x || vector.x > 0 && chaVertor.x >= rE.x)
            {
                targeDistance.x = 0;
                return;
            }

            //角色移动结束
            if (vector.x > 0 && moveDistance.x >= targeDistance.x || vector.x < 0 && moveDistance.x <= targeDistance.x)
            {
                targeDistance.x = 0;
                return;
            }
            //角色基本速度
            float baseSpeed = MoveSpeed * Time.deltaTime;
            //角色相对之前移动距离
            Vector3 chaDistanceVertor = chaVertor - lE;
            //角色移动屏幕边缘至左半边
            if (chaDistanceVertor.x >= Screen.width / 2 && rE.x - chaDistanceVertor.x >= Screen.width / 2) //当角色移动屏幕中心点时，控制相机跟随
            {
                SkeletonCamera.depth = 0;
                Transform cameraTransform = SkeletonCamera.transform;
                cameraTransform.transform.Translate(vector * baseSpeed);
                accumulationIndicatorObject.Translate(vector * baseSpeed);
            }

            //移动角色
            CharaterGameobject.transform.Translate(vector * baseSpeed);
            //角色朝向
            skeletonGraphicScript.UpdateReverseX(vector.x < 0);
            //累计距离
            Vector3 newVC = GetScreenPointByWorld(CharaterGameobject.transform.position);
            AddDistance(newVC, chaVertor);
        }

        /// <summary>
        /// 摇杆输入-移动角色
        /// </summary>
        /// <param name="vector"></param>
        void MoveSkeObjV2(Vector3 vector)
        {
            //角色状态
            skeletonGraphicScript.UpdatState(CharaterBodyState.Running);

            Vector3 chaVertor = GetScreenPointByWorld(CharaterGameobject.transform.position);
            Vector3 lE = GetScreenPointByWorld(CharaterLeftEndCentter.transform.position);
            Vector3 rE = GetScreenPointByWorld(CharaterRightEndCentter.transform.position);
            //角色移动屏幕边缘
            if (vector.x < 0 && chaVertor.x <= lE.x || vector.x > 0 && chaVertor.x >= rE.x)
            {
                return;
            }

            //角色基本速度
            float baseSpeed = MoveSpeed * Time.deltaTime;
            Vector3 chaDistanceVertor = chaVertor - lE;
            //角色移动屏幕边缘至左半边
            if (chaDistanceVertor.x >= Screen.width / 2 && rE.x - chaDistanceVertor.x >= Screen.width / 2) //当角色移动屏幕中心点时，控制相机跟随
            {
                SkeletonCamera.depth = 0;
                Transform cameraTransform = SkeletonCamera.transform;
                cameraTransform.transform.Translate(vector * baseSpeed);
            }
            //移动角色
            CharaterGameobject.transform.Translate(vector * baseSpeed);
            //角色朝向
            skeletonGraphicScript.UpdateReverseX(vector.x < 0);
        }
    }

    
}