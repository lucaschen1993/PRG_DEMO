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
    }

    public override void UpdateState()
    {
        //巡逻移动的方法
        Debug.Log("i'm patrolling");
        ScanPlayer();
    }

    public override void ExitState(BaseState preState)
    {
        Debug.Log("Exit Patrol State. " + preState.GetStateType());
    }

    public override void InputHandle()
    {

    }

    private void ScanPlayer()
    {
        Debug.Log("i'm scanning");
        //从该对象的transform开始发射圆周射线
        //射线碰撞的对象的tag如果是player则判断两者之间的距离是否小于ScanRadius
        //小于ScanRadius则把该player对象放入players保存

        //或者能不能给该对象添加一个碰撞体？

        //Player player = null;
        //if ()
        //{
        //    _enemy.TargPlayer = player;
        //}

        //好好做你Patrol的事情
    }
}
