using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class BulletDestory : MonoBehaviour
{
    int count = 0;

    int maxCount = 100;

    public event System.Action<int> CountEvent;

    //子弹销毁
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("Bullet"))
        {
            count += 1;
            Destroy(other.gameObject);
            UpdateTxt();
        }
        
    }

    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            count += 1;
            Destroy(collision.gameObject);
            UpdateTxt();
        }
    }

    void UpdateTxt()
    {
        //CountEvent(count);
        //BlowsTxt.text = "次数”：" + count;
    }
}
