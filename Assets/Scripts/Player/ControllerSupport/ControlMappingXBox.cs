using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMappingXBox : ControlMapping
{
	// Mapping for XBox Controller
	public override string controllerName 	{ get { return "XBox";} protected set {} }
	public override string[] thrustAxis		{ get { return new string[]{"3"};} protected set {} }
	public override bool invertThrust 		{ get {return true; } protected set {} }
	public override string[] lateralAxis 	{ get { return new string[]{"4"};} protected set {} }
	public override bool invertLateral 		{ get {return false; } protected set {} }
	public override string[] verticalAxis 	{ get { return new string[]{"5"};} protected set {} }
	public override bool invertVertical 	{ get {return false; } protected set {} }
	public override string[] pitchAxis 		{ get { return new string[]{"y"};} protected set {} }
	public override bool invertPitch 		{ get {return false; } protected set {} }
	public override string[] yawAxis 		{ get { return new string[]{"x"};} protected set {} }
	public override bool invertYaw 			{ get {return true; } protected set {} }
	public override string[] rollAxis 		{ get { return new string[]{"6"};} protected set {} }
	public override bool invertRoll 		{ get {return true; } protected set {} }
}
