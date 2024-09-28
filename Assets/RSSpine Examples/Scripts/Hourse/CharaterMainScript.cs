using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


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

        //朝向
        bool FaceLeft;

        //动画事件
        public Action<string> EventAction;

        // Start is called before the first frame update
        void Start()
        {
            sg.AnimationState.Event += HandleAnimationStateEvent;

            eventData = sg.Skeleton.Data.FindEvent(eventName);
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

        public void PlayJump(AnimationState.TrackEntryDelegate Complete)
        {
            //sg.AnimationState.SetAnimation(0, JumpAnimation, false);
            TrackEntry jumpTrack = sg.AnimationState.SetAnimation(0, JumpAnimation, false);
            jumpTrack.Complete += Complete;
        }

        public void PlayIdle(bool loop = true)
        {
            sg.AnimationState.SetAnimation(0, IdleAnimation, loop);
        }   
    }
}