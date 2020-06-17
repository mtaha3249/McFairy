using McFairy.Logger;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace McFairy.Base
{
    public class NetworkInitialization
    {
        public const int interstitialID = 0;
        public const int rewardedVideoID = 1;
        public const int bannerID = 2;
        public const int nativeID = 3;

        public static Dictionary<int, object> dictionaryNetworks =
            new Dictionary<int, object>();

        /// <summary>
        /// get object of intialized ad network
        /// </summary>
        /// <param name="networkName">network name to get (Generic Enum of ad)</param>
        /// <returns>object of network (Interstitial, Rewarded, Banner)</returns>
        public static T1 GetAdNetwork<T, T1>(T networkName)
        {
            object adNetwork;
            Enum convertedEnum = Enum.Parse(typeof(T), networkName.ToString()) as Enum;
            dictionaryNetworks.TryGetValue(Convert.ToInt32(convertedEnum), out adNetwork);
            return (T1)adNetwork;
        }

        /// <summary>
        /// Initialize Ad base on AdType
        /// </summary>
        /// <typeparam name="T">Generic Ad Type</typeparam>
        /// <param name="adType">type of ad to initialzed</param>
        /// <param name="initializedAds">list of ads initialized</param>
        /// <param name="uIdentifier">unique identifier of ad Network, use const of this class</param>
        /// <returns>true if initialzed, false if already initialzed</returns>
        public static bool InitializeAd<T>(T adType, List<int> initializedAds, int uIdentifier = 0)
        {
            Enum convertedEnum = Enum.Parse(typeof(T), adType.ToString()) as Enum;
            switch (uIdentifier)
            {
                case interstitialID:
                    if (!initializedAds.Contains(Convert.ToInt32(convertedEnum)) && GetAdNetwork<T, InterstitialBase>(adType) != null)
                    {
                        GetAdNetwork<McFairyAdsData.InterstitialAdType, InterstitialBase>((McFairyAdsData.InterstitialAdType)convertedEnum).Initialize
                            (McFairyAdsData.getId(adType), McFairyAdsData.getAppId(adType));
                        Logs.ShowLog(adType.ToString() + " Interstitial Initialized", LogType.Log);
                        initializedAds.Add(Convert.ToInt32(convertedEnum));
                        return true;
                    }
                    break;
                case rewardedVideoID:
                    if (!initializedAds.Contains(Convert.ToInt32(convertedEnum)) && GetAdNetwork<T, RewardedBase>(adType) != null)
                    {
                        GetAdNetwork<McFairyAdsData.RewardedAdType, RewardedBase>((McFairyAdsData.RewardedAdType)convertedEnum).Initialize
                            (McFairyAdsData.getId(adType), McFairyAdsData.getAppId(adType));
                        Logs.ShowLog(adType.ToString() + " Rewarded Initialized", LogType.Log);
                        initializedAds.Add(Convert.ToInt32(convertedEnum));
                        return true;
                    }
                    break;
                case bannerID:
                    if (!initializedAds.Contains(Convert.ToInt32(convertedEnum)) && GetAdNetwork<T, BannerBase>(adType) != null)
                    {
                        GetAdNetwork<McFairyAdsData.BannerAdType, BannerBase>((McFairyAdsData.BannerAdType)convertedEnum).Initialize
                            (McFairyAdsData.getId(adType), McFairyAdsData.getAppId(adType));
                        Logs.ShowLog(adType.ToString() + " Banner Initialized", LogType.Log);
                        initializedAds.Add(Convert.ToInt32(convertedEnum));
                        return true;
                    }
                    break;
                case nativeID:
                    if (!initializedAds.Contains(Convert.ToInt32(convertedEnum)) && GetAdNetwork<T, NativeBase>(adType) != null)
                    {
                        GetAdNetwork<McFairyAdsData.NativeAdType, NativeBase>((McFairyAdsData.NativeAdType)convertedEnum).Initialize
                            (McFairyAdsData.getId(adType), McFairyAdsData.getAppId(adType));
                        Logs.ShowLog(adType.ToString() + " Banner Initialized", LogType.Log);
                        initializedAds.Add(Convert.ToInt32(convertedEnum));
                        return true;
                    }
                    break;
            }
            return false;
        }
    }
}