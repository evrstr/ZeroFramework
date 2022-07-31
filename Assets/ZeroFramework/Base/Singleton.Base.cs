using UnityEngine;
using ZeroFramework.Log;

namespace ZeroFramework
{
    /// <summary>
    /// 单例模式基类，线程安全的
    /// </summary>
    /// <typeparam name="T"></typeparam>
    // public abstract partial class SingletonBase<T> where T : class, new()
    public abstract partial class SingletonBase<T> where T : SingletonBase<T>, new()
    {
        private static T _instance;

        // 用于lock块的对象
        private static readonly object _synclock = new object();

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_synclock)
                    {
                        if (_instance == null)
                        {
                            // 若T class具有私有构造函数,那么则无法使用SingletonProvider<T>来实例化new T();
                            _instance = new T();
                            //初始化调用方法
                            _instance.OnInit();
                            //测试用，如果T类型创建了实例，则输出它的类型名称
#if UNITY_EDITOR
                            Debug.Log($"{typeof(T).Name}创建了单例对象");

#endif
                            //ZLog.LogDebug($"{typeof(T).Name}创建了单例对象");
                        }
                    }
                }
                return _instance;
            }
            set { _instance = value; }
        }

        /// <summary>
        /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
        /// </summary>
        /// <returns></returns>
        public static T GetInstance()
        {
            return Instance;
        }

        /// <summary>
        /// 单例实例化完成后调用，因此该方法可以访问该单例
        /// </summary>
        protected abstract void OnInit();

        protected void Destroy()
        {
            Instance = null;
        }

        protected SingletonBase()
        { }
    }
}