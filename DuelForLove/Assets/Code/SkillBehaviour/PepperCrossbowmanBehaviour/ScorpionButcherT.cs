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

		GameObject bullet = ShowSkillBullet(mc.transform.position + mc.transform.forward * 0.75f, Quaternion.LookRotation(mc.transform.forward)) as GameObject;
		//Procudurally do these |OR| manually set up the bullet effect prefab
		sb = bullet.GetComponent<SkillBullet>();
		sb.InitBullet(this, ToxicArrow);
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
		DeBuff_Dot_Poison poisonBuff = new DeBuff_Dot_Poison(skillDataInstance.effectDuration, target, skillDataInstance.optionalParams[0].value, skillDataInstance.damage);
		target.Cbc.AddBuff(poisonBuff, BuffTypeUI.Dot);
	}
}
