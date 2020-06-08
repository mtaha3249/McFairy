using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using McFairy.Base;
using McFairy.Logger;
using McFairy.SO;

namespace McFairy
{
    public class McFairyAdsMediation : MonoBehaviour
    {
        List<int> initializedInterstitialAds = new List<int>();
        List<int> initializedRewardedAds = new List<int>();
        List<int> initializedBannerAds = new List<int>();
        List<int> initializedNativeAds = new List<int>();

        public RewardedBase.AdLoaded OnRewardedAdLoaded;
        public RewardedBase.AdCompleted OnRewardedAdCompleted;

        public static McFairyAdsMediation Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            InitMcFairy();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            LoadAds();
        }

        private void InitMcFairy()
        {
            EditableScript.InitializeAdNetworks(() =>
            {
                // scripts validated and checked which are in the project
                InitializeAds(() =>
                {
                    // as initiliazation of class is done
                    RewardedBase.OnAdLoaded += OnAdLoaded; ;
                    RewardedBase.OnAdCompleted = OnAdCompleted;
                });
            });
            Logs.ShowLog("McFairy Initialized", LogType.Log);
        }

        void OnAdLoaded()
        {
            if (OnRewardedAdLoaded != null)
                OnRewardedAdLoaded.Invoke();
        }

        void OnAdCompleted()
        {
            if (OnRewardedAdCompleted != null)
                OnRewardedAdCompleted.Invoke();
        }

        private void InitializeAds(Action OnCompleteInitialization = null)
        {
            // iterate all sequences
            for (int x = 0; x < AdSequence.Instance.sequence.Length; x++)
            {
                // interstitial Initalization
                // iterate each scene
                for (int y = 0; y < AdSequence.Instance.sequence[x].interstitial.sequence.Length; y++)
                {
                    NetworkInitialization.InitializeAd(AdSequence.Instance.sequence[x].interstitial.sequence[y], initializedInterstitialAds, NetworkInitialization.interstitialID);
                }
                NetworkInitialization.InitializeAd(AdSequence.Instance.sequence[x].interstitial.failOver, initializedInterstitialAds, NetworkInitialization.interstitialID);

                // Rewarded Initalization
                // iterate each scene
                for (int y = 0; y < AdSequence.Instance.sequence[x].rewarded.sequence.Length; y++)
                {
                    NetworkInitialization.InitializeAd(AdSequence.Instance.sequence[x].rewarded.sequence[y], initializedRewardedAds, NetworkInitialization.rewardedVideoID);
                }
                NetworkInitialization.InitializeAd(AdSequence.Instance.sequence[x].rewarded.failOver, initializedRewardedAds, NetworkInitialization.rewardedVideoID);
            }

            Logs.ShowLog("InitializeAds Initialized", LogType.Log);

            if (OnCompleteInitialization != null)
            {
                OnCompleteInitialization.Invoke();
            }
        }

        public void LoadAds()
        {
            // load all interstitial
            foreach (int adindex in initializedInterstitialAds)
            {
                InterstitialBase adNetwork = NetworkInitialization.GetAdNetwork<EditableScript.InterstitialAdType, InterstitialBase>
                    ((EditableScript.InterstitialAdType)adindex);
                adNetwork.LoadAd();
            }

            // load all rewarded
            foreach (int adindex in initializedRewardedAds)
            {
                RewardedBase adNetwork = NetworkInitialization.GetAdNetwork<EditableScript.RewardedAdType, RewardedBase>
                    ((EditableScript.RewardedAdType)adindex);
                adNetwork.LoadAd();
            }
        }

        public void ShowInterstitialAd(int sceneId)
        {
            InterstitialBase adNetwork;
            if (sceneId > AdSequence.Instance.sequence.Length - 1)
            {
                Logs.ShowLog("Scene Id is out of range", LogType.Error);
                return;
            }
            // running sequence
            for (int y = 0; y < AdSequence.Instance.sequence[sceneId].interstitial.sequence.Length; y++)
            {
                adNetwork = NetworkInitialization.GetAdNetwork<EditableScript.InterstitialAdType, InterstitialBase>
                    (AdSequence.Instance.sequence[sceneId].interstitial.sequence[y]);
                if (adNetwork.isAdLoaded)
                {
                    adNetwork.ShowAd();
                    return;
                }
            }
            //running failover
            adNetwork = NetworkInitialization.GetAdNetwork<EditableScript.InterstitialAdType, InterstitialBase>
                (AdSequence.Instance.sequence[sceneId].interstitial.failOver);
            if (adNetwork.isAdLoaded)
            {
                adNetwork.ShowAd();
                return;
            }
        }

        public void ShowRewardedAd(int sceneId)
        {
            RewardedBase adNetwork;
            if (sceneId > AdSequence.Instance.sequence.Length - 1)
            {
                Logs.ShowLog("Scene Id is out of range", LogType.Error);
                return;
            }
            // running sequence
            for (int y = 0; y < AdSequence.Instance.sequence[sceneId].rewarded.sequence.Length; y++)
            {
                adNetwork = NetworkInitialization.GetAdNetwork<EditableScript.RewardedAdType, RewardedBase>
                    (AdSequence.Instance.sequence[sceneId].rewarded.sequence[y]);
                if (adNetwork.isAdLoaded)
                {
                    adNetwork.ShowAd();
                    return;
                }
            }
            //running failover
            adNetwork = NetworkInitialization.GetAdNetwork<EditableScript.RewardedAdType, RewardedBase>
                (AdSequence.Instance.sequence[sceneId].rewarded.failOver);
            if (adNetwork.isAdLoaded)
            {
                adNetwork.ShowAd();
                return;
            }
        }
    }
}