using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
	public float force = 40f;
	BoxCollider col;

	public List<Rigidbody> rbs = new List<Rigidbody>();

	void Start()
	{
		col = GetComponent<BoxCollider>();
	}

	void FixedUpdate ()
	{
		foreach(Rigidbody rb in rbs)
		{
			rb.AddForce((posClosestToBounds(rb.transform.position) - rb.transform.position).normalized * force, ForceMode.Acceleration);
			//Debug.DrawLine(rb.transform.position, posClosestToBounds(rb.transform.position), Color.green);
		}
	}

	void OnTriggerExit(Collider otherCol)
	{
		Rigidbody rb = otherCol.gameObject.GetComponent<Rigidbody>();
		if(rb != null)
		{
			rbs.Add(rb);
		}
	}

	void OnTriggerEnter(Collider otherCol)
	{
		Rigidbody rb = otherCol.gameObject.GetComponent<Rigidbody>();
		if(rb != null)
		{
			rbs.Remove(rb);
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
