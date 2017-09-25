using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBuffUIController : MonoBehaviour
{
	public BuffDataUI[] buffData;

	private StateBuffUI[] buffs;
	private List<StateBuffUI> currentBuffs;

	void Awake()
	{
		buffs = GetComponentsInChildren<StateBuffUI>();
		currentBuffs = new List<StateBuffUI>();
	}

	//return an int tracker, allocate the number to caller so that caller can remove this buff
	public int SetBuffUI(BuffType bType)
	{
		//grab data
		BuffDataUI data = null;
		foreach(BuffDataUI d in buffData)
		{
			if(d.type == bType)
			{
				data = d;
				break;
			}
		}
		if(data == null)
		{
			Debug.Log("The buff type is not relating to a buff data.");
			return -1;
		}

		//grab an empty place
		foreach(StateBuffUI sbu in buffs)
		{
			if(!sbu.toggled)
			{
				//set display content
				sbu.Set(data.sprite, data.text);
				sbu.Show();
				//track
				currentBuffs.Add(sbu);
				return currentBuffs.Count - 1;
			}
		}

		Debug.LogError("Not enough state buff placeholder!");
		return -1;
	}

	public void RemoveBuffUI(int trackingIndex)
	{
		//hide display content
		StateBuffUI buffToRemove = currentBuffs[trackingIndex];
		buffToRemove.Hide();
		//track end
		currentBuffs.RemoveAt(trackingIndex);
	}
}

[System.Serializable]
public class BuffDataUI
{
	public BuffType type;
	public Sprite sprite;
	public string text = "两字Buff名";
}

public enum BuffType
{
	Stun,
	Trap,
	Silence,
	Slow,
	Weak,
	Recover,
	Enthusiasm,
	Dot
}