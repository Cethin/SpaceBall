using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShipData
{
	public static Vector3 thrustScale = Vector3.one * 10f;
	public static Vector3 torqueScale = Vector3.one * 3f;
	public const float thrustEffectBuffer = .01f;
	public static float[] thrustPIDVars = 	new float[3]{1f, .1f, .0001f};
	public static float[] rotPIDVars = 		new float[3]{1f, .00025f, .01f};
}
