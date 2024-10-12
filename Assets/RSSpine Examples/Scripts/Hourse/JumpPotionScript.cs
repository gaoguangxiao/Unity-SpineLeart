using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPotionScript : MonoBehaviour
{

    public float jumpHeight = 1.0f; // 跳跃的高度

    public float jumpSpeed = 5.0f; // 跳跃的速度

    //private float targetY; // 目标Y位置

    private float orignY; // 目标Y位置

    // Start is called before the first frame update
    void Start()
    {
        Vector3 dVector = Camera.main.WorldToScreenPoint(transform.position);
        // 初始化目标Y位置为当前Y位置
        orignY = dVector.y;
    }

    // Update is called once per frame
    void Update()
    {

        //Vector3 dVector = Camera.main.WorldToScreenPoint(transform.position);
        //// 当物体到达顶部或底部时改变目标Y位置实现跳动
        //if (dVector.y > orignY - jumpHeight)
        //{
        //    transform.Translate(Vector3.down * jumpSpeed * Time.deltaTime);
        //} else
        //{
        //    transform.Translate(Vector3.up * jumpSpeed * Time.deltaTime);
        //}
    }
}
