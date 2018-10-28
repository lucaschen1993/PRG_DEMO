using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AIEnemy : AllCharacter
{
    //Enemy的属性
    public int EnemyHp;

    [HideInInspector]
    public FiniteStateMachine FsMachine;

    public delegate void EnemyStateChange(AllCharacter.StateType stateType);
    public event EnemyStateChange StateChange;

    //ScanPlayer需要的参数
    private Player _targPlayer; //保存扫描到的player
    public float ScanRadius; //扫描范围
    public float ChaseRange;//追击范围
    public float AttackRange;//攻击范围
    public float OutOfRangeTime;//追击超时

    private float _attackCoolDownTime;//攻击间隔计时
    public float AtkCoolDown;//攻击间隔
    public float ChasingTime; //追逐时间
    private float _countChasingTime;//追逐计时
    public float GuardTime;//警戒时间
    private float _countGuardTime;//警戒时间计时
    public float PatrolOffset; //巡逻坐标偏移量
    public float PatrolTime; //巡逻循环时间
    [SerializeField]
    private float _countPatrolTime;//巡逻循环时间计时

    //原始地点
    private Vector3 _oriPosition;
    //生成地点
    private Vector3 _spawnPosition;

    private Animator _anim;
    private NavMeshAgent _nav;

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

        //导航与动画
        _anim = GetComponentInChildren<Animator>();
        _nav = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        //初始化Enemy属性
        _targPlayer = null;
        EnemyHp = 100;
        ScanRadius = 10f;
        ChaseRange = 7f;
        AttackRange = 2.5f;
        OutOfRangeTime = 10f;
        //攻击时间与攻击间隔
        _attackCoolDownTime = 0;
        AtkCoolDown = 2.5f;
        //追逐时间
        ChasingTime = 8f;
        _countChasingTime = 0;
        //警戒时间
        GuardTime = 3f;
        _countChasingTime = 0;
        //巡逻重置时间
        PatrolTime = 5f;
        _countPatrolTime = 0;
        PatrolOffset = 10f;
        //保留初始位置
        _oriPosition = transform.position;

        //TargPlayer = GameObject.Find("Player").GetComponent<Player>();
        _targPlayer = null;
        StateChange(StateType.STATE_PATROL);
    }
	// Update is called once per frame
	void Update () {
	    FsMachine.OnUpdate();

        //当目标角色不为空的时候，注视目标角色
	    if (_targPlayer != null)
	    {
	        transform.LookAt(_targPlayer.transform);
        }
        //当目标玩家不为空的时候  感觉可以用switch-case来做的
        AllCharacter.StateType aiState = FsMachine.CurBaseState.GetStateType();

        switch (aiState)
        {
            case StateType.STATE_PATROL:
                //当找到目标的时候
                if (IsHaveTarget())
                {
                    //巡逻状态切换到追击状态
                    if (LessThanChaseRange())
                    {
                        _countGuardTime += Time.deltaTime;
                        //打开境界模式
                        GuardMode(true);
                        if (_countGuardTime >= GuardTime)
                        {//大于警戒时间
                            GuardMode(false);
                            StateChange(StateType.STATE_CHASE);
                        }
                        else
                        {
                            GuardMode(false);
                        }
                    }
                }
                else
                {
                    _countPatrolTime += Time.deltaTime;
                }
                break;
            case StateType.STATE_CHASE:
                _countChasingTime += Time.deltaTime;
                //追击状态切换到攻击状态 
                //若enemy与player的距离小于attackrange就进入攻击状态
                if (_countChasingTime < ChasingTime)
                {
                    if (LessThanAttackRange())
                    {
                        StateChange(StateType.STATE_ATTACK);
                    }
                    else if (!LessThanScanRange())
                    {//如果enemy与player的距离大于ScanRange
                        StateChange(StateType.STATE_PATROL);
                        BackToOriginPosition();
                    }
                }
                else
                {//超过追逐时间则回到巡逻状态
                    StateChange(StateType.STATE_PATROL);
                }
                break;
            case StateType.STATE_ATTACK:
                //计算攻击时间的间隔
                _attackCoolDownTime += Time.deltaTime;
                //攻击状态，目标击杀
                if (_targPlayer.IsPlayerDead())
                {
                    StateChange(StateType.STATE_PATROL);
                }
                else if (!LessThanAttackRange() && LessThanScanRange())
                {
                    //若enemy与player的距离大于attackrange 小于 scanrange，则继续回到chasestate
                    StateChange(StateType.STATE_CHASE);
                }
                else if (Time.deltaTime > OutOfRangeTime)
                {
                    //若enemy进入战斗，则开始计时，超过计时并且范围太远就脱离战斗
                    StateChange(StateType.STATE_PATROL);
                }
                break;
        }
    }

    public bool IsEnemyDead()
    {
        if (EnemyHp <= 0)
            return true;

        return false;
    }

    #region 状态动画切换的方法
    #region RandomAttack_animation

    public void ResetAttackCoolDown()
    {
        _attackCoolDownTime = 0;
    }

    public bool CanAttack()
    {
        if (_attackCoolDownTime > AtkCoolDown)
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
        int atkFactor = UnityEngine.Random.Range(1, 3);
        _anim.SetInteger("AttackFactor", atkFactor);
        yield return null;
        _anim.SetInteger("AttackFactor", 0);
    }

    public void ExitAttack()
    {
        _anim.SetFloat("AttackFactor", 0);
    }
    #endregion

    public void ExitChase()
    {
        _anim.SetFloat("MoveFactor", 0);
    }

    public void DeadAnimation()
    {
        _anim.SetTrigger("Death");
    }

    private void EnemyDead()
    {
        StateChange(StateType.STATE_DEAD);
    }
    //判断小于追击范围
    private bool LessThanChaseRange()
    {
        if (Vector3.Distance(transform.position, _targPlayer.transform.position) < ChaseRange)
            return true;
        return false;
    }
    //判断小于攻击距离
    private bool LessThanAttackRange()
    {
        if (Vector3.Distance(transform.position, _targPlayer.transform.position) < AttackRange)
            return true;
        return false;
    }
    //判断小于扫描距离
    private bool LessThanScanRange()
    {
        if (Vector3.Distance(transform.position, _targPlayer.transform.position) < ScanRadius)
            return true;
        return false;
    }

    //追击目标
    public void ChasingTarget()
    {
        _anim.SetFloat("MoveFactor", 1f);
        _nav.speed = 5f;
        _nav.stoppingDistance = AttackRange-0.1f;
        if (!LessThanAttackRange())
        {//追击目标时，Enemy与Target距离小于Enemy攻击距离时
            _nav.SetDestination(_targPlayer.transform.position);
        }
    }

    //回到初始位置
    public void BackToOriginPosition()
    {
        transform.LookAt(_spawnPosition);
        _anim.SetFloat("MoveFactor", 0.2f);
        _nav.speed = 2.0f;
        _nav.stoppingDistance = 0.2f;
        _nav.SetDestination(_spawnPosition);
    }
    //重置追逐时间计时
    public void ResetCountChasingTime()
    {
        _countChasingTime = 0;
    }

    private void GuardMode(bool tik)
    {
        transform.LookAt(_targPlayer.transform);
        if (tik)
        {
            Debug.Log("i found the player");
            _nav.isStopped = true;
        }
        else
        {
            _nav.isStopped = false;
            _countGuardTime = 0;
        }
    }
    #endregion

    #region PatrolState
    //扫描周围
    public void ScanAround()
    {
        Debug.Log("i'm scanning");
    }
    //巡逻方法  以后使用伪随机方法，设置几个点好吧
    public void Patrolling()
    {
        if (_countPatrolTime >= PatrolTime)
        {
            Vector3 newPosition = GetRandomPosition();
            //新点与出生点的长度小于PatrolOffset圆形范围
            if (EnemyAndTargetDistance(newPosition,_spawnPosition)< PatrolOffset)
            {
                _nav.SetDestination(newPosition);
                _anim.SetFloat("MoveFactor", 0.2f);
                _nav.speed = 2.0f;
                _nav.stoppingDistance = 0.2f;
                _countPatrolTime = 0;

                if (EnemyAndTargetDistance(transform.position, newPosition) < _nav.stoppingDistance)
                {
                    _anim.SetFloat("MoveFactor", 0);
                }
            }
            else
            {
                GetRandomPosition();
                _anim.SetFloat("MoveFactor", 0);
                _countPatrolTime = 0;
            }
        }
    }
    //寻找一个随机的点
    public Vector3 GetRandomPosition()
    {
        //获取当前地点的坐标值
        Vector3 currentPosition = transform.position;
        //在设置范围内随机生成一个点
        float x = Random.Range(currentPosition.x - PatrolOffset, currentPosition.x + PatrolOffset);
        float y = currentPosition.y;
        float z = Random.Range(currentPosition.z - PatrolOffset, currentPosition.z + PatrolOffset);

        return new Vector3(x,y,z);
    }

    //清除选中的目标
    public void ClearTarget()
    {
        _targPlayer = null;
    }
    #endregion

    //计算Enemy与目标点的距离
    public float EnemyAndTargetDistance(Vector3 v1, Vector3 v2)
    {
        return Vector3.Magnitude(v1 - v2);
    }

    public bool IsHaveTarget()
    {
        if (_targPlayer!=null)
            return true;
        return false;
    }

}
