using UnityEngine;
using UnityEngine.Advertisements;
using McFairy.Base;
using McFairy.SO;
using McFairy.Logger;

namespace McFairy.Adpater.UnityAds
{
    public class UnityInterstitial : InterstitialBase, IUnityAdsListener
    {
        string placementID = "";
        public override void Initialize(string id = "", string appId = "")
        {
            Advertisement.AddListener(this);
            Advertisement.Initialize(appId);
            placementID = AdSequence.Instance.adIds.unityAdsIds.InterstitialPlacementID;
        }

        public override void LoadAd()
        {
            Advertisement.Load(placementID);
        }

        public void OnUnityAdsDidError(string message)
        {
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if (placementId == placementID)
            {
                isAdLoaded = false;
                LoadAd();
                Logs.ShowLog("UnityAds Interstitial Completed", LogType.Log);
            }
        }

        public void OnUnityAdsDidStart(string placementId)
        {
        }

        public void OnUnityAdsReady(string placementId)
        {
            if (placementId == placementID)
            {
                isAdLoaded = true;
                Logs.ShowLog("UnityAds Interstitial Available", LogType.Log);
            }
        }

        public override void ShowAd()
        {
            if (isAdLoaded)
                Advertisement.Show(AdSequence.Instance.adIds.unityAdsIds.InterstitialPlacementID);
        }
    }
}