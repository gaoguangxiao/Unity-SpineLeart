using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundWindow : MonoBehaviour
{
    public GameObject LeftStartGameObject;

    // Start is called before the first frame update
    void Start()
    {

        //屏幕起始点
        Vector3 dVector = Camera.main.ScreenToWorldPoint(new Vector3(0, 0 ,2));
        LeftStartGameObject.transform.position = dVector;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
