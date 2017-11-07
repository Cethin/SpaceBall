using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDirection : MonoBehaviour
{
	private Quaternion rot;

	// Use this for initialization
	void Start ()
	{
		rot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.rotation = rot;
	}
}
