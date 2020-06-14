using UnityEngine;
using UnityEngine.UI;

namespace McFairy.Base
{
    public abstract class NativeBase
    {
        /// <summary>
        /// initialization of ad network
        /// </summary>
        /// <param name="id">interstitial id</param>
        /// <param name="appId">application id</param>
        public abstract void Initialize(string id = "", string appId = "");

        /// <summary>
        /// Load ad of ad network
        /// </summary>
        public abstract void LoadAd(GameObject registerObject,RawImage adIcon, RawImage adChoices, Text adHeadline, Text adCallToAction);

        /// <summary>
        /// Hide ad of ad network
        /// </summary>
        public abstract void HideAd();
    }
}