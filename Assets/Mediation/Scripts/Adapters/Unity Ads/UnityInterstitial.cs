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

        public override void Initialize(string id = "", string appId = "", NetworkType.Consent consent = NetworkType.Consent.Default)
        {
            if (!Advertisement.isInitialized)
            {
                Advertisement.Initialize(appId);
                SetConsent(consent);
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

        /// <summary>
        /// Consent Set to AdNetwork
        /// </summary>
        /// <param name="consent">consent by user</param>
        void SetConsent(NetworkType.Consent consent)
        {
            MetaData gdprMetaData;
            switch (consent)
            {
                case NetworkType.Consent.Granted:
                    gdprMetaData = new MetaData("gdpr");
                    gdprMetaData.Set("consent", "true");
                    Advertisement.SetMetaData(gdprMetaData);
                    break;
                case NetworkType.Consent.Revoked:
                    gdprMetaData = new MetaData("gdpr");
                    gdprMetaData.Set("consent", "false");
                    Advertisement.SetMetaData(gdprMetaData);
                    break;
                case NetworkType.Consent.Default:
                    // No gdpr
                    break;
            }
        }
    }
}