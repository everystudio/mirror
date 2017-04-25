using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WebCameraController : MonoBehaviour {

	public int Width = 1920;
	public int Height = 1080;
	public int FPS = 30;

	public enum STATUS
	{
		PLAYING		= 0,
		PAUSE		,
		SAVE,
		END,
		MAX,
	}
	public STATUS m_eStatus;
	public STATUS m_eStatusPre;


	[SerializeField]
	private AutoScaleQuad m_quad;

	private Renderer m_renderer;

	public WebCamDevice webcamDevice;
	public WebCamTexture webcamTexture;
	public Color32[] color32;

	public void SetStatus( STATUS _eStatus)
	{
		m_eStatus = _eStatus;
	}

	void Start()
	{
		m_eStatus = STATUS.PLAYING;
		m_eStatusPre = STATUS.MAX;

		m_renderer = m_quad.useRenderer;

		WebCamDevice[] devices = WebCamTexture.devices;
		// display all cameras
		for (var i = 0; i < devices.Length; i++)
		{
			Debug.Log(devices[i].name);
		}
		webcamDevice = devices[devices.Length - 1];		

		Debug.Log(string.Format("height:{0}", Screen.height));
		Debug.Log(string.Format("width:{0}", Screen.width));

		//m_renderer.gameObject.transform.localScale = new Vector3((float)Screen.height * 0.005f, (float)Screen.width * 0.005f, 1.0f);
		Width = Screen.width;
		Height = Screen.height;
#if UNITY_ANDROID
		webcamTexture = new WebCamTexture(webcamDevice.name, Height, Width, FPS);
#elif UNITY_IOS
		webcamTexture = new WebCamTexture(webcamDevice.name, Width,Height,  FPS);
#endif
		//GetComponent<Renderer>().material.mainTexture = webcamTexture;
		m_renderer.material.mainTexture = webcamTexture;
		webcamTexture.Play();

	}

	public void OnAction()
	{
		if (webcamTexture.isPlaying)
		{
			Debug.LogError("Camera.OnAction(Playing)");
			webcamTexture.Pause();
		}
		else
		{
			Debug.LogError("Camera.OnAction(NotPlaying)");
			webcamTexture.Stop();
			webcamTexture.Play();
		}
	}

	public void OnSave()
	{
		Debug.LogError("Camera.OnSave");
		color32 = webcamTexture.GetPixels32();

		Texture2D texture = new Texture2D(webcamTexture.width, webcamTexture.height);
		//GameObject.Find("Quad").GetComponent<Renderer>().material.mainTexture = texture;
		m_renderer.material.mainTexture = texture;

		texture.SetPixels32(color32);
		texture.Apply();

		var bytes = texture.EncodeToPNG();

		string strPersistent = Application.persistentDataPath + "/images/" +
			string.Format("{0}年{1:D2}月{2:D2}日{3:D2}時{4:D2}分{5:D2}秒.png" ,
			System.DateTime.Today.Year,
			System.DateTime.Today.Month,
			System.DateTime.Today.Day,
			System.DateTime.Now.Hour,
			System.DateTime.Now.Minute,
			System.DateTime.Now.Second
			);
		File.WriteAllBytes(strPersistent, bytes);

		m_renderer.material.mainTexture = webcamTexture;

	}

	public void OnSwitchSide()
	{
#if UNITY_ANDROID
		m_renderer.transform.localScale = new Vector3(
			m_renderer.transform.localScale.x,
			m_renderer.transform.localScale.y * -1,
			m_renderer.transform.localScale.z
			);
#elif UNITY_IOS
		m_renderer.transform.localScale = new Vector3(
			m_renderer.transform.localScale.x * -1,
			m_renderer.transform.localScale.y,
			m_renderer.transform.localScale.z
			);
#endif
	}



	void _Update()
	{
		bool bInit = false;
		if( m_eStatusPre != m_eStatus)
		{
			Debug.Log(m_eStatus);
			m_eStatusPre  = m_eStatus;
			bInit = true;
		}

		switch( m_eStatus )
		{
			case STATUS.PLAYING:
				if (bInit)
				{
					webcamTexture.Stop();
					webcamTexture.Play();
				}

				if ( Input.GetMouseButtonDown(0))
				{
					if (webcamTexture.isPlaying)
					{
						m_eStatus = STATUS.PAUSE;
					}
				}
				break;

			case STATUS.PAUSE:
				if( bInit)
				{
					webcamTexture.Pause();
				}
				if (Input.GetMouseButtonDown(0))
				{
					m_eStatus = STATUS.PLAYING;
				}
				break;

			case STATUS.SAVE:
				if( bInit)
				{
					color32 = webcamTexture.GetPixels32();

					Texture2D texture = new Texture2D(webcamTexture.width, webcamTexture.height);
					//GameObject.Find("Quad").GetComponent<Renderer>().material.mainTexture = texture;
					m_renderer.material.mainTexture = texture;

					texture.SetPixels32(color32);
					texture.Apply();

					var bytes = texture.EncodeToPNG();
					File.WriteAllBytes(Application.dataPath + "/camera.png", bytes);

				}
				break;

			case STATUS.MAX:
			default:
				break;
		}
	}

}
