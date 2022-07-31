using UnityEngine;
using SimpleJSON;

using ZeroFramework.Log;

using Cysharp.Threading.Tasks;
using cfg;

using YooAsset;

namespace ZeroFramework.Config
{
    public partial class ConfigMgr : SingletonBase<ConfigMgr>
    {
        //游戏配置文件游戏数据，通过Luban加载Excel生成的json数据。
        public static Tables LubanTables { get; private set; }

        // 配置表里的ui路径
        public static cfg.Game.TbUIPath UIPath
        { get { return LubanTables.TbUIPath; } }

        public async UniTask LoadAllLubanTablesFromYooAssetAsync()
        {
            if (LubanTables == null)
            {
                LubanTables = new cfg.Tables();
            }
            await LubanTables.LoadAsync(async (file) =>
            {
                var asset = YooAssets.LoadAssetAsync<TextAsset>("luban/" + file + ".json");
                await asset.ToUniTask();
                if (asset.Status != EOperationStatus.Succeed)
                {
                    ZLog.LogFatal("配置文件：" + file + ".json 加载失败！");
                }
                return JSON.Parse((asset.AssetObject as TextAsset).text);
            });
        }

        /// <summary>
        /// 获取本地化字符串文本
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetText(string key)
        {
            return SaveMgr.Instance.gameSetting.systemSetting.lANGUAGE switch
            {
                LANGUAGE.简体中文 => LubanTables.TbLocalization.GetOrDefault(key)?.TextCn ?? key,
                LANGUAGE.English => LubanTables.TbLocalization.GetOrDefault(key)?.TextEn ?? key,
                _ => key,
            };
        }

        public static cfg.Game.TbUIPath GetUIPath()
        {
            //return LubanTables.TbUIPath.GetOrDefault(key)?.Path ?? key;
            return LubanTables.TbUIPath;
        }
    }
}