using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMappingStick : ControlMapping
{
	// Mapping for Logitech Extreme 3D Pro
	public override string controllerName 	{ get { return "Logitech Extreme 3D Pro";} protected set {} }
	public override string[] thrustAxis		{ get { return new string[]{"4"};} protected set {} }
	public override string[] lateralAxis 	{ get { return new string[]{"5"};} protected set {} }
	public override string[] verticalAxis 	{ get { return new string[]{"6"};} protected set {} }
	public override string[] pitchAxis 		{ get { return new string[]{"y"};} protected set {} }
	public override string[] yawAxis 		{ get { return new string[]{"3"};} protected set {} }
	public override string[] rollAxis 		{ get { return new string[]{"x"};} protected set {} }
}
