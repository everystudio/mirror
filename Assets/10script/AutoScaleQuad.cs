using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScaleQuad : MonoBehaviour {
	public Camera targetCamera;

	public Renderer m_rAndroid;
	public Renderer m_rIos;

	public Renderer useRenderer
	{
		get
		{
#if UNITY_ANDROID
			return m_rAndroid;
#elif UNITY_IOS
			return m_rIos;
#endif
		}
	}

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
#if UNITY_ANDROID
		m_rAndroid.gameObject.transform.localScale = new Vector3(height, width,  1.0f);
		m_rIos.gameObject.transform.localScale = Vector3.zero;
#elif UNITY_IOS
		m_rAndroid.gameObject.transform.localScale = Vector3.zero;
		m_rIos.gameObject.transform.localScale = new Vector3(height, width, 1.0f);
#endif
	}
}

