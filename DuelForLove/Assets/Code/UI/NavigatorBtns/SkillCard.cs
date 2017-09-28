using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCard : NavigationalButton
{
	public SkillData data;
	public HeroInfoUI infoUI;
	[Header("Extend Utility - Decision")]	//TODO separate this function
	public bool thisIsDecisionCard;
	public enum Decision{Back, Confirm}
	public Decision decision;

	private Image imageComponent;

	void Awake()
	{
		imageComponent = GetComponent<Image>();
		isSelected = false;
		frame.gameObject.SetActive(false);
	}

	public void InitCard()
	{
		if(!thisIsDecisionCard)
			imageComponent.sprite = data.skillImage;
	}

	public override void SelectMe(HeroSelectionController selector)
	{
		if(isSelected)
			return;
		
		isSelected = true;
		frame.gameObject.SetActive(true);
		selector.currentSkillCard = this;

		if(!thisIsDecisionCard)
		{
			infoUI.SetSkillInfo(data);
		}else
		{
			infoUI.HideSkillCardImmediately();
		}
	}
	public override void LeaveMe()
	{
		if(!isSelected)
			return;

		isSelected = false;
		frame.gameObject.SetActive(false);
	}

	public void OnPressBtn(HeroSelectionController selector)
	{
		if(!thisIsDecisionCard)
		{
			infoUI.ToggleSkillCard();
		}
		else
		{
			switch(decision)
			{
			case Decision.Back:
				LeaveMe();
				infoUI.Hide();
				selector.herosPanel.alpha = 1f;
				selector.isNavigatingSkillUI = false;
				break;
			case Decision.Confirm:
				infoUI.Hide();
				selector.herosPanel.alpha = 1;
				selector.ConfirmSelection();
				break;
			default:
				break;
			}
		}
	}
}
