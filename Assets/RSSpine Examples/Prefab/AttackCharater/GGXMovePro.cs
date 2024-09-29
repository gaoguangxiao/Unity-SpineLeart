using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGXMovePro : MonoBehaviour
{
    //移动速度
    public float speed = 1;

    //移动方向
    public Vector3 diretionVector = Vector3.left;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //设置
        transform.Translate(diretionVector * speed * Time.deltaTime * 5,Space.World);
    }

}
