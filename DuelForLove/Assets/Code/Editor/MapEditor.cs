using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Map))]
public class MapEditor : Editor 
{
	Map self;

	void OnEnable()
	{
		self = target as Map;
	}

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		EditorGUILayout.Space();

		if(GUILayout.Button("Create Map"))
		{
			self.GenerateMap();
		}

		if(GUILayout.Button("Clear Map"))
		{
			self.ClearMap();
		}
	}
}
