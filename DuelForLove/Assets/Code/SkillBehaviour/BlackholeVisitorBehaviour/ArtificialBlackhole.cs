using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtificialBlackhole : SkillBehaviour
{
	public SkillCastCircle circle;

	private bool blackholeHasBeenCreated;
	private float holeTimer;
	private Vector3 holePos;

	protected override void PreCast ()
	{
		CommonOnPreCast();
		//into control circle state
		holePos = Vector3.zero;
		triggerCastingUpdate = true;	//trigger casting update
		PlayRandomSkillAudio(skillDataInstance.hitClips);
		mc.SetMovementPermission(false, false);
		circle.gameObject.SetActive(true);
		circle.InitSet(mc.transform.position + mc.transform.forward * 3f, Vector3.one * skillDataInstance.range);
	}

	protected override void Casting ()
	{
		if(!blackholeHasBeenCreated)
		{
			if(Input.GetButtonDown(mc.skill_break))
			{
				OnCircleCast(false);
			}
			else if(Input.GetButtonDown(mc.skill_3_Axis))
			{				
				OnCircleCast(true);
				CommonOnCastSuccessfully();
				ShowCastEffect(circle.transform.position, Quaternion.identity);
			}
		}

		if(blackholeHasBeenCreated)
		{
			//suck in
			Collider[] targets = Physics.OverlapSphere(holePos, skillDataInstance.range);
			Character cc = null;
			foreach(Collider t in targets)
			{
				if(t.gameObject != mc.gameObject && t.GetComponent<Character>())
				{
					cc = t.GetComponent<Character>();
				}
			}
			if(cc != null)
			{
				cc.Cmm.AddForce(skillDataInstance.knockForce, (holePos - cc.transform.position).normalized);
			}
			//count end
			holeTimer += Time.deltaTime;
			if(holeTimer > skillDataInstance.effectDuration)
			{
				if(castEffectInstance.activeInHierarchy)
					castEffectInstance.SetActive(false);
				
				EndCast();
			}
		}
	}

	protected override void EndCast ()
	{
		CommonOnEndCast();
		blackholeHasBeenCreated = false;
		holeTimer = 0.0f;
	}
	void OnCircleCast(bool holeCreated)
	{
		if(!holeCreated)
		{
			triggerCastingUpdate = false;
		}else
		{
			hero.TriggerSkillAnim(sIndex);
			holePos = new Vector3(circle.transform.position.x, mc.transform.position.y, circle.transform.position.z);
		}
		blackholeHasBeenCreated = holeCreated;
		holeTimer = 0.0f;
		mc.SetMovementPermission(true, true);
		mc.Csc.CurrentCastingAnimEnd();
		circle.HideCircle();
	}
}
