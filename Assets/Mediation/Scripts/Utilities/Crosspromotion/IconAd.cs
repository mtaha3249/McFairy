using McFairy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconAd : MonoBehaviour
{
    public int SequenceIndex;
    private void Start()
    {
        ShowIconAd();
    }

    public void ShowIconAd()
    {
        McFairyAdsMediation.Instance.ShowIconAd(gameObject, SequenceIndex);
    }

    public void HideIconAd()
    {
        McFairyAdsMediation.Instance.HideIconAd(gameObject);
    }
}
