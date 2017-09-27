using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScovillePower : SkillBehaviour
{

	protected override void PreCast ()
	{
		CommonOnPreCast();

		StartCoroutine(SedSkillCoroutine());
	}
		
	protected override void CommonOnPreCast ()
	{
		base.CommonOnPreCast ();
		mc.SetMovementPermission(false, true);
	}

	protected override void CommonOnCastSuccessfully ()
	{
		base.CommonOnCastSuccessfully ();

		ShowCastEffect(mc.transform.position, Quaternion.identity);
		Invoke("DelayFreeMovement", skillDataInstance.skillDuration);

		Buff_Enthusiasm buffEn = new Buff_Enthusiasm(skillDataInstance.effectDuration, mc, skillDataInstance.optionalParams[0].value,
			skillDataInstance.optionalParams[1].value, skillDataInstance.optionalClips[0].clip);
		mc.Cbc.AddBuff(buffEn, BuffTypeUI.Enthusiasm);
	}

	protected override void CommonOnCastFailed ()
	{
		base.CommonOnCastFailed ();
		DelayFreeMovement();
	}

	void DelayFreeMovement()
	{
		mc.SetMovementPermission(true, true);
	}

	protected override void Casting ()
	{
		EndCast();
	}

	protected override void EndCast ()
	{
		CommonOnEndCast();
	}

}
