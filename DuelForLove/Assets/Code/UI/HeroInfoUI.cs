using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroInfoUI : MonoBehaviour 
{
	public Image heroImage;
	public SkillCard[] skillCards;

	public Text heroName;
	public Text heroOccu;
	public Text heroDescription;

	public CanvasGroup skillDetailPanel;
	public Image detailSkillImage;
	public Text detailSkillName;
	public Text detailSkillDescription;
	public Text detailSkillOneLineDesc;

	private CanvasGroup self;

	void Awake()
	{
		self = GetComponent<CanvasGroup>();
	}

	void OnEnable()
	{
		Hide();
		
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

		for(int i = 0; i < skillCards.Length; i++)
		{
			skillCards[i].data = hc.skillData[i];
			skillCards[i].InitCard();
		}
	}
	///Set the infomation in skill panel, prepare to be displayed(not showing until confirm).
	public void SetSkillInfo(SkillData data)
	{
		detailSkillImage.sprite = data.skillImage;
		detailSkillName.text = data.skillName;
		detailSkillDescription.text = data.detailDescription;
		detailSkillOneLineDesc.text = data.oneLineDescription;
	}

	public void ToggleSkillCard()
	{
		float targetAlpha = skillDetailPanel.alpha > 0 ? 0 : 1;
		StartCoroutine(FadeSkillCard(targetAlpha));
	}
	IEnumerator FadeSkillCard(float targetAlpha)
	{
		if(Mathf.Abs(targetAlpha - skillDetailPanel.alpha) < 0.05f)
			yield break;
		
		float timer = 0.0f;
		float startAlpha = targetAlpha > 0 ? 0 : 1;
		skillDetailPanel.alpha = startAlpha;
		while(timer < 0.3f)
		{
			skillDetailPanel.alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / 0.3f);
			timer += Time.deltaTime;
			yield return null;
		}
		skillDetailPanel.alpha = targetAlpha;
	}
	public void HideSkillCardImmediately()
	{
		skillDetailPanel.alpha = 0f;
	}

	public void Hide()
	{
		HideSkillCardImmediately();
		self.alpha = 0f;
	}
	public void Show()
	{
		self.alpha = 1f;
	}
}
