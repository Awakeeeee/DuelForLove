using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterMovement : MonoBehaviour 
{
	[Header("Movement")]
	public bool smoothRotate;
	public bool smoothAcceleration;
	public float movementStartDeadzone = 0f;
	public float movementStopDeadzone = 0.05f;

	[Header("Physics")]
	public int rayNumber = 8;
	public float radius = 0.6f;
	public LayerMask collideLayer;
	public float collidingMovementPercent = 0f;

	[Header("Shake")]
	public Transform shakerTransform;
	private float shakeAmount;
	private float shakeTime;
	private float shakeFactor;

	private bool forceMove;
	private float externalForceAmount;
	private Vector3 externalForceDir;

	private float intervalAngle;
	private float collidingMultiplier;
	private Character mc;

	void Awake()
	{
		Debug.Log("Movement compo Awake!");

		mc = GetComponent<Character>();
	}

	void Start()
	{
		intervalAngle = 360f / rayNumber;
		collidingMultiplier = 1f;
		forceMove = false;
	}

	void Update()
	{
		CastSurroundingRays();

		Rotate();
		Move();

		ForcedMovement();
	}

	/// Collide with other character but not push each other.
	void CastSurroundingRays()
	{
		for(int i = 0; i < rayNumber; i++)
		{
			Debug.DrawRay(transform.position, AngleToRayDir(i * intervalAngle) * radius, Color.red);
			if(Physics.Raycast(transform.position, AngleToRayDir(i * intervalAngle), radius, collideLayer, QueryTriggerInteraction.Ignore))
			{
				//stop
				collidingMultiplier = collidingMovementPercent;
				//get out of trap state
				if((!Physics.Raycast(transform.position, transform.forward, radius, collideLayer, QueryTriggerInteraction.Ignore) && Input.GetAxisRaw("Vertical") > 0f)
					||(!Physics.Raycast(transform.position, -transform.forward, radius, collideLayer, QueryTriggerInteraction.Ignore) && Input.GetAxisRaw("Vertical") < 0f))
				{
					collidingMultiplier = 1f;
				}

				return;
			}
		}

		collidingMultiplier = 1f;
	}

	Vector3 AngleToRayDir(float angleInDegree)
	{
		return new Vector3(Mathf.Sin(angleInDegree * Mathf.Deg2Rad), 0f, Mathf.Cos(angleInDegree * Mathf.Deg2Rad));
	}

	void Rotate()
	{
		if(!mc.rotationPermission)
			return;
		
		float rotateAmount = smoothRotate ? Input.GetAxis(mc.horizontalAxis) : Input.GetAxisRaw(mc.horizontalAxis);
		transform.Rotate(Vector3.up, mc.McData.rotateSpeed * rotateAmount * Time.deltaTime);
	}

	void Move()
	{
		if(!mc.movementPermission)
		{
			mc.TransitState(Character.PlayerState.Idle);
			mc.Csc.SetMoveAnim(false);
			return;	
		}
		
		float moveAmount = smoothAcceleration ? Input.GetAxis(mc.verticalAxis) : Input.GetAxisRaw(mc.verticalAxis);
		transform.position += transform.forward * mc.McData.moveSpeed * moveAmount * Time.deltaTime * collidingMultiplier;

		if(Mathf.Abs(moveAmount) > movementStartDeadzone && mc.CurrentState == Character.PlayerState.Idle)
		{
			mc.TransitState(Character.PlayerState.Moving);
			mc.Csc.SetMoveAnim(true);
		}
		else if(Mathf.Abs(moveAmount) < movementStopDeadzone && mc.CurrentState == Character.PlayerState.Moving)
		{
			mc.TransitState(Character.PlayerState.Idle);
			mc.Csc.SetMoveAnim(false);
		}
	}

	void ForcedMovement()
	{
		if(!forceMove)
			return;

		externalForceAmount -= mc.McData.knockBackResist * Time.deltaTime;
		if(externalForceAmount <= 0f)
		{
			externalForceAmount = 0f;
			externalForceDir = Vector3.zero;
			forceMove = false;	//end forced movement
		}

		transform.position += externalForceAmount * externalForceDir * Time.deltaTime;
	}

	///Trigger forced movement.
	public void AddForce(float forceAmount, Vector3 forceDir)
	{
		forceMove = true;
		externalForceAmount = forceAmount;
		externalForceDir = forceDir;
	}
		
	public void TriggerShake(float amount = 0.1f, float time = 0.2f, float factor = 1)
	{
		shakeAmount = amount;
		shakeTime = time;
		shakeFactor = factor;

		StopAllCoroutines();
		StartCoroutine(Shake());
	}
	IEnumerator Shake()
	{
		float shakeTimer = 0.0f;
		if(shakerTransform == null)
			yield break;

		while(shakeTimer < shakeTime)
		{
			float amount = Mathf.Lerp(shakeAmount, 0f, shakeTimer / shakeTime);
			Vector3 shakeVec = Random.insideUnitSphere * amount;
			shakerTransform.localPosition = new Vector3(shakeVec.x, 0f, shakeVec.z);
			shakeTimer += Time.deltaTime * shakeFactor;
			yield return null;
		}
		shakerTransform.localPosition = Vector3.zero;
		shakeTimer = 0.0f;
	}
}
