using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using ItemSpace;
using LitJson;

using System.Linq;
using ZeroFramework.Config;

namespace ZeroFramework
{
    /// <summary>
    /// 语言选项枚举
    /// </summary>
    public enum LANGUAGE
    {
        简体中文, English
    }

    /// <summary>
    /// 游戏系统设定，如音量，分辨率等
    /// </summary>
    [System.Serializable]
    public class SystemSetting
    {
        public float volume = 1;
        public string display = "1920x1080 分辨率";
        public LANGUAGE lANGUAGE = LANGUAGE.简体中文;
        public int yooAssetVersion = 0;
    }

    /// <summary>
    /// 游戏的全部设置，所有子项设置都在这里
    /// </summary>
    [System.Serializable]
    public class GameSetting
    {
        public KeyBoardBind keyBoardBind = new KeyBoardBind();
        public SystemSetting systemSetting = new SystemSetting();
    }

    /// <summary>
    /// 存储模块单例。 设置文件路径：Application.persistentDataPath + "setting/GameSetting.json"
    /// </summary>
    public class SaveMgr : SingletonBase<SaveMgr>
    {
        // public KeyBoardBind keyBoardBind = new KeyBoardBind(); //键位设置
        // public GameSetting gameSetting = new GameSetting();//游戏设置

        public GameSetting gameSetting;//游戏设置
        public List<BagItem> gameData = new List<BagItem>(); //背包数据列表

        public SaveMgr()
        {
            gameSetting = new GameSetting();

            string path = Path.Combine(Application.persistentDataPath, "setting/GameSetting.json");
            // File.WriteAllText(path, JsonUtility.ToJson(this, true));
#if UNITY_EDITOR
            Debug.Log(JsonUtility.ToJson(this, true));
            //C:/Users/42942/AppData/LocalLow/DefaultCompany/PokeZero/setting
            Debug.Log($"游戏设置文件存储路径：{path}");

#endif
        }

        /// <summary>
        /// 读取游戏设置文件
        /// </summary>
        public void LoadGameSetting()
        {
            string path = Path.Combine(Application.persistentDataPath, "setting/GameSetting.json");
#if UNITY_EDITOR
            //编辑器里每次都会初始化
            InitDefaultGameSetting();
#endif
            //判断是否存在设置文件
            if (!File.Exists(path))
            {
                //不存在
                Debug.Log("不存在设置文件");
                InitDefaultGameSetting();
            }
            else
            {
                //存在
                string json = File.ReadAllText(Path.Combine(Application.persistentDataPath, "setting/GameSetting.json"));
                GameSetting dat = JsonMapper.ToObject<GameSetting>(json);
                if (dat is null)
                {
                    InitDefaultGameSetting();
                }
                //初始化键位设置
                InputMgr.Instance.Keys = dat.keyBoardBind; //(data as SaveMgr).keyBoardBind;
            }
        }

        /// <summary>
        /// 从一个key获取设置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string LoadGameSettingFromKey(string key)
        {
            foreach (var p in GetFields<GameSetting>(gameSetting))
            {
                // Debug.Log($"Name:{ p.Name} Value:{ p.GetValue(gameSetting)}");
                // Debug.Log(p.GetType());
                Debug.Log($"Name: {p.Key} value: {p.Value}");

                if (p.Key == key)
                {
                    return p.Value.ToString();
                }
            }
            return null;
        }

        /// <summary>
        /// 获取所有字段和值，返回一个字典
        /// </summary>
        /// <param name="t"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>字典 字段和值 key,value</returns>
        public Dictionary<string, object> GetFields<T>(T t)
        {
            Dictionary<string, object> ListStr = new Dictionary<string, object>();
            if (t == null)
            {
                return ListStr;
            }
            FieldInfo[] fields = t.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (fields.Length <= 0)
            {
                return ListStr;
            }
            foreach (System.Reflection.FieldInfo item in fields)
            {
                string name = item.Name; //名称
                object value = item.GetValue(t);  //值

                // Debug.Log($"Name: {name} value: {value}");

                if (item.FieldType.IsValueType || item.FieldType.Name.StartsWith("String"))
                {
                    // Debug.Log($"Name: {name} value: {value}");
                    ListStr.Add(name, value);
                }
                else
                {
                    ListStr = ListStr.Concat(GetFields(value)).ToDictionary(kv => kv.Key, kv => kv.Value);
                }
            }
            return ListStr;
        }

        /// <summary>
        /// 存储游戏设置
        /// </summary>
        public void SaveGameSetting()
        {
            string path = Path.Combine(Application.persistentDataPath, "setting/GameSetting.json");
            //判断是否存在设置文件
            if (!File.Exists(path))
            {
                //不存在
                Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "setting"));
            }
            JsonData _gameSetting = JsonMapper.ToObject(JsonMapper.ToJson(gameSetting));
            File.WriteAllText(path, _gameSetting.ToJson());
        }

        /// <summary>
        /// 初始化默认配置
        /// /// </summary>
        private void InitDefaultGameSetting()
        {
            // keyBoardBind = new KeyBoardBind();
            gameSetting = new GameSetting();
            SaveGameSetting();
        }

        //下面的应该删除

        /// <summary>
        ///
        /// </summary>
        /// <param name="Key">Bag</param>
        /// <param name="data"></param>
        public void SaveBagGameData(string Key, List<BagItem> data)
        {
            string path = Path.Combine(Application.persistentDataPath, "data/data.json");
            //判断是否存在设置文件
            if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "data")))
            {
                //不存在
                Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "data"));
                File.WriteAllText(path, "");
            }
            JsonData jd = new JsonData();

            //解析原有json
            string json = File.ReadAllText(path); //读取原来的
            JsonData jsonList = new JsonData(); //= JsonMapper.ToObject(json);

            foreach (BagItem item in data)
            {
                if (item.Item != null)
                {
                    JsonData tm = new JsonData();
                    JsonData tmp1 = JsonMapper.ToObject(JsonMapper.ToJson(item.Item));
                    tm["Item"] = tmp1;
                    tm["Num"] = item.Num;
                    tm["BagGrideNum"] = item.BagGrideNum;
                    jsonList.Add(tm);
                }
                else
                {
                    JsonData tm = new JsonData();
                    // JsonData tmp1 = JsonMapper.ToObject(JsonMapper.ToJson(item.Item));

                    tm["Item"] = null;
                    tm["Num"] = item.Num;
                    tm["BagGrideNum"] = item.BagGrideNum;
                    jsonList.Add(tm);
                }
            }
            //生成json数据
            // jsonList[Key] = new JsonData();
            // jsonList["Bag"].Add(jd);
            jd[Key] = jsonList;

            Debug.Log(JsonMapper.ToJson(jd));
            File.WriteAllText(path, JsonMapper.ToJson(jd));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key">Bag</param>
        /// <returns></returns>
        public List<BagItem> LoadBagGameData(string key)
        {
            string path = (Application.persistentDataPath + "/data/data.json");
            //判断是否存在设置文件
            if (!File.Exists(path))
            {
                //不存在
                Debug.Log("不存在文件：" + path);
                Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "data"));
                File.WriteAllText(path, "");
            }
            //解析原有json
            string json = File.ReadAllText(path); //读取原来的
            Debug.Log(json);
            JsonData jsonList = JsonMapper.ToObject(json);

            // if (!jsonList.ContainsKey(key))
            // {
            //     return null;
            // }
            Debug.Log(jsonList[key].ToJson());
            // T dat = JsonMapper.ToObject<T>(jsonList[key].ToJson());
            JsonData dat = JsonMapper.ToObject(jsonList[key].ToJson());
            // Debug.Log(dat);

            if (dat != null)
            {
                Debug.Log(dat.ToJson());
                for (int i = 0; i < dat.Count; i++)
                {
                    if (dat[i]["Item"] != null)
                    {
                        BagItem tmp = new BagItem();
                        tmp.Num = int.Parse(dat[i]["Num"].ToJson());
                        tmp.BagGrideNum = int.Parse(dat[i]["BagGrideNum"].ToJson());

                        switch ((ItemType)Enum.Parse(typeof(ItemType), dat[i]["Item"]["ItemType"].ToString()))
                        {
                            // Debug.Log(dat[i]);
                            case ItemType.Weapon:

                                // Weapon weapon = JsonMapper.ToObject<Weapon>(dat[i]["Item"].ToJson());
                                tmp.Item = JsonMapper.ToObject<Weapon>(dat[i]["Item"].ToJson());
                                break;

                            case ItemType.Prop:
                                // Prop prop = JsonMapper.ToObject<Prop>(dat[i]["Item"].ToJson());
                                tmp.Item = JsonMapper.ToObject<Prop>(dat[i]["Item"].ToJson());
                                break;

                            case ItemType.Equipment:
                                // Equipment equipment = JsonMapper.ToObject<Equipment>(dat[i]["Item"].ToJson());
                                tmp.Item = JsonMapper.ToObject<Equipment>(dat[i]["Item"].ToJson());
                                break;

                            case ItemType.Other:
                                // Other other = JsonMapper.ToObject<Other>(dat[i]["Item"].ToJson());
                                tmp.Item = JsonMapper.ToObject<Other>(dat[i]["Item"].ToJson());
                                break;

                            default:
                                // gameData.Add(JsonMapper.ToObject<Item>(dat[i]["Item"].ToJson()));
                                tmp.Item = JsonMapper.ToObject<Item>(dat[i]["Item"].ToJson());
                                break;
                        }
                        gameData.Add(tmp);
                    }
                }
            }
            return gameData;
        }

        protected override void OnInit()
        {
        }
    }
}