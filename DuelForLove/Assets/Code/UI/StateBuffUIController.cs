using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBuffUIController : MonoBehaviour
{
	private StateBuffUI[] buffs;
	public BuffData[] buffData;

	void Awake()
	{
		buffs = GetComponentsInChildren<StateBuffUI>();
	}

	//Return the buff reference so that when it is finished I know which one to turn off
	public StateBuffUI SetBuff(BuffType _type)
	{
		BuffData data = null;
		foreach(BuffData bd in buffData)
		{
			if(bd.type == _type)
			{
				data = bd;
				break;
			}
		}

		if(data == null)
		{
			Debug.LogError("Invlid buff tyep.");
			return null;
		}

		foreach(StateBuffUI sbu in buffs)
		{
			if(!sbu.toggled)
			{
				sbu.Set(data.sprite, data.text);
				sbu.Show();
				return sbu;
			}
		}

		Debug.LogError("Not enough state buff placeholder!");
		return null;
	}
}

[System.Serializable]
public class BuffData
{
	public Sprite sprite;
	public string text;
	public BuffType type;
}

public enum BuffType
{
	Stun,
	Slow,
	Toxic
}
