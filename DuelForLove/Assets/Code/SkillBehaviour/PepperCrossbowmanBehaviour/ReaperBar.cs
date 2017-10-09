using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The bar made by PepperCrossbowMan Carolina Reaper.
//TODO this script almost only work for carolinaReaper bar. It is very specific but cannot be reuse for similar behaviours, I'm not sure if this is a good design.
public class ReaperBar : MonoBehaviour 
{
	public Transform appearance;
	public ObjectPool bulletPool;
	private AudioSource ads;

	private SkillBehaviour belongedSkill;
	private List<Vector3> dirs = new List<Vector3>();
	private float fireInterval;
	private float fireTimer;
	private int chillyNum;
	private float barRotateSpeed;
	private float rotationCount;
	private AudioClip shootClip;

	private bool hasBeenSetup;

	public void Init(SkillBehaviour thisSkill)
	{
		if(hasBeenSetup)
			return;
		
		belongedSkill = thisSkill;

		fireInterval = belongedSkill.skillDataInstance.optionalParams[1].value;
		fireTimer = 0f;

		barRotateSpeed = belongedSkill.skillDataInstance.optionalParams[2].value;

		chillyNum = (int)belongedSkill.skillDataInstance.optionalParams[0].value;
		float intervalAngle = 360f / chillyNum;

		for(int i = 0; i < chillyNum; i++)
		{
			dirs.Add(AngleToRayDir(intervalAngle * i));
		}

		bulletPool.obj = belongedSkill.skillDataInstance.skillBullet;

		shootClip = belongedSkill.skillDataInstance.optionalClips[0].clip;

		hasBeenSetup = true;
	}

	void OnEnable()
	{
		
	}

	void OnDisable()
	{
		rotationCount = 0f;
		fireTimer = 0f;
	}

	void Start()
	{
		ads = GetComponent<AudioSource>();
	}

	void Update()
	{
		if(!hasBeenSetup)
			return;

		rotationCount += barRotateSpeed * Time.deltaTime;
		appearance.localRotation = Quaternion.Euler(new Vector3(0f, 0f, rotationCount));

		if(fireTimer < fireInterval)
		{
			fireTimer += Time.deltaTime;
		}else
		{
			Fire();
		}
	}

	void Fire()
	{
		fireTimer = 0f;
		ads.PlayOneShot(shootClip);

		for(int i = 0; i < dirs.Count; i++)
		{
			SkillBullet chilly = bulletPool.GetPooledObj().GetComponent<SkillBullet>();
			if(!chilly)
			{
				Debug.LogWarning("Add bullet behaviour to PepperCrossbow - Carolina Reaper - Reaper bar's chilly bullet!");
				return;
			}
			chilly.transform.position = this.transform.position + dirs[i] * 1.7f;
			chilly.transform.forward = dirs[i];

			//TODO multiple hit effect problem
			chilly.InitBullet(belongedSkill);
		}
	}

	//TODO make all these general help methods in one place? this one is also used in movement
	Vector3 AngleToRayDir(float angleInDegree)
	{
		return new Vector3(Mathf.Sin(angleInDegree * Mathf.Deg2Rad), 0f, Mathf.Cos(angleInDegree * Mathf.Deg2Rad));
	}
}
