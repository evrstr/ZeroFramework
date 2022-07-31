using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroFramework
{
    public class FSM<T> where T : Enum
    {
        private T mCurType; //当前类型
        private IState<T> mCurrentState; //当前状态
        private readonly Dictionary<T, IState<T>> states = new(); //new Dictionary<T, IState<T>>();//所有状态

        public FSM(Dictionary<T, IState<T>> states, T initState)
        {
            this.states = states;
            mCurType = initState;
            TransitionState(initState);
        }

        public void OnUpdate()
        {
            mCurrentState.OnUpdate();
        }

        public void TransitionState(T toType)
        {
            ZeroFramework.Log.ZLog.LogInfo($"切换到：{toType}");
            if (toType == null)
            {
                return;
            }
            // 结束当前的状态
            if (mCurrentState != null)
            {
                mCurrentState.OnExit(toType);
            }
            // 进入另一个状态
            mCurrentState = states[toType];
            mCurrentState.OnEnter(mCurType);
            mCurType = toType;
        }
    }
}