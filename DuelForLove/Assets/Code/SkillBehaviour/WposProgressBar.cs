using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WposProgressBar : MonoBehaviour 
{
	public Image bar;
	public Image barProgress;

	void Start()
	{
		bar.gameObject.SetActive(false);
	}

	public void ToggleBar(bool state)
	{
		bar.gameObject.SetActive(state);
		barProgress.fillAmount = 1f;
	}
	public void UpdateBar(float value)
	{
		barProgress.fillAmount = value;
	}
}
