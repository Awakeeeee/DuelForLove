using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceWalk : SkillBehaviour
{
	protected override void PreCast ()
	{
		CommonOnPreCast();

		StartCoroutine(SedSkillCoroutine());
	}

	protected override void Casting ()
	{
		EndCast();
	}

	protected override void EndCast ()
	{
		CommonOnEndCast();
	}

	protected override void CommonOnPreCast ()
	{
		base.CommonOnPreCast ();
		mc.SetMovementPermission(false, true);
	}
	protected override void CommonOnCastFailed ()
	{
		base.CommonOnCastFailed ();
		mc.SetMovementPermission(true, true);
	}

	protected override void CommonOnEndCast ()
	{
		triggerCastingUpdate = false;
		PlayRandomSkillAudio(skillDataInstance.endClips);
	}

	public void OnSpaceWalkTransist()
	{
		Vector3 destination = Vector3.zero;
		Ray ray = new Ray(mc.transform.position, mc.transform.forward);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, skillDataInstance.range, skillDataInstance.targetLayer, QueryTriggerInteraction.Ignore))
		{
			destination = hit.point;	//hit block
			Character cc = hit.transform.GetComponent<Character>();
			if(cc)	//hit character
			{
				cc.Chp.TakeDamage(skillDataInstance.damage);
				cc.Cmm.TriggerShake();
				destination = mc.transform.position + mc.transform.forward * skillDataInstance.range;
			}
		}else	//not hit anything
		{
			destination = mc.transform.position + mc.transform.forward * skillDataInstance.range;
		}

		mc.transform.position = destination;
		PlayRandomSkillAudio(skillDataInstance.hitClips);
		ShowHitEffect(mc.transform.position, Quaternion.identity);
		mc.SetMovementPermission(true, true);
		mc.Csc.CurrentCastingAnimEnd();
	}
}
