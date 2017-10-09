using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPrompt : MonoBehaviour 
{
	public Text text;

	public void SetText(string t)
	{
		text.text = t;
	}

	public void SetTextCol(Color col)
	{
		text.color = col;
	}
}
