using GoogleMobileAds.Api;
using System;
using UnityEngine;
using McFairy.Base;
using McFairy.Logger;

namespace McFairy.Adpater.Admob
{
    public class AdmobRewarded : RewardedBase
    {
        bool _isAdLoaded = false;
        private RewardedAd rewardedAd;
        NetworkType.Consent Adconsent;

        public override void Initialize(string id = "", string appId = "", NetworkType.Consent consent = NetworkType.Consent.Default)
        {
            MobileAds.Initialize(initStatus => { });
            this.rewardedAd = new RewardedAd(id);
            Adconsent = consent;

            this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
            this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
            this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        }

        private void HandleRewardedAdClosed(object sender, EventArgs e)
        {
            Logs.ShowLog("Admob Rewarded Video Closed", LogType.Log);
        }

        private void HandleUserEarnedReward(object sender, Reward e)
        {
            _isAdLoaded = false;
            if (OnAdCompleted != null)
            {
                OnAdCompleted.Invoke();
            }
            Logs.ShowLog("Admob Rewarded Video giving reward", LogType.Log);
            LoadAd();
        }

        private void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs e)
        {
            Logs.ShowLog("Admob Rewarded Video fail to show", LogType.Log);
        }

        private void HandleRewardedAdOpening(object sender, EventArgs e)
        {
            Logs.ShowLog("Admob Rewarded Video opened", LogType.Log);
        }

        private void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs e)
        {
            LoadAd();
            Logs.ShowLog("Admob Rewarded Video fail to load", LogType.Log);
        }

        private void HandleRewardedAdLoaded(object sender, EventArgs e)
        {
            _isAdLoaded = true;
            if (OnAdLoaded != null)
            {
                OnAdLoaded.Invoke();
            }
            Logs.ShowLog("Admob Rewarded Video Loaded", LogType.Log);
        }

        public override void LoadAd()
        {
            AdRequest request = SetConsent(Adconsent);
            this.rewardedAd.LoadAd(request);
        }

        public override void ShowAd()
        {
            if (isAdLoaded())
                rewardedAd.Show();
        }

        public override bool isAdLoaded()
        {
            return _isAdLoaded;
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