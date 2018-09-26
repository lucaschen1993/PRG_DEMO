using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandState : BaseState
{
    private readonly Player _player;
    public StandState(Player player)
    {
        //向状态机发送事件，请求转换状态
        this._player = player;
    }
    public override Player.PlayerState GetStateType()
    {
        return Player.PlayerState.STATE_STAND;
    }
    public override void EnterState(FiniteStateMachine fsMachine, BaseState preState)
    {
        if (preState != null)
        {
            Debug.Log("Enter the StandState the preState is:" + preState.GetStateType());
        }
        else
        {
            Debug.Log("Enter the StandState");
        }
        Debug.Log(_player.FsMachine.curBaseState);
    }

    public override void UpdateState()
    {
        Debug.Log("I'm Standing.");
    }

    public override void ExitState(BaseState preState)
    {
        Debug.Log("I Exit the Stand State. ");
    }

    public override void InputHandle()
    {

    }
}
