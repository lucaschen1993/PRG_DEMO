using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class BaseState
{
    //获取角色
    public AllCharacter Character;
    
    //得到当前状态
    public virtual AllCharacter.StateType GetStateType()
    {
        return Character.CharaterState;
    }
    //进入状态
    public virtual void EnterState(FiniteStateMachine fsMachine, BaseState preState)
    {

    }
    //状态更新
    public virtual void UpdateState()
    {

    }
    //状态结束 退出状态
    public virtual void ExitState(BaseState preState)
    {

    }
    //输入控制
    public virtual void InputHandle()
    {

    }

}
