using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackState : BaseState
{
    private readonly AIEnemy _enemy;
    public AIAttackState(AIEnemy enemy)
    {
        this._enemy = enemy;
    }
    public override AllCharacter.StateType GetStateType()
    {
        return AllCharacter.StateType.STATE_ATTACK;
    }
    public override void EnterState(FiniteStateMachine fsMachine, BaseState preState)
    {
        if (preState != null)
        {
            Debug.Log("Enter the AttackState the preState is:" + preState.GetStateType());
        }
        else
        {
            Debug.Log("Enter the AttackState");
        }
        //Debug.Log(_player.CurBaseState);
    }

    public override void UpdateState()
    {
        Debug.Log("i'm in the attack state");
        Debug.Log("hit the player");

    }

    public override void ExitState(BaseState preState)
    {
        Debug.Log("Exit Attack State. " + preState.GetStateType());
    }

    public override void InputHandle()
    {

    }
}
