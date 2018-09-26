using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BaseState
{
    private readonly Player _player;
    public DeadState(Player player)
    {
        //向状态机发送事件，请求转换状态
        this._player = player;
    }
    public override Player.PlayerState GetStateType()
    {
        return Player.PlayerState.STATE_DEAD;
    }
    public override void EnterState(FiniteStateMachine fsMachine, BaseState preState)
    {
        if (preState != null)
        {
            Debug.Log("Enter the EnterState the preState is:" + preState.GetStateType());
        }
        else
        {
            Debug.Log("Enter the EnterState");
        }
        Debug.Log(_player.FsMachine.curBaseState);
    }

    public override void UpdateState()
    {
        Debug.Log("I'm Dead.");
    }

    public override void ExitState(BaseState preState)
    {
        Debug.Log("Exit Dead State. " + preState.GetStateType());
    }

    public override void InputHandle()
    {

    }
}
