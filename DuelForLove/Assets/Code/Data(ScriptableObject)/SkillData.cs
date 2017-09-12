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
	public float hpCost;
	public float enegyCost;

	[Header("Game Effect")]
	public float damage;
	public float knockForce;		//Note: let knock force larger than move speed in number. Use knock back resist on Character data to compromise. This is for knocking back while moving effect.
	public float duration;
	public float range;
	public LayerMask targetLayer;
	public Buff[] buffs;
	public Debuff[] debuffs;
}

[System.Serializable]
public class Buff
{
	public float value;
	public string info;
}

[System.Serializable]
public class Debuff
{
	public float value;
	public string info;
}
