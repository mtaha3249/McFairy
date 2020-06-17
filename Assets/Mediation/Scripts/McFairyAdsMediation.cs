using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using McFairy.Base;
using McFairy.Logger;
using McFairy.SO;
using UnityEngine.UI;

namespace McFairy
{
    public class McFairyAdsMediation : MonoBehaviour
    {
        List<int> initializedInterstitialAds = new List<int>();
        List<int> initializedRewardedAds = new List<int>();
        List<int> initializedBannerAds = new List<int>();
        List<int> initializedNativeAds = new List<int>();

        /// <summary>
        /// Trigger Event when Rewarded Video Loaded
        /// </summary>
        public RewardedBase.AdLoaded OnRewardedAdLoaded;
        /// <summary>
        /// Trigger Event when Rewarded Video Completed
        /// </summary>
        public RewardedBase.AdCompleted OnRewardedAdCompleted;

        /// <summary>
        /// Static Intance of class
        /// </summary>
        public static McFairyAdsMediation Instance;

        /// <summary>
        /// current sceneId
        /// </summary>
        int sceneId;

        /// <summary>
        /// property which give you current scene id
        /// </summary>
        public int SceneId
        {
            get
            {
                return sceneId;
            }
        }

        /// <summary>
        /// McFairy Initialized value
        /// </summary>
        bool isInitialized = false;

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

            if (AdSequence.Instance.Init == NetworkType.InitializiationType.Auto)
            {
                Initialize();
            }
        }

        public void Initialize()
        {
            if (AdSequence.Instance.hideAds)
                return;
            InitMcFairy();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            LoadAds();
        }

        private void InitMcFairy()
        {
            McFairyAdsData.InitializeAdNetworks(() =>
            {
                // scripts validated and checked which are in the project
                InitializeAds(() =>
                {
                    // as initiliazation of class is done
                    RewardedBase.OnAdLoaded += OnAdLoaded;
                    RewardedBase.OnAdCompleted = OnAdCompleted;
                    isInitialized = true;
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

                // Banner Initalization
                // iterate each scene
                for (int y = 0; y < AdSequence.Instance.sequence[x].banner.sequence.Length; y++)
                {
                    NetworkInitialization.InitializeAd(AdSequence.Instance.sequence[x].banner.sequence[y], initializedBannerAds, NetworkInitialization.bannerID);
                }
                NetworkInitialization.InitializeAd(AdSequence.Instance.sequence[x].banner.failOver, initializedBannerAds, NetworkInitialization.bannerID);

                // NativeAd Initalization
                // iterate each scene
                if (AdSequence.Instance.sequence[x].nativeAd._enable)
                    NetworkInitialization.InitializeAd(AdSequence.Instance.sequence[x].nativeAd.sequence, initializedNativeAds, NetworkInitialization.nativeID);
            }

            Logs.ShowLog("InitializeAds Initialized", LogType.Log);

            if (OnCompleteInitialization != null)
            {
                OnCompleteInitialization.Invoke();
            }
        }

        /// <summary>
        /// Load All Initialized Ads.
        /// Calls automatically when new scene loaded.
        /// You can call that on demand
        /// </summary>
        public void LoadAds()
        {
            if (AdSequence.Instance.hideAds)
                return;

            if (!isInitialized)
            {
                Debug.LogError("McFairy is not Initialize. Call Initialize Method first");
                return;
            }
            // load all interstitial
            foreach (int adindex in initializedInterstitialAds)
            {
                InterstitialBase adNetwork = NetworkInitialization.GetAdNetwork<McFairyAdsData.InterstitialAdType, InterstitialBase>
                    ((McFairyAdsData.InterstitialAdType)adindex);
                adNetwork.LoadAd();
            }

            // load all rewarded
            foreach (int adindex in initializedRewardedAds)
            {
                RewardedBase adNetwork = NetworkInitialization.GetAdNetwork<McFairyAdsData.RewardedAdType, RewardedBase>
                    ((McFairyAdsData.RewardedAdType)adindex);
                adNetwork.LoadAd();
            }
        }

        /// <summary>
        /// Show Interstitial Ad base on availability
        /// </summary>
        /// <param name="sceneId">scene index identifier</param>
        public void ShowInterstitialAd(int sceneId)
        {
            if (AdSequence.Instance.hideAds)
                return;

            if (!isInitialized)
            {
                Debug.LogError("McFairy is not Initialize. Call Initialize Method first");
                return;
            }
            InterstitialBase adNetwork;
            if (sceneId > AdSequence.Instance.sequence.Length - 1)
            {
                Logs.ShowLog("Scene Id is out of range", LogType.Error);
                return;
            }
            this.sceneId = sceneId;
            // running sequence
            for (int y = 0; y < AdSequence.Instance.sequence[sceneId].interstitial.sequence.Length; y++)
            {
                adNetwork = NetworkInitialization.GetAdNetwork<McFairyAdsData.InterstitialAdType, InterstitialBase>
                    (AdSequence.Instance.sequence[sceneId].interstitial.sequence[y]);
                if (adNetwork.isAdLoaded())
                {
                    adNetwork.ShowAd();
                    return;
                }
            }
            //running failover
            adNetwork = NetworkInitialization.GetAdNetwork<McFairyAdsData.InterstitialAdType, InterstitialBase>
                (AdSequence.Instance.sequence[sceneId].interstitial.failOver);
            if (adNetwork.isAdLoaded())
            {
                adNetwork.ShowAd();
                return;
            }
        }

        /// <summary>
        /// Show Rewarded Ad base on availability
        /// </summary>
        /// <param name="sceneId">scene index identifier</param>
        public void ShowRewardedAd(int sceneId)
        {
            if (AdSequence.Instance.hideAds)
                return;

            if (!isInitialized)
            {
                Debug.LogError("McFairy is not Initialize. Call Initialize Method first");
                return;
            }
            RewardedBase adNetwork;
            if (sceneId > AdSequence.Instance.sequence.Length - 1)
            {
                Logs.ShowLog("Scene Id is out of range", LogType.Error);
                return;
            }
            this.sceneId = sceneId;
            // running sequence
            for (int y = 0; y < AdSequence.Instance.sequence[sceneId].rewarded.sequence.Length; y++)
            {
                adNetwork = NetworkInitialization.GetAdNetwork<McFairyAdsData.RewardedAdType, RewardedBase>
                    (AdSequence.Instance.sequence[sceneId].rewarded.sequence[y]);
                if (adNetwork.isAdLoaded())
                {
                    adNetwork.ShowAd();
                    return;
                }
            }
            //running failover
            adNetwork = NetworkInitialization.GetAdNetwork<McFairyAdsData.RewardedAdType, RewardedBase>
                (AdSequence.Instance.sequence[sceneId].rewarded.failOver);
            if (adNetwork.isAdLoaded())
            {
                adNetwork.ShowAd();
                return;
            }
        }

        /// <summary>
        /// banner ad network is out of function beacuse when we can cache ad of which ad network is showing so we can hide ad of only that ad network
        /// </summary>
        BannerBase bannerAdNetwork;
        bool isShowingBanner;
        public bool ShowingBanner
        {
            get
            {
                return isShowingBanner;
            }
        }

        /// <summary>
        /// Show Banner Ad base on availability
        /// </summary>
        /// <param name="sceneId">scene index identifier</param>
        public void ShowBanner(int sceneId)
        {
            if (AdSequence.Instance.hideAds)
                return;

            if (!isInitialized)
            {
                Debug.LogError("McFairy is not Initialize. Call Initialize Method first");
                return;
            }
            if (sceneId > AdSequence.Instance.sequence.Length - 1)
            {
                Logs.ShowLog("Scene Id is out of range", LogType.Error);
                return;
            }
            this.sceneId = sceneId;
            // running sequence
            for (int y = 0; y < AdSequence.Instance.sequence[sceneId].banner.sequence.Length; y++)
            {
                bannerAdNetwork = NetworkInitialization.GetAdNetwork<McFairyAdsData.BannerAdType, BannerBase>
                    (AdSequence.Instance.sequence[sceneId].banner.sequence[y]);
                bannerAdNetwork.LoadAd(AdSequence.Instance.sequence[sceneId].banner.BannerSize, AdSequence.Instance.sequence[sceneId].banner.bannerPosition);
                return;
            }
            //running failover
            bannerAdNetwork = NetworkInitialization.GetAdNetwork<McFairyAdsData.BannerAdType, BannerBase>
                    (AdSequence.Instance.sequence[sceneId].banner.failOver);
            bannerAdNetwork.LoadAd(AdSequence.Instance.sequence[sceneId].banner.BannerSize, AdSequence.Instance.sequence[sceneId].banner.bannerPosition);

            isShowingBanner = true;

            return;
        }

        /// <summary>
        /// Hide current showing ad Banner
        /// </summary>
        public void HideBanner()
        {
            if (AdSequence.Instance.hideAds)
                return;

            if (bannerAdNetwork != null)
                bannerAdNetwork.HideAd();
        }

        /// <summary>
        /// Icon Ad Showing
        /// </summary>
        /// <param name="_icon">Registering gameobject</param>
        /// <param name="sceneId">index of scene in which ad is shown</param>
        public void ShowIconAd(GameObject _icon, int sceneId)
        {
            if (AdSequence.Instance.hideAds)
                return;

            if (!isInitialized)
            {
                Debug.LogError("McFairy is not Initialize. Call Initialize Method first");
                return;
            }
            if (sceneId > AdSequence.Instance.sequence.Length - 1)
            {
                Logs.ShowLog("Scene Id is out of range", LogType.Error);
                return;
            }
            this.sceneId = sceneId;
            if (AdSequence.Instance.sequence[sceneId].iconAd._enable && AdSequence.Instance._icon)
            {
                _icon.SetActive(true);
                _icon.GetComponent<Image>().sprite = AdSequence.Instance._icon;
                _icon.GetComponent<RectTransform>().sizeDelta = AdSequence.Instance.sequence[sceneId].iconAd._size;
                _icon.AddComponent<Button>();
                _icon.GetComponent<Button>().onClick.AddListener(() =>
                {
                    Application.OpenURL(AdSequence.Instance._url);
                });
            }
            else
            {
                _icon.SetActive(false);
            }
        }

        /// <summary>
        /// Hide icon ad
        /// </summary>
        /// <param name="_icon">registered gameobject</param>
        public void HideIconAd(GameObject _icon)
        {
            if (AdSequence.Instance.hideAds)
                return;

            _icon.SetActive(false);
        }

        /// <summary>
        /// native adNetwork object to cache so when calling Hide its avaialaible
        /// </summary>
        NativeBase nativeAdNetwork;

        /// <summary>
        /// Show Native Ad
        /// </summary>
        /// <param name="sceneId">Scene Index</param>
        /// <param name="registerObject">Native Ad Object/Parent</param>
        /// <param name="adIcon">RawImage where ad is shown</param>
        /// <param name="adChoices">Icon of Ad</param>
        /// <param name="adHeadline">Title of Ad</param>
        /// <param name="adCallToAction">On Click Text</param>
        public void ShowNativeAd(int sceneId, GameObject registerObject, RawImage adIcon, RawImage adChoices, Text adHeadline, Text adCallToAction)
        {
            if (AdSequence.Instance.hideAds)
                return;

            if (!isInitialized)
            {
                Debug.LogError("McFairy is not Initialize. Call Initialize Method first");
                return;
            }
            if (sceneId > AdSequence.Instance.sequence.Length - 1)
            {
                Logs.ShowLog("Scene Id is out of range", LogType.Error);
                return;
            }
            if (!AdSequence.Instance.sequence[sceneId].nativeAd._enable)
            {
                return;
            }
            this.sceneId = sceneId;
            nativeAdNetwork = NetworkInitialization.GetAdNetwork<McFairyAdsData.NativeAdType, NativeBase>
                   (AdSequence.Instance.sequence[sceneId].nativeAd.sequence);

            nativeAdNetwork.LoadAd(registerObject, adIcon, adChoices, adHeadline, adCallToAction);
        }

        public void HideNativeAd()
        {
            if (AdSequence.Instance.hideAds)
                return;

            if (nativeAdNetwork != null)
                nativeAdNetwork.HideAd();
        }
    }
}