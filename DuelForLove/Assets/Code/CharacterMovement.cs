using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterMovement : MonoBehaviour 
{
	public bool smoothRotate;
	public bool smoothAcceleration;
	public float movementDeadZone = 0.05f;

	Character mc;

	void Awake()
	{
		mc = GetComponent<Character>();
	}

	void Update()
	{
		Rotate();
		Move();
	}

	void Rotate()
	{
		float rotateAmount = smoothRotate ? Input.GetAxis(mc.horizontalAxis) : Input.GetAxisRaw(mc.horizontalAxis);
		transform.Rotate(Vector3.up, mc.rotateSpeed * rotateAmount * Time.deltaTime);
	}

	void Move()
	{
		float moveAmount = smoothAcceleration ? Input.GetAxis(mc.verticalAxis) : Input.GetAxisRaw(mc.verticalAxis);
		transform.position += transform.forward * mc.moveSpeed * moveAmount * Time.deltaTime;

		if(Mathf.Abs(moveAmount) > movementDeadZone && mc.CurrentState == Character.PlayerState.Idle)
		{
			mc.TransitState(Character.PlayerState.Moving);
		}
		else if(Mathf.Abs(moveAmount) < movementDeadZone && mc.CurrentState == Character.PlayerState.Moving)
		{
			mc.TransitState(Character.PlayerState.Idle);
		}
	}
}
