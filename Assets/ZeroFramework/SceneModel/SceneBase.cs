using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 每个场景的自身基本信息
/// </summary>
public class SceneBase
{
    //场景Id
    public int sceneId;

    //场景名字
    public string sceneName;

    public bool bLoadConfigComplete = false;
    public bool bLoadGameObjectComplete = false;
    public bool bLoadUIComplete = false;

    public SceneBase(int sceneId, string sceneName)
    {
        this.sceneId = sceneId;
        this.sceneName = sceneName;
    }

    //1加载场景完成
    public virtual void LoadSceneComplete()
    {
        Debug.Log("1加载场景完成");
    }

    //2加载场景配置
    public virtual void LoadSceneConfig()
    {
        this.bLoadConfigComplete = true;
        Debug.Log("2加载场景配置");
    }

    //3加载场景物体
    public virtual void LoadSceneGameObject()
    {
        this.bLoadGameObjectComplete = true;
        Debug.Log("3加载场景物体");
    }

    //4加载场景UI
    public virtual void LoadSceneUI()
    {
        this.bLoadUIComplete = true;
        Debug.Log("4加载场景UI");
    }

    //5所有资源加载完成
    public virtual void LoadSceneAllResComplete()
    {
        Debug.Log("5所有资源加载完成");
        SceneManager.UnloadSceneAsync(ZeroFramework.SceneMgr.Instance.loadingScene.loadingSceneName);
    }
}