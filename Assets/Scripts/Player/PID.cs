using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PID : MonoBehaviour
{
	private Vector3 processVariable = Vector3.zero;
	private Vector3 setPoint = Vector3.zero;
	public float kp = 1;
	public float ki = 1;
	public float kd = 1;
	private Vector3 integralVal = Vector3.zero;
	private Vector3 derivativeVal = Vector3.zero;


	public Vector3 SetPoint
	{
		get { return setPoint; }
		set { setPoint = value; }
	}
	public Vector3 SP
	{
		get { return SetPoint; }
		set
		{
			SetPoint = value;
			reset();
		}
	}

	public Vector3 ProcessVariable
	{
		get { return processVariable; }
		set
		{
			D = (value - ProcessVariable) / Time.fixedDeltaTime;
			processVariable = value;
			I += (E * Time.fixedDeltaTime);
		}
	}
	public Vector3 PV
	{
		get { return ProcessVariable; }
		set { ProcessVariable = value;}
	}

	private Vector3 Error
	{
		get { return SP - PV; }
	}
	private Vector3 E
	{
		get { return Error; }
	}

	public float Kp
	{
		get{ return kp; }
		set{ kp = Mathf.Abs(value); }
	}

	public float Ki
	{
		get{ return ki; }
		set{ ki = Mathf.Abs(value); }
	}

	public float Kd
	{
		get{ return kd; }
		set{ kd = Mathf.Abs(value); }
	}

	private Vector3 IntegralVal
	{
		get{ return integralVal; }
		set{ integralVal = value; }
	}
	private Vector3 I
	{
		get{ return IntegralVal; }
		set{ IntegralVal = value; }
	}

	private Vector3 DerivativeVal
	{
		get { return derivativeVal; }
		set{ derivativeVal = value; }
	}
	private Vector3 D
	{
		get { return DerivativeVal; }
		set{ DerivativeVal = value; }
	}




	public void reset()
	{
		PV = SP;
		I = Vector3.zero;
		D = Vector3.zero;
	}

	public override string ToString()
	{
		return string.Format("Kp: {0}, Ki: {1}, Kd: {2}, SP: {3}, PV: {4}, E: {5}, I: {6}, D: {7}, PID: {8}", Kp, Ki, Kd, SP, PV, E, I, D, pid());
	}

	// --- CALCULATORS ---
	private Vector3 proportional()
	{
		return Error * Kp;
	}

	public Vector3 p()
	{
		return proportional();
	}

	public Vector3 integral()
	{
		return I * Ki;
	}

	public Vector3 i()
	{
		return integral();
	}

	public Vector3 derivative()
	{
		return D * Kd;
	}

	public Vector3 d()
	{
		return derivative();
	}

	public Vector3 pi()
	{
		return p() + i();
	}

	public Vector3 pd()
	{
		return p() + d();
	}
	
	public Vector3 pid()
	{
		return p() + i() + d();
	}
}
