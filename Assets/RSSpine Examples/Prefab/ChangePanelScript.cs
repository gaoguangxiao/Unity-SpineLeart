using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ChangePanelScript : MonoBehaviour
{
    public Text titleTxt;

    //按钮点击
    public Action<int,string> action;

    //循环数组
    public List<string> LoopList = new List<string>();

    //标题
    public string title;

    private int Index = 1;

    private void Start()
    {
        setTxt(title + "1");
    }

    public void setTxt(string txt) {

        titleTxt.text = txt;
    }

    public void OnLastClick()
    {
        changeIndex(-1);
    }

    public void OnNextClick()
    {
        changeIndex(1);
    }

    void changeIndex(int index)
    {
        Index += index;
        int intValue = Mathf.Abs(Index);
        if (intValue >= LoopList.Count) Index = 0;
        if (Index < 0) Index = LoopList.Count - 1;
        setTxt(title + Index);
        action(Index,LoopList[Index]);
    }
}
