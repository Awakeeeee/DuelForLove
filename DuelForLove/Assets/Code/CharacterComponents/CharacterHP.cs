using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHP : HealthBase
{
	[Header("Enegy")]
	public float maxMP;
	private float currentMP;
	public float CurrentMP {get{return currentMP;}}

	public float naturalMPRecover;
	public float naturalHPRecover;
	private float recoverMPCount;
	private float recoverHPCount;

	private ElaborateHPBar hpBarUI;
	private ElaborateHPBar enegyBarUI;

	protected override void Start ()
	{
		Debug.Log("HP compo Start!");

		base.Start ();
		currentMP = maxMP;

		if(hpBarUI)
			hpBarUI.ResetBar(maxHP, 1f);
		if(enegyBarUI)
			enegyBarUI.ResetBar(maxMP, 1f);
	}

	void Update()
	{
		NaturalRecoverEnegy();
		NaturalRecoverHealth();
	}

	void NaturalRecoverEnegy()
	{
		if(currentMP >= maxMP || naturalMPRecover <= 0f)
			return;
		
		if(recoverMPCount < 1)
		{
			recoverMPCount += naturalMPRecover * Time.deltaTime;
		}else{
			recoverMPCount = 0f;
			currentMP += 1f;
			if(enegyBarUI)
			{
				enegyBarUI.UpdateHP(-1f);
			}
		}
	}
	void NaturalRecoverHealth()
	{
		if(currentHP >= maxHP || naturalHPRecover <= 0f)
			return;

		if(recoverHPCount < 1)
		{
			recoverHPCount += naturalHPRecover * Time.deltaTime;
		}else{
			recoverHPCount = 0f;
			currentHP += 1f;
			if(hpBarUI)
			{
				hpBarUI.UpdateHP(-1f);
			}
		}
	}

	public void ConsumeEnegy(float amount)
	{
		currentMP -= amount;
		if(enegyBarUI)
		{
			enegyBarUI.UpdateHP(amount);
		}
	}

	public void LinkUI(ElaborateHPBar hpBar = null, ElaborateHPBar enegyBar = null)
	{
		if(hpBar)
			hpBarUI = hpBar;
		if(enegyBar)
			enegyBarUI = enegyBar;
	}

	public override void TakeDamage (float damage)
	{
		currentHP -= damage;
		hpBarUI.UpdateHP(damage);

		if(currentHP <= 0f)
		{
			currentHP = 0f;
			Die();
		}
	}

	void Die()
	{
		Debug.Log(this.gameObject.name + " dies.");
	}
}
