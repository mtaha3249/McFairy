using AudienceNetwork;
using AudienceNetwork.Utility;
using McFairy.Base;
using McFairy.Logger;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace McFairy.Adpater.AudienceNetwork
{
    public class AudienceNetworkBanner : BannerBase
    {
        private string ID = "";
        private AdView adView;
        NetworkType.BannerSize bannerSize;
        NetworkType.BannerPosition bannerPosition;

        public struct AdValue
        {
            public AdPosition pos;
            public double width;
            public double xposition;
        }

        public override T GetAdPosition<T, T1>(T1 bannerPosition)
        {
            Enum convertedEnum = Enum.Parse(typeof(T1), bannerPosition.ToString()) as Enum;
            AdValue adValue = new AdValue();
            double size = 0;
            switch (convertedEnum)
            {
                case NetworkType.BannerPosition.Top_Center:
                    adValue.pos = AdPosition.TOP;
                    size = GetAdSize<double, NetworkType.BannerSize>(bannerSize);
                    adValue.width = size;
                    if (size == 320)
                    {
                        adValue.xposition = Screen.width / 2 - size - (size / 2);
                    }
                    else
                    {
                        adValue.xposition = 0;
                    }
                    break;
                case NetworkType.BannerPosition.Top_Left:
                    adValue.pos = AdPosition.TOP;
                    size = GetAdSize<double, NetworkType.BannerSize>(bannerSize);
                    adValue.width = size;
                    if (size == 320)
                    {
                        adValue.xposition = 0;
                    }
                    else
                    {
                        adValue.xposition = 0;
                    }
                    break;
                case NetworkType.BannerPosition.Top_Right:
                    adValue.pos = AdPosition.TOP;
                    size = GetAdSize<double, NetworkType.BannerSize>(bannerSize);
                    adValue.width = size;
                    if (size == 320)
                    {
                        adValue.xposition = Screen.width / 2 - size;
                    }
                    else
                    {
                        adValue.xposition = 0;
                    }
                    break;
                case NetworkType.BannerPosition.Bottom_Center:
                    adValue.pos = AdPosition.BOTTOM;
                    size = GetAdSize<double, NetworkType.BannerSize>(bannerSize);
                    adValue.width = size;
                    if (size == 320)
                    {
                        adValue.xposition = Screen.width / 2 - size - (size / 2);
                    }
                    else
                    {
                        adValue.xposition = 0;
                    }
                    break;
                case NetworkType.BannerPosition.Bottom_Left:
                    adValue.pos = AdPosition.BOTTOM;
                    size = GetAdSize<double, NetworkType.BannerSize>(bannerSize);
                    adValue.width = size;
                    if (size == 320)
                    {
                        adValue.xposition = 0;
                    }
                    else
                    {
                        adValue.xposition = 0;
                    }
                    break;
                case NetworkType.BannerPosition.Bottom_Right:
                    adValue.pos = AdPosition.BOTTOM;
                    size = GetAdSize<double, NetworkType.BannerSize>(bannerSize);
                    adValue.width = size;
                    if (size == 320)
                    {
                        adValue.xposition = Screen.width / 2 - size;
                    }
                    else
                    {
                        adValue.xposition = 0;
                    }
                    break;
            }
            return (T)Convert.ChangeType(adValue, typeof(T));
        }

        public override T GetAdSize<T, T1>(T1 bannerSize)
        {
            double adSize = new double();
            Enum convertedEnum = Enum.Parse(typeof(T1), bannerSize.ToString()) as Enum;
            switch (convertedEnum)
            {
                case NetworkType.BannerSize.Banner:
                    adSize = 320;
                    break;
                case NetworkType.BannerSize.SmartBanner:
                    adSize = AdUtility.Width();
                    break;
            }
            return (T)Convert.ChangeType(adSize, typeof(T));
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

        public override void Initialize(string id = "", string appId = "", NetworkType.Consent consent = NetworkType.Consent.Default)
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

            adView = new AdView(ID, AdSize.BANNER_HEIGHT_50);
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
            Show(GetAdPosition<AdValue, NetworkType.BannerPosition>(bannerPosition));
        }
#endif
        void Show(AdValue adValue)
        {
            adView.ShowAd(adValue.pos, adValue.width, adValue.xposition);
        }
    }
}