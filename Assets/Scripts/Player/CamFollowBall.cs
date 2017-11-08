using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowBall : MonoBehaviour
{
	private Transform ball;
	private Transform focus;
	private Vector3 distance;
	private Vector3 target;
	public float lerpSpeed = .1f;
	public bool on = true;

	void Start()
	{
		setup();
	}

	void Update ()
	{
		if(!setup())
		{
			return;
		}

		if(on)
		{
			target = ball.position - transform.position;
		}
		else
		{
			target = transform.parent.forward;
		}

		//transform.up = Vector3.Slerp(transform.up, transform.parent.up, lerpSpeed);
		transform.forward = Vector3.Slerp(transform.forward, target, lerpSpeed);
	}

	public bool toggle()
	{
		on = !on;
		return on;
	}

	private bool setup()
	{
		if(ball == null)
		{
			ball = GameObject.FindGameObjectWithTag("Ball").transform;
		}

		return (ball != null);
	}
}
