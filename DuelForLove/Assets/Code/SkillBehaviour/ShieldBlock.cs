using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The protection block created by some hero skills.
/// </summary>
public class ShieldBlock : MonoBehaviour
{
	Character mc;
	public Character Owner {get {return mc;}}

	public bool isBroken;
	public bool IsBroken {get {return isBroken;}}

	public float maxDamageAbsorb;
	public float currentAbsorbLeft;

	void Awake()
	{
		mc = GetComponentInParent<Character>();
	}

	public void AbsorbDamage(float amount)
	{
		if(isBroken)
			return;
		
		if(currentAbsorbLeft >= amount)
		{
			currentAbsorbLeft -= amount;
		}else{
			float extraDamage = amount - currentAbsorbLeft;
			isBroken = true;
			mc.Chp.TakeDamage(extraDamage);
		}
	}

	public void InitShield(float _maxDamageAbsorb)
	{
		maxDamageAbsorb = _maxDamageAbsorb;
		currentAbsorbLeft = maxDamageAbsorb;
		isBroken = false;
	}
}
