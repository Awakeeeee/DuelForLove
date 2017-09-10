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
	[Header("Monitor")]
	public PlayerSwitch playerSwitch;
	public PlayerState CurrentState;

	[Header("Data")]
	public CharacterData initialData;
	private CharacterData defaultData;
	public float moveSpeed;
	public float rotateSpeed;

	[Header("Permission")]
	public bool movementPermission;
	public bool rotationPermission;

	[Header("Input")]
	public string horizontalAxis;
	public string verticalAxis;
	public string skill_1_Axis, skill_2_Axis, skill_3_Axis, skill_4_Axis;

	public CharacterHP HP;		//hp is a large chunk of data, so separate it out
	public HeroBase hero;

	void Awake()
	{
		Debug.Log("Character Awake!");

		defaultData = Instantiate(initialData);
		ResetAllCharacterData();
	}

	void Start()
	{
		Debug.Log("Character Start!");

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

	//character gameplay state change
	public void ResetAllCharacterData()
	{
		HP.maxHP = defaultData.maxHP;
		HP.maxMP = defaultData.maxMP;
		HP.naturalHPRecover = defaultData.healthRecoverPerSec;
		HP.naturalMPRecover = defaultData.enegyRecoverPerSec;

		moveSpeed = defaultData.moveSpeed;
		rotateSpeed = defaultData.rotateSpeed;

		movementPermission = true;
		rotationPermission = true;
	}

	public void SetMovementPermission(bool move = true, bool rotate = true)
	{
		movementPermission = move;
		rotationPermission = rotate;
	}

	public void ChangeMoveSpeed(float percent)
	{
		moveSpeed = moveSpeed * percent;
	}
	public void ResetMoveSpeed()
	{
		moveSpeed = defaultData.moveSpeed;
	}
}
