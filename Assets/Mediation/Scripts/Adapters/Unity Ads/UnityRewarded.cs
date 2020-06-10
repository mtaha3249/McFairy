using McFairy.Base;
using McFairy.Logger;
using McFairy.SO;
using UnityEngine;
using UnityEngine.Advertisements;

namespace McFairy.Adpater.UnityAds
{
    public class UnityRewarded : RewardedBase, IUnityAdsListener
    {
        bool _isAdLoaded = false;
        string placementID = "";
        public override void Initialize(string id = "", string appId = "")
        {
            Advertisement.AddListener(this);
            if (!Advertisement.isInitialized)
            {
                Advertisement.Initialize(appId);
            }
            placementID = AdSequence.Instance.adIds.unityAdsIds.RewardedPlacementID;
        }

        public void OnUnityAdsDidError(string message)
        {
            LoadAd();
            Logs.ShowLog("UnityAds Rewarded Ad Error: " + message, LogType.Log);
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if (showResult == ShowResult.Finished)
            {
                if (placementId == placementID)
                {
                    _isAdLoaded = false;
                    LoadAd();
                    if (OnAdCompleted != null)
                    {
                        OnAdCompleted.Invoke();
                    }
                    Logs.ShowLog("UnityAds Rewarded Ad Completed", LogType.Log);
                }
            }
            else if (showResult == ShowResult.Skipped)
            {
                Logs.ShowLog("UnityAds Rewarded Ad Skipped", LogType.Log);
            }
            else if (showResult == ShowResult.Failed)
            {
                Logs.ShowLog("UnityAds Rewarded Ad Failed to Complete", LogType.Log);
            }
        }

        public void OnUnityAdsDidStart(string placementId)
        {
        }

        public void OnUnityAdsReady(string placementId)
        {
            if (placementId == placementID)
            {
                _isAdLoaded = true;
                if (OnAdLoaded != null)
                {
                    OnAdLoaded.Invoke();
                }
                Logs.ShowLog("UnityAds Rewarded Ad Available", LogType.Log);
            }
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
            return _isAdLoaded;
        }
    }
}