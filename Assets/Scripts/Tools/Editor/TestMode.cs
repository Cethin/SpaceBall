using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestMode : MonoBehaviour
{
	public static bool on;
	public static float timeScale = 3f;
	public bool onStartVal = false;

	void Start()
	{
		On = onStartVal;
	}

	public static bool On
	{
		get{ return on; }
		set
		{
			on = value;
			if(On)
			{
				Time.timeScale = timeScale;
			}
			else
			{
				Time.timeScale = 1;
			}
		}
	}

	public static bool toggleTestMode()
	{
		On = !On;
		return On;
	}
}

[CustomEditor(typeof(TestMode))]
public class TestModeEditor : Editor
{
	public override void OnInspectorGUI()
	{
		TestMode tmTar = (TestMode)target;

		base.OnInspectorGUI();

		if(GUILayout.Button("Toggle"))
		{
			TestMode.toggleTestMode();
		}
	}
}
