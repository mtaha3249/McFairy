using GoogleMobileAds.Api;
using McFairy.Base;
using McFairy.Logger;
using System;
using UnityEngine;

namespace McFairy.Adpater.Admob
{
    public class AdmobBanner : BannerBase
    {
        private BannerView bannerView;
        string ID = "";
        NetworkType.Consent Adconsent;

        public override void HideAd()
        {
            this.bannerView.Hide();
            this.bannerView.Destroy();

            this.bannerView.OnAdLoaded -= HandleOnAdLoaded;
            this.bannerView.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
            this.bannerView.OnAdOpening -= HandleOnAdOpened;
            this.bannerView.OnAdClosed -= HandleOnAdClosed;
            this.bannerView.OnAdLeavingApplication -= HandleOnAdLeavingApplication;
        }

        public override void Initialize(string id = "", string appId = "", NetworkType.Consent consent = NetworkType.Consent.Default)
        {
            Adconsent = consent;
            MobileAds.Initialize(initStatus => { });
            ID = id;
        }

        public override void LoadAd(NetworkType.BannerSize bannerSize, NetworkType.BannerPosition bannerPosition)
        {
            this.bannerView = new BannerView(ID, GetAdSize<AdSize, NetworkType.BannerSize>(bannerSize),
                GetAdPosition<AdPosition, NetworkType.BannerPosition>(bannerPosition));

            this.bannerView.OnAdLoaded += HandleOnAdLoaded;
            this.bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            this.bannerView.OnAdOpening += HandleOnAdOpened;
            this.bannerView.OnAdClosed += HandleOnAdClosed;
            this.bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;

            AdRequest request = new AdRequest.Builder().Build();
            this.bannerView.LoadAd(request);
        }

        private void HandleOnAdLeavingApplication(object sender, EventArgs e)
        {
            Logs.ShowLog("Admob Banner leaving application", LogType.Log);
        }

        private void HandleOnAdClosed(object sender, EventArgs e)
        {
            Logs.ShowLog("Admob Banner Closed", LogType.Log);
        }

        private void HandleOnAdOpened(object sender, EventArgs e)
        {
            Logs.ShowLog("Admob Banner Opened", LogType.Log);
        }

        private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
        {
            AdRequest request = SetConsent(Adconsent);
            this.bannerView.LoadAd(request);
            Logs.ShowLog("Admob Banner failed to load with error: " + e.Message, LogType.Log);
        }

        private void HandleOnAdLoaded(object sender, EventArgs e)
        {
            this.bannerView.Show();
            Logs.ShowLog("Admob Banner Loaded and showing Banner", LogType.Log);
        }

        public override T GetAdSize<T, T1>(T1 bannerSize)
        {
            object adSize = new object();
            Enum convertedEnum = Enum.Parse(typeof(T1), bannerSize.ToString()) as Enum;
            switch (convertedEnum)
            {
                case NetworkType.BannerSize.Banner:
                    adSize = AdSize.Banner;
                    break;
                case NetworkType.BannerSize.SmartBanner:
                    adSize = AdSize.SmartBanner;
                    break;
            }
            return (T)adSize;
        }

        public override T GetAdPosition<T, T1>(T1 bannerPosition)
        {
            object adPosition = new object();
            Enum convertedEnum = Enum.Parse(typeof(T1), bannerPosition.ToString()) as Enum;
            switch (convertedEnum)
            {
                case NetworkType.BannerPosition.Top_Center:
                    adPosition = AdPosition.Top;
                    break;
                case NetworkType.BannerPosition.Top_Left:
                    adPosition = AdPosition.TopLeft;
                    break;
                case NetworkType.BannerPosition.Top_Right:
                    adPosition = AdPosition.TopRight;
                    break;
                case NetworkType.BannerPosition.Bottom_Center:
                    adPosition = AdPosition.Bottom;
                    break;
                case NetworkType.BannerPosition.Bottom_Left:
                    adPosition = AdPosition.BottomLeft;
                    break;
                case NetworkType.BannerPosition.Bottom_Right:
                    adPosition = AdPosition.BottomRight;
                    break;
            }
            return (T)adPosition;
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