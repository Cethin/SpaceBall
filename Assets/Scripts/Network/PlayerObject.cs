using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObject : NetworkBehaviour
{
	public GameObject playerPref;
	public static int numPlayers = 0;
	public const int maxPlayers = 2;
	
	private static string playerJoinMsg = "{0} has joined the game!";
	private NetworkClient myClient;
	protected GameObject myObject;
	public KeyCode newPlayer = KeyCode.Insert;

	void Start()
	{
		if(isLocalPlayer)
		{
			CmdSpawnUnit();
		}
		else
		{
			Debug.Log(string.Format(playerJoinMsg, netId));
		}
	}

	void Update()
	{
		if(isLocalPlayer && Input.GetKeyDown(newPlayer) && Controller.NumPlayers < Controller.maxLocalPlayers)
		{
			CmdSpawnUnit();
		}
	}

	// Create the visual representation of the player (on the server)
	[Command]
	public void CmdSpawnUnit()
	{
		if(numPlayers < maxPlayers)
		{
			GameObject myObject = Instantiate(playerPref);
			Debug.Log(Controller.NumPlayers);
			SpawnController.spawn(myObject, Controller.NumPlayers % 2);
			NetworkServer.SpawnWithClientAuthority(myObject, connectionToClient);

			numPlayers++;
		}

	}
}
