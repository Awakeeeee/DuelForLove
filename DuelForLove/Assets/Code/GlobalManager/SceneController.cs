using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneController : PersistentSingletonBase<SceneController>
{
	public CanvasGroup fadePanel;
	public int firstSceneToLoad = 1;

	public float fadeTime = 1.0f;
	public bool isFading = false;

	public event Action LoadSceneEvent;
	public event Action UnLoadSceneEvent;

	IEnumerator Start()
	{
		fadePanel.alpha = 1.0f;
		fadePanel.blocksRaycasts = true;

		yield return StartCoroutine(LoadAndActiveScene(firstSceneToLoad));
		yield return StartCoroutine(ScreenFade(0.0f));
	}

	//External point
	public void LoadScene(int sceneID, bool fadeIn = true, bool fadeOut = true)
	{
		if(isFading)
			return;
		
		StartCoroutine(LoadSceneProcess(sceneID, fadeIn, fadeOut));
	}

	IEnumerator LoadSceneProcess(int sceneID, bool fadeIn, bool fadeOut)
	{
		if(fadeIn)
		{
			yield return StartCoroutine(ScreenFade(1f));	
		}

		if(UnLoadSceneEvent != null)
		{
			UnLoadSceneEvent();
		}

		yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

		yield return StartCoroutine(LoadAndActiveScene(sceneID));

		if(LoadSceneEvent != null)
		{
			LoadSceneEvent();
		}

		if(fadeOut)
		{
			fadePanel.alpha = 1f;
			yield return StartCoroutine(ScreenFade(0f));
		}
	}

	IEnumerator LoadAndActiveScene(int sceneID)
	{
		yield return SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Additive);	//load scene in background but not active yet

		Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);	//find the loaded scene

		SceneManager.SetActiveScene(newScene);	//set it active
	}

	IEnumerator ScreenFade(float targetAlpha)
	{
		isFading = true;
		fadePanel.blocksRaycasts = true;

		float fadeSpeed = Mathf.Abs(fadePanel.alpha - targetAlpha) / fadeTime;

		while(!Mathf.Approximately(fadePanel.alpha, targetAlpha))
		{
			float newAlpha = Mathf.MoveTowards(fadePanel.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
			fadePanel.alpha = newAlpha;
				
			yield return null;
		}

		fadePanel.blocksRaycasts = false;
		isFading = false;
	}
}
