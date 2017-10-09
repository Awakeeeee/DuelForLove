using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour 
{
	public Button firstSelectedBtn;
	EventSystem es;

	void Start()
	{
		es = FindObjectOfType<EventSystem>();
		if(es)
		{
			es.SetSelectedGameObject(firstSelectedBtn.gameObject);
		}
	}

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
