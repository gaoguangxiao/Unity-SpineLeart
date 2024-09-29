using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class EnemyDestory : MonoBehaviour
{
    public EnemyModel enemyModel;

    int count = 0;

    float maxCount = 1.0f;

    //size：控制滑块的大小
    public Scrollbar BloodScrollbar;

    public event System.Action<int> CountEvent;

    private void Start()
    {
        //SetMaxBlood(maxCount);
    }

    public void SetMaxBlood(EnemyModel enemy)
    {
        enemyModel = enemy;
        //BloodScrollbar.size = blood;
        //Debug.Log("BloodScrollbar.size is:" + BloodScrollbar.size);
    }

    //销毁
    private void OnTriggerEnter(Collider other)
    {
        handleGameobject(other.gameObject);
    }

    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        handleGameobject(collision.gameObject);

    }

    void handleGameobject(GameObject collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            enemyModel.CurrentBlood += 1;
            UpdateTxt();
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

        if (enemyModel.death) Destroy(gameObject);
    }

    void UpdateTxt()
    {
        if (CountEvent != null) CountEvent(count);
    }
}
