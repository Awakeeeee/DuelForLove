using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCastCircle : MonoBehaviour 
{
	public float moveSpeed = 3;
	Character mc;
	SpriteRenderer sr;

	void Awake()
	{
		mc = GetComponentInParent<Character>();
		sr = GetComponent<SpriteRenderer>();
	}

	void Start()
	{
		sr.color = mc.playerSwitch == Character.PlayerSwitch._1P ? Color.red : Color.blue;
		sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
	}

	void Update()
	{
		float inputX = Input.GetAxisRaw(mc.horizontalAxis);
		float inputY = Input.GetAxisRaw(mc.verticalAxis);

		transform.position += new Vector3(moveSpeed * inputX * Time.deltaTime, 0f, moveSpeed * inputY * Time.deltaTime);
	}

	public void InitSet(Vector3 pos, Vector3 size)
	{
		transform.position = pos;
		transform.localScale = new Vector3(size.x, size.y, 1f);
	}

	public void HideCircle()
	{
		this.gameObject.SetActive(false);
	}
}
