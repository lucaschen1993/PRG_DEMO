using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolState : BaseState
{
    private readonly AIEnemy _enemy;
    public AIPatrolState(AIEnemy enemy)
    {
        this._enemy = enemy;
    }
    public override AllCharacter.StateType GetStateType()
    {
        return AllCharacter.StateType.STATE_PATROL;
    }

    public override void EnterState(FiniteStateMachine fsMachine, BaseState preState)
    {
        if (preState != null)
        {
            Debug.Log("Enter the PatrolState the preState is:" + preState.GetStateType());
        }
        else
        {
            Debug.Log("Enter the PatrolState");
        }
        //Debug.Log(_player.CurBaseState);
        _enemy.ClearTarget();
    }

    public override void UpdateState()
    {
        //巡逻移动的方法
        //没有目标的时候执行
        if (!_enemy.IsHaveTarget())
        {
            _enemy.Patrolling();
            _enemy.ScanAround();
        }

    }

    public override void ExitState(BaseState preState)
    {
        Debug.Log("Exit Patrol State. " + preState.GetStateType());
    }

    public override void InputHandle()
    {

    }
}
