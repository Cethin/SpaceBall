using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ship : MonoBehaviour
{
	public Vector3 thrustScale = Vector3.one;
	public Vector3 torqueScale = Vector3.one;

	private Rigidbody rb;

	public void thrust(Vector3 thrust)
	{
		if(!setup())
		{
			return;
		}

		thrust.x *= thrustScale.x;
		thrust.y *= thrustScale.y;
		thrust.z *= thrustScale.z;

		rb.AddRelativeForce(thrust, ForceMode.Force);
	}

	public void torque(Vector3 torque)
	{
		if(!setup())
		{
			return;
		}

		torque.x *= torqueScale.x;
		torque.y *= torqueScale.y;
		torque.z *= torqueScale.z;

		rb.AddRelativeTorque(torque);
	}

	private bool setup()
	{
		if(rb == null)
		{
			rb = GetComponent<Rigidbody>();
		}

		return (rb != null);
	}
}
