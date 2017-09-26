using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBuffUIController : MonoBehaviour
{
	public BuffDataUI[] buffData;

	private StateBuffUI[] buffs;	//TODO use dynamic list, create and destroy icon
	private List<StateBuffUI> currentBuffs;

	void Awake()
	{
		buffs = GetComponentsInChildren<StateBuffUI>();
		currentBuffs = new List<StateBuffUI>();
	}

	//return an int tracker, allocate the number to caller so that caller can remove this buff
	public int SetBuffUI(BuffTypeUI bType)
	{
		//grab data
		BuffDataUI data = null;
		for(int i = 0; i < buffData.Length; i++)
		{
			BuffDataUI d = buffData[i];
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
		for(int i = 0; i < buffs.Length; i++)
		{
			StateBuffUI sbu = buffs[i];
			if(!sbu.toggled)
			{
				//set display content
				sbu.Set(data.sprite, data.text);
				sbu.Show();
				//track
				currentBuffs.Add(sbu);
				Debug.LogWarning(currentBuffs.Count - 1);
				return currentBuffs.Count - 1;
			}
		}

		Debug.LogError("Not enough state buff placeholder!");
		return -1;
	}

	public void RemoveBuffUI(int trackingIndex)
	{
		if(trackingIndex == -1)
			return;
		//hide display content
		StateBuffUI buffToRemove = currentBuffs[trackingIndex];
		buffToRemove.Hide();
		//track end
		Debug.LogWarning("Removing......" + trackingIndex);
		currentBuffs.RemoveAt(trackingIndex);
	}
}

[System.Serializable]
public class BuffDataUI
{
	public BuffTypeUI type;
	public Sprite sprite;
	public string text = "两字Buff名";
}

public enum BuffTypeUI
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