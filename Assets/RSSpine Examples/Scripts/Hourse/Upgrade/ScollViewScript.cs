using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ScollViewScript : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public GameObject FootPrefab;//末尾

    public GameObject ContentView; // 在Unity编辑器中设置Canvas

    //物品单元
    public GameObject ButtonPrefab;

    private UpgradeGoodsScript updatGoodsScript;

    private ScrollRect ScrollRect;

    private RectTransform HeadRect;

    //末尾增量
    private RectTransform FootRect;

    //private bool isScrolling;

    //private Vector2 _startDragOffset;

    private UpgradeGoodsModel[] LocalGoodModels;//可选中的物品数组
    private int CurrentIndex = 1;//选中的位置


    public UpgradeGoodsModel CurrentGoodModel;//选中的物品

    //public Action<UpgradeGoodsModel> OnDataScrollComplete;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Headjv = GameObject.Instantiate(FootPrefab, ContentView.transform);
        HeadRect = Headjv.GetComponent<RectTransform>();
        HeadRect.sizeDelta = new Vector2(Screen.width / 2 - 150, 10);

        updatGoodsScript = GetComponent<UpgradeGoodsScript>();
        updatGoodsScript.OnDataLoadComplete += OnDataLoadComplete;
        //获取可更新数据
        updatGoodsScript.RefreshData();

        ScrollRect = GetComponent<ScrollRect>();
        if (ScrollRect == null)
        {
            Debug.LogError("ScrollRect component not found!");
            return;
        }
        //ScrollRect.onValueChanged.AddListener(OnScrollValueChanged);
    }

    //滚动监听
    //滑动时调用
    //public void OnScrollValueChanged(Vector2 position)
    //{
    //    //Debug.Log("Scroll Value Changed: " + position);
    //    Vector2 sizeDelta = ScrollRect.content.sizeDelta;
    //    float xx = position.x * (sizeDelta.x - HeadRect.sizeDelta.x - FootRect.sizeDelta.x);
    //    //Debug.Log("Scroll Value xx: " + xx);
    //    //int index = Mathf.CeilToInt(xx / 450.0f);//取整向0.1->1
    //    int index = Mathf.FloorToInt(xx / 450.0f);//取整向0.1->0

    //    if (!isScrolling)
    //    {
    //        if (CurrentIndex == index) return;
    //        CurrentIndex = index;
    //        Debug.Log("Scroll Value index: " + CurrentIndex);
    //    }

    //}

    public void OnBeginDrag(PointerEventData eventData)
    {
        //_startDragOffset = ScrollRect.content.anchoredPosition;
        Debug.Log("OnBeginDrag");
        //isScrolling = true;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        //isScrolling = false;

        float endDragOffset = Mathf.Abs(ScrollRect.content.anchoredPosition.x);

        //Debug.Log("Scroll Value xx: " + xx);
        CurrentIndex = Mathf.CeilToInt(endDragOffset / 450.0f);//取整向0.1->1
        //int index = Mathf.FloorToInt(endDragOffset / 450.0f);//取整向0.1->0

        //Debug.Log("Scroll Value index: " + CurrentIndex);

        Debug.Log("选中的物品：" + CurrentIndex);

        //获取选中的物品
        GameObject itemContent = ScrollRect.content.GetChild(CurrentIndex).gameObject;
        //Debug.Log("itemContent：" + itemContent);
        if (itemContent.tag != "Upgrade") return;
        UpgradeItemScript selectedScript = itemContent.GetComponent<UpgradeItemScript>();
        Debug.Log("selectedScript：" + selectedScript.LocalGoodsModel.Name);

        for (int i = 0; i < ScrollRect.content.childCount; i++)
        {
            GameObject _itemContent = ScrollRect.content.GetChild(i).gameObject;
            if (_itemContent.tag == "Upgrade")
            {
                UpgradeItemScript _nomalItemScript = _itemContent.GetComponent<UpgradeItemScript>();
                if (selectedScript.LocalGoodsModel.Name == _nomalItemScript.LocalGoodsModel.Name)
                {
                    _nomalItemScript.LocalGoodsModel.IsSelected = true;
                }
                else
                {
                    _nomalItemScript.LocalGoodsModel.IsSelected = false;
                }

                _nomalItemScript.ReloadData();
            }
        }

        OnUpdateUI(selectedScript.LocalGoodsModel);
        //}
    }

    void OnUpdateUI(UpgradeGoodsModel goodsModel)
    {
        CurrentGoodModel = goodsModel;
        Debug.Log("要添加的家具：" + goodsModel.Name);
    }

    void OnDataLoadComplete(UpgradeGoodsModel[] upgradeGoods)
    {
        LocalGoodModels = upgradeGoods;
        foreach (var item in upgradeGoods)
        {
            GameObject buttonGameObject = GameObject.Instantiate(ButtonPrefab, ContentView.transform);
            UpgradeItemScript buttonScript = buttonGameObject.GetComponent<UpgradeItemScript>();
            //buttonGameObject.GetComponent<Button>().onClick.AddListener(() => OnClickUpgrade(item));
            buttonScript.SetGoods(item);
        }

        GameObject fonjv = GameObject.Instantiate(FootPrefab, ContentView.transform);
        FootRect = fonjv.GetComponent<RectTransform>();
        FootRect.sizeDelta = new Vector2(Screen.width / 2 - 150, 10);
    }

    //public void OnClickUpgrade()
    //{
    //    Debug.Log("添加家具：" + CurrentGoodModel.Name);
    //    if (OnDataScrollComplete != null)
    //        OnDataScrollComplete(CurrentGoodModel);

    //}
}
