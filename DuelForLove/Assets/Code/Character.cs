using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Character Data:
/// Let other components get data from here,
/// And affect character by changing the data listed in here.
public class Character : MonoBehaviour 
{
	public enum PlayerSwitch
	{
		_1P,
		_2P
	}
	public enum PlayerState
	{
		Idle,
		Moving
	}

	public PlayerSwitch playerSwitch;
	public PlayerState CurrentState;
	public float moveSpeed = 2;
	public float rotateSpeed = 150;

	[Header("Input")]
	public string horizontalAxis;
	public string verticalAxis;
	public string skill_1_Axis, skill_2_Axis, skill_3_Axis, skill_4_Axis;

	//hp is a large chunk of data, so separate it out
	private CharacterHP hp;
	public CharacterHP HP {get{return hp;}}

	void Awake()
	{
		hp = GetComponent<CharacterHP>();
	}

	void Start()
	{
		horizontalAxis = "Horizontal" + playerSwitch.ToString();
		verticalAxis = "Vertical" + playerSwitch.ToString();
		skill_1_Axis = "Skill_1" + playerSwitch.ToString();
		skill_2_Axis = "Skill_2" + playerSwitch.ToString();
		skill_3_Axis = "Skill_3" + playerSwitch.ToString();
		skill_4_Axis = "Skill_4" + playerSwitch.ToString();

		CurrentState = PlayerState.Idle;
	}

	public void TransitState(PlayerState state)
	{
		CurrentState = state;
	}
}
