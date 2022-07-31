using System;
using UnityEngine;

namespace ZeroFramework.UI
{
    public class PanelTypeS : IEquatable<PanelTypeS>
    {
        public string PanelName;//excel表里的配置
        public int HashCode;//实例化面板时的hashcode，理论上全局唯一

        public bool Equals(PanelTypeS other)
        {
            return other != null && (PanelName, HashCode).Equals((other.PanelName, other.HashCode));
        }

        //重写Equals

        public override bool Equals(object obj)
        {
            Debug.Log("通用比较 存在额外性能消耗");
            if (ReferenceEquals(obj, null)) return false;//判断是否为null
            if (ReferenceEquals(this, obj)) return true;//判断是否为引用相等
            return obj is PanelTypeS panel && Equals(panel);//判断类型是否相等并且调用具体方法
        }

        public override int GetHashCode()
        {
            return (PanelName, HashCode).GetHashCode();
        }
    }
}