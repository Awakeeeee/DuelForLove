using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroCard : NavigationalButton
{
	[Header("Data")]
	public Character characterPrefab;
	public CharacterData characterData;
	public SkillData[] skillData;
	public Image heroImage;

	void Awake()
	{
		InitCard();
	}

	void InitCard()
	{
		frame.transform.localScale = Vector3.one;
		isSelected = false;
		heroImage.sprite = characterData.avatar;
	}

	public override void SelectMe(HeroSelectionController selector)
	{
		if(isSelected)
			return;
		
		frame.gameObject.SetActive(true);
		isSelected = true;
		selector.currentHeroCard = this;
		if(selector.infoUI != null)
		{
			selector.infoUI.SetInfo(this);
		}
	}

	public override void LeaveMe()
	{
		if(!isSelected)
			return;
		
		frame.gameObject.SetActive(false);
		isSelected = false;
	}

	public void ConfirmHero()
	{
		frame.transform.localScale *= 0.8f;
	}
}
