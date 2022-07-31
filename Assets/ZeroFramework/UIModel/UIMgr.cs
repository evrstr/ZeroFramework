using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using ZeroFramework.Log;
using ZeroFramework.Config;

using Cysharp.Threading.Tasks;

using YooAsset;
using System;
using cfg.Game;

namespace ZeroFramework.UI
{
    // 精华：showpanel 里的回调函数！在showpanel异步加载完成后调用的函数
    // UI层级
    public enum E_UI_Layer
    {
        BOT,
        MID,
        TOP,
        SYS
    }

    /// <summary>
    /// UI管理器
    /// 1. 管理所有显示的面板
    /// 2. 提供给外部 显示和隐藏等等接口
    /// </summary>
    ///
    public class UIMgr : SingletonBase<UIMgr>
    {
        public Dictionary<string, UIControllerBase> panelDicts = new();

        private Transform bot;
        private Transform mid;
        private Transform top;
        private Transform sys;

        public GameObject Canvas { get; private set; }
        public GameObject EventSystem { get; private set; }

        protected override async void OnInit()
        {
            ZLog.LogDebug("UIMgr OnInit");
            Canvas = GameObject.Find("Canvas");
            if (Canvas == null)
            {
                var canvasPrefab = YooAssets.LoadAssetAsync<GameObject>(ConfigMgr.UIPath.Canvas);
                await canvasPrefab.ToUniTask();
                Canvas = canvasPrefab.InstantiateSync();
                Canvas.name = "Canvas";
            }
            EventSystem = Canvas.transform.Find("EventSystem").gameObject;
            // 找到各个层
            this.bot = Canvas.transform.Find("Bot");
            this.mid = Canvas.transform.Find("Mid");
            this.top = Canvas.transform.Find("Top");
            this.sys = Canvas.transform.Find("Sys");
            GameObject.DontDestroyOnLoad(Canvas);
        }

        public async UniTask<T> ShowPanel<T>(string panelName, E_UI_Layer layer) where T : UIControllerBase
        {
            ZLog.LogDebug($"加载{panelName}面板...");
            if (panelDicts.ContainsKey(panelName))
            {
                panelDicts[panelName].Show();
                return panelDicts[panelName] as T;
            }
            var panelPrefab = YooAssets.LoadAssetAsync<GameObject>(panelName);
            await panelPrefab.ToUniTask();
            var o = panelPrefab.InstantiateAsync(GetLayer(layer));
            await o.ToUniTask();

            var _name = panelName.Split('/')[^1].Replace(".prefab", "");
            //o.Result.name = arr[^1].Replace(".prefab", "");
            o.Result.name = _name;

            if (!o.Result.TryGetComponent(out T ctrl))
            {
                //T Controllertype = Type.GetType(_name + "Controller") as T;
                //Type Viewtype = Type.GetType(_name + "View");

                // o.Result.AddComponent(Viewtype);
                ctrl = o.Result.AddComponent<T>();
                ctrl.PreInit();
            }
            panelDicts.Add(panelName, ctrl);

            return ctrl;
        }

        public void HidePanel(string panelName)
        {
            if (panelDicts.ContainsKey(panelName))
            {
                panelDicts[panelName].GetComponent<UIControllerBase>().Hide();
            }
        }

        public void RemovePanel(string panelName)
        {
            if (panelDicts.ContainsKey(panelName))
            {
                GameObject.Destroy(panelDicts[panelName]);
                panelDicts.Remove(panelName);
            }
        }

        public void AddBtnListener(UIControllerBase ctrl, string vieName, UnityAction action)
        {
            Button btn = ctrl.View.ViewDicts[vieName].GetComponent<Button>();
            if (btn == null)
            {
                ZLog.LogError($"{vieName}按钮不存在");
                return;
            }
            btn.onClick.AddListener(action);
        }

        private Transform GetLayer(E_UI_Layer layer)
        {
            return layer switch
            {
                E_UI_Layer.TOP => top,
                E_UI_Layer.MID => mid,
                E_UI_Layer.BOT => bot,
                E_UI_Layer.SYS => sys,
                _ => top,
            };
        }

        /// <summary>
        /// 给 UI 设置层
        /// </summary>
        /// <param name="obj">GameObject</param>
        /// <param name="layer">层类型</param>
        public void SetLayer(GameObject obj, E_UI_Layer layer)
        {
            obj.transform.SetParent(GetLayer(layer), false);
        }

        /// <summary>
        /// 给 UI 设置层
        /// </summary>
        /// <param name="obj">Transform</param>
        /// <param name="layer">层类型</param>
        public void SetLayer(Transform obj, E_UI_Layer layer)
        {
            obj.SetParent(GetLayer(layer), false);
        }
    }
}