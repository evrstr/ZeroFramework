using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZeroFramework.Event;

namespace ZeroFramework
{
    [System.Serializable]
    public class KeyBoardBind
    {
        public KeyCode left = KeyCode.A;
        public KeyCode right = KeyCode.D;
        public KeyCode top = KeyCode.W;
        public KeyCode bottom = KeyCode.S;
        public KeyCode bag = KeyCode.B;//背包开关
    }

    internal class InputMgr : SingletonBase<InputMgr>
    {
        public bool IsStart { get; set; }
        public KeyBoardBind Keys { get; set; }

        protected override void OnInit()
        {
            Debug.Log("InputMgr OnInit");
            Debug.Log(InputMgr.Instance.GetHashCode());
            MonoMgr.Instance.AddUpdateListener(InputUpdate);
        }

        private void InputUpdate()
        {
#if UNITY_EDITOR
            if (Keys == null)
            {
                Debug.Log("没有加载游戏设置文件,因此 kes没有实例化!");
                SaveMgr.Instance.LoadGameSetting();
            }
#endif

            if (!IsStart)
                return;
            CheckKeyCode(Keys.left);
            CheckKeyCode(Keys.right);
            CheckKeyCode(Keys.top);
            CheckKeyCode(Keys.bottom);
            CheckKeyCode(Keys.bag);
        }

        public void StartCheck(bool isStartCheck)
        {
            if (Keys == null)
            {
                SaveMgr.Instance.LoadGameSetting();
            }
            IsStart = isStartCheck;
        }

        private void CheckKeyCode(KeyCode key)
        {
            if (Input.GetKeyDown(key))
            {
                EventCenter.Instance.EventTrigger("InputDown", key);
            }

            if (Input.GetKeyUp(key))
            {
                EventCenter.Instance.EventTrigger("InputUp", key);
            }
        }
    }
}