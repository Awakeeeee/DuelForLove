using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///For now I attemp to list all possition infomation of a skill, let's see if I need to make gategories later
[CreateAssetMenu(fileName = "New Skill", menuName = "New Skill")]
public class SkillData : ScriptableObject
{
	[Header("Info")]
	public int ID;
	public string skillName;
	[Multiline]public string detailDescription;
	public string oneLineDescription;
	public string hero;

	[Header("Limitation")]
	public float cd;
	public float hpCost;
	public float enegyCost;

	[Header("Game Effect")]
	public float damage;
	public float duration;
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
