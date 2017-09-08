using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthBase : MonoBehaviour 
{
	public float maxHP;
	protected float currentHP;
	protected bool isDead;

	public float CurrentHP{get{return currentHP;}}
	public bool IsDead{get{return isDead;}}

	protected virtual void Start()
	{
		currentHP = maxHP;
		isDead =false;
	}

	public abstract void TakeDamage(float damage);
}
