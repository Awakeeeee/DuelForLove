using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCard : NavigationalButton
{
	public int sceneID;

	public override void SelectMe(MapSelectionController selector)
	{
		if(isSelected)
			return;
		
		frame.gameObject.SetActive(true);
		isSelected = true;

		selector.currentMap = this;
	}

	public override void LeaveMe()
	{
		if(!isSelected)
			return;
		
		frame.gameObject.SetActive(false);
		isSelected = false;
	}
}
