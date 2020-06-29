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
        NetworkType.Consent Adconsent;

        public override void Initialize(string id = "", string appId = "", NetworkType.Consent consent = NetworkType.Consent.Default)
        {
            MobileAds.Initialize(initStatus => { });
            this.interstitial = new InterstitialAd(id);
            Adconsent = consent;

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
            AdRequest request = SetConsent(Adconsent);
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

        /// <summary>
        /// Consent Set to AdNetwork
        /// </summary>
        /// <param name="consent">consent by user</param>
        AdRequest SetConsent(NetworkType.Consent consent)
        {
            AdRequest request;
            switch (consent)
            {
                case NetworkType.Consent.Granted:
                    request = new AdRequest.Builder().Build();
                    return request;
                case NetworkType.Consent.Revoked:
                    request = new AdRequest.Builder().AddExtra("npa", "1").Build();
                    return request;
                case NetworkType.Consent.Default:
                    request = new AdRequest.Builder().Build();
                    return request;
            }
            return null;
        }
    }
}