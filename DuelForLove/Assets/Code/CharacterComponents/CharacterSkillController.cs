using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillController : MonoBehaviour
{
	[Header("Skills")]
	public SkillBehaviour skill_1;
	public SkillBehaviour skill_2;
	public SkillBehaviour skill_3;
	public SkillBehaviour skill_4;

	[Header("Animation")]
	public string movingBool = "isMoving";
	public string skill_trigger_1 = "skill_1";
	public string skill_trigger_2 = "skill_2";
	public string skill_trigger_3 = "skill_3";
	public string skill_trigger_4 = "skill_4";
	public string skill_break_1 = "skill_break_1";
	public string skill_break_2 = "skill_break_2";
	public string skill_break_3 = "skill_break_3";
	public string skill_break_4 = "skill_break_4";
	//string to hash
	private int movingBoolHash;
	private int skill_1_hash;
	private int skill_2_hash;
	private int skill_3_hash;
	private int skill_4_hash;
	private int break_1_hash;
	private int break_2_hash;
	private int break_3_hash;
	private int break_4_hash;
	private bool aSkillIsAnimating;
	public bool SkillIsAnimating {get {return aSkillIsAnimating;}}

	private Character mc;
	private Animator anim;
	private SkillUIController skillsUI;

	protected virtual void Awake()
	{
		Debug.Log("Hero base Awake!");

		mc = GetComponent<Character>();
		if(!mc)
		{
			mc = GetComponentInParent<Character>();
		}
		if(!mc)
		{
			Debug.LogError("A hero needs to have a basic character component.");
		}
		anim = GetComponent<Animator>();
	}

	protected virtual void Start()
	{
		Debug.Log("Hero base Start!");
		InitAnimation();

		skill_1.Init(1, mc, this, skillsUI);
		skill_2.Init(2, mc, this, skillsUI);
		skill_3.Init(3, mc, this, skillsUI);
		skill_4.Init(4, mc, this, skillsUI);
	}

	public void LinkUI(SkillUIController suc)
	{
		skillsUI = suc;
		SkillData[] data = new SkillData[]{skill_1.skillDataInstance, skill_2.skillDataInstance, skill_3.skillDataInstance, skill_4.skillDataInstance};
		skillsUI.SetSkillUIContent(data);
	}

	protected virtual void Update()
	{
		if(!mc.actionPermission)
			return;
		
		if(Input.GetButtonDown(mc.skill_1_Axis))
		{
			skill_1.Cast();
		}
		if(Input.GetButtonDown(mc.skill_2_Axis))
		{
			skill_2.Cast();
		}
		if(Input.GetButtonDown(mc.skill_3_Axis))
		{
			skill_3.Cast();
		}
		if(Input.GetButtonDown(mc.skill_4_Axis))
		{
			skill_4.Cast();
		}
	}

	//---------Animation related------------
	void InitAnimation()
	{
		movingBoolHash = Animator.StringToHash(movingBool);
		skill_1_hash = Animator.StringToHash(skill_trigger_1);
		skill_2_hash = Animator.StringToHash(skill_trigger_2);
		skill_3_hash = Animator.StringToHash(skill_trigger_3);
		skill_4_hash = Animator.StringToHash(skill_trigger_4);
		break_1_hash = Animator.StringToHash(skill_break_1);
		break_2_hash = Animator.StringToHash(skill_break_2);
		break_3_hash = Animator.StringToHash(skill_break_3);
		break_4_hash = Animator.StringToHash(skill_break_4);

		aSkillIsAnimating = false;
	}

	public void SetMoveAnim(bool state)
	{
		anim.SetBool(movingBoolHash, state);
	}

	public void TriggerSkillAnim(int index)
	{
		aSkillIsAnimating = true;

		switch(index)
		{
		case 1:
			anim.SetTrigger(skill_1_hash);
			break;
		case 2:
			anim.SetTrigger(skill_2_hash);
			break;
		case 3:
			anim.SetTrigger(skill_3_hash);
			break;
		case 4:
			anim.SetTrigger(skill_4_hash);
			break;
		default:
			break;
		}
	}

	public void BreakSkillAnim(int index)
	{
		switch(index)
		{
		case 1:
			anim.SetTrigger(break_1_hash);
			break;
		case 2:
			anim.SetTrigger(break_2_hash);
			break;
		case 3:
			anim.SetTrigger(break_3_hash);
			break;
		case 4:
			anim.SetTrigger(break_4_hash);
			break;
		default:
			break;
		}

		CurrentCastingAnimEnd();
	}

	public void CurrentCastingAnimEnd()
	{
		aSkillIsAnimating = false;
	}
}
