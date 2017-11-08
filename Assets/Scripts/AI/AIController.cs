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
		return avoidObs(target.transform.position + 
						((target.transform.position - goal).normalized) *
						target.GetComponent<SphereCollider>().radius * target.transform.localScale.x,
						target.GetComponent<SphereCollider>());
	}

	private Vector3 avoidObs(Vector3 tar, SphereCollider sc)
	{
		if(Physics.Raycast(transform.position, tar - transform.position, tar.magnitude, gameObject.layer))
		{
			Vector3 fromSCtoTar = tar - sc.transform.position;
			Vector3 fromThisToTar = tar - transform.position;
			//Vector3 newTar = Vector3.Project(fromSCtoTar, )
		}

		return tar;
	}

	private Vector3 bisector(Vector3 v1, Vector3 v2)
	{
		return v1.normalized + v2.normalized;
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
