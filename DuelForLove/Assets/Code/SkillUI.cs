using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour 
{
	[Header("Data")]
	public SkillData skillData;

	[Header("UI Components")]
	public Image baseImage;
	public Image cdCoverImage;
	public Text nameText;

	[Header("Anim Numbers")]
	public float endScale;
	public float oneWayDuration;

	private bool isCounting;

	void Start()
	{
		Init();
	}

	void Init()
	{
		if(skillData.skillImage)
			baseImage.sprite = skillData.skillImage;
		nameText.text = skillData.skillName;
		isCounting = false;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.B))
		{
			StartCount();
		}
	}

	public void StartCount()
	{
		if(isCounting)
			return;
		
		StopCoroutine(CountCo());
		StartCoroutine(CountCo());
	}

	IEnumerator CountCo()
	{
		isCounting = true;
		cdCoverImage.fillAmount = 1f;

		float timer = 0f;
		while(timer < skillData.cd)
		{
			cdCoverImage.fillAmount = (skillData.cd - timer) / skillData.cd;
			
			timer += Time.deltaTime;
			yield return null;
		}

		cdCoverImage.fillAmount = 0f;
		isCounting = false;
		yield return StartCoroutine(CountEndAnim());
	}

	IEnumerator CountEndAnim()
	{
		float timer = 0.0f;

		while(timer < oneWayDuration)
		{
			float scale = Mathf.Lerp(1f, endScale, timer / oneWayDuration);
			transform.localScale = new Vector3(scale, scale, 1f);
			timer += Time.deltaTime;
			yield return null;
		}
		transform.localScale = new Vector3(endScale, endScale, 1f);
		timer = 0.0f;

		while(timer < oneWayDuration)
		{
			float scale = Mathf.Lerp(endScale, 1f, timer / oneWayDuration);
			transform.localScale = new Vector3(scale, scale, 1f);
			timer += Time.deltaTime;
			yield return null;
		}

		transform.localScale = Vector3.one;
	}
}
