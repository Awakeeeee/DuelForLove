using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : SkillBehaviour
{
	private float castingTimer;
	
	protected override void PreCast ()
	{
		CommonOnPreCast();

		CommonOnExitPreCastSuccessfully();
		hero.MC.ChangeMoveSpeed(skillDataInstance.debuffs[0].value);
	}

	protected override void Casting ()
	{
		castingTimer += Time.deltaTime;

		if(castingTimer >= skillDataInstance.duration || Input.GetButtonUp(hero.MC.skill_2_Axis))	//TODO the axis..
		{
			EndCast();	
		}
	}

	protected override void EndCast ()
	{
		casting = false;
		hero.BreakSkillAnim(this.sIndex);

		hero.MC.ResetMoveSpeed();
		castingTimer = 0.0f;
	}
}
