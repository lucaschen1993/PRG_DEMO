﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChaseState : BaseState
{
    private readonly AIEnemy _enemy;
    public AIChaseState(AIEnemy enemy)
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