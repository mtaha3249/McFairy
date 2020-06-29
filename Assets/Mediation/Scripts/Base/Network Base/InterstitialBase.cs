namespace McFairy.Base
{
    public abstract class InterstitialBase
    {
        /// <summary>
        /// initialization of ad network
        /// </summary>
        /// <param name="id">interstitial id</param>
        /// <param name="appId">application id</param>
        /// <param name="consent">consent type provided by user</param>
        public abstract void Initialize(string id = "", string appId = "", NetworkType.Consent consent = NetworkType.Consent.Default);

        /// <summary>
        /// Load ad of ad network
        /// </summary>
        public abstract void LoadAd();

        /// <summary>
        /// show ad of ad network
        /// </summary>
        public abstract void ShowAd();

        /// <summary>
        /// check if ad is loaded
        /// </summary>
        /// <returns>bool value true if loaded and false if not</returns>
        public abstract bool isAdLoaded();
    }
}