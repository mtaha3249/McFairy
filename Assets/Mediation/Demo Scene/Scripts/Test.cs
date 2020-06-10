using McFairy;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject rewardedButton;
    int SequenceID;

    private void Start()
    {
        rewardedButton.SetActive(false);

        McFairyAdsMediation.Instance.OnRewardedAdCompleted += OnRewardedCompleted;
        McFairyAdsMediation.Instance.OnRewardedAdLoaded += OnRewardedAdLoaded;
    }

    public void UpdateSequenceID(int index)
    {
        SequenceID = index;
        HideBanner();
        LoadAds();
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

    public void HideBanner()
    {
        McFairyAdsMediation.Instance.HideBanner();
    }
}
