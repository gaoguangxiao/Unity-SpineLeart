using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity.Examples;

//角色重生和死亡
public class CharaterDestory : MonoBehaviour
{
    CharaterMainScript model;

    bool IsGameOver;

    private void Start()
    {
        model = GetComponent<CharaterMainScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter" + other.gameObject);

        if (other.gameObject.CompareTag("Enemy") && !IsGameOver)
        {

            IsGameOver = true;

            Destroy(other.gameObject);

            model.TryDeathEvent();
            //
            StartCoroutine(WaitUntilStopped(other));
        }
    }


    //恢复游戏
    IEnumerator WaitUntilStopped(Collider collision)
    {
        yield return new WaitForSeconds(2.0f);

        IsGameOver = false;
        
        model.TryResurgenceEvent();
    }
}
