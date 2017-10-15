using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ME_HpMp : MonoBehaviour 
{
	public enum MeType
	{
		HP,
		MP
	}

	public MeType mt;
	public float recoverAmount = 20f;
	public GameObject collectVFX;
	public AudioClip collectSFX;

	void OnTriggerEnter(Collider other)
	{
		CharacterHP hp = other.GetComponent<CharacterHP>();

		if(!hp)
			return;

		if(SoundManager.Instance && collectSFX)
		{
			SoundManager.Instance.PlayGlobalSFX(collectSFX);
		}
		if(collectVFX)
		{
			GameObject vfxInstance = Instantiate(collectVFX, this.transform.position, Quaternion.identity);
		}

		if(mt == MeType.HP)
		{
			hp.RecoverHP(recoverAmount);
		}else if(mt == MeType.MP)
		{
			hp.RecoverMP(recoverAmount);
		}

		Destroy(this.gameObject);
	}
}
