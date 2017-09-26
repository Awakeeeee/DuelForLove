using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeBuff_Dot : StatusBuff
{
	protected float dotTimer;
	protected float dotInterval;
	protected float dotDamage;

	public DeBuff_Dot(float duration, Character mc, float di, float dd) : base(duration, mc)
	{
		dotInterval = di;
		dotDamage = dd;
		dotTimer = 0.0f;
	}

	#region implemented abstract members of StatusBuff

	public override void StartBuff ()
	{
		//base dot has no effect
	}

	public override void UpdateBuff ()
	{
		dotTimer += Time.deltaTime;
		if(dotTimer > dotInterval)
		{
			mc.Chp.TakeDamage(dotDamage);
			dotTimer = 0.0f;
		}
	}

	public override void EndBuff ()
	{
		//base dot has no effect
	}

	#endregion


}
