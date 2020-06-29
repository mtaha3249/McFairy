using System;
using UnityEngine;

namespace McFairy.Base
{
    public class NetworkType
    {
        public enum SceneName
        {
            MainMenu = 0,
            LevelSelection = 1,
            PlayerSelection = 2,
            Gameplay = 3,
            Store = 4,
            Paused = 5,
            LevelComplete = 6,
            LevelFail = 7,
            MidScene1 = 8,
            MidScene2 = 9,
            MidScene3 = 10
        }

        public enum Consent
        {
            Granted = 0,
            Revoked = 1,
            Default = 2
        }

        public enum BannerPosition
        {
            Top_Center = 0,
            Top_Right = 1,
            Top_Left = 2,
            Bottom_Center = 3,
            Bottom_Right = 4,
            Bottom_Left = 5
        }

        public enum BannerSize
        {
            Banner = 0,
            SmartBanner = 1
        }

        public enum Platforms
        {
            Google = 0,
            Apple = 1
        }

        public enum LogLevel
        {
            None = 0,
            Full = 1
        }

        public enum InitializiationType
        {
            Auto = 0,
            OnDemad = 1
        }

        [System.Serializable]
        public struct IconAd
        {
            public bool _enable;
            public Vector2 _size;
        }

        [System.Serializable]
        public struct NativeAd
        {
            public bool _enable;
            public McFairyAdsData.NativeAdType sequence;
        }

        [System.Serializable]
        public struct McFairy
        {
            public GameObject Interstitial;
            [Tooltip("Only used when useServerConfig == true.")]
            public bool isLoaded;
        }

        [Serializable]
        public struct SceneSequence
        {
            public SceneName sceneName;
            public Interstitial interstitial;
            public Rewarded rewarded;
            public Banner banner;
            public NativeAd nativeAd;
            public IconAd iconAd;
        }

        [Serializable]
        public struct Interstitial
        {
            public McFairyAdsData.InterstitialAdType[] sequence;
            public McFairyAdsData.InterstitialAdType failOver;
        }

        [Serializable]
        public struct Rewarded
        {
            public McFairyAdsData.RewardedAdType[] sequence;
            public McFairyAdsData.RewardedAdType failOver;
        }

        [Serializable]
        public struct Banner
        {
            public bool Enable;
            public BannerSize BannerSize;
            public BannerPosition bannerPosition;
            public McFairyAdsData.BannerAdType[] sequence;
            public McFairyAdsData.BannerAdType failOver;
        }
    }
}