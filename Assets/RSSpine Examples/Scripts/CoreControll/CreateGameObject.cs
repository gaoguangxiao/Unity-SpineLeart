using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreateGameObject : MonoBehaviour
{
    //对象预制体
    public GameObject ObjectPrefab;
    //父物体
    public Transform SuperObjectFolder;
    //出生的位置
    public Transform BornPoint;
    //定义子弹方向炮塔的炮管
    public Transform cannonAngle;
    //定义对象速度
    public float BulletSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        //连发
        //InvokeRepeating("Fire", 1, (float)0.2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Create()
    {
        //Debug.Log("生成子弹");
        //根据某预制体和父类 实例化游戏对象
        GameObject gameObject = Object.Instantiate(ObjectPrefab, SuperObjectFolder);
        //gameObject.GetComponent<BulletPro>().speed = BulletSpeed;
        //Debug.Log("射击子弹 + " + firePoint.position);
        //游戏对象的位置
        gameObject.transform.position = BornPoint.position;
        //将子弹旋转
        //gameObject.transform.localEulerAngles = new Vector3(0,0,90);
        gameObject.transform.eulerAngles = this.transform.eulerAngles;
        //
    }
}
