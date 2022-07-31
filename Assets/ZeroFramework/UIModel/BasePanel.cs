using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace ZeroFramework.UI
{
    /// <summary>
    /// 面板基类
    /// 找到所有自己面板下的控件对象
    /// 方便我们再子类中处理逻辑
    /// 节约找控件的工作量
    /// UI基类 BasePanel.cs，以后子类只需要继承这个基类
    /// 一定一定一定要注意泛型的约束 where T: [class]
    /// </summary>
    public class BasePanel : MonoBehaviour
    {
        // 里氏转换原则，所有UI都会继承UIBehaviour
        protected Dictionary<string, List<UIBehaviour>> uiDic = new Dictionary<string, List<UIBehaviour>>();

        protected virtual void Awake()
        {
            FindChildrenUIComponent<Button>();
            FindChildrenUIComponent<Image>();
            FindChildrenUIComponent<Text>();
            FindChildrenUIComponent<TMP_Text>();
            FindChildrenUIComponent<Toggle>();
            FindChildrenUIComponent<Slider>();
            FindChildrenUIComponent<ScrollRect>();
            FindChildrenUIComponent<InputField>();
            FindChildrenUIComponent<TMP_InputField>();
        }

        // 显示自己
        public virtual void ShowMe()
        {
            gameObject.SetActive(true);
        }

        // 隐藏自己
        public virtual void HideMe()
        {
            gameObject.SetActive(false);
        }

        public virtual void DestroyMe()
        {
            Destroy(this.gameObject);
        }

        protected T GetControl<T>(string name) where T : UIBehaviour
        {
            if (uiDic.ContainsKey(name))
            {
                for (int i = 0; i < uiDic[name].Count; i++)
                {
                    if (uiDic[name][i] is T) return uiDic[name][i] as T;
                }
            }
            return null;
        }

        private void FindChildrenUIComponent<T>() where T : UIBehaviour
        {
            T[] array = this.GetComponentsInChildren<T>();
            string objName;
            for (int i = 0; i < array.Length; i++)
            {
                objName = array[i].gameObject.name;
                if (uiDic.ContainsKey(objName))
                {
                    uiDic[objName].Add(array[i]);
                }
                else
                {
                    uiDic.Add(objName, new List<UIBehaviour>() { array[i] });
                }
            }
        }
    }
}