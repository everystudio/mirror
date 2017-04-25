using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShowImage : CPanel {

	public string m_strFilename;

	[SerializeField]
	private Image m_img;

	public void SetFilename(string _strFilename)
	{
		m_strFilename = _strFilename;
	}

	protected override void panelStart()
	{
		base.panelStart();
		m_img.sprite = SpriteManager.Instance.LoadSprite(m_strFilename);
	}




}
