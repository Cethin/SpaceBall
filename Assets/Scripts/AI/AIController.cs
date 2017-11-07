using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ship))]
public class AIController : MonoBehaviour
{
	private Ship ship;
	public GameObject target;
	public Vector3 goal;
	private PID controlPID;
	public float[] controlPIDVars = new float[3];

	void FixedUpdate ()
	{
		if(!setup())
		{
			return;
		}

		//targetPID.ProcessVariable = target.transform.position;
		controlPID.ProcessVariable =  transform.position - targetPoint(); //targetPID.pid();
		ship.thrustInput(controlPID.pid());

		Debug.DrawLine(transform.position, targetPoint(), Color.green);

		//Debug.Log(controlPID);
	}

	private Vector3 targetPoint()
	{
		return target.transform.position + ((target.transform.position - goal).normalized) * target.GetComponent<SphereCollider>().radius * target.transform.localScale.x;
	}

	private bool setup()
	{
		if(ship == null)
		{
			ship = GetComponent<Ship>();
		}

		if(controlPID == null)
		{
			controlPID = new PID(controlPIDVars);
		}

		return (ship != null);
	}
}
