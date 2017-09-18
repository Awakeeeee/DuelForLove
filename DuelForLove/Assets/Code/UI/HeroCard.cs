using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroCard : MonoBehaviour 
{
	[Header("Data")]
	public Character characterPrefab;
	public CharacterData characterData;
	public SkillData skillData_1;
	public SkillData skillData_2;
	public SkillData skillData_3;
	public SkillData skillData_4;
	[Header("Neighbours")]
	public HeroCard up;
	public HeroCard down;
	public HeroCard right;
	public HeroCard left;
	[Header("UI Components")]
	public Image heroImage;
	public Image frame_1P;
	public Image frame_2P;
	public bool isSelected_1P;
	public bool isSelected_2P;
	public bool IsSelected {get {return isSelected_1P || isSelected_2P;}}

	void Awake()
	{
		InitCard();
	}

	void InitCard()
	{
		frame_1P.transform.localScale = Vector3.one;
		frame_2P.transform.localScale = Vector3.one;
		isSelected_1P = false;
		isSelected_2P = false;

		heroImage.sprite = characterData.avatar;
	}

	public void SelectMe(HeroSelectionController selector)
	{
		switch(selector.index)
		{
		case 1:
			frame_1P.gameObject.SetActive(true);
			isSelected_1P = true;
			selector.currentCard = this;
			if(selector.infoUI != null)
			{
				selector.infoUI.SetInfo(this);	
			}
			break;
		case 2:
			frame_2P.gameObject.SetActive(true);
			isSelected_2P = true;
			selector.currentCard = this;
			if(selector.infoUI != null)
			{
				selector.infoUI.SetInfo(this);	
			}
			break;
		default:
			Debug.LogError("Invalid player index.");
			break;
		}
	}

	public void LeaveMe(HeroSelectionController leaver)
	{
		switch(leaver.index)
		{
		case 1:
			frame_1P.gameObject.SetActive(false);
			isSelected_1P = false;
			break;
		case 2:
			frame_2P.gameObject.SetActive(false);
			isSelected_2P = false;
			break;
		default:
			Debug.LogError("Invalid player index.");
			break;
		}
	}

	public void ConfirmHero(HeroSelectionController hc)
	{
		switch(hc.index)
		{
		case 1:
			frame_1P.transform.localScale *= 0.8f;
			break;
		case 2:
			frame_2P.transform.localScale *= 0.8f;
			break;
		default:
			break;
		}
	}
}
