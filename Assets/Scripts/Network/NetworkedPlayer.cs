using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedPlayer : NetworkBehaviour
{
	public GameObject localAttatchments;

	protected GameObject localGos;

	void Update()
	{
		spawnLocalObjs();
	}

	protected void spawnLocalObjs()
	{
		if(hasAuthority && localGos == null)
		{
			localGos = Instantiate(localAttatchments, transform.position, transform.rotation, transform);
			gameObject.AddComponent<Controller>();

			Camera cam = GetComponentInChildren<Camera>();
			if(cam != null)
			{
				ScreenModeController.addCam(cam);
			}
		}
	}
}
