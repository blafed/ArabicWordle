using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class AdsManager : Manager<AdsManager>, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
	//Ads

	internal bool isRewardedLoaded
	{
		get => _isRewardedLoaded;
		set
		{
			_isRewardedLoaded = value;
			FlowOut.rewardedAdLoaded = value;
		}
	}
	private bool _isRewardedLoaded;

	private IUnityAdsInitializationListener unityAdsInitializationListenerImplementation;
	private string gameId => AdsConfig.instance.gameId;
	private string bannerId => AdsConfig.instance.bannerId;
	private string interstitialId => AdsConfig.instance.interstitialId;
	private string rewardId => AdsConfig.instance.rewardedId;
	

	// Start is called before the first frame update

	private void Update()
	{
		if(FlowIn.showRewardedAd)
			if(isRewardedLoaded)
				ShowRewarded();
	}

	protected override void Init()
	{
		Advertisement.Initialize(gameId, AdsConfig.instance.isTestMode, this);
	}

	void LoadBanner()
	{
		BannerLoadOptions bannerLoadOptions = new BannerLoadOptions()
		{
			loadCallback = OnBannerLoaded,
			errorCallback = OnBannerError
		};
		Advertisement.Banner.SetPosition(UnityEngine.Advertisements.BannerPosition.BOTTOM_CENTER);
		if (PlayerPrefs.GetInt("Ads", 1) == 1) Advertisement.Banner.Load(bannerId, bannerLoadOptions);
	}

	void ShowBanner()
	{
		BannerOptions bannerOptions = new BannerOptions()
		{
			clickCallback = OnBannerClick,
			showCallback = OnBannerShow,
			hideCallback = OnBannerHide
		};
		if (PlayerPrefs.GetInt("Ads", 1) == 1) Advertisement.Banner.Show(bannerId, bannerOptions);
	}

	public void HideBanner()
	{
		Advertisement.Banner.Hide();
	}

	void LoadInterstitial()
	{
		print("Loading Interstitial");
		Advertisement.Load(interstitialId, this);
	}

	public void ShowInterstitial()
	{
		print("Showing Interstitial");
		if (PlayerPrefs.GetInt("Ads", 1) == 1) Advertisement.Show(interstitialId, this);
	}

	void LoadRewarded()
	{
		print("Loading Rewarded");
		Advertisement.Load(rewardId, this);
	}

	public void ShowRewarded()
	{
		print("Showing Rewarded");
		if (PlayerPrefs.GetInt("Ads", 1) == 1) Advertisement.Show(rewardId, this);
	}

	public void OnInitializationComplete()
	{
		print("Ads initialized");
		LoadBanner();
		LoadInterstitial();
		LoadRewarded();
	}

	public void OnInitializationFailed(UnityAdsInitializationError error, string message)
	{
		print("Ads initialization failed: " + message);
	}

	void OnBannerLoaded()
	{
		print("Banner loaded");
		ShowBanner();
	}

	void OnBannerError(string message)
	{
		print("Banner error: " + message);
	}

	void OnBannerClick()
	{
		print("Banner clicked");
	}

	void OnBannerShow()
	{
		print("Banner shown");
	}

	void OnBannerHide()
	{
		print("Banner hidden");
	}

	public void OnUnityAdsAdLoaded(string placementId)
	{
		if (placementId.Equals(rewardId))
		{
			print("Rewarded loaded");
			isRewardedLoaded = true;
			//owRewarded();
		}
		else if (placementId.Equals(interstitialId))
		{
			print("Interstitial loaded");
			//ShowInterstitial();
		}
	}

	public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
	{
		if (placementId.Equals(rewardId))
		{
			isRewardedLoaded = false;
		}
		print("interstitial failed loading");
	}

	public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
	{
		print("interstitial show failed");
	}

	public void OnUnityAdsShowStart(string placementId)
	{
		//throw new System.NotImplementedException();
	}

	public void OnUnityAdsShowClick(string placementId)
	{
		//throw new System.NotImplementedException();
	}

	public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
	{
		if (placementId.Equals(rewardId) && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
		{
			print("user should claim reward");
			ProgressManager.instance.ApplyAdReward();
			isRewardedLoaded = false;
			LoadRewarded();
		}
		else if (placementId.Equals(interstitialId))
		{
			LoadInterstitial();
		}
	}


}
