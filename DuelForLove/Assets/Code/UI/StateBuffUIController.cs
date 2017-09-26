using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBuffUIController : MonoBehaviour
{
	public StateBuffUI buffPromptPrefab;
	public BuffDataUI[] buffData;

	private List<StateBuffUI> currentBuffs;

	void Awake()
	{
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
		StateBuffUI newBuffPrompt = Instantiate(buffPromptPrefab, this.transform);
		newBuffPrompt.Set(data.sprite, data.text);
		//save the prompt reference in back-end
		for(int i = 0; i < currentBuffs.Count; i++)
		{
			if(currentBuffs[i] == null)
			{
				currentBuffs[i] = newBuffPrompt;
				return i;
			}
		}
		currentBuffs.Add(newBuffPrompt);
		return currentBuffs.Count - 1;
	}

	public void RemoveBuffUI(int trackingIndex)
	{
		if(trackingIndex == -1)
			return;
		//destroy prompt
		StateBuffUI buffToRemove = currentBuffs[trackingIndex];
		Destroy(buffToRemove.gameObject);
		//remove reference in back-end
		currentBuffs[trackingIndex] = null;
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