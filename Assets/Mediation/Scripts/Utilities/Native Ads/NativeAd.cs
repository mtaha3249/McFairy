using McFairy;
using UnityEngine;
using UnityEngine.UI;

public class NativeAd : MonoBehaviour
{
    public int SequenceID = 0;
    [Header("UI Elements")]
    public GameObject RegisterObject;
	public RawImage adIcon;
	public RawImage adChoices;
	public Text adHeadline;
	public Text adCallToAction;

    private void OnEnable()
    {
        ShowNativeAd();
    }

    private void OnDisable()
    {
        HideNativeAd();
    }

    void ShowNativeAd()
    {
        // Remove Ads Check
        RegisterObject.SetActive(false);
        McFairyAdsMediation.Instance.ShowNativeAd(SequenceID, RegisterObject, adIcon, adChoices, adHeadline, adCallToAction);
    }

	void HideNativeAd()
    {
        // Remove Ads Check
		McFairyAdsMediation.Instance.HideNativeAd();
    }
}
