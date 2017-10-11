using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Skill behaviour will be a statemachine. Think of ember can T+C and razer can absorb+F, skill is not at one state at a time.
/// </summary>
public abstract class SkillBehaviour : MonoBehaviour 
{
	[Header("Data")]
	public int sIndex;
	public SkillData skillDataDefault;
	[HideInInspector]public SkillData skillDataInstance;
	public int initSkillBulletNumber = 5;

	[Header("Process Mornitor")]
	public float cdTimer;
	public bool triggerCastingUpdate;

	//the character/hero this skill belongs to
	protected Character mc;
	public Character Mc {get{return mc;}}
	protected CharacterSkillController hero;
	protected SkillUIController ui;

	protected GameObject castEffectInstance;
	protected GameObject[] skillBulletInstance;
	protected GameObject hitEffectInstance;

	protected bool currentHasEnoughMana;

	protected void Awake()
	{
		skillDataInstance = Instantiate(skillDataDefault);
		skillBulletInstance = new GameObject[initSkillBulletNumber];

		triggerCastingUpdate = false;
		cdTimer = skillDataInstance.cd;
		PreloadEffects();
	}

	protected void Update()
	{
		GeneralTimerUpdate();
		GeneralManaDetection();

		if(triggerCastingUpdate)
		{
			Casting();
		}
	}
		
	/// Initialization when the skill is set to a character/hero.
	public void Init(int index, Character characterBelongTo, CharacterSkillController heroBelongTo, SkillUIController uiBelongTo)
	{
		sIndex = index;
		mc = characterBelongTo;
		hero = heroBelongTo;
		ui = uiBelongTo;

		currentHasEnoughMana = (mc.Chp.CurrentMP >= skillDataInstance.enegyCost);
	}

	//external link point
	public void Cast()
	{
		if(GeneralCastTest() == false)
		{
			return;
		}
		PreCast();
	}

	protected void GeneralTimerUpdate()
	{
		if(cdTimer <= skillDataInstance.cd)
			cdTimer += Time.deltaTime;
	}
	protected void GeneralManaDetection()
	{
		if((mc.Chp.CurrentMP < skillDataInstance.enegyCost) && currentHasEnoughMana)
		{
			currentHasEnoughMana = false;
			ui.NotEnoughMana(sIndex);
		}else if((mc.Chp.CurrentMP >= skillDataInstance.enegyCost) && !currentHasEnoughMana)
		{
			currentHasEnoughMana = true;
			ui.GotEnoughMana(sIndex);
		}
	}

	protected bool GeneralCastTest()
	{
		if(triggerCastingUpdate)	//cannot cast the same skill if it is casting
		{
			return false;
		}
		if(hero.SkillIsAnimating)	//TODO if a skill is animating, cannot cast another skill
		{
			return false;
		}

		if(cdTimer < skillDataInstance.cd)
		{
			Debug.LogWarning(mc.playerSwitch.ToString() + " " + skillDataInstance.skillName + " is not ready yet!");
			return false;
		}
		if(mc.Chp.CurrentMP < skillDataInstance.enegyCost)
		{
			Debug.LogWarning(mc.playerSwitch.ToString() + " " + skillDataInstance.skillName + " not enough enegy!");
			return false;
		}

		return true;
	}

	//Most time no need to override this two, override it only when a skill is special(for example, the skill cost HP instead of MP)

	///Call this in PreCast() when a skill cast is ready.
	///Including flag reset, consumpetion, UI change, start SFX.
	protected virtual void CommonOnCastSuccessfully()
	{
		cdTimer = 0.0f;
		triggerCastingUpdate = true;
		mc.Chp.ConsumeEnegy(skillDataInstance.enegyCost);
		ui.SkillUIColdDown(sIndex);
		PlayRandomSkillAudio(skillDataInstance.castClips);
	}
	///Call this in PreCast() when a skill cast is good to use but being 's-ed'.
	protected virtual void CommonOnCastFailed()
	{
		mc.Csc.BreakSkillAnim(sIndex);
		triggerCastingUpdate = false;
	}
	///Call this when enter PreCast().
	///Including start animation.
	protected virtual void CommonOnPreCast()
	{
		hero.TriggerSkillAnim(sIndex);
	}
		
	///On pre-cast you can 's' the skill to determine fail on success
	protected virtual IEnumerator SedSkillCoroutine()
	{
		float timer = 0.0f;
		while(timer < skillDataInstance.preCastTime)
		{
			if(Input.GetButtonDown(mc.skill_break))	//worked!!!!!!!!
			{
				CommonOnCastFailed();
				yield break;
			}

			timer += Time.deltaTime;
			yield return null;
		}
		CommonOnCastSuccessfully();
	}
		
	/// Stop casting update. Anim to free state. Play end audio clip.
	protected virtual void CommonOnEndCast()
	{
		triggerCastingUpdate = false;
		mc.Csc.CurrentCastingAnimEnd();
		PlayRandomSkillAudio(skillDataInstance.endClips);
	}

	protected abstract void PreCast(); //do preparation, if preparation successfully finishes, cost resources and starts casting
	protected abstract void Casting(); //casting behavioour, and check for end cast
	protected abstract void EndCast();

	protected AudioClip GetRandomSkillAudio(AudioClip[] audioGroup)
	{
		if(audioGroup == null || audioGroup.Length <= 0)
			return null;

		int ran = Random.Range(0, audioGroup.Length);
		return audioGroup[ran];
	}
	public void PlayRandomSkillAudio(params AudioClip[] audios)
	{
		AudioClip ac = GetRandomSkillAudio(audios);
		if(ac)
		{
			mc.Ads.PlayOneShot(ac);
		}
	}
	public void PlayOptionalClip(int clipIndex)
	{
		if(skillDataInstance.optionalClips.Length <= 0 || skillDataInstance.optionalClips[clipIndex] == null)
			return;

		AudioClip oa = skillDataInstance.optionalClips[clipIndex].clip;
		mc.Ads.PlayOneShot(oa);
	}

	//optimization
	void PreloadEffects()
	{
		if(skillDataInstance.castEffect)
		{
			castEffectInstance = Instantiate(skillDataInstance.castEffect);
			castEffectInstance.SetActive(false);
		}
		if(skillDataInstance.skillBullet)
		{
			for(int i = 0; i < skillBulletInstance.Length; i++)
			{
				skillBulletInstance[i] = Instantiate(skillDataInstance.skillBullet) as GameObject;
				skillBulletInstance[i].SetActive(false);
			}
		}
		if(skillDataInstance.hitEffect)
		{
			hitEffectInstance = Instantiate(skillDataInstance.hitEffect);
			hitEffectInstance.SetActive(false);
		}
	}

	public GameObject ShowCastEffect(Vector3 wpos, Quaternion rot, bool startActivate = true)
	{
		if(castEffectInstance)
		{
			castEffectInstance.transform.position = wpos;
			castEffectInstance.transform.rotation = rot;
			if(startActivate)
				castEffectInstance.SetActive(true);
			return castEffectInstance;
		}else
		{
			castEffectInstance = Instantiate(skillDataInstance.castEffect);
			castEffectInstance.SetActive(false);
		}
		return null;
	}
	public GameObject ShowHitEffect(Vector3 wpos, Quaternion rot, bool startActivate = true)
	{
		if(hitEffectInstance)
		{
			hitEffectInstance.transform.position = wpos;
			hitEffectInstance.transform.rotation = rot;
			if(startActivate)
				hitEffectInstance.SetActive(true);
			return hitEffectInstance;
		}
		return null;
	}
	public GameObject ShowSkillBullet(Vector3 wpos, Quaternion rot, bool startActivate = true)
	{
		for(int i = 0; i < skillBulletInstance.Length; i++)
		{
			if(skillBulletInstance[i] == null)
			{
				Debug.LogWarning("Skill trying to use path effect but no effect is set in skill data.");
				return null;
			}

			if(!skillBulletInstance[i].activeInHierarchy)
			{
				skillBulletInstance[i].transform.position = wpos;
				skillBulletInstance[i].transform.rotation = rot;
				if(startActivate)
					skillBulletInstance[i].SetActive(true);
				return skillBulletInstance[i];
			}
		}

		return null;
	}
}
