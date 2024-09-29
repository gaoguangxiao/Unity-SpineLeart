using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

//输入控制-虚拟轴
public class CharaterInputScript : MonoBehaviour
{
    
    public string HorizontalAxis = "Horizontal";

    public string VerticalAxis = "Vertical";

    public Spine.Unity.Examples.CharaterMainScript model;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (model == null) return;

        //
        float currentHorizontal = Input.GetAxisRaw(HorizontalAxis);
        model.TryMoveToHorizontal(currentHorizontal);

        float currentVertical = Input.GetAxisRaw(VerticalAxis);
        model.TryMoveToVertical(currentVertical);
       
    }
}
