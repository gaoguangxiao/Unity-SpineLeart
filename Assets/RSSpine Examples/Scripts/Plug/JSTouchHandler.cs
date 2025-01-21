using UnityEngine;
using System.Runtime.InteropServices;

public class TouchHandler : MonoBehaviour
{
#if UNITY_WEBGL

    //[DllImport("__Internal")]
    //private static extern void RegisterTouchCallbacks();
#endif

    void Start()
    {
#if UNITY_WEBGL
        //RegisterTouchCallbacks();
#endif
    }


    // 触摸开始的回调函数
    public void TouchStart(float x, float y)
    {
        Debug.Log("Touch Start at: " + x + ", " + y);
    }

    // 触摸移动的回调函数
    public void TouchMove(float x, float y)
    {
        Debug.Log("Touch Move to: " + x + ", " + y);
    }

    // 触摸结束的回调函数
    public void TouchEnd(float x, float y)
    {
        Debug.Log("Touch End at: " + x + ", " + y);
    }
}
