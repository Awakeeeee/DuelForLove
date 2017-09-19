using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSelectionController : MonoBehaviour
{
	[Tooltip("1 = 1P, 2 = 2P etc.")]
	public int index;
	public string horizontalAxis;
	public string verticalAxis;
	public string checkSkillInfoAxis;
	public string confirmSelectionAxis;
	public HeroCard currentCard;
	public HeroInfoUI infoUI;
	public SkillCard currentSkillCard;

	public bool selecting;
	public bool isNavigatingSkillUI;

	void Start()
	{
		selecting = true;
		isNavigatingSkillUI = false;
		currentCard.SelectMe(this);
	}

	void Update()
	{
		if(!selecting)
			return;

		if(Input.GetButtonDown(checkSkillInfoAxis))
		{
			isNavigatingSkillUI = !isNavigatingSkillUI;
			if(isNavigatingSkillUI)
			{
				currentSkillCard = infoUI.skillCards[0];
				currentSkillCard.SelectMe();
			}else
			{
				if(currentSkillCard)
					currentSkillCard.LeaveMe();
				infoUI.HideSkillCardImmediately();
			}
		}

		if(!isNavigatingSkillUI)
		{
			NavigateHeroCards();
		}
		else
		{
			NavigateSkillCards();
		}
	}

	void NavigateHeroCards()
	{
		//navigating hero cards
		if(Input.GetButtonDown(horizontalAxis))
		{
			currentCard.LeaveMe(this);
			float dir = Input.GetAxisRaw(horizontalAxis);
			if(dir > 0)
				currentCard.right.SelectMe(this);
			else
				currentCard.left.SelectMe(this);
		}
		else if(Input.GetButtonDown(verticalAxis))
		{
			currentCard.LeaveMe(this);
			float dir = Input.GetAxisRaw(verticalAxis);
			if(dir > 0)
				currentCard.up.SelectMe(this);
			else
				currentCard.down.SelectMe(this);
		}

		//confirm hero card
		if(Input.GetButtonDown(confirmSelectionAxis))
		{
			if(currentCard == null)
				return;

			selecting = false;
			GameManager.Instance.SetSelectedHero(this);
			currentCard.ConfirmHero(this);
		}
	}

	void NavigateSkillCards()
	{
		if(Input.GetButtonDown(verticalAxis))
		{
			currentSkillCard.LeaveMe();
			float dir = Input.GetAxisRaw(verticalAxis);
			if(dir > 0)
			{
				currentSkillCard = currentSkillCard.up;
				currentSkillCard.SelectMe();
			}
			else
			{
				currentSkillCard = currentSkillCard.down;
				currentSkillCard.SelectMe();	
			}
		}

		if(Input.GetButtonDown(confirmSelectionAxis))
		{
			if(currentSkillCard == null)
				return;
			
			infoUI.ToggleSkillCard();
		}
	}
}
