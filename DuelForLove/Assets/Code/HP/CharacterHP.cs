using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHP : HealthBase
{
	[Header("Enegy")]
	public float maxEnegy;
	[SerializeField]private float currentEnegy;
	public float naturalEnegyRecover;

	public float CurrentEnegy {get{return currentEnegy;}}

	protected override void Start ()
	{
		base.Start ();
		currentEnegy = maxEnegy;
	}

	void Update()
	{
		NaturalRecoverEnegy();
	}

	void NaturalRecoverEnegy()
	{
		if(currentEnegy >= maxEnegy)
			return;
		
		if(currentEnegy < maxEnegy)
		{
			currentEnegy += naturalEnegyRecover * Time.deltaTime;
		}else{
			currentEnegy = maxEnegy;
		}
	}

	public void ConsumeEnegy(float amount)
	{
		currentEnegy -= amount;
	}

	public override void TakeDamage (float damage)
	{
		Debug.Log("ah!");
	}
}
