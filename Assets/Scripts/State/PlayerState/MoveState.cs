using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    private readonly Player _player;
    public MoveState(Player player)
    {
        this._player = player;
    }
    public override AllCharacter.StateType GetStateType()
    {
        return AllCharacter.StateType.STATE_MOVE;
    }
    public override void EnterState(FiniteStateMachine fsMachine, BaseState preState)
    {
        if (preState != null)
        {
            Debug.Log("Enter the MoveState the preState is:" + preState.GetStateType());
        }
        else
        {
            Debug.Log("Enter the MoveState");
        }
        //Debug.Log(_player.CurBaseState);
    }

    public override void UpdateState()
    {
        Debug.Log("I'm Moving.");
    }

    public override void ExitState(BaseState preState)
    {
        Debug.Log("Exit Move State. " + preState.GetStateType());
    }

    public override void InputHandle()
    {

    }
}
