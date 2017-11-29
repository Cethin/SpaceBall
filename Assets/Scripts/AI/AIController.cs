using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ship))]
public class AIController : MonoBehaviour
{
	public GameObject target;
	public GameObject goal;
	public LayerMask avoidanceMask;
	private Ship ship;
	private PID controlPID;

	public bool debugMode = false;

	void FixedUpdate ()
	{
		if(!setup())
		{
			return;
		}

		//targetPID.ProcessVariable = target.transform.position;
		Vector3 tarPoint = targetPoint();
		controlPID.ProcessVariable =  transform.position - tarPoint; //targetPID.pid();
		ship.thrustInput(controlPID.pid());

		if(debugMode)
		{
			Debug.DrawLine(transform.position, tarPoint, Color.magenta);
			//Debug.Log(controlPID);
		}
	}

	private Vector3 targetPoint()
	{
		Vector3 toGoal = goal.transform.position - target.transform.position;
		Vector3 vel = target.GetComponent<Rigidbody>().velocity;
		Vector3 tar = target.transform.position + ((((vel - toGoal).normalized)).normalized * ((target.GetComponent<SphereCollider>().radius * target.transform.localScale.x)));
		

		if(debugMode)
		{
			Debug.DrawLine(target.transform.position, target.transform.position + toGoal, Color.blue);
			Debug.DrawLine(target.transform.position, target.transform.position + vel, Color.red);
			Debug.DrawLine(target.transform.position, tar, Color.white);
		}

		return avoidObs(tar);
	}

	private Vector3 avoidObs(Vector3 tar)
	{
		if(debugMode)
		{
			Debug.DrawLine(transform.position, tar, Color.green);
		}

		RaycastHit hitInfo = new RaycastHit();
		if(Physics.Raycast(transform.position, tar - transform.position, out hitInfo, (transform.position - tar).magnitude - (AIData.rayCastBuffer + AIData.positionBuffer), avoidanceMask))
		{
			Collider c = hitInfo.collider;

			Vector3 fromCtoTar = tar - c.transform.position;
			Vector3 fromCtoThis = transform.position -  c.transform.position;
			Vector3 bi = bisector(fromCtoTar, fromCtoThis);

			if(c.tag == "Ball")
			{
				float radius = ((SphereCollider)c).radius * c.transform.localScale.x;

				Vector3 newTar = (bi * (radius + 3)) + c.transform.position;
				//newTar = addShipSize(newTar, c.gameObject);

				if(debugMode)
				{
					Debug.DrawLine(c.transform.position, newTar, Color.yellow);
				}

				return newTar;
			}
		}

		return tar;
	}

	private Vector3 bisector(Vector3 v1, Vector3 v2)
	{
		return (v1.normalized + v2.normalized).normalized;
	}

	private Vector3 addShipSize(Vector3 tarPoint, GameObject tarObj)
	{
		BoxCollider bc = GetComponent<BoxCollider>();
		Vector3 extents = bc.bounds.extents * transform.localScale.x;

		// Rotate extents by rotation
		extents = transform.worldToLocalMatrix * extents;	// convert to local rotation

		if(tarPoint.x < tarObj.transform.position.x)	// Tar is to the left
		{
			tarPoint.x -= extents.x;
		}
		else if(tarPoint.x > tarObj.transform.position.x)	// tar is the the right
		{
			tarPoint.x += extents.x;
		}

		if(tarPoint.y < tarObj.transform.position.y)	// Tar is to the left
		{
			tarPoint.y -= extents.y;
		}
		else if(tarPoint.y > tarObj.transform.position.y)	// tar is the the right
		{
			tarPoint.y += extents.y;
		}

		if(tarPoint.z < tarObj.transform.position.z)	// Tar is to the left
		{
			tarPoint.z -= extents.z;
		}
		else if(tarPoint.z > tarObj.transform.position.z)	// tar is the the right
		{
			tarPoint.z += extents.z;
		}

		return tarPoint;
	}

	private bool setup()
	{
		if(ship == null)
		{
			ship = GetComponent<Ship>();
		}

		if(controlPID == null)
		{
			controlPID = new PID(AIData.controlPIDVars);
		}

		return (ship != null);
	}
}
