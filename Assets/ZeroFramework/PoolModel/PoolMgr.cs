using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using YooAsset;

namespace ZeroFramework
{
    /// <summary>
    ///  柜子 对象缓存池，内存换时间
    /// </summary>
    internal partial class PoolMgr : SingletonBase<PoolMgr>
    {
        // 资源名称，该名称的所有对象的队列。 AddressableName => Queue<GameObject>
        private Dictionary<string, Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();

        //空物体 用来存放所有抽屉
        private static GameObject poolEmpty;

        static PoolMgr()
        {
            // 初始化一个空物体 用来存放所有抽屉
            poolEmpty = GameObject.Find("Pool");
            if (poolEmpty == null)
            {
                poolEmpty = new GameObject("Pool");
            }
            GameObject.DontDestroyOnLoad(poolEmpty);
        }

        /// <summary>
        /// 从池中取出一个对象
        /// </summary>
        /// <param name="AddressableName"></param>
        /// <param name="callback">对象初始化函数，从对象池取出的对象都视为带有脏数据</param>
        /// <returns></returns>
        public GameObject PopObj(string AddressableName, UnityAction callback = null)
        {
            GameObject availableObject = null;

            //一.获取对象（未实例化或者未设置为active的对象）
            // 抽屉存在，且抽屉里的东西数量大于0
            if (pool.ContainsKey(AddressableName) && pool[AddressableName].Count > 0)
            {
                //出列
                availableObject = pool[AddressableName].Dequeue();
            }
            else
            {
                //抽屉存在，但数量为0
                if (pool[AddressableName].Count > 0)
                {
                    var a = YooAssets.LoadAssetAsync<GameObject>(AddressableName);
                    a.WaitForAsyncComplete();
                    //实例化一个对象
                    a.InstantiateAsync();
                }
                //抽屉不存在
                else
                {
                    //1.创建抽屉
                    GameObject gEmpty = new GameObject(AddressableName);
                    //2.把抽屉放到对应位置
                    gEmpty.transform.SetParent(poolEmpty.transform);
                    //3.实例化一个对象
                    var a = YooAssets.LoadAssetAsync<GameObject>(AddressableName);
                    a.WaitForAsyncComplete();
                    a.InstantiateAsync();
                    //4.把抽屉添加到对象池
                    pool[AddressableName] = new Queue<GameObject>();
                    //5. 把对象放到抽屉里
                    availableObject.transform.SetParent(poolEmpty.transform);
                }

                //设置实例对象的名称
                availableObject.name = AddressableName;
                availableObject.SetActive(false);
            }
            //初始化该对象数据
            callback?.Invoke();
            //激活该对象
            availableObject.SetActive(true);
            return availableObject;
        }

        /// <summary>
        /// 放入一个对象到池中
        /// </summary>
        /// <param name="obj"></param>
        public void PushObj(GameObject obj)
        {
            obj.SetActive(false);

            //有抽屉
            if (pool.ContainsKey(obj.name))
            {
                pool[obj.name].Enqueue(obj);
            }
            //没有抽屉
            else
            {
                //1.创建抽屉
                GameObject gEmpty = new GameObject(obj.name);
                //2.把抽屉放到对应位置，对象放到抽屉
                gEmpty.transform.SetParent(poolEmpty.transform);
                obj.transform.SetParent(gEmpty.transform);
                //3. 创建队列
                Queue<GameObject> myQ = new Queue<GameObject>();
                //入列
                myQ.Enqueue(obj);
                //加入缓存
                pool.Add(obj.name, myQ);
            }
            // Debug.Log($"HashCode:{obj.GetHashCode()} dict:{ dict.Count}, Queue:{dict[obj.name].Count}");
        }

        protected override void OnInit()
        {
        }
    }
}