using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownPrompt : MonoBehaviour 
{
	public Text countText;

	public void SetCountText(string text)
	{
		countText.text = text;
	}

	public void OnAnimEnd()
	{
		this.gameObject.SetActive(false);
	}

	public void Pause()
	{
		GameManager.Instance.Pause(true);
	}

	public void Unpause()
	{
		GameManager.Instance.Pause(false);
	}
}
