using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDeadState : BaseState
{
    private readonly AIEnemy _enemy;
    public AIDeadState(AIEnemy enemy)
    {
        this._enemy = enemy;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
