using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBase : MonoBehaviour
{
	[Header("Skill Data")]
	public SkillData skill_1_data;
	public SkillData skill_2_data;
	public SkillData skill_3_data;
	public SkillData skill_4_data;
	//instances
	protected SkillData skill_1;
	protected SkillData skill_2;
	protected SkillData skill_3;
	protected SkillData skill_4;

	[Header("Skill CD timer")]
	public float timer1;
	public float timer2;
	public float timer3;
	public float timer4;

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
	private bool isCasting;
	public bool IsCasting{get{return isCasting;}}

	protected bool updating_s1;
	protected bool updating_s2;
	protected bool updating_s3;
	protected bool updating_s4;

	protected Character mc;
	protected Animator anim;

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

		//use instances, or the scriptable object data changes will be saved during runtime
		skill_1 = Instantiate(skill_1_data);
		skill_2 = Instantiate(skill_2_data);
		skill_3 = Instantiate(skill_3_data);
		skill_4 = Instantiate(skill_4_data);
	}

	protected virtual void Start()
	{
		Debug.Log("Hero base Start!");

		timer1 = skill_1.cd;
		timer2 = skill_2.cd;
		timer3 = skill_3.cd;
		timer4 = skill_4.cd;
		InitAnimation();
	}

	protected virtual void Update()
	{
		TimerUpdating();

		if(Input.GetButtonDown(mc.skill_1_Axis))
		{
			TryCastSkill(skill_1, 1, ref timer1);
		}
		if(Input.GetButtonDown(mc.skill_2_Axis))
		{
			TryCastSkill(skill_2, 2, ref timer2);
		}
		if(Input.GetButtonDown(mc.skill_3_Axis))
		{
			TryCastSkill(skill_3, 3, ref timer3);
		}
		if(Input.GetButtonDown(mc.skill_4_Axis))
		{
			TryCastSkill(skill_4, 4, ref timer4);
		}
	}

	void TimerUpdating()
	{
		if(timer1 <= skill_1.cd)
			timer1 += Time.deltaTime;
		if(timer2 < skill_2.cd)
			timer2 += Time.deltaTime;
		if(timer3 < skill_3.cd)
			timer3 += Time.deltaTime;
		if(timer4 < skill_4.cd)
			timer4 += Time.deltaTime;
	}

	//Basic skill cast pre-condition: cd, enegy, buzy
	protected void TryCastSkill(SkillData skill, int skillIndex, ref float timer)
	{
		if(timer < skill.cd)
		{
			Debug.LogWarning(this.name + " " + skill.skillName + " is not ready yet!");
			return;
		}
		if(mc.HP.CurrentMP < skill.enegyCost)
		{
			Debug.LogWarning(this.name + " " + skill.skillName + " not enough enegy!");
			return;
		}

		if(mc.hero.IsCasting)
			return;

		switch(skillIndex)
		{
		case 1: CastSkill_1();
			break;
		case 2: CastSkill_2();
			break;
		case 3: CastSkill_3();
			break;
		case 4: CastSkill_4();
			break;
		default:
			break;
		}
	}

	protected virtual void CastSkill_1()
	{
		timer1 = 0.0f;
		mc.HP.ConsumeEnegy(skill_1.enegyCost);
		mc.hero.TriggerSkill(1);
		updating_s1 = true;
	}
	protected virtual void CastSkill_2()
	{
		timer2 = 0.0f;
		mc.HP.ConsumeEnegy(skill_2.enegyCost);
		mc.hero.TriggerSkill(2);
		updating_s2 = true;
	}
	protected virtual void CastSkill_3()
	{
		timer3 = 0.0f;
		mc.HP.ConsumeEnegy(skill_3.enegyCost);
		mc.hero.TriggerSkill(3);
		updating_s3 = true;
	}
	protected virtual void CastSkill_4()
	{
		timer4 = 0.0f;
		mc.HP.ConsumeEnegy(skill_4.enegyCost);
		Debug.Log("Cast skill 4");
		updating_s4 = true;
	}

	//---------Animation related------------
	protected void InitAnimation()
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

	protected void TriggerSkill(int index)
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

	protected void BreakSkill(int index)
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
