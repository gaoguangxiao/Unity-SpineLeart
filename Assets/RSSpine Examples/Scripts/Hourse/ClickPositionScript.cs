using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPositionScript : MonoBehaviour
{

    //音频播放脚本
    public AudioManagerScript AudioPlayTool;

    // Start is called before the first frame update
    void Start()
    {
        //AudioPlayTool = GetComponent<AudioManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowClickPostion(bool isActive)
    {
        if (gameObject.active == isActive) return;

        this.gameObject.SetActive(isActive);
        
        if(isActive)
        {
            //Debug.Log("播放点击音");
            AudioPlayTool.PlayAudio("/static/click.64c61995.mp3");
        }
    }
}
