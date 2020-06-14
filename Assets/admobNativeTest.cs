using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class admobNativeTest : MonoBehaviour
{
    private UnifiedNativeAd adNative;
    private bool nativeLoaded = false;

    public string idNative;

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
    }

    private void RequestNativeAd()
    {
        AdLoader adLoader = new AdLoader.Builder(idNative).ForUnifiedNativeAd().Build();
        adLoader.OnUnifiedNativeAdLoaded += this.HandleOnUnifiedNativeAdLoaded;
        adLoader.LoadAd(AdRequestBuild());
    }

    //events
    private void HandleOnUnifiedNativeAdLoaded(object sender, UnifiedNativeAdEventArgs args)
    {
        this.adNative = args.nativeAd;
        nativeLoaded = true;
    }

    AdRequest AdRequestBuild()
    {
        return new AdRequest.Builder().Build();
    }
}
