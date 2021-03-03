using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;

public class AdmobAds : MonoBehaviour
{
    public static AdmobAds Instance;

    //private string appID = "ca-app-pub-9354246231789271~2672711901";

    private BannerView bannerView;
    private string bannerID = "";

    private InterstitialAd interstitial;
    private string interstitialID = "";

    private RewardedAd rewardedAd;
    private string rewardedID = "";

    private void Awake()
    {
        Instance = this;
        PlayMarketMode();
        //TestMode();
    }

    private bool init = false;

    private void TestMode()
    {
        bannerID = "ca-app-pub-3940256099942544/6300978111";
        interstitialID = "ca-app-pub-3940256099942544/1033173712";
        rewardedID = "ca-app-pub-3940256099942544/5224354917";
    }

    private void PlayMarketMode()
    {
        bannerID = "ca-app-pub-9354246231789271/4463559234";
        interstitialID = "ca-app-pub-9354246231789271/7149432322";
        rewardedID = "ca-app-pub-9354246231789271/9757809014";
    }

    void Update()
    {
        if (!init)
        {
            if (CheckInternetConnection())
            {
                MobileAds.Initialize(initStatus =>
                {
                    init = true;
                    this.RequestInterstitial();
                    this.RequestRewarded();
                });
            }
        }
    }

    //baner
    public void ShowBanner()
    {
        HideBanner();
        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        this.bannerView.LoadAd(request);
        //this.bannerView.Show();
    }

    public void HideBanner()
    {
        if (this.bannerView != null)
        {
            this.bannerView.Destroy();
        }
    }

    //video
    private void RequestInterstitial()
    {
        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(interstitialID);

        this.interstitial.OnAdClosed += this.HandleOnAdClosed;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    public bool CheckInternetConnection()
    {
        return !(Application.internetReachability == NetworkReachability.NotReachable);
    }

    public void ShowVideo()
    {
        if (CheckInternetConnection())
        {
            if (this.interstitial.IsLoaded())
            {
                this.interstitial.Show();
                //this.RequestInterstitial();
            }
        }
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        this.RequestInterstitial();
    }

    //videoBonus
    private void RequestRewarded()
    {
        // Initialize an InterstitialAd.
        this.rewardedAd = new RewardedAd(rewardedID);

        this.rewardedAd.OnAdClosed += this.RewardedOnAdClosed;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.rewardedAd.LoadAd(request);
    }

    public void ShowVideoRewarded()
    {
        if (CheckInternetConnection())
        {
            if (this.rewardedAd.IsLoaded())
            {
                this.rewardedAd.Show();
                //this.RequestRewarded();
            }
        }
    }

    public void RewardedOnAdClosed(object sender, EventArgs args)
    {
        this.RequestRewarded();
    }
}


//private string appID = "ca-app-pub-9354246231789271~2672711901";
//test
//private string appID = "ca-app-pub-3940256099942544~3347511713";

//private BannerView bannerView;
//private string bannerID = "ca-app-pub-9354246231789271/4463559234";
//test
//private string bannerID = "ca-app-pub-3940256099942544/6300978111";

//private InterstitialAd interstitial;
//private string interstitialID = "ca-app-pub-9354246231789271/7149432322";
//test
//private string interstitialID = "ca-app-pub-3940256099942544/1033173712";

//private RewardedAd rewardedAd;
//private string rewardedID = "ca-app-pub-9354246231789271/9757809014";
//test
//private string rewardedID = "ca-app-pub-3940256099942544/5224354917";