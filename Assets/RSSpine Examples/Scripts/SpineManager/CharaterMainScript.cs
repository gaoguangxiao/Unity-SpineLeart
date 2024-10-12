using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CharaterBodyState
{
    Idle,
    Running,
    Death,
    Jumping
}

//对某个角色动作皮肤的
namespace Spine.Unity.Examples
{
    //伙伴之家角色移动声音。脚步带粒子效果
    public class CharaterMainScript : MonoBehaviour
    {
   
        public SkeletonAnimation sg;

        [SpineEvent(dataField: "sg", fallbackToTextField: true)]
        public string eventName;

        [SpineAnimation(dataField: "sg" ,fallbackToTextField = true)]
        public string runAnimation;

        [SpineAnimation(dataField: "sg", fallbackToTextField = true)]
        public string IdleAnimation;

        //摔倒
        [SpineAnimation(dataField: "sg", fallbackToTextField = true)]
        public string DeathAnimation;

        [SpineAnimation(dataField: "sg", fallbackToTextField = true)]
        public string JumpAnimation;

        Spine.EventData eventData;

        //之前状态
        public CharaterBodyState previousViewState = CharaterBodyState.Idle;

        //角色状态
        public CharaterBodyState state = CharaterBodyState.Idle;

        //角色垂直移动速度
        public event System.Action<float> VerticalEvent;

        public event System.Action<float> HorizontalEvent;
        //角色死亡
        public event System.Action DeathEvent;
        //角色复活
        public event System.Action ResurgenceEvent;

        //[Range(-1f, 1f)]
        //public float currentSpeed;

        //朝向
        public bool FaceLeft;

        //动画事件
        public Action<string> EventAction;

        // Start is called before the first frame update
        void Start()
        {
            sg.AnimationState.Event += HandleAnimationStateEvent;

            eventData = sg.Skeleton.Data.FindEvent(eventName);

            //
            sg.Skeleton.ScaleX = FaceLeft? 1 : -1;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        //脚步声
        void HandleAnimationStateEvent(TrackEntry trackEntry, Event e)
        {
            //Debug.Log("Event fired! " + e.Data.Name);

            bool eventMatch = (eventData == e.Data);

            if (eventMatch)
            {
                EventAction(e.Data.Name);
            }
        }

        public void TryMoveToVertical(float speed)
        {
            VerticalEvent(speed);
        }

        public void TryMoveToHorizontal(float speed)
        {
            HorizontalEvent(speed);
        }

        /// <summary>
        /// 角色重新开始
        /// </summary>
        public void TryResurgenceEvent()
        {
            state = CharaterBodyState.Idle;
            PlayIdle();

            ResurgenceEvent();
        }

        public void TryDeathEvent()
        {
            state = CharaterBodyState.Death;
            PlayDeath();
            DeathEvent();
        }

        /// <summary>
        /// 翻转Sleketon
        /// </summary>
        /// <param name="value"></param>
        public void UpdateReverseX(bool value)
        {
            if (value == FaceLeft) return;
            Skeleton skeleton = sg.Skeleton;
            FaceLeft = value;
            skeleton.ScaleX = -skeleton.ScaleX;
        }

        public void PlayRun(bool loop = true)
        {
            sg.AnimationState.SetAnimation(0, runAnimation, loop);
        }

        public void PlayDeath()
        {
            sg.AnimationState.SetAnimation(0, DeathAnimation, false);
        }

        public void PlayJump(AnimationState.TrackEntryDelegate complete)
        {
            state = CharaterBodyState.Jumping;
            TrackEntry jumpTrack = sg.AnimationState.SetAnimation(0, JumpAnimation, false);
            jumpTrack.Complete += JumpComplete;
        }

        void JumpComplete(TrackEntry track)
        {
            state = previousViewState;

            PlayStateAnimation();
        }

        public void PlayIdle(bool loop = true)
        {
            sg.AnimationState.SetAnimation(0, IdleAnimation, loop);
        }

        public void UpdatState(CharaterBodyState newState)
        {
           
            if (state == newState) return;
            state = newState;

            //跳跃状态
            if (state == CharaterBodyState.Jumping)
            {
                return;
            }

            if (previousViewState != state)
            {
                previousViewState = state;
                PlayStateAnimation();
            }
        }

        public void PlayStateAnimation()
        {
            if (state == CharaterBodyState.Idle) PlayIdle();
            else if (state == CharaterBodyState.Running) PlayRun();  
        }
    }
}