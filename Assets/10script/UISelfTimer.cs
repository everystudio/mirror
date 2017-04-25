using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelfTimer : CPanel {

	[SerializeField]
	private Text m_txtCountDown;

	[SerializeField]
	private WebCameraController m_wcc;

	public float m_fTimer;
	protected override void panelStart()
	{
		base.panelStart();
		m_fTimer = 3.0f;
	}

	void Update()
	{
		m_fTimer -= Time.deltaTime;
		if (m_fTimer < 0.0f)
		{
			m_fTimer = 0.0f;
			UIAssistant.main.ShowPage("mirror");
			m_wcc.OnSave();
		}
		int iDispTime = 1+(int)m_fTimer;
		if( 3 < iDispTime)
		{
			iDispTime = 3;
		}

		m_txtCountDown.text = string.Format("{0}", iDispTime);

	}



}
