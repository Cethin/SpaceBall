using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(Ship))]
public class Controller : NetworkBehaviour
{
	private static int numPlayers = 0;
	public  const int maxLocalPlayers = 2;
	public int playerID = 1;

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

	public ControlMapping controlMap = new ControlMappingXBox();

	private Ship ship;
	private CamFollowBall camFollowBall;

	public void newPlayer()
	{
		playerID = ++numPlayers;

		// Load controls from config
	}

	void Start()
	{
		newPlayer();
		
		string[] joyNames = Input.GetJoystickNames();
		foreach(string s in joyNames)
		{
			Debug.Log(s);
		}
	}
	
	void FixedUpdate ()
	{
		if(!hasAuthority)
		{
			return;
		}

		if(!setup())
		{
			return;
		}

		ship.thrustInput(latMov());
		ship.torqueInput(rotMov());
	}

	void Update()
	{
		if(!hasAuthority)
		{
			return;
		}
		
		if(Pid())
		{
			ship.pidToggle();
		}

		if(CamMode())
		{
			camFollowBall.toggle();
		}
	}

	public static int NumPlayers
	{
		get{ return numPlayers; }
		private set{ numPlayers = value; }
	}

	public bool Forward() 	{ return inputIsDown(forward) 	|| ((!controlMap.invertThrust && axesPos(controlMap.thrustAxis)) 	|| (controlMap.invertThrust) && axesNeg(controlMap.thrustAxis)); }
	public bool Back() 		{ return inputIsDown(back)		|| ((controlMap.invertThrust && axesPos(controlMap.thrustAxis)) 	|| (!controlMap.invertThrust) && axesNeg(controlMap.thrustAxis)); }
	public bool Left() 		{ return inputIsDown(left) 		|| ((!controlMap.invertLateral && axesPos(controlMap.lateralAxis)) 	|| (controlMap.invertLateral) && axesNeg(controlMap.lateralAxis)); }
	public bool Right() 	{ return inputIsDown(right) 	|| ((controlMap.invertLateral && axesPos(controlMap.lateralAxis)) 	|| (!controlMap.invertLateral) && axesNeg(controlMap.lateralAxis)); }
	public bool Up() 		{ return inputIsDown(up) 		|| ((!controlMap.invertVertical && axesPos(controlMap.verticalAxis))|| (controlMap.invertVertical) && axesNeg(controlMap.verticalAxis)); }
	public bool Down() 		{ return inputIsDown(down) 		|| ((controlMap.invertVertical && axesPos(controlMap.verticalAxis)) || (!controlMap.invertVertical) && axesNeg(controlMap.verticalAxis)); }
	public bool PitchUp() 	{ return inputIsDown(pitchUp) 	|| ((!controlMap.invertPitch && axesPos(controlMap.pitchAxis)) 		|| (controlMap.invertPitch) && axesNeg(controlMap.pitchAxis)); }
	public bool PitchDown() { return inputIsDown(pitchDown) || ((controlMap.invertPitch && axesPos(controlMap.pitchAxis)) 		|| (!controlMap.invertPitch) && axesNeg(controlMap.pitchAxis)); }
	public bool YawLeft() 	{ return inputIsDown(yawLeft) 	|| ((!controlMap.invertYaw && axesPos(controlMap.yawAxis)) 			|| (controlMap.invertYaw) && axesNeg(controlMap.yawAxis)); }
	public bool YawRight() 	{ return inputIsDown(yawRight) 	|| ((controlMap.invertYaw && axesPos(controlMap.yawAxis)) 			|| (!controlMap.invertYaw) && axesNeg(controlMap.yawAxis)); }
	public bool RollLeft() 	{ return inputIsDown(rollLeft) 	|| ((!controlMap.invertRoll && axesPos(controlMap.rollAxis)) 		|| (controlMap.invertRoll) && axesNeg(controlMap.rollAxis)); }
	public bool RollRight() { return inputIsDown(rollRight) || ((controlMap.invertRoll && axesPos(controlMap.rollAxis)) 		|| (!controlMap.invertRoll) && axesNeg(controlMap.rollAxis)); }

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
			float val = Input.GetAxis(a + "-" + playerID);
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

		return ((controlMap.invertThrust ? -1 : 1) * axesVal(controlMap.thrustAxis));
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

		return ((controlMap.invertLateral ? -1 : 1) * axesVal(controlMap.lateralAxis));
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

		return ((controlMap.invertVertical ? -1 : 1) * axesVal(controlMap.verticalAxis));
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

		return ((controlMap.invertPitch ? -1 : 1) * axesVal(controlMap.pitchAxis));
	}

	public float yaw()
	{
		if(YawLeft())
		{
			return -1;
		}
		else if(YawRight())
		{
			return 1;
		}

		return ((controlMap.invertYaw ? -1 : 1) * axesVal(controlMap.yawAxis));
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

		return ((controlMap.invertRoll ? -1 : 1) * axesVal(controlMap.rollAxis));
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
