using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionButcherT : SkillBehaviour {

	SkillBullet sb;

	protected override void PreCast ()
	{
		CommonOnPreCast();
		PlayOptionalClip(0);

		StartCoroutine(SedSkillCoroutine());
	}

	protected override void CommonOnCastSuccessfully ()
	{
		base.CommonOnCastSuccessfully ();

		GameObject bullet = ShowPathEffect(mc.transform.position + mc.transform.forward * 0.75f, Quaternion.LookRotation(mc.transform.forward)) as GameObject;
		//Procudurally do these |OR| manually set up the bullet effect prefab
		sb = bullet.GetComponent<SkillBullet>();
		sb.InitBullet(this, mc, ToxicArrow);
	}

	protected override void Casting ()
	{
		EndCast();
	}

	protected override void EndCast ()
	{
		CommonOnEndCast();
	}

	void ToxicArrow(Character target)
	{
		//target.Toxicosis(skillDataInstance.effectDuration, skillDataDefault.damage, skillDataInstance.optionalParams[0].value);
	}
}
