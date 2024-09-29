using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireGGX : MonoBehaviour
{
    //子弹预制体
    public GameObject bulletPrefab;
    //子弹夹
    public Transform bulletFolder;
    //定义子弹的位置
    public Transform firePoint;
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

    public void Fire()
    {
        //Debug.Log("生成子弹");
        //根据某预制体和父类 实例化游戏对象
        GameObject gameObject = Object.Instantiate(bulletPrefab, bulletFolder);
        gameObject.GetComponent<BulletPro>().speed = BulletSpeed;
        //Debug.Log("射击子弹 + " + firePoint.position);
        //游戏对象的位置
        gameObject.transform.position = firePoint.position;
        //将子弹旋转
        //gameObject.transform.localEulerAngles = new Vector3(0,0,90);
        gameObject.transform.eulerAngles = this.transform.eulerAngles;
        //
    }
}
