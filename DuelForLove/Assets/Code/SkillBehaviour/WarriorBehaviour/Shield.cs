using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : SkillBehaviour
{
	private float castingTimer;
	
	protected override void PreCast ()
	{
		CommonOnPreCast();

		CommonOnCastSuccessfully();
		mc.ChangeMoveSpeed(skillDataInstance.optionalParams[0].value);
	}

	protected override void Casting ()
	{
		castingTimer += Time.deltaTime;

		if(castingTimer >= skillDataInstance.skillDuration || Input.GetButtonUp(mc.skill_2_Axis))	//TODO the axis..
		{
			EndCast();
		}
	}

	protected override void EndCast ()
	{
		CommonOnEndCast();

		hero.BreakSkillAnim(this.sIndex);
		mc.ResetMoveSpeed();
		castingTimer = 0.0f;
	}
}
