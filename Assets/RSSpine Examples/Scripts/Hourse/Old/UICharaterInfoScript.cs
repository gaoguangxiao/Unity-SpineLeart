using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharaterInfoScript : MonoBehaviour
{
    //攻速描述
    public Text AttackSpeedText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAttackInfo(CharaterAttackModel attackModel)
    {
        AttackSpeedText.text = "攻击速度：" + attackModel.AttackSpeed.ToString("F2");
    }

}
