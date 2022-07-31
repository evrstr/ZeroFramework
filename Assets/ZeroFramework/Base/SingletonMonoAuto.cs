using UnityEngine;
using ZeroFramework.Log;

namespace ZeroFramework
{
    /// <summary>
    /// 继承了 MonoBehaviour 的单例模式基类,OnInit方法在base.Awake()之后 在Awake()之前
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonMonoAuto<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static bool ApplicationIsQuitting;
        private static readonly object _locker = new object();

        static SingletonMonoAuto()
        {
            ApplicationIsQuitting = false;
        }

        private void OnApplicationQuit()
        {
            ApplicationIsQuitting = true;
        }

        public static T Instance
        {
            get
            {
                if (ApplicationIsQuitting)
                {
                    if (Debug.isDebugBuild) Debug.LogError("can't get instance");
                }
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            _instance = FindObjectOfType<T>();
                            if (_instance == null)
                            {
                                GameObject obj = new GameObject("[" + typeof(T) + "]");
                                DontDestroyOnLoad(obj);
                                _instance = obj.AddComponent<T>();
                            }
                        }
                    }
                }
                return _instance;
            }
        }
    }
}