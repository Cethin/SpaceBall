using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class TextureOffsetByPosVert : MonoBehaviour
{
	public float xScrollSpeed = 1;
	public float yScrollSpeed = 1;

	private Material mat;

	void Update ()
	{
		if(!setup())
		{
			return;
		}

		mat.SetTextureOffset("_MainTex", new Vector2(xScrollSpeed * transform.position.x, yScrollSpeed * transform.position.y));
	}

	private bool setup()
	{
		if(mat == null)
		{
			mat = GetComponent<MeshRenderer>().material;
		}

		return (mat != null);
	}
}
