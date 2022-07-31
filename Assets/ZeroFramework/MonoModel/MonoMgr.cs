using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using ZeroFramework;
//https://zhuanlan.zhihu.com/p/156693039
//使用 MonoManager.Instance.StartCoroutine(Test123());
// IEnumerator Test123()
// {
//     yield return new WaitForSeconds(1f);
//     Debug.Log("123");
// }

public class MonoMgr : SingletonBase<MonoMgr>
{
    private MonoController controller;

    public MonoMgr()
    {
        GameObject obj = new GameObject("MonoController");
        controller = obj.AddComponent<MonoController>();
    }

    public void AddUpdateListener(UnityAction fun)
    {
        controller.AddUpdateListener(fun);
    }

    public void RemoveUpdateListener(UnityAction fun)
    {
        controller.RemoveUpdateListener(fun);
    }

    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }
    // public Coroutine StartCoroutine(ref string routine)
    // {
    //     return controller.StartCoroutine(routine);
    // }


    protected override void OnInit()
    {

    }
}