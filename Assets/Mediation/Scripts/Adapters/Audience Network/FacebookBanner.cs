using AudienceNetwork;
using AudienceNetwork.Utility;
using McFairy.Base;
using McFairy.Logger;
using System;
using UnityEngine;

namespace McFairy.Adpater.AudienceNetwork
{
    public class FacebookBanner : BannerBase
    {

        private string ID = "";
        private AdView adView;
        NetworkType.BannerSize bannerSize;
        NetworkType.BannerPosition bannerPosition;

        public override T GetAdPosition<T, T1>(T1 bannerPosition)
        {
            object adPosition = new object();
            Enum convertedEnum = Enum.Parse(typeof(T1), bannerPosition.ToString()) as Enum;
            McFairyAdSize adSize = new McFairyAdSize();
            switch (convertedEnum)
            {
                case NetworkType.BannerPosition.Top_Center:
                    adSize.y = 0;
                    adSize.x = 0;
                    adPosition = adSize;
                    break;
                case NetworkType.BannerPosition.Top_Left:
                    adSize.y = 0;
                    adSize.x = -100;
                    adPosition = adSize;
                    break;
                case NetworkType.BannerPosition.Top_Right:
                    adSize.y = 0;
                    adSize.x = 100;
                    adPosition = adSize;
                    break;
                case NetworkType.BannerPosition.Bottom_Center:
                    adSize.y = 200;
                    adSize.x = 0;
                    adPosition = adSize;
                    break;
                case NetworkType.BannerPosition.Bottom_Left:
                    adSize.y = Screen.height - AdUtilityBridge.Instance.Height();
                    adSize.x = 0;
                    adPosition = adSize;
                    break;
                case NetworkType.BannerPosition.Bottom_Right:
                    adSize.y = 200;
                    adSize.x = 1;
                    adPosition = adSize;
                    break;
            }
            return (T)adPosition;
        }

        public override T GetAdSize<T, T1>(T1 bannerSize)
        {
            object adSize = new object();
            Enum convertedEnum = Enum.Parse(typeof(T1), bannerSize.ToString()) as Enum;
            switch (convertedEnum)
            {
                case NetworkType.BannerSize.Banner:
                    adSize = AdSize.BANNER_HEIGHT_90;
                    break;
                case NetworkType.BannerSize.SmartBanner:
                    adSize = AdSize.BANNER_HEIGHT_50;
                    break;
            }
            return (T)adSize;
        }

        public override void HideAd()
        {
#if !UNITY_EDITOR
            adView.AdViewDidLoad -= BannerLoaded;
            adView.AdViewDidFailWithError -= BannerFailedToLoad;
            adView.AdViewWillLogImpression -= BannerImpressionLog;
            adView.AdViewDidClick -= BannerClicked;

            if (adView)
            {
                adView.Dispose();
            }
#endif
        }

        public override void Initialize(string id = "", string appId = "")
        {
#if !UNITY_EDITOR
            if (!AudienceNetworkAds.IsInitialized())
            {
                AudienceNetworkAds.Initialize();
            }
            ID = id;
#endif
        }

        public override void LoadAd(NetworkType.BannerSize bannerSize, NetworkType.BannerPosition bannerPosition)
        {
#if !UNITY_EDITOR
            this.bannerSize = bannerSize;
            this.bannerPosition = bannerPosition;

            adView = new AdView(ID, GetAdSize<AdSize, NetworkType.BannerSize>(bannerSize));
            adView.Register(McFairyAdsMediation.Instance.gameObject);

            adView.AdViewDidLoad += BannerLoaded;
            adView.AdViewDidFailWithError += BannerFailedToLoad;
            adView.AdViewWillLogImpression += BannerImpressionLog;
            adView.AdViewDidClick += BannerClicked;

            adView.LoadAd();
#endif
        }

#if !UNITY_EDITOR
        void BannerFailedToLoad(string error)
        {
            LoadAd(bannerSize, bannerPosition);
            Logs.ShowLog("Audience Network Banner failed to Load with error: " + error, LogType.Log);
        }

        void BannerImpressionLog()
        {
            Logs.ShowLog("Audience Network Banner Impression Logged", LogType.Log);
        }

        void BannerClicked()
        {
            Logs.ShowLog("Audience Network Banner Clicked", LogType.Log);
        }

        void BannerLoaded()
        {
            Logs.ShowLog("Audience Network Banner Loaded", LogType.Log);
            Show(GetAdPosition<McFairyAdSize, NetworkType.BannerPosition>(bannerPosition));
        }
#endif

        public struct McFairyAdSize
        {
            public double x;
            public double y;
        }
        void Show(McFairyAdSize adSize)
        {
            adView.Show(adSize.x, adSize.y);
        }
    }
}