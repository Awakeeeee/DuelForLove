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

	public bool selecting;

	void Start()
	{
		selecting = true;
		currentCard.SelectMe(this);
	}

	void Update()
	{
		if(!selecting)
			return;

		//navigating
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
			float dir = Input.GetAxisRaw(horizontalAxis);
			if(dir > 0)
				currentCard.up.SelectMe(this);
			else
				currentCard.down.SelectMe(this);
		}

		//confirm
		if(Input.GetButtonDown(confirmSelectionAxis))
		{
			if(currentCard == null)
				return;

			selecting = false;
			GameManager.Instance.SetSelectedHero(this);
			currentCard.ConfirmHero(this);
		}
	}
}
