using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageBurst : SkillBehaviour
{
	protected override void PreCast ()
	{
		CommonOnPreCast();

		CommonOnExitPreCastSuccessfully();
	}

	protected override void Casting ()
	{
		EndCast();
	}

	protected override void EndCast ()
	{
		casting = false;
	}
}
