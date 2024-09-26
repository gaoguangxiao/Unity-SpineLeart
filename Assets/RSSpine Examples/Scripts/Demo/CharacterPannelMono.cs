using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickType;
using UnityEngine.UI;
using Spine.Unity.Examples;
/// 示例demo
namespace Spine.Unity.Examples
{
    //using
    public class CharacterPannelMono : MonoBehaviour
    {
        public GameObject ContentView; // 在Unity编辑器中设置Canvas

        //按钮预置体
        public GameObject ButtonPrefab;

        public ChildControlScript childControlScript;

        //角色数据
        public CharacterScript characterScriptData;

        //角色脚本
        public SkeletonGraphicScript skeletonGraphicScript;
        //改版角色
        //public CharaterSpineScript charaterSpineScript;

        public ActionControl actionControl;//动作
        public TypeMono typeMono;//子列表类型

        Datum[] Datums;//显示的角色列表
        private Datum currentDatum;//当前角色

        private void Start()
        {
            //v2之前为网路数据
            //characterScriptData.OnDataLoadComplete += RefreshCharaterData;
            //characterScriptData.RefreshCharacterData();

            typeMono.action += TypeUpdate;
            actionControl.OrientationAction += UpdateOrientation;
            //本地json读取
            RefreshCharaterData();
            //ActionGamePannel.action = ItemAction;
        }

        void UpdateOrientation(bool value)
        {
            skeletonGraphicScript.UpdateReverseX(value);
        }

        /// <summary>
        /// 获取角色数据
        /// </summary>
        public void RefreshCharaterData()
        {
            //加载角色资产数据
            //Datum[] datums = characterScriptData.GetCharaterData();
            Datums = characterScriptData.GetCharacterDataJSONV5();

            if (Datums.Length == 0) return;
            //渲染角色数据面板
            CreateCharaterUI(Datums);

            //默认显示第一个角色
            currentDatum = Datums[0];
            //筛选UI列表
            RefreshUI();
        }

        void CreateCharaterUI(Datum[] datums)
        {

            for (int i = 0; i < datums.Length; i++)
            {
                GameObject buttonGameObject = Object.Instantiate(ButtonPrefab, ContentView.transform);

                GXButtonScript buttonScript = buttonGameObject.GetComponent<GXButtonScript>();

                Datum datum = datums[i];
                buttonScript.buttonIndex = datum.Id;
                //向spine预制体数组填充可用的模型
                buttonScript.SetText(datum.Name, "");

                Button button = buttonGameObject.GetComponent<Button>();
                //设置选中状态
                //if(i == 0) button.Select();

                //add listener
                button.onClick.AddListener(() => OnButtonClick(buttonScript));
            }
        }

        void OnButtonClick(GXButtonScript buttonScript)
        {
            //切换选中
            Button button = buttonScript.gameObject.GetComponent<Button>();
            button.Select();
            //获取角色
            currentDatum = GetCustomData(buttonScript.buttonIndex);
            //更新其角色皮肤
            //根据角色刷新UI
            RefreshUI();
        }

        /// <summary>
        /// 更新角色皮肤
        /// </summary>
        private void RefreshUI()
        {
            //
            skeletonGraphicScript.ReverseX = actionControl.AniReverseX;


            //更新角色显示
            skeletonGraphicScript.UpdateSpineDataAsset(currentDatum);
            //更换资产
            //skeletonGraphicScript.UpdateSpineDataAssetV2(currentDatum);

            currentDatum.ActionList = skeletonGraphicScript.animationNames;
            currentDatum.SkinList = skeletonGraphicScript.skinNames;

            //currentDatum.isPartSKin = //
            //创建Item列表
            //createList.CreateUI(currentDatum, typeMono.listType);
            //加载角色动作或皮肤
            //ActionGamePannel.CreateUI(currentDatum, typeMono.listType);
            //v3
            childControlScript.CreateUI(currentDatum, typeMono.listType);
            //初始化-同上一行代码，内部有回调
            //typeMono.UpdateListType(typeMono.listType);
        }

        /// <summary>
        /// 更新皮肤
        /// </summary>
        /// <param name="type"></param>
        void TypeUpdate(int type)
        {
            //更新子列表
            //ActionGamePannel.CreateUI(currentDatum, type);
            //v3
            childControlScript.CreateUI(currentDatum, type);
        }
        /// <summary>
        /// 通过角色Id获取当前角色面板的数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Datum GetCustomData(long Id)
        {
            Datum spineData = new Datum();
            foreach (Datum data in Datums)
            {
                if (data.Id == Id)
                {
                    spineData = data;
                    break;
                }
            }
            if (spineData == null)
            {
                Debug.Log("没有角色数据");
            }
            return spineData;
        }

    }
}
