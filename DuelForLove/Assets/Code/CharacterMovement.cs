using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterMovement : MonoBehaviour 
{
	public bool smoothRotate;
	public bool smoothAcceleration;
	public float movementStartDeadzone = 0f;
	public float movementStopDeadzone = 0.05f;

	Character mc;

	void Awake()
	{
		Debug.Log("Movement compo Awake!");

		mc = GetComponent<Character>();
	}

	void Update()
	{
		Rotate();
		Move();
	}

	void Rotate()
	{
		if(!mc.rotationPermission)
			return;
		
		float rotateAmount = smoothRotate ? Input.GetAxis(mc.horizontalAxis) : Input.GetAxisRaw(mc.horizontalAxis);
		transform.Rotate(Vector3.up, mc.rotateSpeed * rotateAmount * Time.deltaTime);
	}

	void Move()
	{
		if(!mc.movementPermission)
		{
			mc.TransitState(Character.PlayerState.Idle);
			mc.hero.SetMoveAnim(false);
			return;	
		}
		
		float moveAmount = smoothAcceleration ? Input.GetAxis(mc.verticalAxis) : Input.GetAxisRaw(mc.verticalAxis);
		transform.position += transform.forward * mc.moveSpeed * moveAmount * Time.deltaTime;

		if(Mathf.Abs(moveAmount) > movementStartDeadzone && mc.CurrentState == Character.PlayerState.Idle)
		{
			mc.TransitState(Character.PlayerState.Moving);
			mc.hero.SetMoveAnim(true);
		}
		else if(Mathf.Abs(moveAmount) < movementStopDeadzone && mc.CurrentState == Character.PlayerState.Moving)
		{
			mc.TransitState(Character.PlayerState.Idle);
			mc.hero.SetMoveAnim(false);
		}
	}
}
