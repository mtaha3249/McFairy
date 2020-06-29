using McFairy;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject rewardedButton;
    public GameObject iconAd;
    public GameObject nativeAd;
    int SequenceID;

    private void Start()
    {
        rewardedButton.SetActive(false);

        McFairyAdsMediation.Instance.OnRewardedAdCompleted += OnRewardedCompleted;
        McFairyAdsMediation.Instance.OnRewardedAdLoaded += OnRewardedAdLoaded;

        HideIconAd();
        HideNativeAd();
    }

    public void GrantConsent()
    {
        McFairyAdsMediation.Instance.GrantConsent();
    }

    public void RevokeConsent()
    {
        McFairyAdsMediation.Instance.RevokeConsent();
    }

    public void InitializeMcFairy()
    {
        McFairyAdsMediation.Instance.Initialize();
    }

    public void UpdateSequenceID(int index)
    {
        SequenceID = index;
        HideBanner();
        LoadAds();
        HideIconAd();
        HideNativeAd();
    }

    void OnRewardedAdLoaded()
    {
        rewardedButton.SetActive(true);
    }

    void OnRewardedCompleted()
    {
        rewardedButton.SetActive(false);
        Debug.Log("<color=green>Rewarded Completed Give Reward to user</color>");
    }

    public void ShowAds()
    {
        McFairyAdsMediation.Instance.ShowInterstitialAd(SequenceID);
    }

    public void LoadAds()
    {
        McFairyAdsMediation.Instance.LoadAds();
    }

    public void ShowRewardedAds()
    {
        McFairyAdsMediation.Instance.ShowRewardedAd(SequenceID);
    }

    public void ShowBanner()
    {
        McFairyAdsMediation.Instance.ShowBanner(SequenceID);
    }

    void HideBanner()
    {
        McFairyAdsMediation.Instance.HideBanner();
    }

    public void ShowIconAd()
    {
        iconAd.SetActive(true);
    }

    void HideIconAd()
    {
        iconAd.SetActive(false);
    }

    public void ShowNativeAd()
    {
        nativeAd.SetActive(true);
    }

    void HideNativeAd()
    {
        nativeAd.SetActive(false);
    }
}
