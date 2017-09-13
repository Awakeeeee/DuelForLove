using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageBurst : SkillBehaviour
{
	protected override void PreCast ()
	{
		CommonOnPreCast();

		CommonOnCastSuccessfully();
	}

	protected override void Casting ()
	{
		EndCast();
	}

	protected override void EndCast ()
	{
		CommonOnEndCast();
	}

	public void RageBurstHitGround()
	{
		PlayRandomSkillAudio(skillDataInstance.hitClips);
		Instantiate(skillDataInstance.hitEffect, mc.transform.position, Quaternion.identity);

		mc.Cmm.TriggerShake();

		Collider[] cols = Physics.OverlapSphere(mc.transform.position, skillDataInstance.range, skillDataInstance.targetLayer, QueryTriggerInteraction.Ignore);

		for(int i = 0; i < cols.Length; i++)
		{
			Character cc = cols[i].GetComponent<Character>();

			if(cc != null && cc != this.mc)
			{
				Vector3 dir = (cc.transform.position - mc.transform.position).normalized;
				cc.Cmm.AddForce(skillDataInstance.knockForce, dir);
				cc.Chp.TakeDamage(skillDataInstance.damage);
			}
		}
	}
}
