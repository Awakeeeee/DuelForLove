using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Play global SFX, UI SFX, BGM.
public class SoundManager : PersistentSingletonBase<SoundManager> 
{
	public AudioSource source_BGM;
	public AudioSource source_UI;
	public AudioSource source_Global;

	[Header("UI clip")]
	public AudioClip navigateClip;
	public AudioClip confirmHeroClip;
	public AudioClip checkSkillClip;

	/// <summary>
	/// 0 = navigate hero, 1 = confirm here, 2 = navigate skill, 3 = confirm skill.
	/// </summary>
	public void PlaySoundUI(int index)
	{
		AudioClip clip = null;
		switch(index)
		{
		case 0:
			clip = navigateClip;
			break;
		case 1:
			clip = confirmHeroClip;
			break;
		case 2:
			clip = checkSkillClip;
			break;
		default:
			break;
		}

		if(clip)
			source_UI.PlayOneShot(clip);
	}

	public void PlayGlobalSFX(AudioClip clip, float pitch = 1f, float volumeScale = 1f)
	{
		source_Global.pitch = pitch;
		source_Global.PlayOneShot(clip, volumeScale);
	}
}
