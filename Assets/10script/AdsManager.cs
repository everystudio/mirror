using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using GoogleMobileAds.Api;

public class AdsManager : Singleton<AdsManager>
{
	private AdsBanner m_adsBannerGameBottom;

	void loadedScene(Scene scenename, LoadSceneMode SceneMode)
	{
		Debug.LogError("今のSceneの名前 = " + scenename.name);
		//setup(scenename.name);
	}

	public void OnShowPage( string _strName)
	{
		Debug.Log(string.Format("pagename:{0}", _strName));
		Show(_strName);
	}
	public override void Initialize()
	{
		m_adsBannerGameBottom = null;
		//Debug.LogError(SceneManager.GetActiveScene().name);

		Show(UIAssistant.main.GetCurrentPage());
		UIAssistant.onShowPage += OnShowPage;
		SceneManager.sceneLoaded += loadedScene;
		Debug.LogError("AdsManager.Initialize");

	}

	public void OnSkitStarted()
	{
		ShowBanner(false);
	}

	public void OnSkitFinished()
	{
		ShowBanner(true);
	}

	private void cleanup()
	{
		if (m_adsBannerGameBottom != null)
		{
			Destroy(m_adsBannerGameBottom);
			m_adsBannerGameBottom = null;
		}
	}

	public void Show( string _strName )
	{
		if (m_adsBannerGameBottom == null)
		{
			m_adsBannerGameBottom = gameObject.AddComponent<AdsBanner>();
		}
		m_adsBannerGameBottom.Show();
	}

	private void setup(string _strSceneName)
	{
		cleanup();
		switch (_strSceneName)
		{
			case "Command":
			default:
				if (m_adsBannerGameBottom == null)
				{
					m_adsBannerGameBottom = gameObject.AddComponent<AdsBanner>();
				}
				break;
		}
	}

	public void ShowBanner(bool _bFlag)
	{
		if( m_adsBannerGameBottom == null)
		{
			return;
		}
		if (_bFlag)
		{
			m_adsBannerGameBottom.Show();
		}
		else
		{
			m_adsBannerGameBottom.Hide();
		}
	}
}

