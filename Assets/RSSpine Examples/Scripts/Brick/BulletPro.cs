using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletPro : MonoBehaviour
{
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //设置飞行
        transform.Translate(Vector3.right * speed * Time.deltaTime * 5);
    }
}
