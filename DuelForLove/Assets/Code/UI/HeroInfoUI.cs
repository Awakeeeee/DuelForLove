using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroInfoUI : MonoBehaviour 
{
	public Image heroImage;
	public Image skill_1;
	public Image skill_2;
	public Image skill_3;
	public Image skill_4;

	public Text heroName;
	public Text heroOccu;
	public Text heroDescription;

	public CanvasGroup skillDetailPanel;
	public Image detailSkillImage;
	public Text detailSkillName;
	public Text detailSkillDescription;
	public Text detailSkillOneLineDesc;

	void OnEnable()
	{
		skillDetailPanel.alpha = 0f;
		skillDetailPanel.interactable = false;
		skillDetailPanel.blocksRaycasts = false;
	}

	public void SetInfo(HeroCard hc)
	{
		heroImage.sprite = hc.characterData.avatar;
		heroName.text = hc.characterData.nameText;
		heroOccu.text = hc.characterData.heroText;
		heroDescription.text = hc.characterData.descriptionText;

		skill_1.sprite = hc.skillData_1.skillImage;
		skill_2.sprite = hc.skillData_2.skillImage;
		skill_3.sprite = hc.skillData_3.skillImage;
		skill_4.sprite = hc.skillData_4.skillImage;
	}
}
