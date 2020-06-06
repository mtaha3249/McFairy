using AudienceNetwork;
using UnityEngine;

namespace McFairy
{
    public class FacebookInterstitial : AdNetwork
    {
        private InterstitialAd interstitialAd;
        public override void Initialize(string id = "", string appId = "")
        {
#if !UNITY_EDITOR
            AudienceNetworkAds.Initialize();
            interstitialAd = new InterstitialAd(id);
            interstitialAd.Register(McFairy.Instance.gameObject);

            interstitialAd.InterstitialAdDidLoad = delegate ()
            {
                Logs.ShowLog("Interstitial ad loaded.", LogType.Log);
                isAdLoaded = true;
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
                isAdLoaded = false;
            };
#endif
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
            if (isAdLoaded)
                interstitialAd.Show();
#endif
        }
    }
}