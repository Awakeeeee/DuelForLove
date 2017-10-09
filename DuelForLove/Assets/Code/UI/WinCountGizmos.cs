using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCountGizmos : MonoBehaviour 
{
	public Color deactiveCol;
	public Color activeCol;
	public List<Image> gizmos1p = new List<Image>();
	public List<Image> gizmos2p = new List<Image>();

	public void ResetGizmos()
	{
		for(int i = 0; i < gizmos1p.Count; i++)
		{
			gizmos1p[i].color = deactiveCol;
			gizmos2p[i].color = deactiveCol;
		}
	}

	public void ActiveWinGizmos(int winnerIndex, int winCount)
	{
		int gizmosIndex = winCount - 1;
		if(winnerIndex == 1)
		{
			gizmos1p[gizmosIndex].color = activeCol;
		}else if(winnerIndex == 2)
		{
			gizmos2p[gizmosIndex].color = activeCol;
		}else
		{
			Debug.LogError("Invaild Winner Index passed to UI win count gizmos!");
		}
	}
}
