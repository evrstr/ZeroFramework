using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZeroFramework;
using UnityEngine.EventSystems;
using ItemSpace;
using ZeroFramework.Event;

namespace ZeroFramework
{
    public class BagItem
    {
        public Item Item { get; set; }
        public int Num { get; set; }//物品数量
        public int BagGrideNum { get; set; }//在哪个格子

        public BagItem()
        {
            this.BagGrideNum = -1;
        }

        public BagItem(int BagGrideNum)
        {
            this.BagGrideNum = BagGrideNum;
        }
    }

    public class BagMgr : SingletonBase<BagMgr>
    {
        // public Dictionary<string, Iteminfo> BagDict { get; set; }
        public List<BagItem> BagDict { get; set; }

        // private int maxSize = 30;//最大数量

        public BagMgr()
        {
            BagDict = new List<BagItem>();
        }

        public void Sell()
        {
            Debug.Log("Sell");
        }

        public void Use()
        {
            Debug.Log("Use");
        }

        /// <summary>
        /// 增加一个物品
        /// </summary>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        public void AddItem<T>(T item) where T : Item
        {
            if (item is Prop)//可叠加
            {
                foreach (BagItem i in BagDict)
                {
                    if (i.Item == item)
                    {
                        i.Num += 1;
                        EventCenter.Instance.EventTrigger("RefreshBag");
                        return;
                    }
                }
                foreach (BagItem i in BagDict)
                {
                    if (i.Item != null)
                    {
                        i.Item = item;
                        i.Num = 1;
                        EventCenter.Instance.EventTrigger("RefreshBag");
                        return;
                    }
                }
            }
            else//不可叠加
            {
                foreach (BagItem i in BagDict)
                {
                    if (i.Item == null)
                    {
                        i.Item = item;
                        i.Num = 1;
                        EventCenter.Instance.EventTrigger("RefreshBag");
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 销毁一个物品
        /// </summary>
        /// <param name="i"></param>
        /// <param name="num"></param>
        public void RemoveItem(int i, int num = 1)
        {
            BagDict[i].Item = null;
            if (BagDict[i].Num != 1)
            {
                BagDict[i].Num -= 1;
            }
            else
            {
                BagDict[i].Num = 0;
            }
            EventCenter.Instance.EventTrigger("RefreshBag");
        }

        protected override void OnInit()
        {
        }
    }
}