using GoogleMobileAds.Api;
using McFairy.Base;
using McFairy.Logger;
using UnityEngine;
using UnityEngine.UI;

namespace McFairy.Adpater.Admob
{
    public class AdmobNativeAd : NativeBase
    {
        private UnifiedNativeAd nativeAd;

        RawImage adIcon;
        RawImage adChoices;
        Text adHeadline;
        Text adCallToAction;

        AdLoader adLoader;

        GameObject registerObject;
        NetworkType.Consent Adconsent;

        public override void HideAd()
        {
            this.registerObject.SetActive(false);
            Logs.ShowLog("Hiding Admob Native Ad", LogType.Log);
        }

        public override void Initialize(string id = "", string appId = "", NetworkType.Consent consent = NetworkType.Consent.Default)
        {
            MobileAds.Initialize(initStatus => { });
            Adconsent = consent;
            adLoader = new AdLoader.Builder(id).ForUnifiedNativeAd().Build();
            adLoader.OnUnifiedNativeAdLoaded += AdmobNativeAdLoaded;
            adLoader.OnAdFailedToLoad += AdmobNativeAdFailed;
            adLoader.OnNativeAdClicked += AdmobNativeAdClicked;
            adLoader.OnNativeAdImpression += AdmobNativeAdImpression;
        }

        private void AdmobNativeAdImpression(object sender, System.EventArgs e)
        {
            Logs.ShowLog("Admob Native Ad Impression", LogType.Log);
        }

        private void AdmobNativeAdClicked(object sender, System.EventArgs e)
        {
            Logs.ShowLog("Admob Native Ad clicked", LogType.Log);
        }

        private void AdmobNativeAdFailed(object sender, AdFailedToLoadEventArgs e)
        {
            Logs.ShowLog("Admob Native Ad Failed to Load", LogType.Log);
        }

        public override void LoadAd(GameObject registerObject, RawImage adIcon, RawImage adChoices, Text adHeadline, Text adCallToAction)
        {
            this.registerObject = registerObject;
            this.adIcon = adIcon;
            this.adChoices = adChoices;
            this.adHeadline = adHeadline;
            this.adCallToAction = adCallToAction;

            adLoader.LoadAd(SetConsent(Adconsent));

            Logs.ShowLog("Loading Admob Native Ad", LogType.Log);
        }

        private void AdmobNativeAdLoaded(object sender, UnifiedNativeAdEventArgs args)
        {
            Logs.ShowLog("Admob Native Ad Loaded", LogType.Log);
            this.nativeAd = args.nativeAd;

            Texture2D iconTexture = this.nativeAd.GetIconTexture();
            Texture2D iconAdChoices = this.nativeAd.GetAdChoicesLogoTexture();
            string headline = this.nativeAd.GetHeadlineText();
            string cta = this.nativeAd.GetCallToActionText();

            adIcon.texture = iconTexture;
            adChoices.texture = iconAdChoices;
            adHeadline.text = headline;
            adCallToAction.text = cta;

            nativeAd.RegisterIconImageGameObject(adIcon.gameObject);
            nativeAd.RegisterAdChoicesLogoGameObject(adChoices.gameObject);
            nativeAd.RegisterHeadlineTextGameObject(adHeadline.gameObject);
            nativeAd.RegisterCallToActionGameObject(adCallToAction.gameObject);

            this.registerObject.SetActive(true);

            Logs.ShowLog("Showing Admob Native Ad", LogType.Log);
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