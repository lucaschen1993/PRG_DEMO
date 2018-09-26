using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    public Player Player;
    public BaseState curBaseState;
    private Dictionary<Player.PlayerState, BaseState> stateDictionary;
    public FiniteStateMachine()
    {
        stateDictionary  = new Dictionary<Player.PlayerState, BaseState>();
        curBaseState = null;
    }
    //注册状态
    public bool RegisterPlayerState(BaseState state)
    {
        if (state == null || stateDictionary.ContainsKey(state.GetStateType()))
        {
            return false;
        }
        stateDictionary.Add(state.GetStateType(), state);
        return true;
    }
    //切换状态委托
    public delegate void SwitchStateDelegate(BaseState targState);
    private event SwitchStateDelegate changeState;
    //切换状态回调
    public void RegisterStateEvent(Player.PlayerState targeStateType)
    {
        BaseState targState = null;
        stateDictionary.TryGetValue(targeStateType, out targState);
        changeState += FiniteStateMachine_changeState;
        changeState(targState);
    }

    private void FiniteStateMachine_changeState(BaseState targState)
    {
        if (curBaseState == null)
        {
            //状态为空
            curBaseState = targState;
            curBaseState.EnterState(this, null);
        }
        else if (curBaseState != null && targState == curBaseState)
        {
            //状态不为空且与目标状态相同
        }else if (curBaseState != null && targState != curBaseState)
        {
            //切换状态
            BaseState preState = curBaseState;
            preState.ExitState(preState);
            curBaseState = targState;
            curBaseState.EnterState(this,preState);
        }
    }

    public void OnUpdate()
    {
        if(curBaseState!=null)
        curBaseState.UpdateState();
    }
}
