using McFairy.Base;
using McFairy.Logger;
using McFairy.SO;
using UnityEngine;
using UnityEngine.UI;

namespace McFairy.Adpater.McFairyAds
{
    public class McFairyInterstitial : InterstitialBase
    {
        GameObject interstitial;
        bool _isAdLoaded;

        public override void Initialize(string id = "", string appId = "", NetworkType.Consent consent = NetworkType.Consent.Default)
        {
            interstitial = AdSequence.Instance.CreateInterstitial(McFairyAdsMediation.Instance.transform);
        }

        public override bool isAdLoaded()
        {
            return _isAdLoaded;
        }

        public override void LoadAd()
        {
            if (AdSequence.Instance._interstitial)
            {
                Logs.ShowLog("McFairy Interstitial Loaded", LogType.Log);
                _isAdLoaded = true;
            }
            else
            {
                Logs.ShowLog("McFairy Interstitial Loading Failed", LogType.Log);
                _isAdLoaded = false;
            }
        }

        public override void ShowAd()
        {
            Logs.ShowLog("Showing McFairy Interstitial", LogType.Log);
            interstitial.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = AdSequence.Instance._interstitial;
            // download Now
            interstitial.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
            interstitial.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
            {
                Application.OpenURL(AdSequence.Instance._url);
            });
            // Close
            interstitial.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
            interstitial.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
            {
                interstitial.SetActive(false);
                if (McFairyAdsMediation.Instance.ShowingBanner)
                    McFairyAdsMediation.Instance.ShowBanner(McFairyAdsMediation.Instance.SceneId);
            });
            interstitial.SetActive(true);
            McFairyAdsMediation.Instance.HideBanner();
        }
    }
}