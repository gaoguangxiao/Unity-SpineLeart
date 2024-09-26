using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ActionControl : MonoBehaviour
{
    //
    public Toggle toggle;//循环
    public bool IsLoop;//动画是否循环

    public Dropdown OrientationDown;//动画朝向
    public bool AniReverseX = false;//0：左，1右

    public Dropdown dropdown;//速度
    public float aniSpeed = 1.0f;

    public Dropdown directionDown;//动画方向
    public bool AniReverse;//动画方向

    //方向切换回调
    public Action<bool> OrientationAction;

    // Start is called before the first frame update
    private void Start()
    {
        toggle.isOn = IsLoop;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateToggle()
    {
        IsLoop = toggle.isOn;
        //Debug.Log("UpdateToggle" + toggle.isOn);
    }

    public void OnDropdownValueChanged()
    {
        //dropdown.value
        if (dropdown.value == 0) aniSpeed = 0.5f;
        if (dropdown.value == 1) aniSpeed = 1.0f;
        if (dropdown.value == 2) aniSpeed = 1.5f;
        dropdown.captionText.text = dropdown.options[dropdown.value].text;
    }

    /// <summary>
    /// 朝向
    /// </summary>
    public void OnOrientationValueChanged()
    {
        AniReverseX = OrientationDown.value == 0;
        OrientationAction(AniReverseX);
        OrientationDown.captionText.text = OrientationDown.options[OrientationDown.value].text;
    }

    /// <summary>
    /// 方向
    /// </summary>
    public void OnDirectionValueChanged()
    {
        AniReverse = directionDown.value == 1;
        directionDown.captionText.text = directionDown.options[directionDown.value].text;
    }
}
