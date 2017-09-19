using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCard : MonoBehaviour 
{
	[Header("Data")]
	public SkillData data;
	[Header("Neighbours")]
	public SkillCard up;
	public SkillCard down;
	[Header("UI components")]
	public Image frame;
	public HeroInfoUI infoUI;

	public bool isSelected;
	private Image imageComponent;

	void Awake()
	{
		imageComponent = GetComponent<Image>();
		isSelected = false;
		frame.gameObject.SetActive(false);
	}

	public void InitCard()
	{
		imageComponent.sprite = data.skillImage;
	}

	public void SelectMe()
	{
		if(isSelected)
			return;
		
		isSelected = true;
		frame.gameObject.SetActive(true);
		infoUI.SetSkillInfo(data);
	}
	public void LeaveMe()
	{
		if(!isSelected)
			return;

		isSelected = false;
		frame.gameObject.SetActive(false);
	}
}
