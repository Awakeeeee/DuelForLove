using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarolinaReaper : SkillBehaviour
{
	private float castingTimer;
	private ReaperBar bar;
	#region implemented abstract members of SkillBehaviour

	protected override void PreCast ()
	{
		CommonOnPreCast();

		CommonOnCastSuccessfully();
	}

	protected override void Casting ()
	{
		castingTimer += Time.deltaTime;
		hero.ProgressBar.UpdateBar(1 - castingTimer / skillDataInstance.skillDuration);
		if(!Input.GetButton(mc.skill_4_Axis) || castingTimer > skillDataInstance.skillDuration)
		{
			EndCast();
		}
	}

	protected override void EndCast ()
	{
		CommonOnEndCast();

		if(castEffectInstance.activeInHierarchy)
		{
			ShowHitEffect(castEffectInstance.transform.position, Quaternion.identity);
			castEffectInstance.SetActive(false);
		}

		hero.ProgressBar.ToggleBar(false);
		hero.BreakSkillAnim(sIndex);
		mc.SetMovementPermission(true, true);
		castingTimer = 0.0f;
	}

	#endregion

	protected override void CommonOnCastSuccessfully ()
	{
		base.CommonOnCastSuccessfully ();

		mc.SetMovementPermission(false, true);

		bar = ShowCastEffect(mc.transform.position + skillDataInstance.range * mc.transform.forward, skillDataInstance.castEffect.transform.rotation).GetComponent<ReaperBar>();
		if(bar == null)
		{
			Debug.LogWarning("Reaper bar is not properly set up.");
			return;
		}
		bar.Init(this);

		hero.ProgressBar.ToggleBar(true);
	}
}
