using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZeroFramework;

namespace ZeroFramework
{
    //单例场景管理器
    public class SceneMgr : SingletonBase<SceneMgr>
    {
        public LoadingScene loadingScene;
        private Dictionary<string, SceneBase> sceneDic = new Dictionary<string, SceneBase>();

        public SceneMgr()
        {
            loadingScene = new LoadingScene(1, "1");
            sceneDic.Add(loadingScene.sceneName, loadingScene);
        }

        protected override void OnInit()
        {
        }

        //加载场景
        public void LoadScene(string sceneName)
        {
            if (this.sceneDic.TryGetValue(sceneName, out SceneBase nextScene))
            {
                loadingScene.LoadScene(nextScene);
            }
            else
            {
                Debug.Log("不存在ID为:" + sceneName + "的场景");
            }
        }
    }

    /// <summary>
    ///  加载中场景
    /// </summary>
    public class LoadingScene : SceneBase
    {
        public string loadingSceneName = "Scenes/loadingScene";

        //进度
        public float progress = 0f;

        public LoadingScene(int sceneId, string sceneName) : base(sceneId, sceneName)
        {
        }

        //加载场景
        public void LoadScene(SceneBase nextScene)
        {
            //1.加载loadingScene场景
            //2.加载下一个场景

            LoadLoadingScene();
            LoadNextScene(nextScene);
        }

        //加载loadingScene场景
        public void LoadLoadingScene()
        {
            SceneMgr.Instance.LoadScene(loadingSceneName);
        }

        //加载下一个场景
        public IEnumerator LoadNextScene(SceneBase nextScene)
        {
            //加载下一个场景

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextScene.sceneName, LoadSceneMode.Additive);

            while (asyncOperation.isDone == false)
            {
                Debug.Log(asyncOperation.progress);
                this.progress += asyncOperation.progress;
                yield return null;
            }
            this.progress = 1f;
            MonoMgr.Instance.StartCoroutine(LoadResProcess(nextScene));
        }

        public IEnumerator LoadResProcess(SceneBase nextScene)
        {
            nextScene.LoadSceneComplete();
            nextScene.LoadSceneConfig();
            nextScene.LoadSceneGameObject();
            nextScene.LoadSceneUI();
            while (!nextScene.bLoadConfigComplete || !nextScene.bLoadGameObjectComplete || !nextScene.bLoadUIComplete)
            {
                yield return null;
            }
            nextScene.LoadSceneAllResComplete();
            Scene scene = SceneManager.GetSceneByName(nextScene.sceneName);
            SceneManager.SetActiveScene(scene);
        }
    }
}