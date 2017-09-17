using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErgoshpereElectricity : SkillBehaviour
{
	protected override void PreCast ()
	{
		CommonOnPreCast();
		PlayOptionalClip(0);

		StartCoroutine(SedSkillCoroutine());
	}

	//My note: if the skill can be s-ed, you use PreCastCo() and override OnFail and OnSuccess
	protected override void CommonOnCastSuccessfully ()
	{
		base.CommonOnCastSuccessfully ();

		GameObject bullet = ShowPathEffect(mc.transform.position + mc.transform.forward * 0.75f, Quaternion.LookRotation(mc.transform.forward)) as GameObject;
		//Procudurally do these |OR| manually set up the bullet effect prefab
		if(bullet.GetComponent<SkillBullet>() == null)
		{
			SkillBullet sb = bullet.AddComponent<SkillBullet>();
			sb.InitBullet(this, mc);
		}
		if(bullet.GetComponent<SphereCollider>() == null)
		{
			SphereCollider sc = bullet.AddComponent<SphereCollider>();
			sc.isTrigger = true;
			sc.radius = 0.3f;
			sc.gameObject.layer = LayerMask.NameToLayer("SkillBullet");
		}
		if(bullet.GetComponent<Rigidbody>() == null)
		{
			Rigidbody rb = bullet.AddComponent<Rigidbody>();
			rb.isKinematic = true;
			rb.useGravity = false;
		}
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
