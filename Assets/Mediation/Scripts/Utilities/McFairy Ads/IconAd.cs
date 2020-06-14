using McFairy;
using UnityEngine;

public class IconAd : MonoBehaviour
{
    public int SequenceIndex;
    private void OnEnable()
    {
        ShowIconAd();
    }

    private void OnDisable()
    {
        HideIconAd();
    }

    void ShowIconAd()
    {
        // Remove Ads Check
        McFairyAdsMediation.Instance.ShowIconAd(gameObject, SequenceIndex);
    }

    void HideIconAd()
    {
        // Remove Ads Check
        McFairyAdsMediation.Instance.HideIconAd(gameObject);
    }
}
