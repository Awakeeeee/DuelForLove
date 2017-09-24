using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : SingletonBase<GUIManager>
{
	public ElaborateHPBar hpBar_1p, hpBar_2p;
	public ElaborateHPBar enegyBar_1p, enegyBar_2p;
	public SkillUIController skills_1p, skills_2p;
	public StateBuffUIController stateBuff_1p, stateBuff_2p;
}
