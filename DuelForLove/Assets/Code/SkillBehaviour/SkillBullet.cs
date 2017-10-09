using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBullet : MonoBehaviour 
{
	private SkillBehaviour belongSkill;
	private Character owner;
	private GameObject hitEffect;

	private float speed;
	private float damage;
	private float knockBack;

	public delegate void OptionalImpact(Character target);
	public OptionalImpact DeleMethod;

	void OnEnable()
	{
		
	}

	void Update()
	{
		transform.position += transform.forward * speed * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == owner.gameObject)
			return;

		Character cc = other.GetComponent<Character>();
		ShieldBlock sb = other.GetComponent<ShieldBlock>();
		//hit character
		if(cc)
		{
			//general
			cc.Chp.TakeDamage(damage);
			cc.Cmm.AddForce(knockBack, transform.forward);
			//custom
			if(DeleMethod != null)
				DeleMethod(cc);
		}
		//hit shield
		if(sb)
		{
			sb.Owner.Cmm.TriggerShake();
			sb.Owner.Cmm.AddForce(knockBack, transform.forward);
			sb.AbsorbDamage(damage);
		}

		//hit other stuff
		hitEffect.transform.position = this.transform.position;
		hitEffect.SetActive(true);

		belongSkill.PlayRandomSkillAudio(belongSkill.skillDataInstance.hitClips);
		this.gameObject.SetActive(false);
	}

	public void InitBullet(SkillBehaviour _belongSkill, OptionalImpact optionalEffectMethod = null, GameObject specifiedHitEffect = null)
	{
		belongSkill = _belongSkill;
		owner = belongSkill.Mc;

		speed = belongSkill.skillDataInstance.bulletSpeed;
		damage = belongSkill.skillDataInstance.damage;
		knockBack = belongSkill.skillDataInstance.knockForce;

		//bullet custom hit impact
		if(optionalEffectMethod != null)
		{
			DeleMethod = null;
			DeleMethod += optionalEffectMethod;	
		}

		//specified hit effect, by default is the same as skill data hit effect
		if(specifiedHitEffect != null)
		{
			hitEffect = specifiedHitEffect;
		}else
		{
			hitEffect = belongSkill.ShowHitEffect(Vector3.zero, Quaternion.identity, false);
		}
	}
}
