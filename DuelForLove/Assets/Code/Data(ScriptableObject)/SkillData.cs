using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///For now I attemp to list all possition infomation of a skill, let's see if I need to make gategories later
/// 
///Should I save common data in thisScriptableObject, and save specific data in skill Monobehaviour?
[CreateAssetMenu(fileName = "New Skill", menuName = "New Skill")]
public class SkillData : ScriptableObject
{
	[Header("Info")]
	public int ID;
	public string skillName;
	public Sprite skillImage;
	[Multiline]public string detailDescription;
	public string oneLineDescription;
	public string hero;

	[Header("Limitation")]
	public float cd;
	[Tooltip("With the time, you can 's' the skill")]
	public float preCastTime;
	public float hpCost;
	public float enegyCost;

	[Header("Game Effect")]
	public float damage;
	public float knockForce = 20f;	//Note: let knock force larger than move speed in number. Use knock back resist in Character data to compromise. This is for knocking back while moving effect.
	public float skillDuration;
	public float effectDuration;
	[Tooltip("If the skill shoot a bullet, this is the bullet speed")]
	public float bulletSpeed;
	public float range;
	public LayerMask targetLayer;
	public OptionalParam[] optionalParams;

	[Header("VFX")]
	public GameObject castEffect;
	public GameObject pathEffect;
	public GameObject hitEffect;

	[Header("SFX")]
	public AudioClip[] castClips;
	public AudioClip[] hitClips;
	public AudioClip[] endClips;
	public OptionalSound[] optionalClips;
}

[System.Serializable]
public class OptionalParam
{
	public float value;
	public string info;
}
[System.Serializable]
public class OptionalSound
{
	public AudioClip clip;
	public string info;
}
