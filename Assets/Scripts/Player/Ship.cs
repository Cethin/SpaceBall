using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Ship : MonoBehaviour
{
	public Vector3 thrustScale = Vector3.one;
	public Vector3 torqueScale = Vector3.one;


	// ---  THRUSTERS ---
	public ParticleSystem aftPart;
	public ParticleSystem forPart;
	public ParticleSystem aftLeftPart;
	public ParticleSystem aftRightPart;
	public ParticleSystem aftTopPart;
	public ParticleSystem aftBotPart;
	public ParticleSystem forLeftPart;
	public ParticleSystem forRightPart;
	public ParticleSystem forTopPart;
	public ParticleSystem forBotPart;

	public float thrustEffectBuffer = .01f;

	private bool aftOn = false;
	private bool forOn = false;
	private bool aftLeftOn = false;
	private bool aftRightOn = false;
	private bool aftTopOn = false;
	private bool aftBotOn = false;
	private bool forLeftOn = false;
	private bool forRightOn = false;
	private bool forTopOn = false;
	private bool forBotOn = false;
	// ---  END THRUSTERS ---

	private Rigidbody rb;
	private bool pidOn = true;
	private PID thrustPID;
	public float[] thrustPIDVars = new float[3];
	private PID rotPID;
	public float[] rotPIDVars = new float[3];

	public Toggle pidUI;

	public bool PIDOn
	{
		get { return pidOn; }
		set
		{
			pidOn = value;
			if(pidOn)
			{
				thrustPID.reset();
				rotPID.reset();
			}

			pidUI.isOn = PIDOn;
		}
	}

	public bool pidToggle()
	{
		PIDOn = !PIDOn;
		return PIDOn;
	}

	private bool setup()
	{
		if(rb == null)
		{
			rb = GetComponent<Rigidbody>();
		}

		if(thrustPID == null)
		{
			thrustPID = new PID(thrustPIDVars);
		}

		if(rotPID == null)
		{
			rotPID = new PID(rotPIDVars);
		}

		return (rb != null && thrustPID != null && rotPID != null);
	}

	void Update()
	{
		//Debug.Log(thrustersString());
		playParticles();
	}

	void FixedUpdate()
	{
		if(!setup())
		{
			return;
		}

		if(pidOn)
		{
			thrustPID.ProcessVariable = transform.InverseTransformDirection(rb.velocity);
			thrust(thrustPID.pid());

			rotPID.ProcessVariable = transform.InverseTransformDirection(rb.angularVelocity);
			torque(rotPID.pid());

			//Debug.Log(thrustPID);
			//Debug.Log(rotPID);
		}
	}

	public void thrustInput(Vector3 input)
	{
		if(!setup())
		{
			return;
		}

		if(pidOn)
		{
			thrustPID.SetPoint = input * 1000;
		}

		else
		{
			thrust(input);
		}
	}

	public void thrust(Vector3 thrust)
	{
		if(!setup())
		{
			return;
		}

		if(thrust == Vector3.zero)
		{
			return;
		}

		thrust.Normalize();

		thrust.x *= thrustScale.x;
		thrust.y *= thrustScale.y;
		thrust.z *= thrustScale.z;

		rb.AddRelativeForce(thrust, ForceMode.Force);

		if(thrust.z > thrustEffectBuffer)
		{
			aftOn = true;
		}
		else if(thrust.z < -thrustEffectBuffer)
		{
			forOn = true;
		}

		if(thrust.x > thrustEffectBuffer)
		{
			forLeftOn = true;
			aftLeftOn = true;
		}
		else if(thrust.x < -thrustEffectBuffer)
		{
			forRightOn = true;
			aftRightOn = true;
		}

		if(thrust.y > thrustEffectBuffer)
		{
			forBotOn = true;
			aftBotOn = true;
		}
		else if(thrust.y < -thrustEffectBuffer)
		{
			forTopOn = true;
			aftTopOn = true;
		}
	}

	public void torqueInput(Vector3 input)
	{
		if(!setup())
		{
			return;
		}

		if(pidOn)
		{
			rotPID.SetPoint = input * 1000;
		}

		else
		{
			torque(input);
		}
	}

	public void torque(Vector3 torque)
	{
		// This fucks up the pid. Let's reset it to get a working, but unstarted one.
		// TODO: FIX THIS!
		thrustPID.reset();

		if(!setup())
		{
			return;
		}

		if(torque == Vector3.zero)
		{
			return;
		}

		torque.Normalize();

		torque.x *= torqueScale.x;
		torque.y *= torqueScale.y;
		torque.z *= torqueScale.z;

		rb.AddRelativeTorque(torque);

		// TODO: Add RCS thrusters for roll.
		if(torque.z > thrustEffectBuffer)
		{
		}
		else if(torque.z < -thrustEffectBuffer)
		{
		}

		if(torque.x > thrustEffectBuffer)
		{
			forTopOn = true;
			aftBotOn = true;
		}
		else if(torque.x < -thrustEffectBuffer)
		{
			forBotOn = true;
			aftTopOn = true;
		}

		if(torque.y > thrustEffectBuffer)
		{
			forLeftOn = true;
			aftRightOn = true;
		}
		else if(torque.y < -thrustEffectBuffer)
		{
			forRightOn = true;
			aftLeftOn = true;
		}
	}

	private void resetThrusterStats()
	{
		aftOn = false;
		forOn = false;
		aftLeftOn = false;
		aftRightOn = false;
		aftTopOn = false;
		aftBotOn = false;
		forLeftOn = false;
		forRightOn = false;
		forTopOn = false;
		forBotOn = false;
	}

	private void playParticles()
	{
		ParticleSystem.EmissionModule aftEm = aftPart.emission;
		ParticleSystem.EmissionModule forEm = forPart.emission;
		ParticleSystem.EmissionModule aftLeftEm = aftLeftPart.emission;
		ParticleSystem.EmissionModule aftRightEm = aftRightPart.emission;
		ParticleSystem.EmissionModule aftTopEm = aftTopPart.emission;
		ParticleSystem.EmissionModule aftBotEm = aftBotPart.emission;
		ParticleSystem.EmissionModule forLeftEm = forLeftPart.emission;
		ParticleSystem.EmissionModule forRightEm = forRightPart.emission;
		ParticleSystem.EmissionModule forTopEm = forTopPart.emission;
		ParticleSystem.EmissionModule forBotEm = forBotPart.emission;

		aftEm.enabled = aftOn;
		forEm.enabled = forOn;
		aftLeftEm.enabled = aftLeftOn;
		aftRightEm.enabled = aftRightOn;
		aftTopEm.enabled = aftTopOn;
		aftBotEm.enabled = aftBotOn;
		forLeftEm.enabled = forLeftOn;
		forRightEm.enabled = forRightOn;
		forTopEm.enabled = forTopOn;
		forBotEm.enabled = forBotOn;

		resetThrusterStats();
	}

	private string thrustersString()
	{
		return string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}",
		aftOn,
		forOn,
		aftLeftOn,
		aftRightOn,
		aftTopOn,
		aftBotOn,
		forLeftOn,
		forRightOn,
		forTopOn,
		forBotOn);
	}
}
