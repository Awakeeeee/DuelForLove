using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingletonBase<GameManager>
{
	public HeroCard currentCharacter_1P;
	public HeroCard currentCharacter_2P;

	public Character player_1p;
	public Character player_2p;

	void Start()
	{
		Debug.Log("GameManager Start!");

		player_1p.Chp.LinkUI(GUIManager.Instance.hpBar_1p, GUIManager.Instance.enegyBar_1p);
		player_1p.Csc.LinkUI(GUIManager.Instance.skills_1p);
		player_2p.Chp.LinkUI(GUIManager.Instance.hpBar_2p, GUIManager.Instance.enegyBar_2p);
		player_2p.Csc.LinkUI(GUIManager.Instance.skills_2p);
	}
}
