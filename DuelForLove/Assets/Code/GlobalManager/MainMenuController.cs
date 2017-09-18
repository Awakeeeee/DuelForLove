using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour 
{
	public void OnClickQuitButton()
	{
		Debug.Log("Game quit. Have a good day.");
		Application.Quit();
	}

	public void OnClickPlayButton()
	{
		if(!SceneController.Instance)
			return;

		SceneController.Instance.LoadScene(2);
	}
}
