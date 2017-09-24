using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateBuffUI : MonoBehaviour 
{
	private CanvasGroup buffObj;
	private Image buffIcon;
	private Text buffName;
	private float duration;

	public bool toggled;

	void Awake()
	{
		buffObj = GetComponent<CanvasGroup>();
		buffIcon = GetComponentInChildren<Image>();
		buffName =GetComponentInChildren<Text>();
	}

	void Start()
	{
		Hide();
	}

	public void Set(Sprite _s, string _t)
	{
		buffIcon.sprite = _s;
		buffName.text = _t;
	}

	public void Show()
	{
		buffObj.alpha = 1;
		toggled = true;
	}
	public void Hide()
	{
		buffObj.alpha = 0f;
		toggled = false;
	}
}
