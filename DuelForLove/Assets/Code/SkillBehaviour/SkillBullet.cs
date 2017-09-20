using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBullet : MonoBehaviour 
{
	private SkillBehaviour belongSkill;
	private Character owner;

	private float speed;
	private float damage;
	private float knockBack;

	//delegate?

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
		if(cc)
		{
			cc.Chp.TakeDamage(damage);
			cc.Cmm.AddForce(knockBack, transform.forward);
		}
		if(sb)
		{
			sb.Owner.Cmm.TriggerShake();
			sb.Owner.Cmm.AddForce(knockBack, transform.forward);
			sb.AbsorbDamage(damage);
		}

		belongSkill.ShowHitEffect(this.transform.position, Quaternion.identity);
		belongSkill.PlayRandomSkillAudio(belongSkill.skillDataInstance.hitClips);
		this.gameObject.SetActive(false);
	}

	public void InitBullet(SkillBehaviour _belongSkill, Character _owner)
	{
		belongSkill = _belongSkill;
		owner = _owner;

		speed = belongSkill.skillDataInstance.bulletSpeed;
		damage = belongSkill.skillDataInstance.damage;
		knockBack = belongSkill.skillDataInstance.knockForce;
	}
}
