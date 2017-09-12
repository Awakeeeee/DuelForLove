using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : SkillBehaviour
{
	protected override void PreCast ()
	{
		CommonOnPreCast();

		CommonOnExitPreCastSuccessfully();	//there is no if statement to call Fail Cast, meaning this skill cannot be s-ed
	}

	protected override void Casting ()
	{
		//TODO gameplay implementation

		EndCast();
	}

	protected override void EndCast ()
	{
		casting = false;
	}
}
