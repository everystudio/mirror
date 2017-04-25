using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsInterstential : MonoBehaviour {

	[SerializeField]
	string adUnitIdAndroid = "";
	[SerializeField]
	string adUnitIdIOS = "";

	public enum SHOW_TYPE
	{
		COUNT	= 0,
		RANDOM	
	};
	public SHOW_TYPE m_eShowType;

	public int m_iShowIntervalCount;
	public int m_iShowRequestCount;

	[Range(0, 100)]
	public int m_iShowRandomProb;

	InterstitialAd interstitial;
	private AdRequest request;

	private bool m_bStandby;
	// Use this for initialization
	void Start () {
		if (adUnitIdAndroid.Equals(""))
		{
			Debug.LogError("no set AdsInterstential.adUnitIdAndroid");
		}
		if (adUnitIdIOS.Equals(""))
		{
			Debug.LogError("no set AdsInterstential.adUnitIdIOS");
		}

		if (m_iShowIntervalCount < -1)
		{
			m_iShowIntervalCount = 0;
		}
		m_iShowRequestCount = 0;
		Load();
	}

	private void Load()
	{
		if (m_bStandby)
		{
			Debug.LogError("Ready");
			return;
		}

		string strUnitId = "";
#if UNITY_ANDROID
		strUnitId = adUnitIdAndroid;
#elif UNITY_IOS
		strUnitId = adUnitIdIOS;
#endif
		interstitial = new InterstitialAd(strUnitId);

		request = new AdRequest.Builder()
			.AddTestDevice("B58A62380C00BF9DC7BA75C756B5F550")
			.AddTestDevice("30ec665ef7c68238905003e951174579")
			.Build();

		interstitial.LoadAd(request);
		interstitial.OnAdLoaded += OnAdLoaded;
		interstitial.OnAdClosed += OnAdClosed;

	}

	public bool Show()
	{
		if (m_eShowType == SHOW_TYPE.COUNT)
		{
			m_iShowRequestCount -= 1;
			if (0 < m_iShowRequestCount)
			{
				return false;
			}
		}
		else if(m_eShowType == SHOW_TYPE.RANDOM)
		{
			int iResult = UtilRand.GetRand(100);
			if(m_iShowRandomProb < iResult)
			{
				return false;
			}
		}

		if (interstitial.IsLoaded())
		{
			interstitial.Show();
		}
		else
		{
			Debug.LogError(" no loaded ");
		}
		return m_bStandby;
	}

	private void OnAdLoaded(object sender, System.EventArgs e)
	{
		m_bStandby = true;
		/*
		InterstitialAd inter = (InterstitialAd)sender;
		inter.Show();
		*/
	}

	// インタースティシャル広告を閉じた時に走る
	void OnAdClosed(object sender, System.EventArgs e)
	{
		m_bStandby = false;
		interstitial.Destroy();
		interstitial = null;
		Load();
	}
}
