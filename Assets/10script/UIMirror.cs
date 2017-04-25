using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UIMirror : CPanel {

	[SerializeField]
	private WebCameraController m_webCameraController;

	[SerializeField]
	private Button m_btnScreen;

	protected override void awake()
	{
		base.awake();
		string image_path = Path.Combine(Application.persistentDataPath, "images");

		if (!Directory.Exists(image_path))
		{
			EditDirectory.MakeDirectory("images");

		}

	}

	protected override void panelStart()
	{
		base.panelStart();

		m_webCameraController.SetStatus(WebCameraController.STATUS.PLAYING);
	}

	public void OnCameraAction()
	{
		m_webCameraController.OnAction();
	}

	public void OnCameraSave()
	{
		m_webCameraController.OnSave();
	}

	public void OnSelfTimer()
	{

	}







}
