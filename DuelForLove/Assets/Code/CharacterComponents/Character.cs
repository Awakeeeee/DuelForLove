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
	public CharacterData dataInstance;
	public float moveSpeed;
	public float rotateSpeed;

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

	public Rigidbody Rb {get {return rb;}}
	public CapsuleCollider Cpc {get {return cpc;}}
	public SpriteRenderer Spr {get {return spr;}}
	public AudioSource Ads {get {return ads;}}
	public CharacterHP Chp {get {return chp;}}
	public CharacterMovement Cmm {get {return cmm;}}
	public CharacterSkillController Csc {get {return csc;}}

	private StateBuffUIController sbu;

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

		dataInstance = Instantiate(dataDefault);
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

	public void LinkUI(StateBuffUIController _sbu)
	{
		sbu = _sbu;
	}

	public void TransitState(PlayerState state)
	{
		CurrentState = state;
	}

	//character gameplay state change
	public StateBuffUI SetStateBuff(BuffType type)
	{
		return sbu.SetBuff(type);
	}
	public void RemoveStateBuff(StateBuffUI buffRef)
	{
		buffRef.Hide();
	}

	public void ResetAllCharacterData()
	{
		Chp.maxHP = dataInstance.maxHP;
		Chp.maxMP = dataInstance.maxMP;
		Chp.naturalHPRecover = dataInstance.healthRecoverPerSec;
		Chp.naturalMPRecover = dataInstance.enegyRecoverPerSec;

		moveSpeed = dataInstance.moveSpeed;
		rotateSpeed = dataInstance.rotateSpeed;

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
		moveSpeed = moveSpeed * percent;
	}
	public void ResetMoveSpeed()
	{
		moveSpeed = dataInstance.moveSpeed;
	}

	public void ChangeKnockResist(float percent)
	{
		dataInstance.knockBackResist *= percent;
	}
	public void ResetKnockResist()
	{
		dataInstance.knockBackResist = dataDefault.knockBackResist;
	}

	//stun
	public void Stunned(float duration)
	{
		StopAllCoroutines();
		StartCoroutine(StunnedCo(duration));
	}
	IEnumerator StunnedCo(float duration)
	{
		StateBuffUI debuffRef = SetStateBuff(BuffType.Stun);
		SetMovementPermission(false, false);
		SetActionPermission(false);
		float timer = 0;
		while(timer < duration)
		{
			timer += Time.deltaTime;
			yield return null;
		}

		RemoveStateBuff(debuffRef);
		SetMovementPermission(true, true);
		SetActionPermission(true);
	}
	//toxic
	public void Toxicosis(float duration, float dot, float dotPeriod)
	{
		Debug.Log("244234234324");
		StopAllCoroutines();
		StartCoroutine(ToxicosisCo(duration, dot, dotPeriod));
	}
	IEnumerator ToxicosisCo(float duration, float dot, float dotPeriod)
	{
		StateBuffUI debuffRef = SetStateBuff(BuffType.Toxic);
		spr.color = Color.green;

		float timer = 0.0f;
		float dotTimer = 0.0f;
		while(timer < duration)
		{
			dotTimer += Time.deltaTime;
			if(dotTimer > dotPeriod)
			{
				chp.TakeDamage(dot);
				dotTimer = 0.0f;
			}

			timer += Time.deltaTime;
			yield return null;
		}
		RemoveStateBuff(debuffRef);
		spr.color = Color.white;
	}
}
