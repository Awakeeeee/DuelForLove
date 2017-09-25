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
	public CharacterData dataDefault;

	private CharacterData mcData;
	public CharacterData McData {get {return mcData;}}

	[Header("Permission")]
	public bool movementPermission;
	public bool rotationPermission;
	public bool actionPermission;

	[Header("Input")]
	public string horizontalAxis;
	public string verticalAxis;
	public string skill_1_Axis, skill_2_Axis, skill_3_Axis, skill_4_Axis;
	public string skill_break;

	private Rigidbody rb;
	private CapsuleCollider cpc;
	private SpriteRenderer spr;
	private AudioSource ads;
	private CharacterHP chp;
	private CharacterMovement cmm;
	private CharacterSkillController csc;
	private CharacterBuffController cbc;

	public Rigidbody Rb {get {return rb;}}
	public CapsuleCollider Cpc {get {return cpc;}}
	public SpriteRenderer Spr {get {return spr;}}
	public AudioSource Ads {get {return ads;}}
	public CharacterHP Chp {get {return chp;}}
	public CharacterMovement Cmm {get {return cmm;}}
	public CharacterSkillController Csc {get {return csc;}}
	public CharacterBuffController Cbc {get {return cbc;}}

	void Awake()
	{
		Debug.Log("Character Awake!");

		rb = GetComponent<Rigidbody>();
		cpc = GetComponent<CapsuleCollider>();
		spr = GetComponentInChildren<SpriteRenderer>();
		ads = GetComponent<AudioSource>();
		chp = GetComponent<CharacterHP>();
		cmm = GetComponent<CharacterMovement>();
		csc = GetComponentInChildren<CharacterSkillController>();
		cbc = GetComponentInChildren<CharacterBuffController>();

		mcData = Instantiate(dataDefault);
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
		skill_break = "Skill_Break" + playerSwitch.ToString();

		CurrentState = PlayerState.Idle;
	}

	public void TransitState(PlayerState state)
	{
		CurrentState = state;
	}

	//character gameplay state change
	public void ResetAllCharacterData()
	{
		Chp.maxHP = McData.maxHP;
		Chp.maxMP = McData.maxMP;
		Chp.naturalHPRecover = McData.healthRecoverPerSec;
		Chp.naturalMPRecover = McData.enegyRecoverPerSec;

		movementPermission = true;
		rotationPermission = true;
		actionPermission = true;
	}

	public void SetMovementPermission(bool move = true, bool rotate = true)	//trapped
	{
		movementPermission = move;
		rotationPermission = rotate;
	}
	public void SetActionPermission(bool action = true)	//silent
	{
		actionPermission = action;
	}

	public void ChangeMoveSpeed(float percent)
	{
		McData.moveSpeed *= percent;
	}
	public void ResetMoveSpeed()
	{
		McData.moveSpeed = dataDefault.moveSpeed;
	}

	public void ChangeKnockResist(float percent)
	{
		McData.knockBackResist *= percent;
	}
	public void ResetKnockResist()
	{
		McData.knockBackResist = dataDefault.knockBackResist;
	}
}
