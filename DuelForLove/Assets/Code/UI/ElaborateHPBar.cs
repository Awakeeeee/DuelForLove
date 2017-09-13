using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElaborateHPBar : MonoBehaviour
{
	[Header("Setting")]
	public float barLength;
	public float shortFreezeTime = 0.1f;
	public float shrunkSpeed = 0.5f;		//a multipiler
	public bool showText;
	public enum TextDisplay
	{
		Percent,
		Number
	}
	public TextDisplay textDisplay;

	[Header("UI Components")]
	public Image background;
	public Image foreground;
	public Image hurtground;
	public CanvasGroup textGroup;
	public Text hpText;
	public bool showRecoverText;
	public Text recoverText;

	[Header("Damage text jump out(For Wpos HP bar)")]
	public Text jumpOutText;

	[Header("Data Monitor")]
	public float maxHp;
	public float currentHp;
	private float hpPivot;	//the current percent of life, floating btw 0 ~ 1

	//flags
	private bool isShrunking = false;

	void Start()
	{
		//ResetBar(100, 1f);
	}

	public void ResetBar(float _maxHp, float startHealthPercent, float recoverAmount = 0.0f)
	{
		maxHp = _maxHp;

		startHealthPercent = Mathf.Clamp(startHealthPercent, 0f, 1f);
		currentHp = maxHp * startHealthPercent;

		SetHpRecoverAmount(recoverAmount);

		hpPivot = startHealthPercent;
		foreground.fillAmount = hpPivot;
		hurtground.fillAmount = hpPivot;

		isShrunking = false;

		if(showText)
			InitText(currentHp, maxHp);
		else
			HideText();

		StopAllCoroutines();
	}

	void Update()
	{
		//TEST
//		if(Input.GetKeyDown(KeyCode.T))
//		{
//			UpdateTakeDamageHP(10f);
//		}
	}

	public void UpdateTakeDamageHP(float reduce)
	{	
		if(jumpOutText != null && currentHp > 0f)
		{
			jumpOutText.text = reduce.ToString();
			jumpOutText.GetComponent<Animator>().SetTrigger("jumpOut");
		}

		currentHp -= reduce;
		currentHp = Mathf.Clamp(currentHp, 0f, maxHp);
		hpPivot = currentHp / maxHp;
		foreground.fillAmount = hpPivot;		//fore ground bar shrunk immediately

		if(showText)
		{
			UpdateText(currentHp);
		}

		StartCoroutine(HurtBarShrunkCo());		//hurt ground bar shrunk gradually and follows fore ground bar
	}
	public void UpdateRecoverHP(float add)
	{
		currentHp += add;
		currentHp = Mathf.Clamp(currentHp, 0f, maxHp);
		hpPivot = currentHp / maxHp;
		foreground.fillAmount = hpPivot;
		background.fillAmount = hpPivot;
		if(showText)
		{
			UpdateText(currentHp);
		}
	}
	public void SetHpRecoverAmount(float amount)
	{
		recoverText.text = "+" + amount.ToString("F1");
	}

	IEnumerator HurtBarShrunkCo()
	{
		if(isShrunking)
			yield break;

		isShrunking = true;
		yield return new WaitForSeconds(shortFreezeTime);

		while(!Mathf.Approximately(foreground.fillAmount, hurtground.fillAmount))
		{
			hurtground.fillAmount = Mathf.MoveTowards(hurtground.fillAmount, foreground.fillAmount, shrunkSpeed * Time.deltaTime);		//let hurt bar follow fore bar
			yield return null;
		}

		hurtground.fillAmount = foreground.fillAmount;
		isShrunking = false;
	}

	//------Hp text display--------
	void InitText(float _currentHP, float _maxHP)
	{
		textGroup.alpha = 1.0f;
		UpdateText(_currentHP);
	}
	void UpdateText(float _currentHP)
	{
		if(textDisplay == TextDisplay.Percent)
			hpText.text = ((_currentHP / maxHp) * 100).ToString("F0") + "%";
		else if(textDisplay == TextDisplay.Number)
			hpText.text = _currentHP.ToString() + "/" + maxHp.ToString();
	}
	public void HideText()
	{
		if(textGroup == null)
			return;
		
		textGroup.alpha = 0.0f;
		UpdateText(0.0f);
	}
}
