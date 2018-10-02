using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChaseState : BaseState
{
    private readonly AIEnemy _enemy;
    public AIChaseState(AIEnemy enemy)
    {
        this._enemy = enemy;
    }
    public override AllCharacter.StateType GetStateType()
    {
        return AllCharacter.StateType.STATE_CHASE;
    }

    public override void EnterState(FiniteStateMachine fsMachine, BaseState preState)
    {
        if (preState != null)
        {
            Debug.Log("Enter the ChaseState the preState is:" + preState.GetStateType());
        }
        else
        {
            Debug.Log("Enter the PatrolState");
        }
        //Debug.Log(_player.CurBaseState);
    }

    public override void UpdateState()
    {
        Debug.Log("i'm in the chase state");
        ChasePlayer();
    }

    public override void ExitState(BaseState preState)
    {
        Debug.Log("Exit Chase State. " + preState.GetStateType());
    }

    public override void InputHandle()
    {

    }

    private void ChasePlayer()
    {
        //_enemy.TargPlayer
        Debug.Log("i'm chasing the player");
    }
}
