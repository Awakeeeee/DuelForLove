using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour 
{
	public string movingBool = "isMoving";
	public string skill_trigger_1 = "skill_1";
	public string skill_trigger_2 = "skill_2";
	public string skill_trigger_3 = "skill_3";
	public string skill_trigger_4 = "skill_4";
	public string skill_break_1 = "skill_break_1";
	public string skill_break_2 = "skill_break_2";
	public string skill_break_3 = "skill_break_3";
	public string skill_break_4 = "skill_break_4";

	private int movingBoolHash;
	private int skill_1_hash;
	private int skill_2_hash;
	private int skill_3_hash;
	private int skill_4_hash;
	private int break_1_hash;
	private int break_2_hash;
	private int break_3_hash;
	private int break_4_hash;

	private bool isCasting;
	public bool IsCasting{get{return isCasting;}}

	Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	void Start()
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

		isCasting = false;
	}

	public void SetMoveAnim(bool state)
	{
		anim.SetBool(movingBoolHash, state);
	}

	public void TriggerSkill(int index)
	{
		isCasting = true;	//so that when a skill is casting, another won't fire

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

	public void BreakSkill(int index)
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

		BackToFreeState();
	}

	public void BackToFreeState()
	{
		isCasting = false;
	}
}
