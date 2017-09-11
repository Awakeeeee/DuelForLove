using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUIController : MonoBehaviour 
{
	public SkillUI[] skills;

	public void SetSkillUIContent(SkillData[] data)
	{
		if(data.Length != skills.Length)
		{
			Debug.LogError("Skill UI number and given Skill data are not consistent.");
		}

		for(int i = 0; i < data.Length; i++)
		{
			skills[i].skillData = data[i];
		}
	}

	public void SkillUIColdDown(int skill_index)
	{
		if((skill_index - 1) < 0 || (skill_index - 1) > skills.Length - 1)
			Debug.LogError("Invalid skill index to array index!");
	
		skills[skill_index - 1].StartCount();
	}
}
