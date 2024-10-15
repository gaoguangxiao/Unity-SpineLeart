using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterAttackModel
{
    //角色攻击速度，基本速度为0.75一次，收到装备的加成
    public float AttackSpeed;
}

public class CharaterAttackScript : MonoBehaviour
{

    //武器
    public CharaterGunScript charaterGunScript;

    //UI面板
    public UICharaterInfoScript infoScript;


    // Start is called before the first frame update
    void Start()
    {
        CharaterAttackModel attackModel = new CharaterAttackModel();
        attackModel.AttackSpeed = 1 / charaterGunScript.shootInterval;
        Debug.Log("攻速：" + attackModel.AttackSpeed);

        infoScript.UpdateAttackInfo(attackModel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //提升攻击速度
    public void IncreaseAttack()
    {
        //当前枪支 30%的
        float aSpeed = charaterGunScript.shootInterval * 0.9f;

        Debug.Log("攻速：" + aSpeed);

        if (aSpeed < 0.40) return;

        charaterGunScript.shootInterval = aSpeed;

        CharaterAttackModel attackModel = new CharaterAttackModel();
        attackModel.AttackSpeed = 1 / charaterGunScript.shootInterval;
        
        infoScript.UpdateAttackInfo(attackModel);
    }

}
