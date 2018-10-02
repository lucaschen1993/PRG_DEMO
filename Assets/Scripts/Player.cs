using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AllCharacter
{
    public delegate void PlayerStateChange(AllCharacter.StateType stateType);
    public event PlayerStateChange StateChange;
    public int PlayerHp;

    [HideInInspector]
    public FiniteStateMachine FsMachine;

    // Use this for initialization
    void Awake ()
    {
        FsMachine = new FiniteStateMachine(this);
        //注册角色的状态
        FsMachine.RegisterState(new AttackState(this));
        FsMachine.RegisterState(new StandState(this));
        FsMachine.RegisterState(new DeadState(this));
        FsMachine.RegisterState(new MoveState(this));
        //注册事件
        StateChange+=new PlayerStateChange(FsMachine.ChangeState);
    }

    void Start()
    {
        //设置角色的初始状态
        PlayerHp = 100;
        EnterStand();
    }

    void Update () {
        FsMachine.OnUpdate();
	}

    public void StickMove()
    {
        Debug.Log("Moving the JoyStick");
        StateChange(AllCharacter.StateType.STATE_MOVE);
    }

    public void DownAttack()
    {
        Debug.Log("Down the Attack");
        StateChange(AllCharacter.StateType.STATE_ATTACK);
    }

    public void EnterStand()
    {
        StateChange(AllCharacter.StateType.STATE_STAND);
    }

    public bool IsPlayerDead()
    {
        if (PlayerHp <= 0)
            return true;
        else
        {
            return false;
        }
    }
}
