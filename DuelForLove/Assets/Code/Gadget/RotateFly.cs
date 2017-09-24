using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFly : MonoBehaviour 
{
	public float speed;
	public float wobbleAmplytude;
	public float wobbleFrequency;
	private float wobblePeriod;

	void LateUpdate()
	{
		transform.Rotate(new Vector3(0f, 0f, speed * Time.deltaTime));

		wobblePeriod += Time.deltaTime * wobbleFrequency;
		float wobbleAmount = wobbleAmplytude * Mathf.Sin(wobblePeriod) * Time.deltaTime;
		transform.position += new Vector3(wobbleAmount, 0f, 0f);
	}
}
