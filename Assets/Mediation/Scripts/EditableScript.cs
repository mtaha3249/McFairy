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
            Facebook = 02,
            Crosspromotion = 03
        }

        public enum RewardedAdType
        {
            Admob = 10,
            Unity = 11,
            Facebook = 12
        }

        public enum BannerAdType
        {
            Admob = 20,
            Unity = 21,
            Facebook = 22
        }

        [Serializable]
        public struct AdIds
        {
            public AdmobIDs admobIds;
            public FacebookIDs facebookIds;
            public UnityAdsIDs unityAdsIds;
        }

        [Serializable]
        public struct AdmobIDs
        {
            public string Interstitial;
            public string Banner;
            public string RewardedVideo;
            public string Native;
        }

        [Serializable]
        public struct FacebookIDs
        {
            public string Interstitial;
            public string Banner;
            public string RewardedVideo;
            public string Native;
        }

        [Serializable]
        public struct UnityAdsIDs
        {
            public string AppId;
            public string InterstitialPlacementID;
            public string BannerPlacementID;
            public string RewardedPlacementID;
        }

        const string _namespace = "McFairy";
        const string _namespaceAdapters = "Adpater";
        const string _namespaceAdaptersAdmob = "Admob";
        const string _namespaceAdaptersFacebook = "AudienceNetwork";
        const string _namespaceAdaptersUnityAds = "UnityAds";
        const string _admobInterstitialClassName = "AdmobInterstitial";
        const string _facebookInterstitialClassName = "FacebookInterstitial";
        const string _unityInterstitialClassName = "UnityInterstitial";

        const string _admobRewardedClassName = "AdmobRewarded";
        const string _facebookRewardedClassName = "FacebookRewarded";
        const string _unityAdsRewardedClassName = "UnityRewarded";

        const string _admobBannerClassName = "AdmobBanner";
        const string _unityBannerClassName = "UnityBanner";
        const string _facebookBannerClassName = "FacebookBanner";

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
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersFacebook;
            NetworkInitialization.dictionaryNetworks.Add((int)InterstitialAdType.Facebook, Validator.validateScript<InterstitialBase>(_newnamespace, _facebookInterstitialClassName));

            // Rewarded Ad scripts finding and initialization
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersAdmob;
            NetworkInitialization.dictionaryNetworks.Add((int)RewardedAdType.Admob, Validator.validateScript<RewardedBase>(_newnamespace, _admobRewardedClassName));
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersFacebook;
            NetworkInitialization.dictionaryNetworks.Add((int)RewardedAdType.Facebook, Validator.validateScript<RewardedBase>(_newnamespace, _facebookRewardedClassName));
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersUnityAds;
            NetworkInitialization.dictionaryNetworks.Add((int)RewardedAdType.Unity, Validator.validateScript<RewardedBase>(_newnamespace, _unityAdsRewardedClassName));

            // Banner Ad scripts finding and initialization
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersAdmob;
            NetworkInitialization.dictionaryNetworks.Add((int)BannerAdType.Admob, Validator.validateScript<BannerBase>(_newnamespace, _admobBannerClassName));
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersFacebook;
            NetworkInitialization.dictionaryNetworks.Add((int)BannerAdType.Facebook, Validator.validateScript<BannerBase>(_newnamespace, _facebookBannerClassName));
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersUnityAds;
            NetworkInitialization.dictionaryNetworks.Add((int)BannerAdType.Unity, Validator.validateScript<BannerBase>(_newnamespace, _unityBannerClassName));

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
                    id = AdSequence.Instance.adIds.facebookIds.Interstitial;
                    break;
                case 03:
                    // Interstitial
                    // Crosspromotion
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
                    id = AdSequence.Instance.adIds.facebookIds.RewardedVideo;
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
                    id = AdSequence.Instance.adIds.facebookIds.Banner;
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
                    // Crosspromotion
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
            }
            return id;
        }
    }
}