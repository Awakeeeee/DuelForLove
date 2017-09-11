using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingletonBase<GameManager>
{
	public Character player_1p;
	public Character player_2p;

	void OnEnable()
	{
		Debug.Log("GameManager OnEnable!");

		player_1p.HP.LinkUI(GUIManager.Instance.hpBar_1p, GUIManager.Instance.enegyBar_1p);
		player_1p.hero.LinkUI(GUIManager.Instance.skills_1p);
	}
}
