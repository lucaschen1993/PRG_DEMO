using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDeadState : BaseState
{
    private readonly AIEnemy _enemy;
    public AIDeadState(AIEnemy enemy)
    {
        this._enemy = enemy;
    }
    public override AllCharacter.StateType GetStateType()
    {
        return AllCharacter.StateType.STATE_DEAD;
    }
    public override void EnterState(FiniteStateMachine fsMachine, BaseState preState)
    {
        if (preState != null)
        {
            Debug.Log("Enter the DeadkState the preState is:" + preState.GetStateType());
        }
        else
        {
            Debug.Log("Enter the DeadState");
        }
        //Debug.Log(_player.CurBaseState);
    }

    public override void UpdateState()
    {
        Debug.Log("i'm dead");

    }

    public override void ExitState(BaseState preState)
    {
        Debug.Log("Exit Dead State. " + preState.GetStateType());
    }

    public override void InputHandle()
    {

    }
}
