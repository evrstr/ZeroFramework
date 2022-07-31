using cfg;
using SimpleJSON;
using UnityEngine;

using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Events;
using ZeroFramework;
using System.Threading.Tasks;
using System;
using System.IO;

using ZeroFramework.Log;

using LitJson;

using Cysharp.Threading.Tasks;

namespace ZeroFramework.Config
{
    [System.Serializable]
    public class GameSetting
    {
        //按键配置
        public KeyCode keyUp;

        public KeyCode keyDown;
        public KeyCode keyLeft;
        public KeyCode keyRight;
        public KeyCode keyA;
        public KeyCode keyB;

        public bool displayViturlKey;//显示虚拟按键
        public int volume; //音量

        public SystemLanguage language; //语言
    }

    /// <summary>
    /// 配置加载模块
    /// </summary>
    public partial class ConfigMgr : SingletonBase<ConfigMgr>
    {
        public GameSetting GameConfig { get; private set; }

        private string gameSettingPath = Application.persistentDataPath + "/setting/GameSetting.json";

        public void LoadConfigFromLocal()
        {
#if UNITY_EDITOR
            //编辑器里每次都会初始化
            ZLog.LogInfo("gameSettingPath:" + gameSettingPath);
            InitDefaultConfig();
#endif
            //判断是否存在设置文件
            if (!File.Exists(gameSettingPath))
            {
                //不存在
                ZLog.LogInfo("不存在游戏设置文件");
                InitDefaultConfig();
                SaveConfigToLocal();
            }
            else
            {
                string json = File.ReadAllText(gameSettingPath);
                GameConfig = JsonMapper.ToObject<GameSetting>(json);
                if (GameConfig is null)
                {
                    InitDefaultConfig();
                }
            }
        }

        public void SaveConfigToLocal()
        {
            if (!File.Exists(gameSettingPath))
            {
                //不存在
                Directory.CreateDirectory(Path.GetDirectoryName(gameSettingPath));
                if (GameConfig == null)
                {
                    InitDefaultConfig();
                }
            }
            JsonData _gameSetting = JsonMapper.ToObject(JsonMapper.ToJson(GameConfig));

            File.WriteAllText(gameSettingPath, JsonMapper.ToJson(GameConfig));
        }

        public void InitDefaultConfig()
        {
            GameConfig = new GameSetting();
            GameConfig.keyUp = KeyCode.W;
            GameConfig.keyDown = KeyCode.S;
            GameConfig.keyLeft = KeyCode.A;
            GameConfig.keyRight = KeyCode.D;
            GameConfig.keyA = KeyCode.Q;
            GameConfig.keyB = KeyCode.E;
            GameConfig.displayViturlKey = true;
            GameConfig.volume = 100;
        }
    }
}