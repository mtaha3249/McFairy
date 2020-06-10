using McFairy.Base;
using McFairy.Logger;
using McFairy.SO;
using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace McFairy.Adpater.UnityAds
{
    public class UnityBanner : BannerBase
    {
        string placementID = "";
        BannerOptions bannerOptions;
        BannerLoadOptions bannerLoadOptions;
        NetworkType.BannerSize bannerSize;
        NetworkType.BannerPosition bannerPosition;

        public override T GetAdPosition<T, T1>(T1 bannerPosition)
        {
            object adPosition = new object();
            Enum convertedEnum = Enum.Parse(typeof(T1), bannerPosition.ToString()) as Enum;
            switch (convertedEnum)
            {
                case NetworkType.BannerPosition.Top_Center:
                    adPosition = BannerPosition.TOP_CENTER;
                    break;
                case NetworkType.BannerPosition.Top_Left:
                    adPosition = BannerPosition.TOP_LEFT;
                    break;
                case NetworkType.BannerPosition.Top_Right:
                    adPosition = BannerPosition.TOP_RIGHT;
                    break;
                case NetworkType.BannerPosition.Bottom_Center:
                    adPosition = BannerPosition.BOTTOM_CENTER;
                    break;
                case NetworkType.BannerPosition.Bottom_Left:
                    adPosition = BannerPosition.BOTTOM_LEFT;
                    break;
                case NetworkType.BannerPosition.Bottom_Right:
                    adPosition = BannerPosition.BOTTOM_RIGHT;
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
                    adSize = "";
                    break;
                case NetworkType.BannerSize.SmartBanner:
                    adSize = "";
                    break;
            }
            return (T)adSize;
        }

        public override void HideAd()
        {
            bannerLoadOptions.errorCallback -= BannerLoadErrorCallBack;
            bannerOptions.showCallback -= BannerShowCallBack;
            bannerOptions.hideCallback -= BannerHideCallBack;
            bannerOptions.clickCallback -= BannerClickCallBack;

            Advertisement.Banner.Hide(true);
        }

        public override void Initialize(string id = "", string appId = "")
        {
            if (!Advertisement.isInitialized)
            {
                Advertisement.Initialize(appId);
            }
            placementID = AdSequence.Instance.adIds.unityAdsIds.BannerPlacementID;
        }

        public override void LoadAd(NetworkType.BannerSize bannerSize, NetworkType.BannerPosition bannerPosition)
        {
            this.bannerSize = bannerSize;
            this.bannerPosition = bannerPosition;

            bannerLoadOptions = new BannerLoadOptions();
            bannerLoadOptions.errorCallback += BannerLoadErrorCallBack;
            bannerLoadOptions.loadCallback = () =>
            {
                Logs.ShowLog("Unity Ads Banner load successful", LogType.Log);
                bannerOptions = new BannerOptions();
                bannerOptions.showCallback += BannerShowCallBack;
                bannerOptions.hideCallback += BannerHideCallBack;
                bannerOptions.clickCallback += BannerClickCallBack;

                Advertisement.Banner.Show(placementID, bannerOptions);
            };
            Advertisement.Banner.SetPosition(GetAdPosition<BannerPosition, NetworkType.BannerPosition>(bannerPosition));
            Advertisement.Banner.Load(placementID, bannerLoadOptions);
        }

        void BannerShowCallBack()
        {
            Logs.ShowLog("Showing Unity Ads Banner", LogType.Log);
        }

        void BannerHideCallBack()
        {
            Logs.ShowLog("Hiding Unity Ads Banner", LogType.Log);
        }

        void BannerClickCallBack()
        {
            Logs.ShowLog("Unity Ads Banner Clicked", LogType.Log);
        }

        void BannerLoadErrorCallBack(string message)
        {
            LoadAd(bannerSize, bannerPosition);
            Logs.ShowLog("Unity Ads Banner Failed to load with error:" + message, LogType.Log);
        }
    }
}