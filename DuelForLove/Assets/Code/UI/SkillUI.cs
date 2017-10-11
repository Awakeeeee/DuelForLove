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
	public CanvasGroup cdGroup;
	public Image cdCoverImage;
	public Text cdText;
	public Text nameText;
	public Text mpText;

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
		baseImage.sprite = skillData.skillImage;

		if(skillData.enegyCost <= 0)
		{
			mpText.text = "";
		}else
		{
			mpText.text = skillData.enegyCost.ToString();
		}

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
		cdGroup.alpha = 1;
		cdText.text = "";
		cdCoverImage.fillAmount = 1f;

		float timer = 0f;
		while(timer < skillData.cd)
		{
			cdCoverImage.fillAmount = (skillData.cd - timer) / skillData.cd;
			cdText.text = (skillData.cd - timer).ToString("F1");
			
			timer += Time.deltaTime;
			yield return null;
		}

		cdText.text = "";
		cdCoverImage.fillAmount = 0f;
		cdGroup.alpha = 0;
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

	public void ManaOut()
	{
		baseImage.color = Color.blue;
	}
	public void ManaReady()
	{
		baseImage.color = Color.white;
	}
}
