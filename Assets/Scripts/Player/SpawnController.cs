using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpawnController
{
	private static GameObject[] blueSpawns;
	private static GameObject[] redSpawns;

	public static void findSpawns()
	{
		blueSpawns = GameObject.FindGameObjectsWithTag("BlueSpawn");
		redSpawns = GameObject.FindGameObjectsWithTag("RedSpawn");
	}

	public static void spawn(GameObject go, int team)
	{
		if(blueSpawns == null)
		{
			findSpawns();
		}

		GameObject spawn = go;

		if(team != 0 && team != 1)
		{
			team = (int)Random.Range(0,1);
		}
		
		if(team == 0)
		{
			spawn = blueSpawns[0];
		}
		else if(team == 1)
		{
			spawn = redSpawns[0];
		}

		go.transform.position = spawn.transform.position;
		go.transform.rotation = spawn.transform.rotation;
	}
}
