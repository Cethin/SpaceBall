using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class TextureOffsetByPosLat : MonoBehaviour
{
	public float xScrollSpeed = 1;
	public float zScrollSpeed = 1;

	private Material mat;

	void Update ()
	{
		if(!setup())
		{
			return;
		}

		mat.SetTextureOffset("_MainTex", new Vector2(zScrollSpeed * transform.position.x, xScrollSpeed * transform.position.z));
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
