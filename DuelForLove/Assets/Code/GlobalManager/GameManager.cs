using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class GameManager : PersistentSingletonBase<GameManager>
{
	[Header("Data")]
	public bool isDevelopmentMode;
	public Character player_1P_data;
	public Vector3 startPos_1P;
	public Character player_2P_data;
	public Vector3 startPos_2P;
	public int bo = 3;

	[Header("Instance")]
	public Character player_1P;
	public Character player_2P;
	public CountDownPrompt countDown;
	public WinCountGizmos winGizmos;
	public WinPrompt winPrompt;
	public int winCount_1P;
	public int winCount_2P;

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
		
	void Update()
	{
		//TEST
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			SceneController.Instance.LoadScene(1);
		}
		if(Input.GetKeyDown(KeyCode.O))
		{
			EndRound(1);
		}
	}

	public void SetSelectedHero(HeroSelectionController hc)
	{
		if(hc.index == 1)
		{
			player_1P_data = hc.currentHeroCard.characterPrefab;
		}
		else if(hc.index == 2)
		{
			player_2P_data = hc.currentHeroCard.characterPrefab;
		}else
		{
			Debug.LogError("Invild character index passed from Hero selection to GameMnager.");
		}
	}

	/// When two players are confirmed and map is chosen, game starts.
	public void StartNewDuel(int mapID)
	{
		if(SceneController.Instance)
		{
			SceneController.Instance.LoadScene(mapID);
		}
	}

	private void CreateCharactersInGameScene()
	{
		if(isDevelopmentMode)
			return;

		if(player_1P_data == null || player_2P_data == null)
		{
			Debug.Log("New non-game scene is loaded.");
			ClearData();
			if(winGizmos.isActiveAndEnabled)
			{
				winGizmos.gameObject.SetActive(false);
				winGizmos.ResetGizmos();
			}

			return;
		}

		player_1P = Instantiate(player_1P_data, startPos_1P, player_1P_data.transform.rotation);
		player_1P.playerSwitch = Character.PlayerSwitch._1P;
		player_1P.Chp.LinkUI(GUIManager.Instance.hpBar_1p, GUIManager.Instance.enegyBar_1p);
		player_1P.Csc.LinkUI(GUIManager.Instance.skills_1p);
		player_1P.Cbc.LinkUI(GUIManager.Instance.stateBuff_1p);

		player_2P = Instantiate(player_2P_data, startPos_2P, player_2P_data.transform.rotation);
		player_2P.playerSwitch = Character.PlayerSwitch._2P;
		player_2P.Chp.LinkUI(GUIManager.Instance.hpBar_2p, GUIManager.Instance.enegyBar_2p);
		player_2P.Csc.LinkUI(GUIManager.Instance.skills_2p);
		player_2P.Cbc.LinkUI(GUIManager.Instance.stateBuff_2p);

		countDown.gameObject.SetActive(true);
		winGizmos.gameObject.SetActive(true);
	}

	/// When a round is over but game is not over, start new round.
	public void EndRound(int winnerIndex)
	{
		StartCoroutine(EndRoundCo(winnerIndex));
	}
	IEnumerator EndRoundCo(int winnerIndex)
	{
		if(SceneController.Instance == null)
			yield break;

		if(winnerIndex == 1)
		{
			winCount_1P++;
			winGizmos.ActiveWinGizmos(winnerIndex, winCount_1P);
		}
		else if(winnerIndex == 2)
		{
			winCount_2P++;
			winGizmos.ActiveWinGizmos(winnerIndex, winCount_2P);
		}else
		{
			Debug.LogError("Invild Winner Index!");
		}

		winPrompt.gameObject.SetActive(true);
		Color col = winnerIndex == 1 ? Color.red : Color.blue;
		winPrompt.SetTextCol(col);
		StringBuilder sBuilder = new StringBuilder();

		if(Mathf.Max(winCount_1P, winCount_2P) < bo - 1) //BO3
		{
			sBuilder.Append(winnerIndex.ToString());
			winPrompt.SetText(sBuilder.ToString());

			yield return new WaitForSeconds(0.5f);

			sBuilder.Append("P");
			winPrompt.SetText(sBuilder.ToString());

			yield return new WaitForSeconds(0.5f);

			sBuilder.Append(" KO!");
			winPrompt.SetText(sBuilder.ToString());

			yield return new WaitForSeconds(1f);

			winPrompt.gameObject.SetActive(false);
			SceneController.Instance.ReloadScene();
		}
		else
		{
			sBuilder.Append(winnerIndex.ToString() + "P Wins The Duel!");
			winPrompt.SetText(sBuilder.ToString());

			yield return new WaitForSeconds(1f);

			ClearData();
			winPrompt.gameObject.SetActive(false);
			SceneController.Instance.LoadScene(2);
		}
	}

	private void ClearData()
	{
		player_1P_data = null;
		player_2P_data = null;
		winCount_1P = 0;
		winCount_2P = 0;
	}

	/// True to pause, false to unpause.
	public void Pause(bool state)
	{
		if(player_1P == null || player_2P == null)
		{
			return;
		}

		player_1P.SetActionPermission(!state);
		player_1P.SetMovementPermission(!state, !state);
		player_2P.SetActionPermission(!state);
		player_2P.SetMovementPermission(!state, !state);
	}
}
