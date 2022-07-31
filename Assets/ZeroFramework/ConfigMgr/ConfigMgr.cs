using cfg;
using SimpleJSON;
using UnityEngine;

using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Events;
using ZeroFramework;
using System.Threading.Tasks;
using System;

using Cysharp.Threading.Tasks;

namespace ZeroFramework.Config
{
    /// <summary>
    /// 配置加载模块
    /// </summary>
    public partial class ConfigMgr : SingletonBase<ConfigMgr>
    {
        public string CurrentLanguage { get; set; }

        /// <summary>
        /// 初始化，从本地包中加载
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        protected override void OnInit()
        {
            LoadConfigFromLocal(); //加载游戏设置
        }
    }
}