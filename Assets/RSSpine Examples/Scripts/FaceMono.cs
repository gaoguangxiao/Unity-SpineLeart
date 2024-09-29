using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickType;

//更换脸部效果
namespace Spine.Unity.Examples
{
    public class FaceMono : MonoBehaviour
    {
        //角色脚本
        public SkeletonControlScript skeletonControlScript;

        //发型控制
        public ChangePanelScript changePanelScript;

        public ChangePanelScript mmPanelScript;

        public ChangePanelScript bzPanelScript;

        public ChangePanelScript yjPanelScript;

        public ChangePanelScript fsPanelScript;
        //收集脸部特征
        List<string> tfls = new List<string>();//发型
        List<string> mmls = new List<string>();//眉毛
        List<string> eyels = new List<string>();//眼睛
        List<string> nosels = new List<string>();//鼻子
        List<string> skinColorls = new List<string>();//肤色
        List<string> tzs = new List<string>();//套装
        List<string> roles = new List<string>();//角色
        //角色
        // Start is called before the first frame update
        void Start()
        {
            if (skeletonControlScript == null) return;

            Refresh(skeletonControlScript);

            if (changePanelScript)
            {
                changePanelScript.LoopList = tfls;
                changePanelScript.action += OnClickEvent;
            }

            if (mmPanelScript)
            {
                mmPanelScript.LoopList = mmls;
                mmPanelScript.action += OnClickEvent;
            }

            if (bzPanelScript)
            {
                bzPanelScript.LoopList = nosels;
                bzPanelScript.action = OnClickEvent;
            }

            if (yjPanelScript)
            {
                yjPanelScript.LoopList = eyels;
                yjPanelScript.action = OnClickEvent;
            }

            if (fsPanelScript)
            {
                fsPanelScript.LoopList = skinColorls;
                fsPanelScript.action = OnClickEvent;
            }
        }

        public void Refresh(SkeletonControlScript script)
        {
            skeletonControlScript = script;
            var SkinList = skeletonControlScript.skinNames;
            for (int i = 0; i < SkinList.Length; i++)
            {
                SkinList skin = SkinList[i];
                if (skin.Name.Contains("toufa")) tfls.Add(skin.Name);
                if (skin.Name.Contains("bizi")) nosels.Add(skin.Name);
                if (skin.Name.Contains("fuse")) skinColorls.Add(skin.Name);
                if (skin.Name.Contains("meimao")) mmls.Add(skin.Name);
                if (skin.Name.Contains("yan")) eyels.Add(skin.Name);
                if (skin.Name.Contains("taozhuang")) tzs.Add(skin.Name);
            }

            roles.Add("moren");
            roles.Add("celuezhizhe");
            roles.Add("cihuiyongzhe");
            roles.Add("langduqishi");
            roles.Add("pindufashi");
            roles.Add("yufazhanglao");
        }

        public void OnClickEvent(int index, string name)
        {
            skeletonControlScript.UpdateMatchSpineSkin(name);
        }


        public void RandomAll()
        {
           string  str = RandomRole();
            if (str == "moren")
            {
                //默认才有脸部
                //RandomHair();
                //RandomEyes();
                //RandomTZ();
            }
        }

        public string RandomRole()
        {
            int index = Random.Range(0, roles.Count - 1);
            string tf = roles[index];
            skeletonControlScript.InitSpineSKin(tf);
            return tf;
        }


        //发型随机
        public void RandomHair()
        {
            int index = Random.Range(0, tfls.Count - 1);
            string tf = tfls[index];
            skeletonControlScript.UpdateMatchSpineSkin(tf);
        }

        //眼睛随机
        public void RandomEyes()
        {
            int index = Random.Range(0, eyels.Count - 1);
            string tf = eyels[index];
            skeletonControlScript.UpdateMatchSpineSkin(tf);
        }

        //套装随机
        public void RandomTZ()
        {
            int index = Random.Range(0, tzs.Count - 1);
            string tf = tzs[index];
            skeletonControlScript.UpdateMatchSpineSkin(tf);
        }
    }
}