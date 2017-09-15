using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceWalk : SkillBehaviour
{
	protected override void PreCast ()
	{
		CommonOnPreCast();

		CommonOnCastSuccessfully();
	}

	protected override void Casting ()
	{

		EndCast();
	}

	protected override void EndCast ()
	{
		CommonOnEndCast();
	}
}
