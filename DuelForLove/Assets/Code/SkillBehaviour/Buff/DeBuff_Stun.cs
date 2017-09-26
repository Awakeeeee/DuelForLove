using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeBuff_Stun : StatusBuff
{
	public DeBuff_Stun(float duration, Character mc) : base(duration, mc)
	{
		
	}

	#region implemented abstract members of StatusBuff

	public override void StartBuff ()
	{
		mc.SetMovementPermission(false, false);
		mc.SetActionPermission(false);
	}

	public override void UpdateBuff ()
	{
		//just wait
	}

	public override void EndBuff ()
	{
		mc.SetMovementPermission(true, true);
		mc.SetActionPermission(true);
	}

	#endregion


}
