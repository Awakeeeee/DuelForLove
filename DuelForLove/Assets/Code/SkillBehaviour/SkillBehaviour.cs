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

	[Header("Process Mornitor")]
	public float timer;
	public bool casting;

	//the character/hero this skill belongs to
	protected Character mc;
	protected CharacterSkillController hero;
	protected SkillUIController ui;

	protected void Awake()
	{
		skillDataInstance = Instantiate(skillDataDefault);

		casting = false;
		timer = skillDataInstance.cd;
	}

	protected void Update()
	{
		GeneralTimerUpdate();
		if(casting)
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
		if(timer <= skillDataInstance.cd)
			timer += Time.deltaTime;
	}

	protected bool GeneralCastTest()
	{
		if(timer < skillDataInstance.cd)
		{
			Debug.LogWarning(mc.playerSwitch.ToString() + " " + skillDataInstance.skillName + " is not ready yet!");
			return false;
		}
		if(mc.Chp.CurrentMP < skillDataInstance.enegyCost)
		{
			Debug.LogWarning(mc.playerSwitch.ToString() + " " + skillDataInstance.skillName + " not enough enegy!");
			return false;
		}
		if(casting)
		{
			return false;
		}
		if(hero.SkillIsAnimating)	//TODO not really?
		{
			return false;
		}

		return true;
	}

	//Most time no need to override this two, override it only when a skill is special(for example, the skill cost HP instead of MP)

	///Call this in PreCast() when a skill cast is ready.
	///Including flag reset, consumpetion, UI change, start SFX.
	protected virtual void CommonOnCastSuccessfully()
	{
		timer = 0.0f;
		casting = true;
		mc.Chp.ConsumeEnegy(skillDataInstance.enegyCost);
		ui.SkillUIColdDown(sIndex);
		PlayRandomSkillAudio(skillDataInstance.castClips);
	}
	///Call this in PreCast() when a skill cast is good to use but being 's-ed'.
	protected virtual void CommonOnCastFailed()
	{
		casting = false;
	}
	///Call this when enter PreCast().
	///Including start animation.
	protected virtual void CommonOnPreCast()
	{
		hero.TriggerSkillAnim(sIndex);
	}
	protected virtual void CommonOnEndCast()
	{
		casting = false;
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
	protected void PlayRandomSkillAudio(params AudioClip[] audios)
	{
		AudioClip ac = GetRandomSkillAudio(audios);
		if(ac)
		{
			mc.Ads.PlayOneShot(ac);
		}
	}
}
