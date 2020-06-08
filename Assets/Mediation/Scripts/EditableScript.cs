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

        /// <summary>
        /// Initialize ad network object
        /// </summary>
        public static void InitializeAdNetworks(Action OnComplete = null)
        {
            string _newnamespace = "";
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersAdmob;
            NetworkInitialization.dictionaryNetworks.Add((int)InterstitialAdType.Admob, Validator.validateScript<InterstitialBase>(_newnamespace, _admobInterstitialClassName));
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersUnityAds;
            NetworkInitialization.dictionaryNetworks.Add((int)InterstitialAdType.Unity, Validator.validateScript<InterstitialBase>(_newnamespace, _unityInterstitialClassName));
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersFacebook;
            NetworkInitialization.dictionaryNetworks.Add((int)InterstitialAdType.Facebook, Validator.validateScript<InterstitialBase>(_newnamespace, _facebookInterstitialClassName));

            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersAdmob;
            NetworkInitialization.dictionaryNetworks.Add((int)RewardedAdType.Admob, Validator.validateScript<RewardedBase>(_newnamespace, _admobRewardedClassName));
            _newnamespace = _namespace + "." + _namespaceAdapters + "." + _namespaceAdaptersFacebook;
            NetworkInitialization.dictionaryNetworks.Add((int)RewardedAdType.Facebook, Validator.validateScript<RewardedBase>(_newnamespace, _facebookRewardedClassName));

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
            }
            return id;
        }
    }
}