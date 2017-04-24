using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WebCameraController : MonoBehaviour {

	public int Width = 1920;
	public int Height = 1080;
	public int FPS = 30;

	[SerializeField]
	private Renderer m_renderer;
	public WebCamTexture webcamTexture;
	public Color32[] color32;

	void Start()
	{
		WebCamDevice[] devices = WebCamTexture.devices;
		// display all cameras
		for (var i = 0; i < devices.Length; i++)
		{
			Debug.Log(devices[i].name);
		}

		Debug.Log(string.Format("height:{0}", Screen.height));
		Debug.Log(string.Format("width:{0}", Screen.width));

		//m_renderer.gameObject.transform.localScale = new Vector3((float)Screen.height * 0.005f, (float)Screen.width * 0.005f, 1.0f);

		Width = Screen.height;
		Height = Screen.height;

		webcamTexture = new WebCamTexture(devices[devices.Length-1].name, Width, Height, FPS);
		//GetComponent<Renderer>().material.mainTexture = webcamTexture;
		m_renderer.material.mainTexture = webcamTexture;
		webcamTexture.Play();
	}

	void OnSaveRequest()
	{
		if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
		{
			color32 = webcamTexture.GetPixels32();

			Texture2D texture = new Texture2D(webcamTexture.width, webcamTexture.height);
			GameObject.Find("Quad").GetComponent<Renderer>().material.mainTexture = texture;

			texture.SetPixels32(color32);
			texture.Apply();

			var bytes = texture.EncodeToPNG();
			File.WriteAllBytes(Application.dataPath + "/camera.png", bytes);
		}
	}

}
