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
            //Debug.Log("Enter the MoveState the preState is:" + preState.GetStateType());
        }
        else
        {
            //Debug.Log("Enter the MoveState");
        }
        _player.SetType(AllCharacter.StateType.STATE_MOVE);
        //Debug.Log(_player.CurBaseState);
    }

    public override void UpdateState()
    {
        Vector3 v3 = _player.GetJoystickAxis();
        float moveFactor = Vector3.Magnitude(v3);
        _player.SetMoveFactor(moveFactor);
        //Debug.Log("I'm Moving.");
    }

    public override void ExitState(BaseState preState)
    {
        _player.SetMoveFactor(0f);
        //Debug.Log("Exit Move State. " + preState.GetStateType());
    }

    public override void InputHandle()
    {

    }
}
