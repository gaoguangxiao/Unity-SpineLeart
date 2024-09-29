using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPro : MonoBehaviour
{
    //敌人移动速度
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //设置
        transform.Translate(Vector3.left * speed * Time.deltaTime * 5);
    }

}
