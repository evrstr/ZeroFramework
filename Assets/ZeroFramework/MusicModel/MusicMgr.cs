using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using YooAsset;

namespace ZeroFramework
{
    public class MusicMgr : SingletonBase<MusicMgr>
    {
        private AudioSource bgMusic = null;     // 唯一的背景音乐组件
        private float bgValue = 1;              // BGM的音乐大小

        private GameObject soundObj = null;
        private List<AudioSource> soundList = new List<AudioSource>();
        private float soundValue = 1;

        public MusicMgr()
        {
            MonoMgr.Instance.AddUpdateListener(Update);
        }

        private void Update()
        {
            for (int i = 0; i < soundList.Count; i++)
            {
                if (!soundList[i].isPlaying)
                {
                    GameObject.Destroy(soundList[i]);
                    soundList.RemoveAt(i);
                }
            }
            // for (int i = soundList.Count - 1; i >= 0; i++)
            // {
            //     if (!soundList[i].isPlaying)
            //     {
            //         GameObject.Destroy(soundList[i]);
            //         soundList.RemoveAt(i);
            //     }
            // }
        }

        public void PlayBgMusic(string ABName)
        {
            if (bgMusic == null)
            {
                GameObject obj = new GameObject("BgMusic");
                bgMusic = obj.AddComponent<AudioSource>();
            }
            // 异步加载背景音乐，加载完成后播放
            YooAssets.LoadAssetAsync<AudioClip>(ABName).Completed += (obj) =>
            {
                bgMusic.clip = (obj.AssetObject as AudioClip);
                bgMusic.volume = bgValue;
                bgMusic.loop = true;
                bgMusic.Play();
            };
        }

        public void PlayBgMusic(string ABName, float v)
        {
            if (bgMusic == null)
            {
                GameObject obj = new GameObject("BgMusic");
                bgMusic = obj.AddComponent<AudioSource>();
            }
            // 异步加载背景音乐，加载完成后播放
            YooAssets.LoadAssetAsync<AudioClip>(ABName).Completed += (obj) =>
            {
                bgMusic.clip = (obj.AssetObject as AudioClip);
                bgMusic.volume = bgValue;
                bgMusic.loop = true;
                bgMusic.Play();
            };
        }

        public void ChangeBgMusicVolume(float v)
        {
            bgValue = v;
            if (bgMusic == null) return;
            bgMusic.volume = bgValue;
        }

        public void PauseBgMusic()
        {
            if (bgMusic == null) return;
            bgMusic.Pause();
        }

        public void StopBgMusic()
        {
            if (bgMusic == null) return;
            bgMusic.Stop();
        }

        public void PlaySound(string name, UnityAction<AudioSource> callback = null)
        {
            if (soundObj == null) soundObj = new GameObject("Sound");

            // 当音效资源异步加载结束后，在添加一个音效

            // 异步加载背景音乐，加载完成后播放

            YooAssets.LoadAssetAsync<AudioClip>("sound").Completed += (obj) =>
            {
                AudioSource source = soundObj.AddComponent<AudioSource>();
                source.clip = (obj.AssetObject as AudioClip);
                source.loop = false;
                source.volume = soundValue;
                source.Play();
                soundList.Add(source);
                callback?.Invoke(source);
            };
        }

        public void PlaySound(string name, float v, UnityAction<AudioSource> callback = null)
        {
            if (soundObj == null) soundObj = new GameObject("Sound");

            // 当音效资源异步加载结束后，在添加一个音效

            YooAssets.LoadAssetAsync<AudioClip>("sound").Completed += (obj) =>
            {
                AudioSource source = soundObj.AddComponent<AudioSource>();
                source.clip = (obj.AssetObject as AudioClip);
                source.loop = false;
                source.volume = v;
                source.Play();
                soundList.Add(source);
                callback?.Invoke(source);
            };
        }

        // 改变音效声音大小
        public void ChangeSoundValue(float value)
        {
            soundValue = value;
            for (int i = 0; i < soundList.Count; i++)
                soundList[i].volume = value;
        }

        public void StopSound(AudioSource source)
        {
            if (soundList.Contains(source))
            {
                soundList.Remove(source);
                source.Stop();
                GameObject.Destroy(source);
            }
        }

        protected override void OnInit()
        {
        }
    }
}