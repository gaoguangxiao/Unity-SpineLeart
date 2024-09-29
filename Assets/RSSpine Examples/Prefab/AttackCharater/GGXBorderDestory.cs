using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GGXBorderDestory : MonoBehaviour
{
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

            Destroy(collision.gameObject);
        }
    }
}

