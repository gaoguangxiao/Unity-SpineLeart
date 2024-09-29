using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace Spine.Unity.Examples
{
    public class BrickMainScript : MonoBehaviour
    {
        //角色脚本
        public CharaterMainScript model;

        public CharaterGunScript gunScript;

        //敌人对象
        public CreateGameObject EnemyGameOBject;

        // Start is called before the first frame update
        void Start()
        {
            //if (skeletonAnimation == null) return;
            model.VerticalEvent += OnInputVertical;
            model.HorizontalEvent += OnInputHorizontal;
            model.DeathEvent += OnDeathEvent;
            model.ResurgenceEvent += OnResurgenceEvent;
        }

        // Update is called once per frame
        void Update()
        {
            //开抢
            if(model.state != CharaterBodyState.Death)
            gunScript.PlayShoot();
        }

        /// <summary>
        /// 上下移动
        /// </summary>
        /// <param name="vSpeed"></param>
        void OnInputVertical(float vSpeed)
        {
            Vector3 vector3 = new Vector3(0, vSpeed, 0);
            //世界坐标
            transform.Translate(vector3 * 5 * Time.deltaTime);
        }

        void OnInputHorizontal(float hSpeed)
        {
            Vector3 vector3 = new Vector3(hSpeed, 0, 0);
            //世界坐标
            transform.Translate(vector3 * 5 * Time.deltaTime);
        }

        void OnDeathEvent()
        {
            EnemyGameOBject.StopCreate();
            
        }

        void OnResurgenceEvent()
        {
            EnemyGameOBject.StartCreate();
            //
        }

        //重新开始
        void ResetGame()
        {

        }

    }
}