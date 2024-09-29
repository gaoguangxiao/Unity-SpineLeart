using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using Spine.Unity;
using Spine.Unity.Examples;

public class CreateGameObject : MonoBehaviour
{
    //对象预制体
    public GameObject ObjectPrefab;
    //定义子弹方向炮塔的炮管
    public Transform cannonAngle;

    [Header("Balance")]
    //定时创建 -1，结束创建
    [DefaultValue(1.0f)]
    public float CreateInterval = 1.0f;

    //上次开抢时间
    float lastCreateTime;

    // Start is called before the first frame update
    void Start()
    {
        //连发
        //InvokeRepeating("Fire", 1, (float)0.2);
    }

    // Update is called once per frame
    void Update()
    {
        if (CreateInterval == -1) return;

        float currentTime = Time.time;
        if (currentTime - lastCreateTime > CreateInterval) {
            lastCreateTime = currentTime;
            CreateRandom(); }

    }

    public void StopCreate()
    {
        CreateInterval = -1;
    }

    public void StartCreate()
    {
        CreateInterval = 1.0f;
    }

    public void Create()
    {
        //Debug.Log("生成对象");
        //根据某预制体和父类 实例化游戏对象
        GameObject gameObject = Object.Instantiate(ObjectPrefab, transform);
        //游戏对象的位置
        gameObject.transform.position = transform.position;
        //将子弹旋转
        //gameObject.transform.localEulerAngles = new Vector3(0,0,90);
        gameObject.transform.eulerAngles = this.transform.eulerAngles;
        //
    }

    //创建飞行的babu
    public void CreateRandom()
    {
        //Debug.Log("生成对象");

        GameObject gameObject = Object.Instantiate(ObjectPrefab, transform);
        gameObject.transform.position = new Vector3(0,0,-0.1f);
        //
        SkeletonAnimation skeletonAnimation = gameObject.GetComponent<SkeletonAnimation>();

        SkeletonDataAsset skeletonDataAsset = SpineAssetsManeger.Instance.GetSpineModel(1);
        skeletonAnimation.skeletonDataAsset = skeletonDataAsset;
        skeletonAnimation.Initialize(true);//运行时初始化

        //控制脚本
        SkeletonControlScript skeletonControlScript = gameObject.AddComponent<SkeletonControlScript>();
        skeletonControlScript.Refresh(skeletonAnimation);

        skeletonControlScript.UpdateSpineSKin("moren");

        //脸部渲染脚本
        FaceMono faceMono = gameObject.AddComponent<FaceMono>();
        faceMono.Refresh(skeletonControlScript);
        faceMono.RandomHair();
        
        //游戏对象的位置
        gameObject.transform.position = transform.position;
        //将子弹旋转
        //gameObject.transform.localEulerAngles = new Vector3(0,0,90);
        gameObject.transform.eulerAngles = this.transform.eulerAngles;
        //
    }

    //将spin对象改为moren
    void GetChaSpine()
    {
       
    }
}
