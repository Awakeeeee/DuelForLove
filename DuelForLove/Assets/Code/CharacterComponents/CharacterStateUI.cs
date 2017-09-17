using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStateUI : MonoBehaviour 
{
	public Image stunPrompt;
	public Image castingProgressBar;
	public Image castingProgress;

	public void HideAll()
	{
		stunPrompt.gameObject.SetActive(false);
		castingProgressBar.gameObject.SetActive(false);
	}

	public void ToggleStunPrompt(bool state)
	{
		HideAll();
		stunPrompt.gameObject.SetActive(state);
	}

	public void ToggleCastingProgress(bool state)
	{
		castingProgressBar.gameObject.SetActive(state);
		castingProgress.fillAmount = 1f;
	}
	public void UpdateCastingProgress(float value)
	{
		castingProgress.fillAmount = value;
	}
}
