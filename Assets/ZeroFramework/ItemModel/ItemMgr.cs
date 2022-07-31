using System.Collections.Generic;
using System;
using UnityEngine;
using System.Reflection;
using System.Collections;

using LitJson;
using ZeroFramework;
using ItemSpace;

public class ItemMgr : SingletonBase<ItemMgr>
{

    //保存物品数据
    public List<Item> ItemList { get; set; }

    public string ABName = "icon";

    public ItemMgr()
    {
        this.ItemList = new List<Item>();
        // this.ItemList = ParseItem(ResMgr.Instance.Load<UnityEngine.Object>("data", "table_gen").ToString(), "");
    }
    /// <summary>
    /// 根据物品编号 从集合中 获取Item信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Item GetItem(int id)
    {
        foreach (Item item in this.ItemList)
            if (item.Id == id)
                return item;
        return null;
    }
    /// <summary>
    /// 根据物品名称 从集合中 获取Item信息
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Item GetItem(string name)
    {
        foreach (Item item in this.ItemList)
            if (item.Name == name)
                return item;
        return null;
    }


    /// <summary>
    /// 解析AB包中的所有json物品数据
    /// </summary>
    /// <param name="json"></param>
    /// <param name="ABName"></param>
    /// <returns></returns>
    public List<Item> ParseItem(string json, string ABName = "icon")
    {

        List<Item> itemList = new List<Item>();                 //新建一个List
        JsonData jsonList = JsonMapper.ToObject(json);
        // jsonList.SetJsonType(JsonType.Array);
        foreach (JsonData item in jsonList["WeaponData"])
        {
            //开始解析一条物品数据
            int Id = int.Parse(item["Id"].ToString());      //物品编号
            string Name = item["Name"].ToString();          //物品名称
            string Icon = item["Icon"].ToString();          //物品名称
            ItemType ItemType = (ItemType)Enum.Parse(typeof(ItemType), item["ItemType"].ToString());//物品类型
            float SellPrice = float.Parse(item["SellPrice"].ToString());            //SellPrice
            float BuyPrice = float.Parse(item["BuyPrice"].ToString());            //BuyPrice
            ItemQuality Quality = (ItemQuality)Enum.Parse(typeof(ItemQuality), item["Quality"].ToString());//品质
            string Desc = item["Desc"].ToString();            //物品描述

            //新建一个空的物品数据
            Item itemInfo = null;

            //根据类型进行构造对象
            if (ItemType == ItemType.Weapon)
            {
                float Atk = float.Parse(item["Atk"].ToString());            //物品描述
                WeaponType WType = (WeaponType)Enum.Parse(typeof(WeaponType), item["WType"].ToString());//武器类型
                float AttackSpeed = float.Parse(item["AttackSpeed"].ToString());            //AttackSpeed
                float AttackRange = float.Parse(item["AttackRange"].ToString());            //AttackRange
                itemInfo = new Weapon(Id, Id, Name, Icon, ItemType, SellPrice, BuyPrice, Quality, Desc, Atk, WType, AttackSpeed, AttackRange);
                Debug.Log(itemInfo.Quality);
            }
            //根据类型进行构造对象
            if (ItemType == ItemType.Prop)
            {
                int Count = int.Parse(item["Count"].ToString());
                float AddHp = float.Parse(item["AddHp"].ToString());
                float AddMp = float.Parse(item["AddMp"].ToString());

                itemInfo = new Prop(Id, Id, Name, Icon, ItemType, SellPrice, BuyPrice, Quality, Desc, Count, AddHp, AddMp);
                Debug.Log(itemInfo.Quality);
            }



            //将物品数据添加到集合中
            if (item != null)
            {
                itemList.Add(itemInfo);
            }
            //输出Item, 检验Item是否解析正确
#if UNITY_EDITOR
            PrintFieldInfo(itemInfo);
            // PrintPropertyInfo(itemInfo);
#endif
        }
        return itemList;
    }

    /// <summary>
    /// 通过物品Id获取物品
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Item GetItemFromId(int id)
    {
        foreach (Item item in ItemList)
        {
            if (item.Id == id)
            {
                return item;
            }
        }
#if UNITY_EDITOR
        Debug.Log($"找不到该ID:{id}的物品");
#endif
        return null;
    }
    /// <summary>
    /// 通过物品名字获取物品
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Item GetItemFromName(string name)
    {
        foreach (Item item in ItemList)
        {
            if (item.Name == name)
            {
                return item;
            }
        }
#if UNITY_EDITOR
        Debug.Log($"找不到该Name:{name}的物品");
#endif
        return null;
    }




    /// <summary>
    /// 调试时使用
    /// </summary>
    /// <param name="classInstance"></param>
    private void PrintFieldInfo(object classInstance)
    {
        FieldInfo[] fields = classInstance.GetType().GetFields();
        foreach (FieldInfo info in fields)
        {
            object value = info.GetValue(classInstance);
            // 使用info.GetType()不会得到准确类型
            Debug.LogFormat("字段值: {0}, 字段类型：{1}", value, info.FieldType);
            if (IsListT(info.FieldType))
            {
                // 获取List<T>的T的类型
                Type listType = value.GetType().GetGenericArguments()[0];
                Debug.LogFormat("列表类型: {0}", listType);
                IEnumerable list = (IEnumerable)value;
                foreach (var item in list)
                {
                    Debug.LogFormat("列表 单位值: {0}", item);
                }
            }
        }

    }

    private void PrintPropertyInfo(object classInstance)
    {
        PropertyInfo[] props = classInstance.GetType().GetProperties();
        foreach (PropertyInfo info in props)
        {
            object value = info.GetValue(classInstance, null);
            // 使用info.GetType()不会得到准确类型
            Debug.LogFormat("属性值: {0}, 属性类型：{1}", value, info.PropertyType);
            if (IsListT(info.PropertyType))
            {
                // 获取List<T>的T的类型
                Type listType = value.GetType().GetGenericArguments()[0];
                Debug.LogFormat("列表类型: {0}", listType);
                IEnumerable list = (IEnumerable)value;
                foreach (var item in list)
                {
                    Debug.LogFormat("列表 单位值: {0}", item);
                }
            }
        }


    }
    private bool IsListT(Type type)
    {
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
        {
            return true;
        }
        return false;

    }

    protected override void OnInit()
    {
        throw new NotImplementedException();
    }
}