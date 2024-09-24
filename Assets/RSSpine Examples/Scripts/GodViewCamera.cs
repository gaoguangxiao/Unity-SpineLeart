using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodViewCamera : MonoBehaviour
{
    //人物对象
    public GameObject CharaterGameobject;

    public Vector3 lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position - CharaterGameobject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = CharaterGameobject.transform.position + lastPosition;
    }
}
