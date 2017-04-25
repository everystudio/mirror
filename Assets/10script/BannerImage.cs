using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BannerImage : MonoBehaviour {

	[SerializeField]
	private Image m_imgIcon;

	[SerializeField]
	private Text m_txtTitle;

	[SerializeField]
	private Button m_btn;

	private string m_strFilename;

	public void Initialize( string _strFilename , UIShowImage _uiShowImage)
	{
		m_strFilename = _strFilename;

		m_imgIcon.sprite = SpriteManager.Instance.LoadSprite(_strFilename);

		string[] string_arr = _strFilename.Split('\\');
		string[] string_arr2 = string_arr[string_arr.Length - 1].Split('/');

		string[] filename = string_arr2[string_arr2.Length-1].Replace(".png", "").Replace(".jpg", "").Split('日');


		
		if (2 <= filename.Length)
		{
			m_txtTitle.text = filename[0] + "日" + '\n' + filename[1];
		}
		else
		{
			m_txtTitle.text = filename[0];
		}

		m_btn.onClick.AddListener(() =>
		{
			Debug.LogError(m_strFilename);
			_uiShowImage.SetFilename(m_strFilename);
			UIAssistant.main.ShowPage("show_image");
		});
	}

}
