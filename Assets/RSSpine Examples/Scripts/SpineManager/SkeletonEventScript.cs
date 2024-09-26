using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity.Examples
{
    public class SkeletonEventScript : MonoBehaviour
    {

        public SkeletonAnimation sg;

        [SpineEvent(dataField: "sg", fallbackToTextField: true)]
        public string eventName;

        SkeletonData skeletonData;

        //spine事件数据
        private EventData eventData;

        // Start is called before the first frame update
        void Start()
        {
            skeletonData = sg.skeleton.Data;

            //获取事件名称
            foreach (var levent in skeletonData.Events)
            {
                Debug.Log("event is :" + levent);
            }
                //
            //对事件监听
            sg.AnimationState.Event += HandleAnimationStateEvent;

            sg.AnimationState.SetAnimation(0, "run", true);
        }

        private void Update()
        {
            //float horizontal = Input.GetAxis("Horizontal");
            //float vertical = Input.GetAxis("Vertical");
        }

        void HandleAnimationStateEvent(TrackEntry trackEntry, Event e)
        {
             Debug.Log("Event fired! " + e.Data.Name);
        }
    }
}