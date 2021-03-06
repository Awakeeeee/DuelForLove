﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSelectionController : MonoBehaviour
{
	[Tooltip("1 = 1P, 2 = 2P etc.")]
	public CanvasGroup herosPanel;
	public int index;
	public string horizontalAxis;
	public string verticalAxis;
	public string confirmSelectionAxis;
	public HeroCard currentHeroCard;
	public SkillCard currentSkillCard;
	public HeroInfoUI infoUI;

	/// Still in the process of deciding hero.
	public bool selecting;
	public bool isNavigatingSkillUI;

	void Start()
	{
		selecting = true;
		isNavigatingSkillUI = false;
		currentHeroCard.SelectMe(this);
	}

	void Update()
	{
		if(!selecting)
			return;

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
			currentHeroCard.LeaveMe();
			float dir = Input.GetAxisRaw(horizontalAxis);
			if(dir > 0)
				currentHeroCard.right.SelectMe(this);
			else
				currentHeroCard.left.SelectMe(this);
			
			if(SoundManager.Instance)
				SoundManager.Instance.PlaySoundUI(0);
		}
		else if(Input.GetButtonDown(verticalAxis))
		{
			currentHeroCard.LeaveMe();
			float dir = Input.GetAxisRaw(verticalAxis);
			if(dir > 0)
				currentHeroCard.up.SelectMe(this);
			else
				currentHeroCard.down.SelectMe(this);

			if(SoundManager.Instance)
				SoundManager.Instance.PlaySoundUI(0);
		}

		//go to hero detail
		if(Input.GetButtonDown(confirmSelectionAxis))
		{
			if(currentHeroCard == null)
				return;

			isNavigatingSkillUI = true;	//switch navigation mode
			herosPanel.alpha = 0f;	//hide this player's hero panel
			infoUI.Show();	//show selected hero detail info panel

			currentSkillCard = infoUI.skillCards[0] as SkillCard;
			currentSkillCard.SelectMe(this);	//by default the first skill is selected

			if(SoundManager.Instance)
				SoundManager.Instance.PlaySoundUI(1);
		}
	}

	void NavigateSkillCards()
	{
		if(Input.GetButtonDown(horizontalAxis))
		{
			currentSkillCard.LeaveMe();
			float dir = Input.GetAxisRaw(horizontalAxis);

			if(dir > 0)
				currentSkillCard.right.SelectMe(this);
			else
				currentSkillCard.left.SelectMe(this);

			if(SoundManager.Instance)
				SoundManager.Instance.PlaySoundUI(0);
		}
		else if(Input.GetButtonDown(verticalAxis))
		{
			currentSkillCard.LeaveMe();
			float dir = Input.GetAxisRaw(verticalAxis);

			if(dir > 0)
				currentSkillCard.up.SelectMe(this);
			else
				currentSkillCard.down.SelectMe(this);

			if(SoundManager.Instance)
				SoundManager.Instance.PlaySoundUI(0);
		}

		if(Input.GetButtonDown(confirmSelectionAxis))
		{
			if(currentSkillCard == null)
				return;
			
			currentSkillCard.OnPressBtn(this);	//maybe trigger decision, or maybe show/hide skill info

			if(SoundManager.Instance)
				SoundManager.Instance.PlaySoundUI(2);
		}
	}

	public void ConfirmSelection()
	{
		selecting = false;
		GameManager.Instance.SetSelectedHero(this);
		currentHeroCard.ConfirmHero();

		if(SoundManager.Instance)
			SoundManager.Instance.PlaySoundUI(1);

		AttempGoMapSelection();
	}

	void AttempGoMapSelection()
	{
		if(GameManager.Instance.player_1P_data == null || GameManager.Instance.player_2P_data == null)
			return;

		MapSelectionController mapSelection = FindObjectOfType<MapSelectionController>();
		if(mapSelection)
		{
			mapSelection.heroCanvas.SetActive(false);
			mapSelection.Show();
		}else
		{
			Debug.LogError("No Map Selection panel in scene");
		}
	}
}
