using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZeroFramework.Event
{
    /// <summary>
    /// 事件消息接口
    /// </summary>
    public interface IEventInfo
    { }

    public class EventInfo : IEventInfo
    {
        public UnityAction action;
    }

    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> action;
    }

    public class EventInfo<T0, T1> : IEventInfo
    {
        public UnityAction<T0, T1> action;
    }

    public class EventInfo<T0, T1, T2> : IEventInfo
    {
        public UnityAction<T0, T1, T2> action;
    }

    public class EventInfo<T0, T1, T2, T3> : IEventInfo
    {
        public UnityAction<T0, T1, T2, T3> action;
    }

    /// <summary>
    /// 事件中心
    /// </summary>
    public class EventCenter : SingletonBase<EventCenter>, IEventMgr
    {
        private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

        public int EventActionCount => eventDic.Count;

        protected override void OnInit()
        {
        }

        public void AddEventListener(string name, UnityAction action)
        {
            //判断字典里有没有对应这个事件，有就执行，没有就加进去。
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo).action += action;
            }
            else
            {
                eventDic.Add(name, new EventInfo() { action = action });
            }
        }

        public void AddEventListener<T>(string name, UnityAction<T> action)
        {
            //判断字典里有没有对应这个事件，有就执行，没有就加进去。
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo<T>).action += action;
            }
            else
            {
                eventDic.Add(name, new EventInfo<T>() { action = action });
            }
        }

        public void AddEventListener<T0, T1>(string name, UnityAction<T0, T1> action)
        {
            //判断字典里有没有对应这个事件，有就执行，没有就加进去。
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo<T0, T1>).action += action;
            }
            else
            {
                eventDic.Add(name, new EventInfo<T0, T1>() { action = action });
            }
        }

        public void AddEventListener<T0, T1, T2>(string name, UnityAction<T0, T1, T2> action)
        {
            //判断字典里有没有对应这个事件，有就执行，没有就加进去。
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo<T0, T1, T2>).action += action;
            }
            else
            {
                eventDic.Add(name, new EventInfo<T0, T1, T2>() { action = action });
            }
        }

        public void AddEventListener<T0, T1, T2, T3>(string name, UnityAction<T0, T1, T2, T3> action)
        {
            //判断字典里有没有对应这个事件，有就执行，没有就加进去。
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo<T0, T1, T2, T3>).action += action;
            }
            else
            {
                eventDic.Add(name, new EventInfo<T0, T1, T2, T3>() { action = action });
            }
        }

        /// <summary>
        /// 移除监听事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public void RemoveEventListener(string name, UnityAction action)
        {
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo).action -= action;
            }
        }

        public void RemoveEventListener<T>(string name, UnityAction<T> action)
        {
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo<T>).action -= action;
            }
        }

        public void RemoveEventListener<T0, T1>(string name, UnityAction<T0, T1> action)
        {
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo<T0, T1>).action -= action;
            }
        }

        public void RemoveEventListener<T0, T1, T2>(string name, UnityAction<T0, T1, T2> action)
        {
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo<T0, T1, T2>).action -= action;
            }
        }

        public void RemoveEventListener<T0, T1, T2, T3>(string name, UnityAction<T0, T1, T2, T3> action)
        {
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo<T0, T1, T2, T3>).action -= action;
            }
        }

        /// <summary>
        /// 事件触发
        /// </summary>
        /// <param name="name">哪一个名字的事件触发了</param>
        public void EventTrigger(string name)
        {
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo).action?.Invoke();
            }
        }

        public void EventTrigger<T>(string name, T info)
        {
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo<T>).action?.Invoke(info);
            }
        }

        public void EventTrigger<T0, T1>(string name, T0 info1, T1 info2)
        {
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo<T0, T1>).action?.Invoke(info1, info2);
            }
        }

        public void EventTrigger<T0, T1, T2>(string name, T0 info1, T1 info2, T2 info3)
        {
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo<T0, T1, T2>).action?.Invoke(info1, info2, info3);
            }
        }

        public void EventTrigger<T0, T1, T2, T3>(string name, T0 info1, T1 info2, T2 info3, T3 info4)
        {
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo<T0, T1, T2, T3>).action?.Invoke(info1, info2, info3, info4);
            }
        }

        /// <summary>
        /// 清空事件中心
        /// 主要用在场景切换时
        /// </summary>
        public void Clear()
        {
            eventDic.Clear();
        }

        public void ClearOne(string name)
        {
            if (eventDic.ContainsKey(name))
            {
                eventDic.Remove(name);
            }
        }

        public bool Check(string e)
        {
            if (eventDic.ContainsKey(e))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}