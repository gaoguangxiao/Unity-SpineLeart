using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelInfoScript : MonoBehaviour
{
    //关卡0-10
    public Text LevelText;

    //击败敌人分数
    public Text AttackScoreText;

    //角色
    public CharaterAttackScript attackScript;

    //关卡
    public int Level = 1;

    //分数
    public float LocalScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        LevelText.text = "第" + Level + "关";
    }

    // Update is called once per frame
    void Update()
    {
        //AttackScoreText.text = "分数：" + enemyManager.LocalScore.ToString("F2");
    }

    public void UpdateLevel(int level)
    {
        Level += level;
        LevelText.text = "第" + Level + "关";
    }

    public void UpdateScore(float score)
    {
        LocalScore += score;

        AttackScoreText.text = "分数：" + LocalScore.ToString("F2");

        //分数超过10，20，40，80升级攻速
        if (LocalScore >= 10 && LocalScore < 20)
        {
            if (Level >= 2) return;
            //攻速提升20%；
            attackScript.IncreaseAttack();
            UpdateLevel(1);
        }
        else if (LocalScore >= 30 && LocalScore < 60)
        {
            if (Level >= 3) return;
            attackScript.IncreaseAttack();
            UpdateLevel(1);
        }
        else if (LocalScore >= 70 && LocalScore < 150)
        {
            if (Level >= 4) return;
            attackScript.IncreaseAttack();
            UpdateLevel(1);
        }
        else if (LocalScore >= 150 && LocalScore < 300)
        {
            attackScript.IncreaseAttack();
            UpdateLevel(1);
        }
        else if (LocalScore >= 300 && LocalScore < 600)
        {
            attackScript.IncreaseAttack();
            UpdateLevel(1);
        }
        else if (LocalScore >= 600 && LocalScore < 1200)
        {
            attackScript.IncreaseAttack();
            UpdateLevel(1);
        }
        else
        {
            //
        }
    }
}
