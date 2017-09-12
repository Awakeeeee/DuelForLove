using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : SkillBehaviour
{
	private float chargeTimer;

	protected override void PreCast ()
	{
		CommonOnPreCast();

		CommonOnExitPreCastSuccessfully();
		mc.SetMovementPermission(false, false);
	}

	protected override void Casting ()
	{
		chargeTimer += Time.deltaTime;
		if(chargeTimer < skillDataInstance.duration)
		{
			mc.transform.position += mc.transform.forward * skillDataInstance.buffs[0].value * Time.deltaTime;
		}else
		{
			EndCast();
		}
	}

	protected override void EndCast ()
	{
		casting = false;
		hero.BreakSkillAnim(this.sIndex);

		chargeTimer = 0.0f;
		mc.SetMovementPermission(true, true);
	}
}
