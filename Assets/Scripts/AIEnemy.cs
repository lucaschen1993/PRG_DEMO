using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : AllCharacter
{
    //Enemy的属性
    public int EnemyHp;

    [HideInInspector]
    public FiniteStateMachine FsMachine;


    public delegate void EnemyStateChange(AllCharacter.StateType stateType);
    public event EnemyStateChange StateChange;

    //ScanPlayer需要的参数
    public Player TargPlayer; //保存扫描到的player
    public float ScanRadius; //扫描范围
    public float ChaseRange;//追击范围
    public float AttackRange;//攻击范围
    public float OutOfRangeTime;//追击超时
    private Vector3 oriPosition;

    // Use this for initialization
    void Awake () {
        FsMachine = new FiniteStateMachine(this);
        //注册ai状态
        FsMachine.RegisterState(new AIChaseState(this));
        FsMachine.RegisterState(new AIPatrolState(this));
        FsMachine.RegisterState(new AIAttackState(this));
        FsMachine.RegisterState(new AIDeadState(this));

        //注册事件
        StateChange += new EnemyStateChange(FsMachine.ChangeState);
        
    }

    void Start()
    {
        //初始化Enemy属性
        TargPlayer = null;
        EnemyHp = 0;

        StateChange(StateType.STATE_PATROL);

        TargPlayer = GameObject.Find("Player").GetComponent<Player>();
    }
	// Update is called once per frame
	void Update () {
	    FsMachine.OnUpdate();

	    if (EnemyHp <= 0)
	    {
	        StateChange(StateType.STATE_DEAD);
	    }

	    //当目标玩家不为空的时候  感觉可以用switch-case来做的
        if (TargPlayer != null)
	    {
	        //巡逻状态切换到追击状态
	        if (FsMachine.CurBaseState.GetStateType() == StateType.STATE_PATROL && Vector3.Distance(transform.position,TargPlayer.transform.position)<ChaseRange)
	        {
	            StateChange(StateType.STATE_CHASE);
	        }
            //追击状态切换到攻击状态 
	        //若enemy与player的距离小于attackrange就进入攻击状态
            if (FsMachine.CurBaseState.GetStateType() == StateType.STATE_CHASE&& Vector3.Distance(transform.position, TargPlayer.transform.position) < AttackRange)
	        {
	            StateChange(StateType.STATE_ATTACK);
	            oriPosition = this.transform.position;
	        }
            //攻击状态，目标击杀
	        if (FsMachine.CurBaseState.GetStateType() == StateType.STATE_ATTACK && TargPlayer.IsPlayerDead())
	        {
	            StateChange(StateType.STATE_PATROL);
	        }
	        else if(FsMachine.CurBaseState.GetStateType() == StateType.STATE_ATTACK && Vector3.Distance(transform.position, TargPlayer.transform.position)>AttackRange&& Vector3.Distance(transform.position, TargPlayer.transform.position)<ChaseRange)
	        {
                //若enemy与player的距离大于attackrange 小于 scanrange，则继续回到chasestate
	            StateChange(StateType.STATE_CHASE);
            }else if
	        (FsMachine.CurBaseState.GetStateType() == StateType.STATE_ATTACK &&
	         Vector3.Distance(transform.position, oriPosition) > ScanRadius)
	        {
                //若enemy与原始位置距离大于扫描范围，则脱离战斗

                //enemy回到初始位置 oriPosition
                //脱离战斗 回到Patrol状态
	            StateChange(StateType.STATE_PATROL);
            }
	        else if(Time.deltaTime>OutOfRangeTime)
	        {
                //若enemy进入战斗，则开始计时，超过计时并且范围太远就脱离战斗
	            StateChange(StateType.STATE_PATROL);
            }

        }

    }

}
