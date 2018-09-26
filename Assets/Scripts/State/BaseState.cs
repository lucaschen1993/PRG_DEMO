using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState
{
    //获取角色的状态机状态
    public FiniteStateMachine MyPlayerState;
    public Player MyPlayer;

    //得到当前状态
    public virtual Player.PlayerState GetStateType()
    {
        return MyPlayer.StateType;
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
