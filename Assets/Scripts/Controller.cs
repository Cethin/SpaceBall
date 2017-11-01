using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Ship))]
public class Controller : MonoBehaviour
{
	private const int NUM_CONTROLS = 10;

	public KeyCode forward = KeyCode.LeftShift;
	public KeyCode back = KeyCode.LeftControl;
	public KeyCode left = KeyCode.J;
	public KeyCode right = KeyCode.K;
	public KeyCode up = KeyCode.Space;
	public KeyCode down = KeyCode.LeftAlt;
	public KeyCode pitchUp = KeyCode.S;
	public KeyCode pitchDown = KeyCode.W;
	public KeyCode yawLeft = KeyCode.A;
	public KeyCode yawRight = KeyCode.D;
	public KeyCode rollLeft = KeyCode.Q;
	public KeyCode rollRight = KeyCode.E;

	private Ship ship;
	
	void FixedUpdate ()
	{
		if(!setup())
		{
			return;
		}

		ship.thrust(latMov());
		ship.torque(rotMov());
	}

	private Vector3 latMov()
	{
		Vector3 lateral = new Vector3();
		if(Input.GetKey(forward))
		{
			lateral += Vector3.forward;
		}
		else if(Input.GetKey(back))
		{
			lateral -= Vector3.forward;
		}


		if(Input.GetKey(left))
		{
			lateral += Vector3.left;
		}
		else if(Input.GetKey(right))
		{
			lateral -= Vector3.left;
		}


		if(Input.GetKey(up))
		{
			lateral += Vector3.up;
		}
		else if(Input.GetKey(down))
		{
			lateral -= Vector3.up;
		}

		return lateral;
	}

	private Vector3 rotMov()
	{
		Vector3 rotation = new Vector3();
		if(Input.GetKey(rollLeft))
		{
			rotation += Vector3.forward;
		}
		else if(Input.GetKey(rollRight))
		{
			rotation -= Vector3.forward;
		}


		if(Input.GetKey(pitchUp))
		{
			rotation += Vector3.left;
		}
		else if(Input.GetKey(pitchDown))
		{
			rotation -= Vector3.left;
		}


		if(Input.GetKey(yawRight))
		{
			rotation += Vector3.up;
		}
		else if(Input.GetKey(yawLeft))
		{
			rotation -= Vector3.up;
		}

		return rotation;
	}

	private bool setup()
	{
		if(ship == null)
		{
			ship = GetComponent<Ship>();
		}

		return (ship != null);
	}
}
