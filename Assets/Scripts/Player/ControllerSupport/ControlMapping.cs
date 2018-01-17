using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControlMapping
{
	// Default mapping (From Logitech Extreme 3D Pro)
	public abstract string controllerName 	{ get; protected set; }
	public abstract string[] thrustAxis 	{ get; protected set; }
	public virtual bool invertThrust 		{ get {return false; } protected set {} }
	public abstract string[] lateralAxis 	{ get; protected set; }
	public virtual bool invertLateral		{ get {return false; } protected set {} }
	public abstract string[] verticalAxis 	{ get; protected set; }
	public virtual bool invertVertical		{ get {return false; } protected set {} }
	public abstract string[] pitchAxis 		{ get; protected set; }
	public virtual bool invertPitch			{ get {return false; } protected set {} }
	public abstract string[] yawAxis 		{ get; protected set; }
	public virtual bool invertYaw			{ get {return false; } protected set {} }
	public abstract string[] rollAxis 		{ get; protected set; }
	public virtual bool invertRoll			{ get {return false; } protected set {} }
}
