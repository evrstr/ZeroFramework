using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//参考: https://zhuanlan.zhihu.com/p/156693039

public class MonoController : MonoBehaviour
{
    public event UnityAction updataEvent;
    public event UnityAction fixedUpdataEvent;
    private void Update()
    {
        updataEvent?.Invoke();
    }

    private void FixedUpdate()
    {
        fixedUpdataEvent?.Invoke();
    }
    public void AddUpdateListener(UnityAction fun)
    {
        updataEvent += fun;
    }

    public void RemoveUpdateListener(UnityAction fun)
    {
        updataEvent -= fun;
    }
    public void AddFixedUpdateListener(UnityAction fun)
    {
        fixedUpdataEvent += fun;
    }

    public void RemoveFixedUpdateListener(UnityAction fun)
    {
        fixedUpdataEvent -= fun;
    }
}