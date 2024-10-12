using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//场景加载器
public class MainScript : MonoBehaviour
{
    private AsyncOperation asyncOperation;

    /// 加载进度文本
    public Text proText;
    private float lastProgress;

    private void Awake()
    {
        BridgeScript.CallRegisterCallBackDelegate();
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLoadingProgress();
    }

    //返回原生界面
    public void OnBackClickNative()
    {
        //Debug.Log("返回原生");
        Message message = new(MessageType.Type_plug, MessageType.UI_UnityClose, "");
        BridgeScript.Instance.CallApp(message);
    }

    public void OnBackClick()
    {
        StartCoroutine(LoadScene(0));
    }

    /// <summary>
    /// Demo演示
    /// </summary>
    public void OnSampleClock() {
        //Debug.Log("OnSampleClock");
        StartCoroutine(LoadScene(1));
    }

    /// <summary>
    /// 捏脸
    /// </summary>
    public void OnFaceClock()
    {
        StartCoroutine(LoadScene(3));
    }

    /// <summary>
    /// 换装
    /// </summary>
    public void OnDressClick()
    {
        StartCoroutine(LoadScene(2));
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnSampleClickV2()
    {
        StartCoroutine(LoadScene(4));
    }

    //加载完毕
    IEnumerator LoadScene(int index)
    {
        asyncOperation = SceneManager.LoadSceneAsync(index);
        asyncOperation.completed += OnLoadScene;

        //lastProgress = 0.0f;
        //Debug.Log("LoadScene");
        //加载进度
        asyncOperation.allowSceneActivation = false;//暂不激活新场景
        yield return asyncOperation;
    }

    //加载完成
    private void OnLoadScene(AsyncOperation obj)
    {
        Debug.Log("load finish");
    }

    // 更新进度并决定是否激活场景
    public void UpdateLoadingProgress()
    {
        if (asyncOperation == null) return;
 
        float progress = asyncOperation.progress;

        if(proText != null)
        proText.text = "加载中：" + progress;

        // 可以在这里添加进度条UI更新的代码
        Debug.Log("load scene progress is: " + progress);
        // 当进度达到某个阈值时，可以启用场景
        if (progress >= 0.9f && asyncOperation.isDone == false)
        {
            asyncOperation.allowSceneActivation = true; // 允许激活新场景
            //lastProgress = progress;
        }
    }

    //获取原生端token相关信息
    public void TestToken()
    {
        Debug.Log("TestToken");
        Dictionary<string, object> paramsDicts = new Dictionary<string, object>();
        paramsDicts.Add("key", "access_token");
        paramsDicts.Add("value", "Bearer eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJCYXpoZkIxMCIsInV1aWQiOiI1ZDg2YThmYjhlNzU0YjVjOTlmZTQxOGViZjc3M2U0" +
            "MCIsInRpbWVzdGFtcCI6MTcyODU0NjA1Njc5N30.IBJsvTBN7XyOMEHZEGkbQj_YH5kuHDpBpKYNCWI0xPR_-HrnuC0YdFLzP98tvvqS6MH6u3FlTsUSdxr8LdtTrg");
        Message message = new (MessageType.Type_UI, MessageType.getStorage, paramsDicts);
        //Message message = new Message(MessageType.Type_UI, MessageType.Net_Token, "");
        MC.Instance.SendCustomMessage(message);
 
    }
}
