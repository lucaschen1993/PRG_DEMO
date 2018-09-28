using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCharacter : MonoBehaviour
{
    public BaseState CurBaseState;

    public enum StateType
    {
        //AI状态
        STATE_PATROL,
        STATE_CHASE,
        //玩家状态
        STATE_STAND,
        STATE_MOVE,
        //共同状态
        STATE_ATTACK,
        STATE_DEAD,
    }

    public StateType CharaterState;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
