using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AudioManagerScript: MonoBase
{
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        NetManager.Instance.Register(this);

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(string url)
    {
       

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
        {
            Dictionary<string, object> paramsDicts = new Dictionary<string, object>();
            paramsDicts.Add("speed", 1);
            paramsDicts.Add("track", "unity_button");
            paramsDicts.Add("url", NetManager.Instance.GetWebHost() + url);

            BridgeScript.Instance.CallApp(new Message(MessageType.Type_plug, MessageType.Command_PlayAudio, paramsDicts));
        }  else
        {
            StartCoroutine(StartPlay(NetManager.Instance.GetWebHost() + url));
        }
    }

    IEnumerator StartPlay(string audioUrl)
    {
        UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(audioUrl, AudioType.MPEG);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            AudioClip audioClip = DownloadHandlerAudioClip.GetContent(request);
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
