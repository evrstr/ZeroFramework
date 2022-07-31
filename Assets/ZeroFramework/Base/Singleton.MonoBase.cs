using UnityEngine;
using ZeroFramework.Log;

namespace ZeroFramework
{
    /// <summary>
    /// 继承了 MonoBehaviour 的单例模式基类,OnInit方法在base.Awake()之后 在Awake()之前
    /// </summary>
    ///
    /// <typeparam name="T"></typeparam>
    public class SingletonMonoBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    if (Debug.isDebugBuild)
                    {//开发版本Debug，发布版本不Debug，降低性能影响
                        Debug.LogError(typeof(T) + " has no instance");
                    }
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null)
            {
                if (Debug.isDebugBuild)
                {
                    Debug.LogError(typeof(T) + " had has an instance");
                }
            }
            _instance = this as T;
            InitAwake();
        }

        protected virtual void InitAwake()
        { }//子类重写该方法当作Awake
    }
}