using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//(De)Buff base class
public abstract class StatusBuff
{
	protected Character mc;
	protected float duration;
	public float Duration{get {return duration;}}
	protected float countTimer;
	public float Timer{get {return countTimer;} set{countTimer = value;}}

	public int uiPromptTrackingIndex = -1;

//	public delegate void BuffStartBehaviour(Character mc);
//	public delegate void BuffEndBehaviour(Character mc);
//	protected BuffStartBehaviour CustomBuffStart;
//	protected BuffEndBehaviour CustomBuffEnd;

	public StatusBuff(){}

	public StatusBuff(float _duration, Character _mc)
	{
		mc = _mc;
		duration = _duration;
		countTimer = 0f;
		uiPromptTrackingIndex = -1;
	}

	public abstract void StartBuff();

	public abstract void UpdateBuff();

	public abstract void EndBuff();
}
