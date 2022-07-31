using ZeroFramework.UI;

namespace ZeroFramework.Event
{
    /// <summary>
    /// 所有事件名
    /// </summary>
    public static class EventName
    {
        /// <summary>
        ///  热更新相关事件
        /// </summary>
        public static class HotUpdate
        {
            public const string StartDownload = nameof(HotUpdate) + "StartDownloadClicked";
            public const string DownloadProg = nameof(HotUpdate) + "DownloadProg";
        }

        public static class NetWork
        {
            public const string OnDisconnectServer = nameof(NetWork) + "OnDisconnectServer";
            public const string ConnectRetryCountMax = nameof(NetWork) + "ConnectRetryCountMax";
        }

        /// <summary>
        /// UI事件命令
        /// </summary>
        public static class UICMD
        {
            public const string Open = nameof(UICMD) + "Show";
            public const string Hide = nameof(UICMD) + "Hide";
            public const string Destroy = nameof(UICMD) + "Destroy";
        }
    }
}