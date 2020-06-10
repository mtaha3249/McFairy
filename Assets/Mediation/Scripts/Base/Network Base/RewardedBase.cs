namespace McFairy.Base
{
    public abstract class RewardedBase
    {
        public delegate void AdCompleted();
        /// <summary>
        /// on rewarded video completed Event
        /// </summary>
        public static AdCompleted OnAdCompleted;
        public delegate void AdLoaded();
        /// <summary>
        /// on rewarded video loaded Event
        /// </summary>
        public static AdLoaded OnAdLoaded;
        /// <summary>
        /// initialization of ad network
        /// </summary>
        /// <param name="id">interstitial id</param>
        /// <param name="appId">application id</param>
        public abstract void Initialize(string id = "", string appId = "");

        /// <summary>
        /// load ad of ad network
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