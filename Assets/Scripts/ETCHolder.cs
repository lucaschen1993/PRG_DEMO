using System.Collections;
using System.Collections.Generic;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class ETCHolder : MonoBehaviour
{
    private Player _player;
	// Use this for initialization
	void Start ()
	{
	    _player = GameObject.Find("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMoveStart()
    {
        _player.StickMove();
    }

    public void OnTouchUp()
    {
        _player.EnterStand();
    }

    public void OnAttackDown()
    {
       _player.DownAttack();
    }
}
