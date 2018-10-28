using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCharacter : MonoBehaviour
{
    [HideInInspector]
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
    [HideInInspector]
    public StateType CharaterState;
}
