using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [HideInInspector]
    public FiniteStateMachine FsMachine;
    public enum PlayerState
    {
        STATE_STAND,
        STATE_MOVE,
        STATE_ATTACK,
        STATE_DEAD
    };
    [HideInInspector]
    public PlayerState StateType;

    // Use this for initialization
    void Awake ()
    {
        FsMachine = new FiniteStateMachine();
        //注册角色的状态
        FsMachine.RegisterPlayerState(new AttackState(this));
        FsMachine.RegisterPlayerState(new StandState(this));
        FsMachine.RegisterPlayerState(new DeadState(this));
        FsMachine.RegisterPlayerState(new MoveState(this));

        //设置角色的初始状态
        FsMachine.RegisterStateEvent(PlayerState.STATE_STAND);
    }
	
	void Update () {
	    FsMachine.OnUpdate();
	}

    public void StickMove()
    {
        Debug.Log("Moving the JoyStick");
        FsMachine.RegisterStateEvent(PlayerState.STATE_MOVE);
    }

    public void DownAttack()
    {
        Debug.Log("Down the Attack");
        FsMachine.RegisterStateEvent(PlayerState.STATE_ATTACK);
    }
}
