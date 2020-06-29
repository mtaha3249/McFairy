using AudienceNetwork;
using UnityEngine;
using McFairy.Base;
using McFairy.Logger;

namespace McFairy.Adpater.AudienceNetwork
{
    public class AudienceNetworkInterstitial : InterstitialBase
    {
        bool _isAdLoaded = false;
        private InterstitialAd interstitialAd;
        public override void Initialize(string id = "", string appId = "", NetworkType.Consent consent = NetworkType.Consent.Default)
        {
#if !UNITY_EDITOR
            if (!AudienceNetworkAds.IsInitialized())
            {
                AudienceNetworkAds.Initialize();
            }
            interstitialAd = new InterstitialAd(id);
            interstitialAd.Register(McFairyAdsMediation.Instance.gameObject);

            interstitialAd.InterstitialAdDidLoad = delegate ()
            {
                Logs.ShowLog("Interstitial ad loaded.", LogType.Log);
                _isAdLoaded = true;
            };
            interstitialAd.InterstitialAdDidFailWithError = delegate (string error)
            {
                interstitialAd.LoadAd();
                Logs.ShowLog("Interstitial ad failed to load with error:" + error, LogType.Log);
            };
            interstitialAd.InterstitialAdWillLogImpression = delegate ()
            {
                Logs.ShowLog("Interstitial ad logged impression.", LogType.Log);
            };
            interstitialAd.InterstitialAdDidClick = delegate ()
            {
                Logs.ShowLog("Interstitial ad clicked.", LogType.Log);
            };
            interstitialAd.InterstitialAdDidClose = delegate ()
            {
                Logs.ShowLog("Interstitial ad did close.", LogType.Log);
                _isAdLoaded = false;
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
            interstitialAd.LoadAd();
#endif
        }

        public override void ShowAd()
        {
#if !UNITY_EDITOR
            if (isAdLoaded())
                interstitialAd.Show();
#endif
        }
    }
}