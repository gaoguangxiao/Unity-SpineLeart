using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGXInputManager : MonoBehaviour
{
    public FixedJoystick fixedJoystick;

    // Start is called before the first frame update
    void Start()
    {
        //fixedJoystick = FindAnyObjectByType<FixedJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("fixedJoystick.Horizontal" + fixedJoystick.Horizontal);
    }
}
