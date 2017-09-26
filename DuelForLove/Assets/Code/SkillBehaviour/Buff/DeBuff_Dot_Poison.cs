using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeBuff_Dot_Poison : DeBuff_Dot
{
	public DeBuff_Dot_Poison(float duration, Character mc, float di, float dd): base(duration, mc, di, dd)
	{
		
	}

	public override void StartBuff()
	{
		mc.Spr.color = Color.green;
	}

	public override void EndBuff ()
	{
		mc.Spr.color = Color.white;
	}
}
