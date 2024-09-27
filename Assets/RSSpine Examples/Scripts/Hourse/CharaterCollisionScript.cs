using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity.Examples;

//障碍物管理
public class CharaterCollisionScript : MonoBehaviour
{

    public GameObject ContentView;

    public GameObject StonePrefab;

    //public Vector3 SpawnPoint;

    //角色移动参照物
    public Transform accumulationIndicatorObject;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //生成下一个障碍物
    public void CreateStone()
    {
        GameObject StonePrefabObject = GameObject.Instantiate(StonePrefab, ContentView.transform);

        float startoffx = accumulationIndicatorObject.position.x + Screen.width;
        //屏幕之后新建一个障碍物
        Vector2 newVector = new Vector3(Random.Range(startoffx, startoffx + Screen.width/2), accumulationIndicatorObject.position.y, 0);

        Debug.Log("newVector is：" + newVector);
        //Camera.main.ScreenToWorldPoint
        Vector3 worldVector = Camera.main.ScreenToWorldPoint(newVector);
        Debug.Log("worldVector is：" + worldVector);
        //X屏幕随机，转移至世界坐标
        StonePrefabObject.transform.position = worldVector;

    }

    public void clearStone(Collision2D collision) {

        Destroy(collision.gameObject);

        CreateStone();

    }

}
