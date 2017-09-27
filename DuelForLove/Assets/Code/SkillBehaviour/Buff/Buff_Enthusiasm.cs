using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Enthusiasm : StatusBuff
{
	protected float moveSpeedLift;
	protected float rotateSpeedLift;
	protected AudioClip loopClip;

	//TODO temp effect parameters
	protected float lerpTimer = 0f;
	protected float lerpMultiplier = 2f;

	public Buff_Enthusiasm(float duration, Character mc, float _m, float _r, AudioClip _l) : base(duration, mc)
	{
		moveSpeedLift = _m;
		rotateSpeedLift = _r;
		loopClip = _l;
	}

	#region implemented abstract members of StatusBuff

	public override void StartBuff ()
	{
		mc.ChangeMoveSpeed(moveSpeedLift);
		mc.ChangeRotateSpeed(rotateSpeedLift);
	}

	public override void UpdateBuff ()
	{
		if(!mc.Ads.isPlaying)	//after any cast audio in the place the buff is triggered
		{
			mc.Ads.clip = loopClip;
			mc.Ads.loop = true;
			mc.Ads.PlayDelayed(1f);
		}
		//TODO temp effect
		lerpTimer += Time.deltaTime * lerpMultiplier;
		float lerpValue = Mathf.PingPong(lerpTimer, 1f);
		Color col = Color.Lerp(Color.white, Color.red, lerpValue);
		mc.Spr.color = col;
	}

	public override void EndBuff ()
	{
		mc.ResetMoveSpeed();
		mc.ResetRotateSpeed();
		mc.Spr.color = Color.white;

		mc.Ads.Stop();
		mc.Ads.clip = null;
		mc.Ads.loop = false;
	}

	#endregion
}
