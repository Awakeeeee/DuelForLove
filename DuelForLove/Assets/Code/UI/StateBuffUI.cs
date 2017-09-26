using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateBuffUI : MonoBehaviour 
{
	private Image buffIcon;
	private Text buffName;

	void Awake()
	{
		buffIcon = GetComponentInChildren<Image>();
		buffName =GetComponentInChildren<Text>();
	}

	public void Set(Sprite _s, string _t)
	{
		buffIcon.sprite = _s;
		buffName.text = _t;
	}
}
