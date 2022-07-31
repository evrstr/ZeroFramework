using UnityEngine.Events;

namespace ZeroFramework.Event
{
    /// <summary>
    /// 事件管理器接口。
    /// </summary>
    public interface IEventMgr
    {
        /// <summary>
        /// 获取事件处理函数的数量。
        /// </summary>
        int EventActionCount
        {
            get;
        }

        /// <summary>
        /// 检查是否存在事件。
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        bool Check(string e);

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        void AddEventListener(string name, UnityAction action);

        void AddEventListener<T>(string name, UnityAction<T> action);

        void AddEventListener<T0, T1>(string name, UnityAction<T0, T1> action);

        void AddEventListener<T0, T1, T2>(string name, UnityAction<T0, T1, T2> action);

        void AddEventListener<T0, T1, T2, T3>(string name, UnityAction<T0, T1, T2, T3> action);

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        void RemoveEventListener(string name, UnityAction action);

        void RemoveEventListener<T>(string name, UnityAction<T> action);

        void RemoveEventListener<T0, T1>(string name, UnityAction<T0, T1> action);

        void RemoveEventListener<T0, T1, T2>(string name, UnityAction<T0, T1, T2> action);

        void RemoveEventListener<T0, T1, T2, T3>(string name, UnityAction<T0, T1, T2, T3> action);

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="name"></param>
        void EventTrigger(string name);

        void EventTrigger<T>(string name, T info);

        void EventTrigger<T0, T1>(string name, T0 info1, T1 info2);

        void EventTrigger<T0, T1, T2>(string name, T0 info1, T1 info2, T2 info3);

        void EventTrigger<T0, T1, T2, T3>(string name, T0 info1, T1 info2, T2 info3, T3 info4);

        /// <summary>
        /// 清空事件中心
        /// 主要用在场景切换时
        /// </summary>
        void Clear();
    }
}