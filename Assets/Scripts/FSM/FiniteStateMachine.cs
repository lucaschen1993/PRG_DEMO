using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    private AllCharacter _character;
    private BaseState curBaseState;
    private Dictionary<AllCharacter.StateType, BaseState> stateDictionary;
    public FiniteStateMachine(AllCharacter character)
    {
        _character = character;
        stateDictionary  = new Dictionary<AllCharacter.StateType, BaseState>();
        curBaseState = null;
    }
    //注册角色状态
    public bool RegisterState(BaseState state)
    {
        if (state == null || stateDictionary.ContainsKey(state.GetStateType()))
        {
            return false;
        }
        stateDictionary.Add(state.GetStateType(), state);
        return true;
    }
    


    public void ChangeState(AllCharacter.StateType stateType)
    {
        BaseState targState = null;
        stateDictionary.TryGetValue(stateType, out targState);

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
        if (curBaseState!=null)
            curBaseState.UpdateState();
    }
}
