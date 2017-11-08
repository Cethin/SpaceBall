using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
	public float force = 40f;
	public float[] pidVals = new float[3]{1,1,1};
	private BoxCollider col;

	private List<Rigidbody> rbs = new List<Rigidbody>();
	private List<PID> pids = new List<PID>();

	void Start()
	{
		col = GetComponent<BoxCollider>();
	}

	void FixedUpdate ()
	{
		for(int i = 0; i < rbs.Count; i++)
		{
			Rigidbody rb = rbs[i];
			PID pid = pids[i];
			pid.ProcessVariable = rb.velocity - (posClosestToBounds(rb.transform.position) - rb.transform.position);
			rb.AddForce(pid.pid() * force, ForceMode.Acceleration);
			//Debug.DrawLine(rb.transform.position, posClosestToBounds(rb.transform.position), Color.green);
		}
	}

	void OnTriggerExit(Collider otherCol)
	{
		Rigidbody rb = otherCol.gameObject.GetComponent<Rigidbody>();
		if(rb != null)
		{
			rbs.Add(rb);
			int i = rbs.IndexOf(rb);
			pids.Insert(i, (new PID(pidVals)));
		}
	}

	void OnTriggerEnter(Collider otherCol)
	{
		Rigidbody rb = otherCol.gameObject.GetComponent<Rigidbody>();
		if(rb != null && rbs.Contains(rb))
		{
			int i = rbs.IndexOf(rb);
			rbs.RemoveAt(i);
			pids.RemoveAt(i);
		}
	}

	private Vector3 posClosestToBounds(Vector3 pos)
	{
		Vector3 target = pos;
		Vector3 extents = col.bounds.extents;


		if(pos.x > transform.position.x + extents.x)
		{
			target.x = transform.position.x + extents.x;
		}
		else if(pos.x < transform.position.x - extents.x)
		{
			target.x = transform.position.x - extents.x;
		}

		
		if(pos.y > transform.position.y + extents.y)
		{
			target.y = transform.position.y + extents.y;
		}
		else if(pos.y < transform.position.y - extents.y)
		{
			target.y = transform.position.y - extents.y;
		}

		
		if(pos.z > transform.position.z + extents.z)
		{
			target.z = transform.position.z + extents.z;
		}
		else if(pos.z < transform.position.z - extents.z)
		{
			target.z = transform.position.z - extents.z;
		}

		return target;
	}
}
