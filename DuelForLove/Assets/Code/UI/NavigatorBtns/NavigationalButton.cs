using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationalButton : MonoBehaviour 
{
	[Header("Neighbours")]
	public NavigationalButton up;
	public NavigationalButton down;
	public NavigationalButton right;
	public NavigationalButton left;
	public bool isSelected;

	[Header("UI components")]
	public Image frame;

	public virtual void SelectMe(HeroSelectionController selector)
	{}
	public virtual void SelectMe(MapSelectionController selector)
	{}
	public virtual void LeaveMe()
	{}
}
