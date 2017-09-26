using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// CharacterBuffController is in charge of trigger Basic Buff
/// Specific buff effect is made in the place anywhere the buff is triggerd
public class CharacterBuffController : MonoBehaviour 
{
	public List<StatusBuff> buffCollection;

	private StateBuffUIController uiController;

	void Awake()
	{
		Debug.Log("Character buff controller Awake!");
		buffCollection = new List<StatusBuff>();
	}

	void LateUpdate()
	{
		if(buffCollection.Count <= 0 || buffCollection == null)
			return;

		for(int i = 0; i < buffCollection.Count; i++)
		{
			StatusBuff buff = buffCollection[i];

			buff.Timer += Time.deltaTime;
			buff.UpdateBuff();

			if(buff.Timer > buff.Duration)
			{
				buff.EndBuff();
				uiController.RemoveBuffUI(buff.uiPromptTrackingIndex);
				buffCollection.Remove(buff);
			}
		}
	}

	public void AddBuff(StatusBuff buff, BuffTypeUI uiType)
	{
		buff.uiPromptTrackingIndex = uiController.SetBuffUI(uiType);
		buff.StartBuff();
		buffCollection.Add(buff);
	}

	public void LinkUI(StateBuffUIController sbc)
	{
		uiController = sbc;
	}
}
