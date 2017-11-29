using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AIData
{


	public const float positionBuffer = .1f;
	public const float rayCastBuffer = .1f;
	public static float[] controlPIDVars = new float[3]{1, 0, .5f};//{.8f, .007f, .5f};
}
