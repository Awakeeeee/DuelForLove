using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Character initial default data
[CreateAssetMenu(fileName = "New Character Data", menuName = "New Character Data")]
public class CharacterData : ScriptableObject
{
	public float maxHP;
	public float maxMP;

	public float moveSpeed;
	public float rotateSpeed;

	public float healthRecoverPerSec;
	public float enegyRecoverPerSec;

	public float knockBackResist;
}
