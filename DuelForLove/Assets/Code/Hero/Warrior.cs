using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : HeroBase
{
	private bool updatingSkill_1;
	private bool updatingSkill_2;
	private bool updatingSkill_3;
	private bool updatingSkill_4;

	private float shieldTimer;
	private float chargeTimer;

	void LateUpdate()
	{
		UpdatingSkill_1();	//balde
		UpdatingSkill_2();	//shield
		UpdatingSkill_3();	//charge
		UpdatingSkill_4();	//rageBurst
	}

	void UpdatingSkill_1()
	{
		if(!updating_s1)
			return;
		
		updating_s1 = false;
	}

	void UpdatingSkill_2()
	{
		if(!updating_s2)
			return;

		shieldTimer += Time.deltaTime;

		if(Input.GetButtonUp(mc.skill_2_Axis) || shieldTimer > skill_2.duration)
		{
			BreakSkill(2);
			mc.ResetMoveSpeed();

			shieldTimer = 0.0f;
			updating_s2 = false;
		}
	}

	void UpdatingSkill_3()
	{
		if(!updating_s3)
			return;

		chargeTimer += Time.deltaTime;
		if(chargeTimer < skill_3.duration)
		{
			mc.transform.position += mc.transform.forward * skill_3.buffs[0].value * Time.deltaTime;
		}else{
			mc.SetMovementPermission(true, true);
			BreakSkill(3);

			chargeTimer = 0.0f;
			updating_s3 = false;
		}
	}

	void UpdatingSkill_4()
	{
		if(!updating_s4)
			return;
		
		updating_s4 = false;
	}

	protected override void CastSkill_1 ()
	{
		base.CastSkill_1();
	}

	protected override void CastSkill_2 ()
	{
		base.CastSkill_2();
		mc.ChangeMoveSpeed(skill_2.debuffs[0].value);
	}

	protected override void CastSkill_3 ()
	{
		base.CastSkill_3();
		mc.SetMovementPermission(false, false);
	}

	protected override void CastSkill_4 ()
	{
		base.CastSkill_4();
	}
}
