using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : AllCharacter
{
    [HideInInspector]
    public FiniteStateMachine FsMachine;
    public BaseState AiBaseState;
    private Dictionary<AllCharacter.StateType, BaseState> aiStatesDict;

    // Use this for initialization
    void Start () {
        FsMachine = new FiniteStateMachine(this);
        //注册ai状态
        FsMachine.RegisterState(new AIChaseState(this));
        FsMachine.RegisterState(new AIPatrolState(this));
        FsMachine.RegisterState(new AIAttackState(this));
        FsMachine.RegisterState(new AIDeadState(this));

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
