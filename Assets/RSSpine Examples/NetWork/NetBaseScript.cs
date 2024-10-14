using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class NetBaseScript<T> : MonoBase
{
    public  RSResponseV2<T> rSResponse;//响应数据

   
    public Action<T> OnDataLoadComplete;


    //子类必须实现数据加载方法
    public abstract void RefreshData();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
