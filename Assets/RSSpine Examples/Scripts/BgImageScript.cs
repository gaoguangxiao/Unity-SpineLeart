using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgImageScript : MonoBehaviour
{
    
    public Transform target; //摄像机

    public Transform farBackground, middleBackground; // 声明两个变量

    private float lastXPos;


    //移动速度
    public float MoveSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        lastXPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 vector3 = Vector3.right * MoveSpeed * Time.deltaTime;

        //target.Translate(vector3);

    }
}
