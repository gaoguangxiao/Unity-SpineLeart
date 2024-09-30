using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDestory : MonoBehaviour
{
    //引用
    public EnemyManager enemyManager;

    public EnemyModel enemyModel;

    //size：控制滑块的大小
    public Scrollbar BloodScrollbar;

    public event System.Action<int> CountEvent;


    private void Start()
    {

    }

    public void SetMaxBlood(EnemyModel enemy)
    {
        enemyModel = enemy;
        //BloodScrollbar.size = blood;
        //Debug.Log("enemy.MaxBlood is:" + enemy.MaxBlood);
    }

    //销毁
    private void OnTriggerEnter(Collider other)
    {
        HandleGameobject(other.gameObject);
    }

    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleGameobject(collision.gameObject);

    }

    void HandleGameobject(GameObject collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            enemyModel.CurrentBlood += 1;
            UpdateTxt();

            //当对象为子弹时，销毁其对象
            Destroy(collision.gameObject);

            float pro = enemyModel.CurrentBlood / enemyModel.MaxBlood;

            if (pro == 1) enemyModel.death = true;
            //Debug.Log("BloodScrollbar.size is:" + count + "pro is： " + pro);
            //value：当前滑块的值 介于0-1之间
            //size、滑块的大小
            BloodScrollbar.size = pro;
        }

        if (collision.gameObject.CompareTag("Border"))
        {
            Destroy(gameObject);
        }

        //判断当前物体是否死亡，
        //Debug.Log("enemyModel is" + enemyModel);
        if (enemyModel != null && enemyModel.death)
        {
            enemyManager.SkillEnemy(enemyModel,gameObject);
        }
    }

    void UpdateTxt()
    {
        //if (CountEvent != null) CountEvent(count);
    }
}
