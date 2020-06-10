using GoogleMobileAds.Api;
using System;
using UnityEngine;
using McFairy.Base;
using McFairy.Logger;

namespace McFairy.Adpater.Admob
{
    public class AdmobInterstitial : InterstitialBase
    {
        bool _isAdLoaded = false;
        private InterstitialAd interstitial;

        public override void Initialize(string id = "", string appId = "")
        {
            MobileAds.Initialize(initStatus => { });
            this.interstitial = new InterstitialAd(id);

            this.interstitial.OnAdLoaded += HandleOnAdLoaded;
            this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            this.interstitial.OnAdOpening += HandleOnAdOpened;
            this.interstitial.OnAdClosed += HandleOnAdClosed;
            this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        }

        private void HandleOnAdLeavingApplication(object sender, EventArgs e)
        {
            Logs.ShowLog("User leaving application", LogType.Log);
        }

        private void HandleOnAdClosed(object sender, EventArgs e)
        {
            interstitial.Destroy();
            LoadAd();
            Logs.ShowLog("Admob Interstitial Ad Closed", LogType.Log);
        }

        private void HandleOnAdOpened(object sender, EventArgs e)
        {
            _isAdLoaded = false;
            Logs.ShowLog("Admob Interstitial Ad Shown", LogType.Log);
        }

        private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
        {
            Logs.ShowLog("Admob Interstitial Ad fail to load", LogType.Log);
        }

        private void HandleOnAdLoaded(object sender, EventArgs e)
        {
            _isAdLoaded = true;
            Logs.ShowLog("Admob Interstitial Ad Loaded", LogType.Log);
        }

        public override void LoadAd()
        {
            AdRequest request = new AdRequest.Builder().Build();
            this.interstitial.LoadAd(request);
        }

        public override void ShowAd()
        {
            if (isAdLoaded())
                interstitial.Show();
        }

        public override bool isAdLoaded()
        {
            return _isAdLoaded;
        }
    }
}