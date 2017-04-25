using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UIAlbum : CPanel {


	[SerializeField]
	private GameObject m_goRoot;

	[SerializeField]
	private UIShowImage m_uiShowImage;

	private List<BannerImage> m_bannerImageList = new List<BannerImage>();

	protected override void panelStart()
	{
		base.panelStart();

		DeleteObjects<BannerImage>(m_goRoot);
		m_bannerImageList.Clear();

		string image_path = Path.Combine(Application.persistentDataPath, "images");

		if (Directory.Exists(image_path))
		{
			string[] filePaths = Directory.GetFiles(image_path);
			foreach (string filePath in filePaths)
			{
				Debug.LogError(filePath);
				BannerImage script = PrefabManager.Instance.MakeScript<BannerImage>("BannerImage",m_goRoot);

				script.Initialize(filePath, m_uiShowImage);
				m_bannerImageList.Add(script);
			}
		}
		else
		{
			Debug.LogError("ディレクトリがありません");
		}

	}

}
