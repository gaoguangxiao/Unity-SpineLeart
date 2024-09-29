using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDestory : MonoBehaviour
{
    int count = 1;

    int maxCount = 2;

    //size：控制滑块的大小
    public Scrollbar BloodScrollbar;

    public event System.Action<int> CountEvent;

    private void Start()
    {
        SetMaxBlood(maxCount);
    }

    public void SetMaxBlood(int blood)
    {
        maxCount = blood;
        //BloodScrollbar.size = blood;
        Debug.Log("BloodScrollbar.size is:" + BloodScrollbar.size);
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
            count += 1;
            UpdateTxt();
            Destroy(collision.gameObject);

            //value：当前滑块的值 介于0-1之间
            //size、滑块的大小
            BloodScrollbar.size = count/maxCount;
        }

        if (collision.gameObject.CompareTag("Border"))
        {
            Destroy(gameObject);
        }

        if (count >= maxCount) Destroy(gameObject);
    }

    void UpdateTxt()
    {
        if (CountEvent != null) CountEvent(count);
    }
}
