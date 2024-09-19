using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TypeMono : MonoBehaviour
{

    public GameObject ActionGameObject;//动作列表控制

    public ActionControl actionControl;

    public GameObject SkinGameObject;//动作列表控制

    public int listType = 0;

    //Toggle action
    public Action<int> action;

  
    public void UpdateListType(int type)
    {
        listType = type;

        //动作列表
        ActionGameObject.SetActive(type == 0);
        //SkinGameObject.SetActive(type == 1);
        action(type);
    }
}
