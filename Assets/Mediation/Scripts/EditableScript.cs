using McFairy.Base;
using McFairy.Logger;
using McFairy.SO;
using System;
using UnityEngine;

namespace McFairy
{
    public class EditableScript
    {
        public enum InterstitialAdType
        {
            Admob = 00,
            Unity = 01,
            AudienceNetwork = 02,
            McFairyAds = 03
        }

        public enum RewardedAdType
        {
            Admob = 10,
            Unity = 11,
            AudienceNetwork = 12
        }

        public enum BannerAdType
        {
            Admob = 20,
            Unity = 21,
            AudienceNetwork = 22
        }

        public enum NativeAdType
        {
            Admob = 30
        }

        [Serializable]
        public struct AdIds
        {
            public AdmobIDs admobIds;
            public AudienceNetworkIDs audienceNetworkIds;
            public UnityAdsIDs unityAdsIds;
        }

        [Serializable]
        public class AdmobIDs
        {
            public string Interstitial = "ca-app-pub-3940256099942544/1033173712";
            public string Banner = "ca-app-pub-3940256099942544/6300978111";
            public string RewardedVideo = "ca-app-pub-3940256099942544/5224354917";
            public string Native = "ca-app-pub-3940256099942544/2247696110";
        }

        [Serializable]
        public class AudienceNetworkIDs
        {
            public string Interstitial= "2295292320706597_2295292590706570";
            public string Banner= "2295292320706597_2295293444039818";
            public string RewardedVideo = "2295292320706597_2820140891555068";
        }

        [Serializable]
        public class UnityAdsIDs
        {
            public string AppId = "2790170";
            public string InterstitialPlacementID = "video";
            public string BannerPlacementID = "banner";
            public string RewardedPlacementID = "rewardedVideo";
        }

        public const string _namespace = "McFairy";
        public const string _namespaceAdapters = "Adpater";
        public const string _namespaceAdaptersAdmob = "Admob";
        public const string _namespaceAdaptersAudienceNetwork = "AudienceNetwork";
        public const string _namespaceAdaptersUnityAds = "UnityAds";
        public const string _namespaceAdaptersMcFairyAds = "McFairyAds";
        public const string _admobInterstitialClassName = "AdmobInterstitial";
        public const string _audienceNetworkInterstitialClassName = "AudienceNetworkInterstitial";
        public const string _unityInterstitialClassName = "UnityInterstitial";
        public const string _mcFairyAdsInterstitialClassName = "McFairyInterstitial";

        public const string _admobRewardedClassName = "AdmobRewarded";
        public const string _audienceNetworkRewardedClassName = "AudienceNetworkRewarded";
        public const string _unityAdsRewardedClassName = "UnityRewarded";

        public const string _admobBannerClassName = "AdmobBanner";
        public const string _unityBannerClassName = "UnityBanner";
        public const string _audienceNetworkBannerClassName = "AudienceNetworkBanner";

        public const string _admobNativeAdClassName = "AdmobNativeAd";

        /// <summary>
        /// Initialize ad network object
        /// </summary>
        public static void InitializeAdNetworks(Action OnComplete = null)
        {
            string _newnamespace = "";

            // interstitial Ad scripts finding and initialization
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersAdmob;
            NetworkInitialization.dictionaryNetworks.Add((int)InterstitialAdType.Admob, Validator.validateScript<InterstitialBase>(_newnamespace, _admobInterstitialClassName));
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersUnityAds;
            NetworkInitialization.dictionaryNetworks.Add((int)InterstitialAdType.Unity, Validator.validateScript<InterstitialBase>(_newnamespace, _unityInterstitialClassName));
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersAudienceNetwork;
            NetworkInitialization.dictionaryNetworks.Add((int)InterstitialAdType.AudienceNetwork, Validator.validateScript<InterstitialBase>(_newnamespace, _audienceNetworkInterstitialClassName));
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersMcFairyAds;
            NetworkInitialization.dictionaryNetworks.Add((int)InterstitialAdType.McFairyAds, Validator.validateScript<InterstitialBase>(_newnamespace, _mcFairyAdsInterstitialClassName));

            // Rewarded Ad scripts finding and initialization
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersAdmob;
            NetworkInitialization.dictionaryNetworks.Add((int)RewardedAdType.Admob, Validator.validateScript<RewardedBase>(_newnamespace, _admobRewardedClassName));
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersAudienceNetwork;
            NetworkInitialization.dictionaryNetworks.Add((int)RewardedAdType.AudienceNetwork, Validator.validateScript<RewardedBase>(_newnamespace, _audienceNetworkRewardedClassName));
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersUnityAds;
            NetworkInitialization.dictionaryNetworks.Add((int)RewardedAdType.Unity, Validator.validateScript<RewardedBase>(_newnamespace, _unityAdsRewardedClassName));

            // Banner Ad scripts finding and initialization
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersAdmob;
            NetworkInitialization.dictionaryNetworks.Add((int)BannerAdType.Admob, Validator.validateScript<BannerBase>(_newnamespace, _admobBannerClassName));
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersAudienceNetwork;
            NetworkInitialization.dictionaryNetworks.Add((int)BannerAdType.AudienceNetwork, Validator.validateScript<BannerBase>(_newnamespace, _audienceNetworkBannerClassName));
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersUnityAds;
            NetworkInitialization.dictionaryNetworks.Add((int)BannerAdType.Unity, Validator.validateScript<BannerBase>(_newnamespace, _unityBannerClassName));

            // Native Ad scripts finding and initialization
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersAdmob;
            NetworkInitialization.dictionaryNetworks.Add((int)NativeAdType.Admob, Validator.validateScript<NativeBase>(_newnamespace, _admobNativeAdClassName));

            Logs.ShowLog("InitializeAdNetworks Initialized", LogType.Log);

            if (OnComplete != null)
            {
                OnComplete.Invoke();
            }
        }

        /// <summary>
        /// find ad id from SO ad sequence and give it back
        /// </summary>
        /// <param name="adType">ad type</param>
        /// <returns>id of ad</returns>
        public static string getId<T>(T adType)
        {
            string id = "";
            Enum convertedEnum = Enum.Parse(typeof(T), adType.ToString()) as Enum;
            switch (Convert.ToInt32(convertedEnum))
            {
                case 00:
                    // Interstitial
                    // Admob
                    id = AdSequence.Instance.adIds.admobIds.Interstitial;
                    break;
                case 01:
                    // Interstitial
                    // Unity
                    id = "";
                    break;
                case 02:
                    // Interstitial
                    // Facebook
                    id = AdSequence.Instance.adIds.audienceNetworkIds.Interstitial;
                    break;
                case 03:
                    // Interstitial
                    // McFairy Ads
                    break;
                case 10:
                    // Rewarded
                    // Admob
                    id = AdSequence.Instance.adIds.admobIds.RewardedVideo;
                    break;
                case 11:
                    // Rewarded
                    // Unity
                    id = "";
                    break;
                case 12:
                    // Rewarded
                    // Facebook
                    id = AdSequence.Instance.adIds.audienceNetworkIds.RewardedVideo;
                    break;
                case 20:
                    // Banner
                    // Admob
                    id = AdSequence.Instance.adIds.admobIds.Banner;
                    break;
                case 21:
                    // Banner
                    // Unity
                    id = "";
                    break;
                case 22:
                    // Banner
                    // Facebook
                    id = AdSequence.Instance.adIds.audienceNetworkIds.Banner;
                    break;
                case 30:
                    // Native
                    // Admob
                    id = AdSequence.Instance.adIds.admobIds.Native;
                    break;
            }
            return id;
        }

        /// <summary>
        /// find app id from SO ad sequence and give it back
        /// </summary>
        /// <param name="adType">ad type</param>
        /// <returns>id of ad</returns>
        public static string getAppId<T>(T adType)
        {
            string id = "";
            Enum convertedEnum = Enum.Parse(typeof(T), adType.ToString()) as Enum;
            switch (Convert.ToInt32(convertedEnum))
            {
                case 00:
                    // Interstitial
                    // Admob
                    id = "";
                    break;
                case 01:
                    // Interstitial
                    // Unity
                    id = AdSequence.Instance.adIds.unityAdsIds.AppId;
                    break;
                case 02:
                    // Interstitial
                    // Facebook
                    id = "";
                    break;
                case 03:
                    // Interstitial
                    // McFairy Ads
                    break;
                case 10:
                    // Rewarded
                    // Admob
                    id = "";
                    break;
                case 11:
                    // Rewarded
                    // Unity
                    id = AdSequence.Instance.adIds.unityAdsIds.AppId;
                    break;
                case 12:
                    // Rewarded
                    // Facebook
                    id = "";
                    break;
                case 20:
                    // Banner
                    // Admob
                    id = "";
                    break;
                case 21:
                    // Banner
                    // Unity
                    id = AdSequence.Instance.adIds.unityAdsIds.AppId;
                    break;
                case 22:
                    // Banner
                    // Facebook
                    id = "";
                    break;
                case 30:
                    // Native
                    // Admob
                    id = "";
                    break;
            }
            return id;
        }
    }
}