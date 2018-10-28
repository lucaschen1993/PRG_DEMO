using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : AllCharacter
{
    public delegate void PlayerStateChange(AllCharacter.StateType stateType);
    public event PlayerStateChange StateChange;
    public int PlayerHp;
    public float AtkCoolDown;
    [SerializeField]
    private float attackCoolDownTime;
    [SerializeField]
    private AllCharacter.StateType playerType;
    [HideInInspector]
    public FiniteStateMachine FsMachine;
    public ETCJoystick MoveJoystick;

    private Animator _anim;
    private NavMeshAgent _nav;

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
        _anim = GetComponentInChildren<Animator>();
        _nav = GetComponent<NavMeshAgent>();
        EnterStand();
        playerType = FsMachine.CurBaseState.GetStateType();
        attackCoolDownTime = 0;
        AtkCoolDown = 2.5f;
    }

    void Update ()
    {
        FsMachine.OnUpdate();
        if (IsPlayerDead())
        {
            StateChange(StateType.STATE_DEAD);
        }
        switch (playerType)
        {
            case StateType.STATE_ATTACK:
                attackCoolDownTime += Time.deltaTime;
                break;
            case StateType.STATE_STAND:
                break;
            case StateType.STATE_DEAD:
                break;
            case StateType.STATE_MOVE:
                break;
        }        
	}

    public void StickMove()
    {
        //Debug.Log("Moving the JoyStick");
        StateChange(AllCharacter.StateType.STATE_MOVE);
    }

    public void DownAttack()
    {
        //Debug.Log("Down the Attack");
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

        return false;

    }

    public void SetType(AllCharacter.StateType stateType)
    {
        playerType = stateType;
    }
    //随机攻击动画
    #region RandomAttack_animation

    public void ResetAttackCoolDown()
    {
        attackCoolDownTime = 0;
    }

    public bool CanAttack()
    {
        if (attackCoolDownTime > AtkCoolDown)
        {
            ResetAttackCoolDown();
            return true;
        }
        return false;

    }

    public void Attack()
    {
        StartCoroutine(AttarCoroutine());
    }
    private IEnumerator AttarCoroutine()
    {
        int atkFactor = Random.Range(1, 3);
        _anim.SetInteger("AttackFactor", atkFactor);
        yield return null;
        _anim.SetInteger("AttackFactor", 0);
    }

    public void StopAttack()
    {
        _anim.SetInteger("AttackFactor", 0);
    }

    #endregion

    //获取ETCJoystick轴值
    public Vector3 GetJoystickAxis()
    {
        float h = MoveJoystick.axisX.axisValue;
        float v = MoveJoystick.axisY.axisValue;

        return new Vector3(h,0,v);
    }

    public void SetMoveFactor(float factor)
    {
        _anim.SetFloat("MoveFactor",factor);
    }

    public void DeadAnimation()
    {
        _anim.SetTrigger("Death");
    } 
}
