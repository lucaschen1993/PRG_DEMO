﻿using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Basic.Math;
using UnityEngine;

public class AttackState : BaseState
{
    private readonly Player _player;
    public AttackState(Player player)
    {
        //向状态机发送事件，请求转换状态
        this._player = player;
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
        _player.SetType(AllCharacter.StateType.STATE_ATTACK);
        _player.ResetAttackCoolDown();
        _player.Attack();
        //Debug.Log(_player.CurBaseState);
    }

    public override void UpdateState()
    {
        //战斗状态不间断平A
        //Debug.Log("I'm in Battle Mode .");
        bool canAttack = _player.CanAttack();
        //攻击间隔
        if (canAttack)
        {
            _player.Attack();
        }
    }

    public override void ExitState(BaseState preState)
    {
        _player.StopAttack();
        _player.ResetAttackCoolDown();
        Debug.Log("Exit Attack State. " + preState.GetStateType());
    }

    public override void InputHandle()
    {
        
    }
}
