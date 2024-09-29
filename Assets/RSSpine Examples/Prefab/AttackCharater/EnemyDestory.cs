using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class EnemyDestory : MonoBehaviour
{
    int count = 1;

    int maxCount = 2;

    public event System.Action<int> CountEvent;

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
