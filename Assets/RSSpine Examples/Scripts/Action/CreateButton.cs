using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using QuickType;
using System;
using System.IO;
using Spine.Unity.Examples;

namespace Spine.Unity.Examples
{
    public class CreateButton : MonoBehaviour
    {
        public Text title;

        public GameObject ContentView; // 在Unity编辑器中设置Canvas

        //聊天单位预置体
        public GameObject ButtonPrefab;

        //Item Action
        public Action<string> action;

        //SkinList
        public Action<GameObject> actionSkin;

        SkinList[] LocalSkinLists = { };

        public void DestroyActionUI()
        {
            foreach (Transform child in ContentView.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void CreateUI(Datum datum, int type)
        {

            //销毁动作列表
            DestroyActionUI();

            if (type == 0)
            {
                //动作+标签
                //CreateActionUI(datum,false);
                //CreateActionUI(datum, true);

                CreateAction(datum);

                title.gameObject.SetActive(false);
            }
            else if (type == 1)
            {
                //title.gameObject.SetActive(true);
                //title.text = "全皮肤或者部位名称：";
                //皮肤
                CreateSkin(datum);
               
            }

        }

       void CreateAction(Datum datum)
        {
            ActionList[] actionLists = datum.ActionList;
            for (int i = 0; i < actionLists.Length; i++)
            {
                GXButtonScript buttonScript = InitButton();
                ActionList actionList = actionLists[i];
                buttonScript.buttonIndex = i;
                buttonScript.SetText(actionList.Name, actionList.Remark);
            }

        }

       void CreateSkin(Datum datum)
        {

            SkinList[] skinLists = datum.SkinList;

            for (int i = 0; i < skinLists.Length; i++)
            {
                GXButtonScript buttonScript = InitButton();
                SkinList skinList = skinLists[i];
                buttonScript.buttonIndex = i;
                buttonScript.SetText(skinList.Name, skinList.Remark);
            }
        }

        /// <summary>
        /// 设置皮肤列表
        /// </summary>
        /// <param name="datum"></param>
        /// <param name="skinType">0全部，1部分</param>
        public void CreateSkin(SkinList[] SkinList)
        {

            DestroyActionUI();

            for (int i = 0; i < SkinList.Length; i++)
            {
                SkinList skinList = SkinList[i];
                GXButtonScript buttonScript = InitButton();
                buttonScript.buttonIndex = i;
                buttonScript.SetText(skinList.Name, "");
            }
        }

        /// <summary>
        /// 设置皮肤列表
        /// </summary>
        /// <param name="datum"></param>
        /// <param name="skinType">0全部，1部分</param>
        public void CreateSkinV2(UnlockDressList[] dressLists)
        {

            DestroyActionUI();

            for (int i = 0; i < dressLists.Length; i++)
            {
                UnlockDressList skinList = dressLists[i];

                GameObject buttonGameObject = GameObject.Instantiate(ButtonPrefab, ContentView.transform);
                SkinObjScript buttonScript = buttonGameObject.GetComponent<SkinObjScript>();
                //add listener
                buttonGameObject.GetComponent<Button>().onClick.AddListener(() => OnButtonGameClick(buttonGameObject));
                //return buttonScript;
                buttonScript.SetSkinV2(skinList);
            }
        }
        //动作数据
        public void CreateActionUI(Datum datum, bool isExp)
        {
            if (isExp)
            {
                ExpressionList[] expressionLists = datum.ExpressionList;
                for (int i = 0; i < expressionLists.Length; i++)
                {
                    GXButtonScript buttonScript = InitButton();
                    ExpressionList expression = expressionLists[i];
                    buttonScript.buttonIndex = i;
                    buttonScript.SetText("biaoqing/" + expression.Name, expression.Remark);
                }
            }
            else
            {
                ActionList[] actionLists = datum.ActionList;
                for (int i = 0; i < actionLists.Length; i++)
                {
                    GXButtonScript buttonScript = InitButton();
                    ActionList actionList = actionLists[i];
                    buttonScript.buttonIndex = i;
                    buttonScript.SetText(actionList.Name, actionList.Remark);
                }
            }
        }

        private GXButtonScript InitButton()
        {
            GameObject buttonGameObject = GameObject.Instantiate(ButtonPrefab, ContentView.transform);
            GXButtonScript buttonScript = buttonGameObject.GetComponent<GXButtonScript>();
            //add listener
            buttonGameObject.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(buttonScript));
            return buttonScript;
        }

        private GXButtonScript InitButton(GameObject prefab, MonoBehaviour monoBehaviour)
        {
            GameObject buttonGameObject = GameObject.Instantiate(prefab, ContentView.transform);
            GXButtonScript buttonScript = buttonGameObject.GetComponent<GXButtonScript>();
            //add listener
            buttonGameObject.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(buttonScript));
            return buttonScript;
//        }
//)
//        {
//            GameObject buttonGameObject = GameObject.Instantiate(ButtonPrefab, ContentView.transform);
//            GXButtonScript buttonScript = buttonGameObject.GetComponent<GXButtonScript>();
//            //add listener
//            buttonGameObject.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(buttonScript));
//            return buttonScript;
        }


        private GXButtonScript InitButtonName(string name,int index,int clickType)
        {
            GameObject buttonGameObject = GameObject.Instantiate(ButtonPrefab, ContentView.transform);
            GXButtonScript buttonScript = buttonGameObject.GetComponent<GXButtonScript>();

            //SkinList skinList = allSkinL[i];
            buttonScript.buttonIndex = index;
            buttonScript.SetText(name, "");
            if (clickType == 0)
            {
                buttonGameObject.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(buttonScript));
            } else
            {
                buttonGameObject.GetComponent<Button>().onClick.AddListener(() => OnButtonSkinClick(buttonScript));
            }

            return buttonScript;
        }

        void OnButtonClick(GXButtonScript buttonScript)
        {
            action(buttonScript.TextButton.text);            
        }

        void OnButtonGameClick(GameObject gameObject)
        {
            actionSkin(gameObject);
        }

        void OnButtonSkinClick(GXButtonScript buttonScript)
        {
            //actionSkin(LocalSkinLists[buttonScript.buttonIndex]);
        }
    }
}