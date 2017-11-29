using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Ship))]
public class Controller : MonoBehaviour
{
	public string playerID = "1";

	public KeyCode[] forward = 		new KeyCode[]{KeyCode.LeftShift};
	public KeyCode[] back = 		new KeyCode[]{KeyCode.LeftControl};
	public KeyCode[] left = 		new KeyCode[]{KeyCode.J};
	public KeyCode[] right = 		new KeyCode[]{KeyCode.K};
	public KeyCode[] up = 			new KeyCode[]{KeyCode.Space};
	public KeyCode[] down = 		new KeyCode[]{KeyCode.LeftAlt};
	public KeyCode[] pitchUp = 		new KeyCode[]{KeyCode.S};
	public KeyCode[] pitchDown =	new KeyCode[]{KeyCode.W};
	public KeyCode[] yawLeft = 		new KeyCode[]{KeyCode.A};
	public KeyCode[] yawRight = 	new KeyCode[]{KeyCode.D};
	public KeyCode[] rollLeft = 	new KeyCode[]{KeyCode.Q};
	public KeyCode[] rollRight =	new KeyCode[]{KeyCode.E};
	public KeyCode[] pid = 			new KeyCode[]{KeyCode.R};
	public KeyCode[] camMode = 		new KeyCode[]{KeyCode.F};

	public string[] thrustAxis = 	new string[]{"thrust"};
	public string[] lateralAxis = 	new string[]{"lateral"};
	public string[] verticalAxis = 	new string[]{"vertical"};
	public string[] pitchAxis = 	new string[]{"pitch"};
	public string[] yawAxis = 		new string[]{"yaw"};
	public string[] rollAxis = 		new string[]{"roll"};

	private Ship ship;
	private CamFollowBall camFollowBall;
	
	void FixedUpdate ()
	{
		if(!setup())
		{
			return;
		}

		ship.thrustInput(latMov());
		ship.torqueInput(rotMov());
	}

	void Update()
	{
		if(Pid())
		{
			ship.pidToggle();
		}

		if(CamMode())
		{
			camFollowBall.toggle();
		}
	}

	public bool Forward() 	{ return inputIsDown(forward) 	|| axesPos(thrustAxis); }
	public bool Back() 		{ return inputIsDown(back)		|| axesNeg(thrustAxis); }
	public bool Left() 		{ return inputIsDown(left) 		|| axesPos(lateralAxis); }
	public bool Right() 	{ return inputIsDown(right) 	|| axesNeg(lateralAxis); }
	public bool Up() 		{ return inputIsDown(up) 		|| axesPos(verticalAxis); }
	public bool Down() 		{ return inputIsDown(down) 		|| axesNeg(verticalAxis); }
	public bool PitchUp() 	{ return inputIsDown(pitchUp) 	|| axesPos(pitchAxis); }
	public bool PitchDown() { return inputIsDown(pitchDown) || axesNeg(pitchAxis); }
	public bool YawLeft() 	{ return inputIsDown(yawLeft) 	|| axesPos(yawAxis); }
	public bool YawRight() 	{ return inputIsDown(yawRight) 	|| axesNeg(yawAxis); }
	public bool RollLeft() 	{ return inputIsDown(rollLeft) 	|| axesPos(rollAxis); }
	public bool RollRight() { return inputIsDown(rollRight) || axesNeg(rollAxis); }

	public bool Pid() { return inputDown(pid); }
	public bool CamMode() { return inputDown(camMode); }

	private bool inputIsDown(KeyCode[] keys)
	{
		foreach(KeyCode k in keys)
		{
			if(Input.GetKey(k))
			{
				return true;
			}
		}

		return false;
	}

	private bool inputDown(KeyCode[] keys)
	{
		foreach(KeyCode k in keys)
		{
			if(Input.GetKeyDown(k))
			{
				return true;
			}
		}

		return false;
	}

	private bool inputUp(KeyCode[] keys)
	{
		foreach(KeyCode k in keys)
		{
			if(Input.GetKeyUp(k))
			{
				return true;
			}
		}

		return false;
	}

	private bool axesNotZero(string[] axes)
	{
		return axesVal(axes) != 0;
	}

	private bool axesPos(string[] axes)
	{
		return axesVal(axes) > 0;
	}

	private bool axesNeg(string[] axes)
	{
		return axesVal(axes) < 0;
	}

	private float axesVal(string[] axes)
	{
		foreach(string a in axes)
		{
			float val = Input.GetAxis(a + playerID);
			if(val != 0)
			{
				return val;
			}
		}

		return 0;
	}


	public float thrust()
	{
		if(Forward())
		{
			return 1;
		}
		else if(Back())
		{
			return -1;
		}

		return axesVal(thrustAxis);
	}

	public float lateral()
	{
		if(Left())
		{
			return 1;
		}
		else if(Right())
		{
			return -1;
		}

		return axesVal(lateralAxis);
	}

	public float vertical()
	{
		if(Up())
		{
			return 1;
		}
		else if(Down())
		{
			return -1;
		}

		return axesVal(verticalAxis);
	}

	public float pitch()
	{
		if(PitchUp())
		{
			return 1;
		}
		else if(PitchDown())
		{
			return -1;
		}

		return axesVal(pitchAxis);
	}

	public float yaw()
	{
		if(YawLeft())
		{
			return 1;
		}
		else if(YawRight())
		{
			return -1;
		}

		return axesVal(yawAxis);
	}

	public float roll()
	{
		if(RollLeft())
		{
			return 1;
		}
		else if(RollRight())
		{
			return -1;
		}

		return axesVal(rollAxis);
	}

	private Vector3 latMov()
	{
		Vector3 inputVector = new Vector3();

		inputVector += Vector3.forward * thrust();
		inputVector += Vector3.left * lateral();
		inputVector += Vector3.up * vertical();

		return inputVector;
	}

	private Vector3 rotMov()
	{
		Vector3 inputVector = new Vector3();
		
		inputVector += Vector3.forward * roll();
		inputVector += Vector3.left * pitch();
		inputVector += Vector3.up * yaw();

		return inputVector;
	}

	private bool setup()
	{
		if(ship == null)
		{
			ship = GetComponent<Ship>();
		}

		if(camFollowBall == null)
		{
			camFollowBall = GetComponentInChildren<CamFollowBall>();
		}

		return (ship != null && camFollowBall);
	}
}
