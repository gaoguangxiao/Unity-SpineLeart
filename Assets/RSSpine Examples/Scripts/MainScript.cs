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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLoadingProgress();
    }

    public void OnBackClick()
    {
        StartCoroutine(LoadScene(0));
    }

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
}
