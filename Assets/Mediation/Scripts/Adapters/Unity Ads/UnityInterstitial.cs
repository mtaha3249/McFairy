using UnityEngine.Advertisements;
using McFairy.Base;
using McFairy.SO;
using UnityEngine;
using McFairy.Logger;

namespace McFairy.Adpater.UnityAds
{
    public class UnityInterstitial : InterstitialBase
    {
        string placementID = "";

        public override void Initialize(string id = "", string appId = "")
        {
            if (!Advertisement.isInitialized)
            {
                Advertisement.Initialize(appId);
            }
            placementID = AdSequence.Instance.adIds.unityAdsIds.InterstitialPlacementID;
        }

        public override void LoadAd()
        {
            Advertisement.Load(placementID);
        }

        public override void ShowAd()
        {
            if (isAdLoaded())
                Advertisement.Show(placementID);
        }

        public override bool isAdLoaded()
        {
            Logs.ShowLog(Advertisement.IsReady(placementID) ? "Unity Ads Interstitial Loaded" : "Unity Ads Interstitial not Loaded", LogType.Log);
            return Advertisement.IsReady(placementID);
        }
    }
}