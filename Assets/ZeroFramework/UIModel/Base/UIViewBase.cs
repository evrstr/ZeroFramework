using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ZeroFramework.UI
{
    public abstract class UIViewBase : MonoBehaviour
    {
        public Dictionary<string, GameObject> ViewDicts = new Dictionary<string, GameObject>();//面板组件字典

        //初始化
        public virtual void PreInit()
        {
            FindAllChildrenUI(this.gameObject, "");
        }

        //查找所有节点
        protected void FindAllChildrenUI(GameObject root, string path)
        {
            foreach (Transform item in root.transform)
            {
                if (this.ViewDicts.ContainsKey(path + item.gameObject.name))
                {
                    Debug.Log("已经存在" + path + item.gameObject.name);
                    continue;
                }
                this.ViewDicts.Add(path + item.gameObject.name, item.gameObject);
                //Debug.Log(path + item.gameObject.name);
                FindAllChildrenUI(item.gameObject, path + item.gameObject.name + "/");
            }
        }

        //public abstract void UpdateInfo(UIModelBase model);
    }
}