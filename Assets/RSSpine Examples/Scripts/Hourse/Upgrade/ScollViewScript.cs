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

    private float PrefabWidth;//item width

    private UpgradeGoodsScript updatGoodsScript;

    private ScrollRect ScrollRect;

    private RectTransform HeadRect;

    //末尾增量
    private RectTransform FootRect;

    private int CurrentIndex = 1;//选中的位置

    public UpgradeGoodsModel CurrentGoodModel;//选中的物品

    public UserInfoScript userInfoScript;//用户数据

    // Start is called before the first frame update
    void Start()
    {
        PrefabWidth = ButtonPrefab.GetComponent<RectTransform>().sizeDelta.x;
        //Debug.Log("item width is: " + PrefabWidth);

        ScrollRect = GetComponent<ScrollRect>();
        if (ScrollRect == null)
        {
            Debug.LogError("ScrollRect component not found!");
            return;
        }

        //可更新物品数据
        updatGoodsScript = GetComponent<UpgradeGoodsScript>();
        updatGoodsScript.OnDataLoadComplete += OnDataLoadComplete;
        updatGoodsScript.RefreshData();
        
        //buddyLevel
        userInfoScript.OnDataLoadComplete += OnUserDataLoadComplete;
    }

   
    private void OnUserDataLoadComplete(UserData userData)
    {
        Debug.Log("伙伴之家等级：" + userData.BuddyLevel);
        CurrentIndex = userData.BuddyLevel;
        OnUpdateUI();

        //滚动指定位置
        Vector2 targetPosition = new (-(CurrentIndex - 1)* PrefabWidth, 0);
        Debug.Log("targetPosition：" + targetPosition);
        ScrollRect.content.anchoredPosition = targetPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        //isScrolling = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag：" + ScrollRect.content.anchoredPosition);
        //isScrolling = false;
        float endDragOffset = Mathf.Abs(ScrollRect.content.anchoredPosition.x);
        //Debug.Log("Scroll Value xx: " + xx);
        CurrentIndex = Mathf.CeilToInt(endDragOffset / PrefabWidth);//取整向0.1->1
        Debug.Log("选中的物品：" + CurrentIndex);
        OnUpdateUI();
    }

    void OnUpdateUI()
    {
        Debug.Log("OnUpdateUI");
        //获取选中的物品
        GameObject itemContent = ScrollRect.content.GetChild(CurrentIndex).gameObject;
        //Debug.Log("itemContent：" + itemContent);
        if (itemContent.tag != "Upgrade") return;
        UpgradeItemScript selectedScript = itemContent.GetComponent<UpgradeItemScript>();
        //Debug.Log("selectedScript：" + selectedScript.LocalGoodsModel.Name);

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
        CurrentGoodModel = selectedScript.LocalGoodsModel;
        //Debug.Log("要添加的家具：" + goodsModel.Name);
    }
    
    void OnDataLoadComplete(UpgradeGoodsModel[] upgradeGoods)
    {
        //update selected item
        CurrentIndex = userInfoScript.rSResponse.Data.BuddyLevel;
        if (CurrentIndex - 1 <= upgradeGoods.Length)
        {
            CurrentGoodModel = upgradeGoods[CurrentIndex - 1];
        }
        Debug.Log("CurrentGoodModel is: " + CurrentGoodModel.Name);
        
        //inset head view
        GameObject Headjv = GameObject.Instantiate(FootPrefab, ContentView.transform);
        HeadRect = Headjv.GetComponent<RectTransform>();
        HeadRect.sizeDelta = new Vector2(Screen.width / 2 - 150, 1);

        //add item view
        foreach (var item in upgradeGoods)
        {
            if (CurrentGoodModel != null && item.Id == CurrentGoodModel.Id) item.IsSelected = true;
            GameObject buttonGameObject = GameObject.Instantiate(ButtonPrefab, ContentView.transform);
            UpgradeItemScript buttonScript = buttonGameObject.GetComponent<UpgradeItemScript>();
            buttonScript.SetGoods(item);
        }

        //inset foot view
        GameObject fonjv = GameObject.Instantiate(FootPrefab, ContentView.transform);
        FootRect = fonjv.GetComponent<RectTransform>();
        FootRect.sizeDelta = new Vector2(Screen.width / 2 - 150, 1);
 
        //scroll to posion
        Vector2 targetPosition = new(-(CurrentIndex - 1) * PrefabWidth, 0);
        ScrollRect.content.anchoredPosition = targetPosition;
    }
}
