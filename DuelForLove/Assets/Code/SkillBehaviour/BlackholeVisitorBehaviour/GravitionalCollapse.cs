using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitionalCollapse : SkillBehaviour
{
	public SkillCastCircle circle;
	Vector3 collapsePos;
	bool collapseHasCreated;
	float intervalTimer;
	float castingTimer;

	protected override void PreCast ()
	{
		CommonOnPreCast();
		//into control circle state
		collapsePos = Vector3.zero;
		triggerCastingUpdate = true;	//trigger casting update
		PlayRandomSkillAudio(skillDataInstance.castClips);
		mc.SetMovementPermission(false, false);
		circle.gameObject.SetActive(true);
		circle.InitSet(mc.transform.position + mc.transform.forward * 3f, Vector3.one * skillDataInstance.range);
	}

	protected override void Casting ()
	{
		if(!collapseHasCreated)
		{
			if(Input.GetButtonDown(mc.skill_break))
			{
				OnCircleCast(false);
			}
			else if(Input.GetButtonDown(mc.skill_4_Axis))
			{
				OnCircleCast(true);
				intervalTimer = 100f;
				CommonOnCastSuccessfully();
				PlayRandomSkillAudio(skillDataInstance.castClips);
				ShowCastEffect(collapsePos, Quaternion.identity);
			}
		}

		if(collapseHasCreated)
		{
			//attack
			Collider[] targets = Physics.OverlapSphere(collapsePos, skillDataInstance.range);
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
				if(intervalTimer > skillDataInstance.optionalParams[0].value)
				{
					float distanceSquare = (cc.transform.position - collapsePos).sqrMagnitude;
					mc.Chp.ConsumeEnegy(skillDataInstance.optionalParams[1].value);	
					cc.Chp.TakeDamage(Mathf.RoundToInt(skillDataInstance.damage / (distanceSquare + 1)));
					intervalTimer = 0.0f;
				}
			}

			//count end
			intervalTimer += Time.deltaTime;
			castingTimer += Time.deltaTime;
			mc.Csu.UpdateCastingProgress(1 - castingTimer / skillDataInstance.skillDuration);
			if(Input.GetButtonUp(mc.skill_4_Axis) || castingTimer > skillDataInstance.skillDuration || mc.Chp.CurrentMP < (skillDataInstance.enegyCost / 10f))
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
		mc.Csu.ToggleCastingProgress(false);
		hero.BreakSkillAnim(sIndex);
		mc.SetMovementPermission(true, true);
		collapseHasCreated = false;
		castingTimer = 0.0f;
		intervalTimer = 0.0f;
	}

	void OnCircleCast(bool holeCreated)
	{
		if(!holeCreated)
		{
			triggerCastingUpdate = false;
			hero.BreakSkillAnim(sIndex);
			mc.SetMovementPermission(true, true);
		}else
		{
			collapsePos = new Vector3(circle.transform.position.x, mc.transform.position.y, circle.transform.position.z);
			mc.Csu.ToggleCastingProgress(true);
		}
		collapseHasCreated = holeCreated;
		castingTimer = 0.0f;
		circle.HideCircle();
	}
}
