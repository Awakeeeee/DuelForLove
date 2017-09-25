using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// CharacterBuffController is in charge of trigger Basic Buff
/// Specific buff effect is made in the place anywhere the buff is triggerd
public class CharacterBuffController : MonoBehaviour 
{
	private bool isStun = false;
	private float stunTimer = 0f;
	private float stunDuration = 0f;
	private int stunTrackingIndex;

	private bool isSlow = false;
	private float slowTimer = 0f;
	private float slowDuration = 0f;
	private float slowPercent;
	private int slowTrackingIndex;

	private bool isDot = false;
	private float dotTimer = 0f;
	private float dotIntervalTimer = 0f;
	private float dotDuration;
	private float dotDamage;
	private float dotInterval;
	private int dotTrackingIndex;

	private Character mc;
	private StateBuffUIController uiController;

	void Awake()
	{
		Debug.Log("MC buff controller Awake!");
		mc = GetComponent<Character>();
	}
		
	public void LinkUI(StateBuffUIController controller)
	{
		uiController = controller;
	}

	void Update()
	{
		Stun();
	}

	//stun
	public void TriggerStun(float duration)
	{
		Debug.Log("called");
		if(isStun)
		{
			uiController.RemoveBuffUI(stunTrackingIndex);	//if two same buff is added to this character, remove the old one(this situation should not happen in Duel)
			stunTrackingIndex = -1;
		}
		isStun = true;
		stunTimer = 0.0f;
		stunDuration = duration;
		mc.SetMovementPermission(false, false);
		mc.SetActionPermission(false);
		stunTrackingIndex = uiController.SetBuffUI(BuffType.Stun);	//set this buff
	}
	void Stun()
	{
		if(!isStun)
			return;

		stunTimer += Time.deltaTime;
		if(stunTimer > stunDuration)
		{
			EndStun();
		}
	}
	public void EndStun()
	{
		if(!isStun)
			return;
		
		mc.SetMovementPermission(true, true);
		mc.SetActionPermission(true);
		isStun = false;
		uiController.RemoveBuffUI(stunTrackingIndex);	//naturally remove buff
		stunTrackingIndex = -1;
	}

	//slow
	public void TriggerSlow(float percent, float duration)
	{
		if(isSlow)
		{
			uiController.RemoveBuffUI(slowTrackingIndex);
			slowTrackingIndex = -1;
		}
		isSlow = true;
		slowTimer = 0.0f;
		slowDuration = duration;
		slowPercent = percent;
		mc.ChangeMoveSpeed(slowPercent);
		slowTrackingIndex = uiController.SetBuffUI(BuffType.Slow);
	}
	void Slow()
	{
		if(!isSlow)
			return;

		slowTimer += Time.deltaTime;
		if(slowTimer > slowDuration)
		{
			EndSlow();
		}
	}
	public void EndSlow()
	{
		if(!isSlow)
			return;
		
		mc.ResetMoveSpeed();
		isSlow = false;
		uiController.RemoveBuffUI(slowTrackingIndex);
		slowTrackingIndex = -1;
	}

	//dot
	public void TriggerDot(float duration, float damage, float interval)
	{
		if(isDot)
		{
			uiController.RemoveBuffUI(dotTrackingIndex);
			dotTrackingIndex = -1; 
		}
		isDot = true;
		dotTimer = 0.0f;
		dotDuration = duration;
		dotInterval = interval;
		dotDamage = damage;
	}
}

////toxic
//public void Toxicosis(float duration, float dot, float dotPeriod)
//{
//	StopAllCoroutines();
//	StartCoroutine(ToxicosisCo(duration, dot, dotPeriod));
//}
//IEnumerator ToxicosisCo(float duration, float dot, float dotPeriod)
//{
//	StateBuffUI debuffRef = SetStateBuff(BuffType.Toxic);
//	spr.color = Color.green;
//
//	float timer = 0.0f;
//	float dotTimer = 0.0f;
//	while(timer < duration)
//	{
//		dotTimer += Time.deltaTime;
//		if(dotTimer > dotPeriod)
//		{
//			chp.TakeDamage(dot);
//			dotTimer = 0.0f;
//		}
//
//		timer += Time.deltaTime;
//		yield return null;
//	}
//	RemoveStateBuff(debuffRef);
//	spr.color = Color.white;
//}