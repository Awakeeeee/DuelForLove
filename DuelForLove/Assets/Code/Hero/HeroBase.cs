using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class HeroBase : MonoBehaviour 
{
	public SkillData skill_1;
	public SkillData skill_2;
	public SkillData skill_3;
	public SkillData skill_4;

	public float timer1;
	public float timer2;
	public float timer3;
	public float timer4;

	protected Character mc;

	protected virtual void Awake()
	{
		mc = GetComponent<Character>();
	}

	protected virtual void Start()
	{
		timer1 = skill_1.cd;
		timer2 = skill_2.cd;
		timer3 = skill_3.cd;
		timer4 = skill_4.cd;
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

	//Basic skill cast pre-condition: cd and enegy
	void TryCastSkill(SkillData skill, int skillIndex, ref float timer)
	{
		if(timer >= skill.cd && mc.HP.CurrentEnegy >= skill.enegyCost)
		{
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
	}

	protected virtual void CastSkill_1()
	{
		timer1 = 0.0f;
		mc.HP.ConsumeEnegy(skill_1.enegyCost);
	}
	protected virtual void CastSkill_2()
	{
		timer2 = 0.0f;
		mc.HP.ConsumeEnegy(skill_2.enegyCost);
	}
	protected virtual void CastSkill_3()
	{
		timer3 = 0.0f;
		mc.HP.ConsumeEnegy(skill_3.enegyCost);
	}
	protected virtual void CastSkill_4()
	{
		timer4 = 0.0f;
		mc.HP.ConsumeEnegy(skill_4.enegyCost);
	}
}
