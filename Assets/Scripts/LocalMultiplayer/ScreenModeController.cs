using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScreenModeController
{
	private static List<Camera> cams;

	public static void addCam(Camera cam)
	{
		setup();

		Rect viewRect = cam.rect;

		if(cams.Count == 0)	// First Camera
		{
			// Fill entire screen
			viewRect.x = 0f;
			viewRect.y = 0f;
			viewRect.width = 1f;
			viewRect.height = 1f;

			cam.rect = viewRect;
		}
		else if(cams.Count == 1) // Horizontal Slice
		{
			viewRect = cams[0].rect;
			viewRect.x = 0f;
			viewRect.y = .5f;
			viewRect.width = 1f;
			viewRect.height = .5f;
			cams[0].rect = viewRect;

			viewRect = cam.rect;
			viewRect.x = 0f;
			viewRect.y = 0f;
			viewRect.width = 1f;
			viewRect.height = .5f;
			cam.rect = viewRect;
		}
		else if(cams.Count == 2)
		{
			viewRect = cams[0].rect;
			viewRect.x = 0f;
			viewRect.y = .5f;
			viewRect.width = .5f;
			viewRect.height = .5f;
			cams[0].rect = viewRect;

			
			viewRect = cams[1].rect;
			viewRect.x = .5f;
			viewRect.y = .5f;
			viewRect.width = .5f;
			viewRect.height = .5f;
			cams[1].rect = viewRect;

			
			viewRect = cam.rect;
			viewRect.x = 0f;
			viewRect.y = 0f;
			viewRect.width = 1f;
			viewRect.height = .5f;
			cam.rect = viewRect;
		}
		else if(cams.Count == 3)
		{
			viewRect = cams[0].rect;
			viewRect.x = 0f;
			viewRect.y = .5f;
			viewRect.width = .5f;
			viewRect.height = .5f;
			cams[0].rect = viewRect;

			
			viewRect = cams[1].rect;
			viewRect.x = .5f;
			viewRect.y = .5f;
			viewRect.width = .5f;
			viewRect.height = .5f;
			cams[1].rect = viewRect;

			
			viewRect = cams[2].rect;
			viewRect.x = 0f;
			viewRect.y = 0f;
			viewRect.width = .5f;
			viewRect.height = .5f;
			cams[2].rect = viewRect;

			
			viewRect = cam.rect;
			viewRect.x = .5f;
			viewRect.y = 0f;
			viewRect.width = .5f;
			viewRect.height = .5f;
			cam.rect = viewRect;
		}
		else
		{
			Debug.LogError("Unsupported Number Of Local Players!");
		}

		cams.Add(cam);
	}

	private static void setup()
	{
		if(cams == null)
		{
			cams = new List<Camera>();
		}
	}
}
