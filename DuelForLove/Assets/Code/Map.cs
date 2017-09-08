using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour 
{
	public int width = 3;
	public int height = 3;
	public float scale = 1;
	public GameObject tile;

	public Vector3[,] grid;

	void Start()
	{
		//GenerateMap();
	}

	public void GenerateMap()
	{
		ClearMap();
		grid = new Vector3[width, height];

		for(int x = 0; x < width; x++)
		{
			for(int y = 0; y < height; y++)
			{
				Vector3 tilePos = new Vector3(-width/2f + 0.5f + x, 0f, -height/2f + 0.5f + y) * scale;
				grid[x, y] = tilePos;
				GameObject newTile = Instantiate(tile, this.transform);
				newTile.transform.position = tilePos;
				newTile.transform.localScale *= scale;
			}
		}
	}

	public void ClearMap()
	{
		List<Transform> childrenTiles = new List<Transform>();
		transform.GetComponentsInChildren<Transform>(true, childrenTiles);
		childrenTiles.RemoveAt(0);	//deselect parent object

		if(childrenTiles == null || childrenTiles.Count <= 0)
			return;
		
		foreach(Transform t in childrenTiles)
		{
			DestroyImmediate(t.gameObject);
		}
	}
}

public class Coord
{
	public int x;
	public int y;

	public Coord(int _x, int _y)
	{
		x = _x;
		y = _y;
	}
}