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
		hero.MC.SetMovementPermission(false, false);
	}

	protected override void Casting ()
	{
		chargeTimer += Time.deltaTime;
		if(chargeTimer < skillDataInstance.duration)
		{
			hero.MC.transform.position += hero.MC.transform.forward * skillDataInstance.buffs[0].value * Time.deltaTime;
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
		hero.MC.SetMovementPermission(true, true);
	}
}
