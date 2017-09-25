using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingletonBase<GameManager>
{
	public bool isDevelopmentMode;
	public Character player_1P;
	public Vector3 startPos_1P;
	public Character player_2P;
	public Vector3 startPos_2P;

	void OnEnable()
	{
		Debug.Log("GameManager OnEnable!");

		if(SceneController.Instance)
		{
			SceneController.Instance.LoadSceneEvent += CreateCharactersInGameScene;
		}
	}

	void OnDisable()
	{
		if(SceneController.Instance)
		{
			SceneController.Instance.LoadSceneEvent -= CreateCharactersInGameScene;
		}
	}

	void Start()
	{
		Debug.Log("GameManager Start!");

		if(isDevelopmentMode)
		{
			player_1P.Chp.LinkUI(GUIManager.Instance.hpBar_1p, GUIManager.Instance.enegyBar_1p);
			player_1P.Csc.LinkUI(GUIManager.Instance.skills_1p);
			player_1P.Cbc.LinkUI(GUIManager.Instance.stateBuff_1p);
			player_2P.Chp.LinkUI(GUIManager.Instance.hpBar_2p, GUIManager.Instance.enegyBar_2p);
			player_2P.Csc.LinkUI(GUIManager.Instance.skills_2p);
			player_2P.Cbc.LinkUI(GUIManager.Instance.stateBuff_2p);
		}
	}

	//TEST
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			SceneController.Instance.LoadScene(1);
		}
	}

	public void SetSelectedHero(HeroSelectionController hc)
	{
		if(hc.index == 1)
		{
			player_1P = hc.currentCard.characterPrefab;
		}
		else if(hc.index == 2)
		{
			player_2P = hc.currentCard.characterPrefab;
		}else
		{
			Debug.LogError("Invild character index passed from Hero selection to GameMnager.");
		}

		AttempFinishSelection();
	}
		
	/// Everytime a player has selected a hero, check if both players done selection, if so, go next scene.
	public void AttempFinishSelection()
	{
		if(player_1P == null || player_2P == null)
			return;

		if(SceneController.Instance)
		{
			SceneController.Instance.LoadScene(3);
		}
	}

	private void CreateCharactersInGameScene()
	{
		if(isDevelopmentMode)
			return;
		
		if(player_1P == null || player_2P == null)
		{
			Debug.Log("New non-game scene is loaded.");
			ClearCharacterData();
			return;
		}

		player_1P = Instantiate(player_1P, startPos_1P, player_1P.transform.rotation);
		player_1P.playerSwitch = Character.PlayerSwitch._1P;
		player_1P.Chp.LinkUI(GUIManager.Instance.hpBar_1p, GUIManager.Instance.enegyBar_1p);
		player_1P.Csc.LinkUI(GUIManager.Instance.skills_1p);
		player_1P.Cbc.LinkUI(GUIManager.Instance.stateBuff_1p);

		player_2P = Instantiate(player_2P, startPos_2P, player_2P.transform.rotation);
		player_2P.playerSwitch = Character.PlayerSwitch._2P;
		player_2P.Chp.LinkUI(GUIManager.Instance.hpBar_2p, GUIManager.Instance.enegyBar_2p);
		player_2P.Csc.LinkUI(GUIManager.Instance.skills_2p);
		player_2P.Cbc.LinkUI(GUIManager.Instance.stateBuff_2p);	
	}

	private void ClearCharacterData()
	{
		player_1P = null;
		player_2P = null;
	}
}
