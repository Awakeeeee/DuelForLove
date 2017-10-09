using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectionController : MonoBehaviour 
{
	public GameObject heroCanvas;
	public string horizontalAxis;
	public string verticalAxis;
	public string confirmSelectionAxis;

	public MapCard currentMap;

	private CanvasGroup self;
	private bool selfEnable;

	void Awake()
	{
		self = GetComponent<CanvasGroup>();
	}

	void Start()
	{
		currentMap.SelectMe(this);
		Hide();
	}

	void Update()
	{
		if(!selfEnable)
			return;
		
		if(Input.GetButtonDown(horizontalAxis))
		{
			currentMap.LeaveMe();
			float dir = Input.GetAxisRaw(horizontalAxis);
			if(dir > 0)
				currentMap.right.SelectMe(this);
			else
				currentMap.left.SelectMe(this);

			if(SoundManager.Instance)
				SoundManager.Instance.PlaySoundUI(0);
		}
		else if(Input.GetButtonDown(verticalAxis))
		{
			currentMap.LeaveMe();
			float dir = Input.GetAxisRaw(verticalAxis);
			if(dir > 0)
				currentMap.up.SelectMe(this);
			else
				currentMap.down.SelectMe(this);

			if(SoundManager.Instance)
				SoundManager.Instance.PlaySoundUI(0);
		}

		//confirm
		if(Input.GetButtonDown(confirmSelectionAxis))
		{
			if(currentMap == null)
				return;

			GameManager.Instance.StartNewDuel(currentMap.sceneID);

			if(SoundManager.Instance)
				SoundManager.Instance.PlaySoundUI(1);
		}
	}

	public void Show()
	{
		self.alpha = 1f;
		selfEnable = true;
	}

	public void Hide()
	{
		self.alpha = 0f;
		selfEnable = false;
	}
}
