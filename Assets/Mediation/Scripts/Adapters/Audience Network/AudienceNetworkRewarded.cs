﻿using AudienceNetwork;
using UnityEngine;
using McFairy.Base;
using McFairy.Logger;

namespace McFairy.Adpater.AudienceNetwork
{
    public class AudienceNetworkRewarded : RewardedBase
    {
        bool _isAdLoaded = false;
        private RewardedVideoAd rewardedVideoAd;
        public override void Initialize(string id = "", string appId = "")
        {
#if !UNITY_EDITOR
            if (!AudienceNetworkAds.IsInitialized())
            {
                AudienceNetworkAds.Initialize();
            }
            rewardedVideoAd = new RewardedVideoAd(id);
            rewardedVideoAd.Register(McFairyAdsMediation.Instance.gameObject);

            rewardedVideoAd.RewardedVideoAdDidLoad = delegate ()
            {
                _isAdLoaded = true;
                if (OnAdLoaded != null)
                {
                    OnAdLoaded.Invoke();
                }
                Logs.ShowLog("Audience Network Rewarded Video Loaded", LogType.Log);
            };
            rewardedVideoAd.RewardedVideoAdDidFailWithError = delegate (string error)
            {
                LoadAd();
                Logs.ShowLog("Audience Network Rewarded Video fail to load with error: " + error, LogType.Log);
            };
            rewardedVideoAd.RewardedVideoAdWillLogImpression = delegate ()
            {
                Logs.ShowLog("Audience Network Rewarded Video log impression", LogType.Log);
            };
            rewardedVideoAd.RewardedVideoAdDidClick = delegate ()
            {
                Logs.ShowLog("Audience Network Rewarded Video ad clicked", LogType.Log);
            };

            rewardedVideoAd.RewardedVideoAdDidSucceed = delegate ()
            {
                Logs.ShowLog("Audience Network Rewarded Video Succeed S2S", LogType.Log);
            };

            rewardedVideoAd.RewardedVideoAdDidFail = delegate ()
            {
                Logs.ShowLog("Audience Network Rewarded Video ad failed to finish S2S", LogType.Log);
            };

            rewardedVideoAd.RewardedVideoAdDidClose = delegate ()
            {
                _isAdLoaded = false;
                if (OnAdCompleted != null)
                {
                    OnAdCompleted.Invoke();
                }
                Logs.ShowLog("Audience Network Rewarded Video Closed", LogType.Log);
            
                LoadAd();
            };
#endif
        }

        public override bool isAdLoaded()
        {
            return _isAdLoaded;
        }

        public override void LoadAd()
        {
#if !UNITY_EDITOR
            rewardedVideoAd.LoadAd();
#endif
        }

        public override void ShowAd()
        {
#if !UNITY_EDITOR
            if (isAdLoaded())
                rewardedVideoAd.Show();
#endif
        }
    }
}