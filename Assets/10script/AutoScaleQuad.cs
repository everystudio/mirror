using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScaleQuad : MonoBehaviour {
	public Camera targetCamera;

	void Start()
	{
		UpdateScale();
	}

	[ContextMenu("execute")]
	void UpdateScale()
	{
		if (targetCamera == null)
		{
			targetCamera = Camera.main;
		}

		float height = targetCamera.orthographicSize * 2;
		float width = height * targetCamera.aspect;

		transform.localScale = new Vector3(height, width,  1.0f);
	}
}

