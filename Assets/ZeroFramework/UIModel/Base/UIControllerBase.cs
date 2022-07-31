using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ZeroFramework.UI
{
    public abstract class UIControllerBase : MonoBehaviour
    {
        public abstract UIViewBase View { get; protected set; }

        public int HashCode { get; protected set; }

        public virtual void PreInit()
        {
            HashCode = this.gameObject.GetHashCode();
        }

        public abstract void Show();//显示

        public abstract void Hide();//隐藏

        public abstract void Destroy();//释放
    }
}